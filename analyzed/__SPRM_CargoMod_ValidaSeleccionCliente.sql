CREATE PROCEDURE [remuneraciones].[__SPRM_CargoMod_ValidaSeleccionCliente](
	@USUARIOCREADOR VARCHAR(MAX),
	@CODIGOSOLICITUD VARCHAR(MAX),
	@CODE VARCHAR(MAX) OUTPUT
)
AS
	
	BEGIN TRY
		
		BEGIN TRANSACTION
			
			IF((SELECT CodigoCliente
			           FROM [remuneraciones].[View_Solicitudes]
					   WHERE CodigoSolicitud = @CODIGOSOLICITUD) IS NOT NULL)
			BEGIN
				
				SET @CODE = '200'

			END
			ELSE
			BEGIN
				
				SET @CODE = '404'

			END

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION

	END CATCH
