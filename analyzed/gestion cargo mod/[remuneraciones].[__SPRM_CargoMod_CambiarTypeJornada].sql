CREATE PROCEDURE [remuneraciones].[__SPRM_CargoMod_CambiarTypeJornada](
	@JORNADA VARCHAR(MAX),
	@CODIGOCARGOMOD VARCHAR(MAX),
	@CODE VARCHAR(MAX) OUTPUT,
	@MESSAGE VARCHAR(MAX) OUTPUT
)
AS
	
	BEGIN TRY
		
		BEGIN TRANSACTION
			
			IF(@JORNADA = 'F' OR @JORNADA = 'P')
			BEGIN

				UPDATE [remuneraciones].[RM_CargosMod]
					   SET TypeJornada = @JORNADA,
						   Horarios = CASE WHEN @JORNADA = 'F' THEN
						                  RTRIM(LTRIM(SUBSTRING(Horarios, CHARINDEX('-', Horarios) + 1, LEN(Horarios))))
									  ELSE
									      Horarios
									  END
					   WHERE CodigoCargoMod = [TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](@CODIGOCARGOMOD)

			END
			ELSE
			BEGIN
				
				UPDATE [remuneraciones].[RM_CargosMod]
					   SET TypeCalculoPT = @JORNADA
					   WHERE CodigoCargoMod = [TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](@CODIGOCARGOMOD)

			END	
				
			SET @CODE = '200'
			SET @MESSAGE = 'Se ha modificado la jornada asociada'

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION

	END CATCH
