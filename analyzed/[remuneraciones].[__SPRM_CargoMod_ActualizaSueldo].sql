CREATE PROCEDURE [remuneraciones].[__SPRM_CargoMod_ActualizaSueldo](
	@USUARIOCREADOR VARCHAR(MAX),
	@CODIGOSOLICITUD VARCHAR(MAX),
	@SUELDO VARCHAR(MAX),
	@TYPE VARCHAR(10),
	@CODE VARCHAR(MAX) OUTPUT,
	@MESSAGE VARCHAR(MAX) OUTPUT
)
AS
	
	BEGIN TRY
		
		BEGIN TRANSACTION
			
			DECLARE @NEWSUELDO FLOAT,
			        @MESSAGEOUT VARCHAR(MAX),
					@SUELDOINPUT FLOAT,
					@ALCANCELIQUIDO FLOAT

			IF(@SUELDO <> '')
			BEGIN

				IF(@TYPE = 'SB' OR @TYPE = 'SBPT')
				BEGIN

					UPDATE [remuneraciones].[RM_CargosMod]
						   SET SueldoBase = CAST(REPLACE(REPLACE(@SUELDO, '.', ''), ',', '') AS FLOAT),
						       SueldoLiquido = NULL,
							   FechaUltimaActualizacion = GETDATE(),
							   UsuarioUltimaActualizacion = @USUARIOCREADOR,
							   UltimoComentario = 'Se actualiza sueldo base asociado al cargo mod',
							   TypeSueldoInput = CASE WHEN TypeJornada = 'F' THEN 'SB' ELSE 'SBPT' END
						   WHERE CodigoCargoMod = [dbo].[FNBase64Decode](@CODIGOSOLICITUD)

					
					SET @CODE = '200'
					SET @MESSAGE = 'Se ha actualizado el sueldo base'

				END
				ELSE IF(@TYPE = 'SL' OR @TYPE = 'SLPT')
				BEGIN
					
					--/** OBTENCION DE SUELDO MINIMO => PARA CALCULO DE SUELDO BASE */
					
					SET @SUELDOINPUT = CAST(REPLACE(REPLACE(@SUELDO, '.', ''), ',', '') AS FLOAT)

					EXEC [remuneraciones].[__SPRMINT_CargoMod_SueldoFromLiquido]
						@SUELDOINPUT,
						@CODIGOSOLICITUD,
						0,
						@NEWSUELDO OUTPUT,
						@MESSAGEOUT OUTPUT

					SET @MESSAGE = @MESSAGEOUT

					UPDATE [remuneraciones].[RM_CargosMod]
						   SET SueldoLiquido = NULL,
						       SueldoBase = ROUND(@NEWSUELDO, 0),
							   FechaUltimaActualizacion = GETDATE(),
							   UsuarioUltimaActualizacion = @USUARIOCREADOR,
							   TypeSueldoInput = CASE WHEN TypeJornada = 'F' THEN 'SL' ELSE 'SLPT' END,
							   AuxSueldoLiquido = @SUELDOINPUT,
							   TypeSueldo = TypeSueldo,
							   UltimoComentario = 'Se actualiza sueldo liquido estimado asociado al cargo mod'
						   WHERE CodigoCargoMod = [dbo].[FNBase64Decode](@CODIGOSOLICITUD)

					SELECT @ALCANCELIQUIDO = VHE.SueldoLiquido
					       FROM [remuneraciones].[View_HaberesEstructura] VHE
						   WHERE VHE.CodigoCargoMod = @CODIGOSOLICITUD

					IF((ROUND(@SUELDOINPUT, 0) - ROUND(@ALCANCELIQUIDO, 0) < 0) OR (ROUND(@SUELDOINPUT, 0) - ROUND(@ALCANCELIQUIDO, 0) > 1))
					BEGIN
						
						EXEC [remuneraciones].[__SPRMINT_CargoMod_SueldoFromLiquido]
							@SUELDOINPUT,
							@CODIGOSOLICITUD,
							1,
							@NEWSUELDO OUTPUT,
							@MESSAGEOUT OUTPUT

						SET @MESSAGE = @MESSAGEOUT

						UPDATE [remuneraciones].[RM_CargosMod]
							   SET SueldoLiquido = NULL,
								   SueldoBase = ROUND(@NEWSUELDO, 0),
								   FechaUltimaActualizacion = GETDATE(),
								   UsuarioUltimaActualizacion = @USUARIOCREADOR,
								   TypeSueldoInput = CASE WHEN TypeJornada = 'F' THEN 'SL' ELSE 'SLPT' END,
								   AuxSueldoLiquido = @SUELDOINPUT,
								   TypeSueldo = TypeSueldo,
								   UltimoComentario = 'Se actualiza sueldo liquido estimado asociado al cargo mod'
							   WHERE CodigoCargoMod = [dbo].[FNBase64Decode](@CODIGOSOLICITUD)

					END

					SET @CODE = '200'
					SET @MESSAGE = 'Se ha actualizado el sueldo liquido estimado'

				END

				SET @CODE = @TYPE

			END


		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION

	END CATCH
