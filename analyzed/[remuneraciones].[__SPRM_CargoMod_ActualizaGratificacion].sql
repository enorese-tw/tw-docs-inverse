CREATE PROCEDURE [remuneraciones].[__SPRM_CargoMod_ActualizaGratificacion](
	@USUARIOCREADOR VARCHAR(MAX),
	@CODIGOSOLICITUD VARCHAR(MAX),
	@GRATIFICACION VARCHAR(MAX),
	@TYPEGRATIF VARCHAR(MAX),
	--@PORCGRATIFPACTADA VARCHAR(MAX),
	@OBSERVACIONES VARCHAR(MAX),
	@CODE VARCHAR(MAX) OUTPUT
)
AS
	
	BEGIN TRY
		
		BEGIN TRANSACTION
			
			DECLARE @NEWSUELDO FLOAT,
			        @MESSAGEOUT VARCHAR(MAX),
					@TYPESUELDO VARCHAR(MAX),
					@SUELDO FLOAT,
					@LIQUIDO FLOAT


			IF(@TYPEGRATIF = 'L' OR @TYPEGRATIF = 'CG' OR @TYPEGRATIF = 'CC' OR @TYPEGRATIF = 'GP')
			BEGIN
				
				IF(@TYPEGRATIF = 'L' OR @TYPEGRATIF = 'CG')
				BEGIN

					UPDATE [remuneraciones].[RM_CargosMod]
						   SET GratificacionPactada = 'N',
							   FechaUltimaActualizacion = GETDATE(),
							   UsuarioUltimaActualizacion = @USUARIOCREADOR,
							   UltimoComentario = 'Se actualiza la gratificación, considerá gratificación legal del 25%',
							   GratifCC = 'N'
						   WHERE CodigoCargoMod = [dbo].[FNBase64Decode](@CODIGOSOLICITUD)

				END
				ELSE IF(@TYPEGRATIF = 'CC')
				BEGIN

					UPDATE [remuneraciones].[RM_CargosMod]
						   SET GratificacionPactada = 'N',
							   FechaUltimaActualizacion = GETDATE(),
							   UsuarioUltimaActualizacion = @USUARIOCREADOR,
							   UltimoComentario = 'Se actualiza la gratificación, considerá gratificación costo 0',
							   GratifCC = 'S'
						   WHERE CodigoCargoMod = [dbo].[FNBase64Decode](@CODIGOSOLICITUD)

				END
				ELSE IF(@TYPEGRATIF = 'GP')
				BEGIN

					UPDATE [remuneraciones].[RM_CargosMod]
						   SET GratificacionPactada = 'S',
							   FechaUltimaActualizacion = GETDATE(),
							   UsuarioUltimaActualizacion = @USUARIOCREADOR,
							   UltimoComentario = 'Se actualiza la gratificación, considerá gratificación convencional pactada',
							   GratifCC = 'N'
						   WHERE CodigoCargoMod = [dbo].[FNBase64Decode](@CODIGOSOLICITUD)

				END

			END
			ELSE IF(@TYPEGRATIF = 'PCC')
			BEGIN

				IF((SELECT COUNT(1)
				           FROM [remuneraciones].[RM_GratificacionConvenida] RMGC WITH (NOLOCK)
						   WHERE CodigoCargoMod = [dbo].[FNBase64Decode](@CODIGOSOLICITUD) AND
						         Estado = 'VIG') = 0)
				BEGIN
					
					UPDATE [remuneraciones].[RM_CargosMod]
						   SET GratificacionPactada = 'S',
							   FechaUltimaActualizacion = GETDATE(),
							   UsuarioUltimaActualizacion = @USUARIOCREADOR,
							   GratifCC = 'N',
							   UltimoComentario = 'Se actualiza gratificación, considerá gratificación convencional pactada con cliente, detalle asociado a la actualización: ' + @OBSERVACIONES
						   WHERE CodigoCargoMod = [dbo].[FNBase64Decode](@CODIGOSOLICITUD)

					INSERT INTO [remuneraciones].[RM_GratificacionConvenida]
					       VALUES([dbo].[FNBase64Decode](@CODIGOSOLICITUD),
						          CAST(REPLACE(REPLACE(@GRATIFICACION, '.', ''), ',', '') AS FLOAT),
								  @OBSERVACIONES,
								  GETDATE(),
								  @USUARIOCREADOR,
								  NULL,
								  NULL,
								  'VIG',
								  CAST(GETDATE() AS DATE),
								  NULL)

				END
				ELSE
				BEGIN
					
					UPDATE [remuneraciones].[RM_CargosMod]
						   SET GratificacionPactada = 'S',
						       SueldoBase = CASE WHEN TypeSueldoInput = 'SB' THEN
												SueldoBase
											ELSE
												@NEWSUELDO
											END,
							   FechaUltimaActualizacion = GETDATE(),
							   UsuarioUltimaActualizacion = @USUARIOCREADOR,
							   GratifCC = 'N',
							   UltimoComentario = 'Se actualiza gratificación, considerá gratificación convencional pactada con cliente, detalle asociado a la actualización: ' + @OBSERVACIONES
						   WHERE CodigoCargoMod = [dbo].[FNBase64Decode](@CODIGOSOLICITUD)

					UPDATE [remuneraciones].[RM_GratificacionConvenida]
					       SET Valor = CAST(REPLACE(REPLACE(@GRATIFICACION, '.', ''), ',', '') AS FLOAT),
						       Observacion = @OBSERVACIONES,
							   FechaUltimaActualizacion = GETDATE(),
							   UsuarioUltimaActualizacion = @USUARIOCREADOR,
							   FechaVigenciaDesde = CAST(GETDATE() AS DATE)
						   WHERE CodigoCargoMod = [dbo].[FNBase64Decode](@CODIGOSOLICITUD) AND
						         Estado = 'VIG'

				END

			END

			SELECT @TYPESUELDO = RMCM.TypeSueldoInput,
			       @SUELDO = RMCM.AuxSueldoLiquido
			       FROM [remuneraciones].[RM_CargosMod] RMCM WITH (NOLOCK)
				   WHERE RMCM.CodigoCargoMod = [dbo].[FNBase64Decode](@CODIGOSOLICITUD)
			
			IF(@TYPESUELDO = 'SL')
			BEGIN

				EXEC [remuneraciones].[__SPRMINT_CargoMod_SueldoFromLiquido]
					@SUELDO,
					@CODIGOSOLICITUD,
					0,
					@NEWSUELDO OUTPUT,
					@MESSAGEOUT OUTPUT

				UPDATE [remuneraciones].[RM_CargosMod]
					   SET SueldoBase = CASE WHEN TypeSueldoInput = 'SB' THEN
											SueldoBase
										ELSE
											@NEWSUELDO
										END
					   WHERE CodigoCargoMod = [dbo].[FNBase64Decode](@CODIGOSOLICITUD)
				   

				SELECT @LIQUIDO = VHE.SueldoLiquido
				       FROM [remuneraciones].[View_HaberesEstructura] VHE
					   WHERE VHE.CodigoCargoMod = @CODIGOSOLICITUD


				IF(@SUELDO - @LIQUIDO > 1 OR @SUELDO - @LIQUIDO < -1)
				BEGIN

					EXEC [remuneraciones].[__SPRMINT_CargoMod_SueldoFromLiquido]
						@SUELDO,
						@CODIGOSOLICITUD,
						1,
						@NEWSUELDO OUTPUT,
						@MESSAGEOUT OUTPUT
									   					
					UPDATE [remuneraciones].[RM_CargosMod]
						   SET SueldoBase = @NEWSUELDO
						   WHERE CodigoCargoMod = [dbo].[FNBase64Decode](@CODIGOSOLICITUD)

				END
			END

			SET @CODE = '200'

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION

	END CATCH
