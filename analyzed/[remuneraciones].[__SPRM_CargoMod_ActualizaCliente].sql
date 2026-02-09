CREATE PROCEDURE [remuneraciones].[__SPRM_CargoMod_ActualizaCliente](
	@USUARIOCREADOR VARCHAR(MAX),
	@CODIGOSOLICITUD VARCHAR(MAX),
	@CLIENTE VARCHAR(MAX),
	@CODE VARCHAR(MAX) OUTPUT
)
AS
	
	BEGIN TRY
		
		BEGIN TRANSACTION
			
			UPDATE [remuneraciones].[RM_CargosMod]
			       SET Cliente = @CLIENTE,
				       CargoMod = @CLIENTE + ' ',
				       FechaUltimaActualizacion = GETDATE(),
					   UsuarioUltimaActualizacion = @USUARIOCREADOR,
					   UltimoComentario = 'Se actualiza el cliente asociado al cargo mod'
				   WHERE CodigoCargoMod = [TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](@CODIGOSOLICITUD)

			SET @CODE = '200'

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION

	END CATCH
