CREATE PROCEDURE [remuneraciones].[__SPRM_CargoMod_ActualizarAFP](
	@CODIGOSOLICITUD VARCHAR(MAX),
	@AFP VARCHAR(MAX),
	@CODE VARCHAR(MAX) OUTPUT,
	@MESSAGE VARCHAR(MAX) OUTPUT
)
AS
	
	BEGIN TRY
		
		BEGIN TRANSACTION
			
			DECLARE @NEWSUELDO FLOAT,
			        @MESSAGEOUT VARCHAR(MAX),
					@TYPESUELDO VARCHAR(MAX),
					@SUELDO FLOAT

			UPDATE [remuneraciones].[RM_CargosMod]
			       SET AFP = @AFP
				   WHERE CodigoCargoMod = [TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](@CODIGOSOLICITUD)

			SELECT @TYPESUELDO = RMCM.TypeSueldoInput,
			       @SUELDO = RMCM.AuxSueldoLiquido
			       FROM [remuneraciones].[RM_CargosMod] RMCM WITH (NOLOCK)
				   WHERE RMCM.CodigoCargoMod = [TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](@CODIGOSOLICITUD)
			
			IF(@TYPESUELDO = 'SL')
			BEGIN

				EXEC [remuneraciones].[__SPRMINT_CargoMod_SueldoFromLiquido]
				@SUELDO,
				@CODIGOSOLICITUD,
				@NEWSUELDO OUTPUT,
				@MESSAGEOUT OUTPUT

			END

			UPDATE [remuneraciones].[RM_CargosMod]
			       SET SueldoBase = CASE WHEN TypeSueldoInput = 'SB' THEN
										SueldoBase
					                ELSE
										@NEWSUELDO
									END
				   WHERE CodigoCargoMod = [TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](@CODIGOSOLICITUD)

			SET @CODE = '200'
			SET @MESSAGE = 'Se ha modificado la afp asociada'

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION

	END CATCH
