CREATE PROCEDURE [remuneraciones].[__SPRM_CargoMod_ActualizaObservaciones](
	@USUARIOCREADOR VARCHAR(MAX),
	@CODIGOSOLICITUD VARCHAR(MAX),
	@OBSERVACIONES VARCHAR(8000),
	@TYPE VARCHAR(MAX),
	@CODE VARCHAR(MAX) OUTPUT,
	@MESSAGE VARCHAR(MAX) OUTPUT
)
AS
	
	BEGIN TRY
		
		BEGIN TRANSACTION
			
			UPDATE [remuneraciones].[RM_CargosMod]
					SET Observaciones2 = @OBSERVACIONES,
						UsuarioUltimaActualizacion = @USUARIOCREADOR,
						FechaUltimaActualizacion = GETDATE(),
						UltimoComentario = 'Se ha actualizado observaciones.'
					WHERE CodigoCargoMod = [dbo].[FNBase64Decode](@CODIGOSOLICITUD)

			SET @CODE = '200'
			SET @MESSAGE = ''

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION

		SELECT '500' 'Code',
		       'Ha ocurrido un problema' 'Message',
			   ERROR_MESSAGE() 'Error'

	END CATCH
