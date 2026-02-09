CREATE PROCEDURE [remuneraciones].[__SPRM_CargoMod_HaberesEstructura](
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

				SELECT '$ ' + [TW_GENERAL_TEAMWORK].[dbo].[FN_CONVERTMONEY](SueldoBase) 'SueldoBase',
					   '$ ' + [TW_GENERAL_TEAMWORK].[dbo].[FN_CONVERTMONEY](SueldoLiquido) 'SueldoLiquido',
					   '$ ' + [TW_GENERAL_TEAMWORK].[dbo].[FN_CONVERTMONEY](Gratificacion) 'Gratificacion',
					   '$ ' + [TW_GENERAL_TEAMWORK].[dbo].[FN_CONVERTMONEY](BaseImponible) 'BaseImponible',
					   '$ ' + [TW_GENERAL_TEAMWORK].[dbo].[FN_CONVERTMONEY](BaseImponibleAFC) 'BaseImponibleAFC',
					   '$ ' + [TW_GENERAL_TEAMWORK].[dbo].[FN_CONVERTMONEY](TotalImponible) 'TotalImponible',
					   '$ ' + [TW_GENERAL_TEAMWORK].[dbo].[FN_CONVERTMONEY](TotalTributable) 'TotalTributable',
					   '$ ' + [TW_GENERAL_TEAMWORK].[dbo].[FN_CONVERTMONEY](TotalHaberes) 'TotalHaberes',
					   '' 'SueldoBaseCifra',
					   '' 'SueldoLiquidoCifra',
					   '' 'GratificacionCifra',
					   '' 'BaseImponibleCifra',
					   '' 'BaseImponibleAFCCifra',
					   '' 'TotalImponibleCifra',
					   '' 'TotalTributableCifra',
					   '' 'TotalHaberesCifra',
					   GratifCC 'GratifCC',
					   GratificacionPactada 'GratificacionPactada',
					   CodigoCargoMod 'CodigoCargoMod',
					   MessageGratif 'MessageGratif',
					   MessageSueldoBase 'MessageSueldoBase',
					   MessageSugGratif 'MessageSugGratif',
					   GratifSugerida 'GratifSugerida'
					   FROM [remuneraciones].[View_HaberesEstructura]
					   WHERE CodigoCargoMod = @CODIGOCARGOMOD

			END
			ELSE
			BEGIN
				
				SELECT (SELECT '$ ' + [dbo].[FNConvertMoney](VEBH.CLP)
				               FROM [remuneraciones].[View_EstructuraBaseHaberes] VEBH
							   WHERE VEBH.CodigoCargoMod = [dbo].[FNBase64Decode](VHE.CodigoCargoMod) AND
							         VEBH.Concepto = 'H001') 'SueldoBase',
					   (SELECT '$ ' + [dbo].[FNConvertMoney](VEBH.CLP)
				               FROM [remuneraciones].[View_EstructuraBaseHaberes] VEBH
							   WHERE VEBH.CodigoCargoMod = [dbo].[FNBase64Decode](VHE.CodigoCargoMod) AND
							         VEBH.Concepto = 'H008') 'SueldoLiquido',
					   (SELECT '$ ' + [dbo].[FNConvertMoney](VEBH.CLP)
				               FROM [remuneraciones].[View_EstructuraBaseHaberes] VEBH
							   WHERE VEBH.CodigoCargoMod = [dbo].[FNBase64Decode](VHE.CodigoCargoMod) AND
							         VEBH.Concepto = 'H002') 'Gratificacion',
					   (SELECT '$ ' + [dbo].[FNConvertMoney](VEBH.CLP)
				               FROM [remuneraciones].[View_EstructuraBaseHaberes] VEBH
							   WHERE VEBH.CodigoCargoMod = [dbo].[FNBase64Decode](VHE.CodigoCargoMod) AND
							         VEBH.Concepto = 'H003') 'BaseImponible',
					   (SELECT '$ ' + [dbo].[FNConvertMoney](VEBH.CLP)
				               FROM [remuneraciones].[View_EstructuraBaseHaberes] VEBH
							   WHERE VEBH.CodigoCargoMod = [dbo].[FNBase64Decode](VHE.CodigoCargoMod) AND
							         VEBH.Concepto = 'H004') 'BaseImponibleAFC',
					   (SELECT '$ ' + [dbo].[FNConvertMoney](VEBH.CLP)
				               FROM [remuneraciones].[View_EstructuraBaseHaberes] VEBH
							   WHERE VEBH.CodigoCargoMod = [dbo].[FNBase64Decode](VHE.CodigoCargoMod) AND
							         VEBH.Concepto = 'H005') 'TotalImponible',
					   (SELECT DISTINCT '$ ' + [dbo].[FNConvertMoney](VEBH.CLP)
				               FROM [remuneraciones].[View_EstructuraBaseHaberes] VEBH
							   WHERE VEBH.CodigoCargoMod = [dbo].[FNBase64Decode](VHE.CodigoCargoMod) AND
							         VEBH.Concepto = 'H006') 'TotalTributable',
					   (SELECT '$ ' + [dbo].[FNConvertMoney](VEBH.CLP)
				               FROM [remuneraciones].[View_EstructuraBaseHaberes] VEBH
							   WHERE VEBH.CodigoCargoMod = [dbo].[FNBase64Decode](VHE.CodigoCargoMod) AND
							         VEBH.Concepto = 'H007') 'TotalHaberes',
					   '' 'SueldoBaseCifra',
					   '' 'SueldoLiquidoCifra',
					   '' 'GratificacionCifra',
					   '' 'BaseImponibleCifra',
					   '' 'BaseImponibleAFCCifra',
					   '' 'TotalImponibleCifra',
					   '' 'TotalTributableCifra',
					   '' 'TotalHaberesCifra',
					   GratifCC 'GratifCC',
					   GratificacionPactada 'GratificacionPactada',
					   CodigoCargoMod 'CodigoCargoMod',
					   MessageGratif 'MessageGratif',
					   MessageSueldoBase 'MessageSueldoBase',
					   MessageSugGratif 'MessageSugGratif',
					   GratifSugerida 'GratifSugerida'
					   FROM [remuneraciones].[View_HaberesEstructura] VHE
					   WHERE CodigoCargoMod = @CODIGOCARGOMOD

			END
		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH

	END CATCH
