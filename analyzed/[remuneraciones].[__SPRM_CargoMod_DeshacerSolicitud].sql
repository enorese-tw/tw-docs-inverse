CREATE PROCEDURE [remuneraciones].[__SPRM_CargoMod_DeshacerSolicitud](
	@CODIGOSOLICITUD VARCHAR(MAX),
	@CODE VARCHAR(MAX) OUTPUT,
	@MESSAGE VARCHAR(MAX) OUTPUT
)
AS
	
	BEGIN TRY
		
		BEGIN TRANSACTION
			
			DELETE FROM [remuneraciones].[RM_BonosCargoMod]
			       WHERE CodigoCargoMod = [TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](@CODIGOSOLICITUD)

			DELETE FROM [remuneraciones].[RM_ANICargoMod]
			       WHERE CodigoCargoMod = [TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](@CODIGOSOLICITUD)

			DELETE FROM [remuneraciones].[RM_CargosMod]
			       WHERE CodigoCargoMod = [TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](@CODIGOSOLICITUD)

			DELETE FROM [remuneraciones].[RM_CargosModLog]
			       WHERE CodigoCargoMod = [TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](@CODIGOSOLICITUD)

			SET @CODE = '200'
			SET @MESSAGE = 'Se ha eliminado la creación de este cargo mod, ya no es recuperrable esta creación.'

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH

	END CATCH
