CREATE PROCEDURE [remuneraciones].[__SPRM_CargoMod_CambiarProvMargGastoExcInc](
	@USUARIOCREADOR VARCHAR(MAX),
	@CODIGOCARGOMOD VARCHAR(MAX),
	@CODIGOVAR VARCHAR(MAX),
	@EXCINC VARCHAR(MAX), 
	@CODE VARCHAR(MAX) OUTPUT, 
	@MESSAGE VARCHAR(MAX) OUTPUT
)
AS
	
	BEGIN TRY
		
		BEGIN TRANSACTION
				
			 IF(@EXCINC = 'EXC')
			 BEGIN
				
				INSERT INTO [remuneraciones].[RM_GastosMargenExc]
				       VALUES([TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](@CODIGOCARGOMOD),
					          @CODIGOVAR,
							  NULL,
							  CAST(GETDATE() AS DATE),
							  NULL,
							  GETDATE(),
					          @USUARIOCREADOR,
							  NULL,
							  NULL,
							  'Se excluye gasto de finanzas de codigo ' + @CODIGOVAR + '.',
					          'VIG')

				SET @CODE = '200'

			 END
			 ELSE IF(@EXCINC = 'INC')
			 BEGIN
				
				DELETE [remuneraciones].[RM_GastosMargenExc]
				       WHERE CodigoCargoMod = [TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](@CODIGOCARGOMOD) AND
					         CodigoGasto = @CODIGOVAR

				SET @CODE = '400'

			 END

			 SET @MESSAGE = ''

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION

	END CATCH
