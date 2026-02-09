CREATE PROCEDURE [remuneraciones].[__SPRM_CargoMod_ValidaStageWizards](
	@CODIGOSOLICITUD VARCHAR(MAX),
	@ESTADO VARCHAR(MAX),
	@CODE VARCHAR(MAX) OUTPUT,
	@MESSAGE VARCHAR(MAX) OUTPUT
)
AS
	
	BEGIN TRY
		
		BEGIN TRANSACTION
			
			DECLARE @__STAGEMAX NUMERIC

			SET @__STAGEMAX = 7

			IF((SELECT COUNT(1)
			           FROM [remuneraciones].[View_StageWizards] VSW
					   WHERE VSW.Stage >= CAST(@ESTADO AS NUMERIC) AND
					         VSW.CodigoCargoMod = @CODIGOSOLICITUD) > 0)
			BEGIN
				
				SET @CODE = '200'
				SET @MESSAGE = ''

			END
			ELSE
			BEGIN
				
				SET @CODE = '403'
				SET @MESSAGE = 'Aun no se encuentra disponible esta etapa de Wizards para creación de cargo mod'

			END

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION

	END CATCH
