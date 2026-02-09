CREATE PROCEDURE [remuneraciones].[__SPRM_CargoMod_AFP](
	@EMPRESA VARCHAR(MAX)
)
AS
	
	BEGIN TRY
		
		BEGIN TRANSACTION
			
			IF(@EMPRESA = 'TWEST')
			BEGIN
				
				SELECT AFP.Nombre 'Nombre'
				       FROM [remuneraciones].[View_AfpTWEST] AFP

			END
			ELSE IF(@EMPRESA = 'TWRRHH')
			BEGIN
				
				SELECT AFP.Nombre 'Nombre'
				       FROM [remuneraciones].[View_AfpTWRRHH] AFP

			END
			ELSE IF(@EMPRESA = 'TWC')
			BEGIN
				
				SELECT AFP.Nombre 'Nombre'
				       FROM [remuneraciones].[View_AfpTWC] AFP

			END

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION

	END CATCH
