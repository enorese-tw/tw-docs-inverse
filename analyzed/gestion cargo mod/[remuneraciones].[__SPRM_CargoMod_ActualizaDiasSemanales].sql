CREATE PROCEDURE [remuneraciones].[__SPRM_CargoMod_ActualizaDiasSemanales](
	@USUARIOCREADOR VARCHAR(MAX),
	@CODIGOSOLICITUD VARCHAR(MAX),
	@DIAS VARCHAR(MAX),
	@CODE VARCHAR(MAX) OUTPUT,
	@MESSAGE VARCHAR(MAX) OUTPUT
)
AS
	
	BEGIN TRY
		
		BEGIN TRANSACTION
			
			UPDATE [remuneraciones].[RM_CargosMod]
			       SET NumeroDias = CAST(REPLACE(@DIAS, '', '.') AS NUMERIC),
				       FechaUltimaActualizacion = GETDATE(),
					   UsuarioUltimaActualizacion = @USUARIOCREADOR,
					   UltimoComentario = 'Se actualiza dias semanales sobre el cargo mod.'
				   WHERE CodigoCargoMod = [TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](@CODIGOSOLICITUD)

			SET @CODE = '200'
			SET @MESSAGE = 'Se actualiza dias semanales sobre el cargo mod'

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION

	END CATCH