CREATE PROCEDURE [remuneraciones].[__SPRM_CargoMod_ValidaStageActual](
	@CODIGOSOLICITUD VARCHAR(MAX),
	@CODE VARCHAR(MAX) OUTPUT,
	@MESSAGE VARCHAR(MAX) OUTPUT
)
AS
	
	BEGIN TRY
		
		BEGIN TRANSACTION
			
			DECLARE @ESTADO VARCHAR(MAX)
			
			SELECT @ESTADO = RMCM.Estado
			       FROM [remuneraciones].[RM_CargosMod] RMCM WITH (NOLOCK)
				   WHERE RMCM.CodigoCargoMod = [TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](@CODIGOSOLICITUD)

			IF(@ESTADO IS NOT NULL)
			BEGIN
				IF(@ESTADO = 'PDAPROB' OR @ESTADO = 'SOLIC')
				BEGIN

					SET @CODE = '200'
					SET @MESSAGE = ''

				END
				ELSE IF(@ESTADO = 'APROB')
				BEGIN
					
					SET @CODE = '201'
					SET @MESSAGE = 
					'
					 <h1 class="color-150x3 family-teamwork">Ya no puede editar esta solicitud</h1>
					 <h3 style="color: rgb(100, 100, 100)" class="family-teamwork">Fue enviada al área de remuneraciones para su respectiva creación en softland</h3>
					 <h4 class="color-150x3 family-teamwork">Nota: si desea volver a editar, deberá ser rechazada la solicitud para que vuelva a su poder.</h4> 
					'

				END

			END
			ELSE
			BEGIN
				
				SET @CODE = '404'
				SET @MESSAGE = 'El cargo mod que esta tratando de solicitarle al sistema no existe!'

			END

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION

	END CATCH
