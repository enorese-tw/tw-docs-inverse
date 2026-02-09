CREATE PROCEDURE [remuneraciones].[__SPRM_CargoMod_DescuentosEstructura](
	@CODIGOCARGOMOD VARCHAR(MAX)
)
AS
	
	BEGIN TRY
		
		BEGIN TRANSACTION
			DECLARE @__ESTADO VARCHAR(MAX)

			SELECT @__ESTADO = VS.EstadoReal
			       FROM [remuneraciones].[View_Solicitudes] VS
				   WHERE VS.CodigoSolicitud = @CODIGOCARGOMOD

			IF(@__ESTADO != 'TER' AND @__ESTADO != 'FD')
			BEGIN

				SELECT CAST(PorcAFP AS VARCHAR(MAX))  + '%' 'PorcAFP',
					   AFP 'AFP',
					   '$ ' + [TW_GENERAL_TEAMWORK].[dbo].[FN_CONVERTMONEY](CLPAFP) 'CLPAFP',
					   CAST(PorcFondoPensiones AS VARCHAR(MAX))  + '%' 'PorcFondoPensiones',
					   CAST(PorcSegInvalidez AS VARCHAR(MAX))  + '%' 'PorcSegInvalidez',
					   CAST(PorcSalud AS VARCHAR(MAX)) + '%' 'PorcSalud',
					   '$ ' + [TW_GENERAL_TEAMWORK].[dbo].[FN_CONVERTMONEY](CLPSalud) 'CLPSalud',
					   '$ ' + [TW_GENERAL_TEAMWORK].[dbo].[FN_CONVERTMONEY](CLPImpuestoUnico) 'CLPImpuestoUnico',
					   CAST(PorcSeguroDesempleo AS VARCHAR(MAX))  + '%' 'PorcSeguroDesempleo',
					   '$ ' + [TW_GENERAL_TEAMWORK].[dbo].[FN_CONVERTMONEY](CLPSeguroDesempleo) 'CLPSeguroDesempleo',
					   '$ ' + [TW_GENERAL_TEAMWORK].[dbo].[FN_CONVERTMONEY](TotalDescuentos) 'TotalDescuentos',
					   CAST(CodigoCargoMod AS VARCHAR(MAX))  'CodigoCargoMod'
					   FROM [remuneraciones].[View_DescuentosEstructura]
					   WHERE CodigoCargoMod = @CODIGOCARGOMOD
			END
			ELSE
			BEGIN
				
				SELECT (SELECT CAST(VERD.Percentage AS VARCHAR(MAX)) + '%'
				               FROM [remuneraciones].[View_EstructuraRentaDescuento] VERD
							   WHERE VERD.CodigoCargoMod = [dbo].[FNBase64Decode](VDE.CodigoCargoMod) AND
							         VERD.Concepto = 'D001') 'PorcAFP',
					   VDE.AFP 'AFP',
					   (SELECT '$ ' + [dbo].[FNConvertMoney](VERD.CLP)
				               FROM [remuneraciones].[View_EstructuraRentaDescuento] VERD
							   WHERE VERD.CodigoCargoMod = [dbo].[FNBase64Decode](VDE.CodigoCargoMod) AND
							         VERD.Concepto = 'D001') 'CLPAFP',
					   (SELECT CAST(VERD.Percentage AS VARCHAR(MAX)) + '%'
				               FROM [remuneraciones].[View_EstructuraRentaDescuento] VERD
							   WHERE VERD.CodigoCargoMod = [dbo].[FNBase64Decode](VDE.CodigoCargoMod) AND
							         VERD.Concepto = 'D002') 'PorcFondoPensiones',
					   (SELECT CAST(VERD.Percentage AS VARCHAR(MAX)) + '%'
				               FROM [remuneraciones].[View_EstructuraRentaDescuento] VERD
							   WHERE VERD.CodigoCargoMod = [dbo].[FNBase64Decode](VDE.CodigoCargoMod) AND
							         VERD.Concepto = 'D003') 'PorcSegInvalidez',
					   (SELECT CAST(VERD.Percentage AS VARCHAR(MAX)) + '%'
				               FROM [remuneraciones].[View_EstructuraRentaDescuento] VERD
							   WHERE VERD.CodigoCargoMod = [dbo].[FNBase64Decode](VDE.CodigoCargoMod) AND
							         VERD.Concepto = 'D005') 'PorcSalud',
					   (SELECT '$ ' + [dbo].[FNConvertMoney](VERD.CLP)
				               FROM [remuneraciones].[View_EstructuraRentaDescuento] VERD
							   WHERE VERD.CodigoCargoMod = [dbo].[FNBase64Decode](VDE.CodigoCargoMod) AND
							         VERD.Concepto = 'D005') 'CLPSalud',
					   (SELECT '$ ' + [dbo].[FNConvertMoney](VERD.CLP)
				               FROM [remuneraciones].[View_EstructuraRentaDescuento] VERD
							   WHERE VERD.CodigoCargoMod = [dbo].[FNBase64Decode](VDE.CodigoCargoMod) AND
							         VERD.Concepto = 'D006') 'CLPImpuestoUnico',
					   (SELECT CAST(VERD.Percentage AS VARCHAR(MAX)) + '%'
				               FROM [remuneraciones].[View_EstructuraRentaDescuento] VERD
							   WHERE VERD.CodigoCargoMod = [dbo].[FNBase64Decode](VDE.CodigoCargoMod) AND
							         VERD.Concepto = 'D007') 'PorcSeguroDesempleo',
					   (SELECT '$ ' + [dbo].[FNConvertMoney](VERD.CLP)
				               FROM [remuneraciones].[View_EstructuraRentaDescuento] VERD
							   WHERE VERD.CodigoCargoMod = [dbo].[FNBase64Decode](VDE.CodigoCargoMod) AND
							         VERD.Concepto = 'D007') 'CLPSeguroDesempleo',
					   (SELECT '$ ' + [dbo].[FNConvertMoney](VERD.CLP)
				               FROM [remuneraciones].[View_EstructuraRentaDescuento] VERD
							   WHERE VERD.CodigoCargoMod = [dbo].[FNBase64Decode](VDE.CodigoCargoMod) AND
							         VERD.Concepto = 'D008') 'TotalDescuentos',
					   CAST(CodigoCargoMod AS VARCHAR(MAX))  'CodigoCargoMod'
					   FROM [remuneraciones].[View_DescuentosEstructura] VDE
					   WHERE CodigoCargoMod = @CODIGOCARGOMOD

			END

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH

	END CATCH
