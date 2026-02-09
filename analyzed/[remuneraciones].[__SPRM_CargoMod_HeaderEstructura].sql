CREATE PROCEDURE [remuneraciones].[__SPRM_CargoMod_HeaderEstructura](
	@CODIGOCARGOMOD VARCHAR(MAX)
)
AS
	
	BEGIN TRY
		
		BEGIN TRANSACTION
			
			SELECT *
			       FROM [remuneraciones].[View_HeaderEstructura]
				   WHERE CodigoCargoMod = @CODIGOCARGOMOD

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH

	END CATCH
