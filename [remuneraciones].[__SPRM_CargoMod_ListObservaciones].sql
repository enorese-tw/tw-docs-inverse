CREATE PROCEDURE [remuneraciones].[__SPRM_CargoMod_ListObservaciones](
	@CODIGOSOLICITUD VARCHAR(MAX)
)
AS
	
	BEGIN TRY
		
		BEGIN TRANSACTION
			
			SELECT *
			       FROM [remuneraciones].[View_ListObservaciones] VLO

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION

	END CATCH
