CREATE PROCEDURE [remuneraciones].[__SPRM_CargoMod_CambiarInputSueldo](
	@SUELDO VARCHAR(MAX),
	@CODIGOCARGOMOD VARCHAR(MAX),
	@CODE VARCHAR(MAX) OUTPUT,
	@MESSAGE VARCHAR(MAX) OUTPUT
)
AS
	
	BEGIN TRY
		
		BEGIN TRANSACTION
			
			DECLARE @__TYPEJORNADA VARCHAR(MAX)

			SELECT @__TYPEJORNADA = VHE.TypeJornada
			       FROM [remuneraciones].[View_HeaderEstructura] VHE
				   WHERE CodigoSolicitud = @CODIGOCARGOMOD

			UPDATE [remuneraciones].[RM_CargosMod]
				   SET TypeSueldoInput = CASE WHEN @__TYPEJORNADA = 'F' THEN
				                            @SUELDO
										 ELSE
											@SUELDO + 'PT'
										 END
				   WHERE CodigoCargoMod = [dbo].[FNBase64Decode](@CODIGOCARGOMOD)
				
			SET @CODE = '200'
			SET @MESSAGE = 'Se ha modificado el tipo de calculo asociada'

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION

	END CATCH
