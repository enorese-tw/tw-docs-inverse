CREATE FUNCTION [remuneraciones].[FNImpuestoUnicoFromLiquido]
(
	@TRIBUTARIO FLOAT,
	@EMPRESA VARCHAR(MAX)
)
RETURNS FLOAT
AS
BEGIN
	
	DECLARE @VALORUTM FLOAT,
	        @PORCIMPUESTO FLOAT,
			@IMPUESTOUNICO FLOAT,
			@CONSTANTE FLOAT,
			@REBAJA FLOAT

	SELECT @VALORUTM = ValorUTM
		   FROM [remuneraciones].[View_UltimaUTM]

	SET @CONSTANTE = @TRIBUTARIO / @VALORUTM 

	IF(@EMPRESA = 'TWEST')
	BEGIN
		
		SELECT @PORCIMPUESTO = VIU.Porcentaje,
		       @REBAJA = VIU.Rebaja
			   FROM [remuneraciones].[View_ImpuestoUnicoTWEST] VIU 
			   WHERE VIU.TopeMin <= @CONSTANTE AND
			         VIU.TopeMax > @CONSTANTE

	END
	ELSE IF(@EMPRESA = 'TWRRHH')
	BEGIN
		
		SELECT @PORCIMPUESTO = VIU.Porcentaje,
		       @REBAJA = VIU.Rebaja
			   FROM [remuneraciones].[View_ImpuestoUnicoTWRRHH] VIU 
			   WHERE VIU.TopeMin <= @CONSTANTE AND
			         VIU.TopeMax > @CONSTANTE

	END
	ELSE IF(@EMPRESA = 'TWC')
	BEGIN
		
		SELECT @PORCIMPUESTO = VIU.Porcentaje,
		       @REBAJA = VIU.Rebaja
			   FROM [remuneraciones].[View_ImpuestoUnicoTWC] VIU 
			   WHERE VIU.TopeMin <= @CONSTANTE AND
			         VIU.TopeMax > @CONSTANTE
			   

	END

	SET @IMPUESTOUNICO = @CONSTANTE * @VALORUTM * @PORCIMPUESTO - @REBAJA * @VALORUTM

	RETURN @IMPUESTOUNICO
END
