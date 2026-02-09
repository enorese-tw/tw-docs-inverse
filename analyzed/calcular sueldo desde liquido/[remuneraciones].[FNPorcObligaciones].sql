CREATE FUNCTION [remuneraciones].[FNPorcObligaciones]
(
	@TIPOCONTRATO INT,
	@EMPRESA VARCHAR(MAX),
	@AFP VARCHAR(MAX)
)
RETURNS FLOAT
AS
BEGIN
	
	DECLARE @OBLIGACIONES FLOAT,
	        @PORCAFP FLOAT,
			@SALUD FLOAT,
			@AFC FLOAT = 0

	IF(@EMPRESA = 'TWEST')
	BEGIN

		SELECT @PORCAFP = VAFP.CargoModAfpPorc
			   FROM [remuneraciones].[View_AfpTWEST] VAFP WITH (NOLOCK)
			   WHERE VAFP.Nombre = @AFP

	END
	ELSE IF(@EMPRESA = 'TWRRHH')
	BEGIN
		
		SELECT @PORCAFP = VAFP.CargoModAfpPorc
			   FROM [remuneraciones].[View_AfpTWRRHH] VAFP WITH (NOLOCK)
			   WHERE VAFP.Nombre = @AFP

	END
	ELSE IF(@EMPRESA = 'TWC')
	BEGIN
		
		SELECT @PORCAFP = VAFP.CargoModAfpPorc
			   FROM [remuneraciones].[View_AfpTWC] VAFP WITH (NOLOCK)
			   WHERE VAFP.Nombre = @AFP

	END

	SELECT @SALUD = RMC.Valor
	       FROM [remuneraciones].[RM_Constantes] RMC WITH (NOLOCK)
		   WHERE RMC.CodigoVariable = 'D005' AND
		         RMC.Estado = 'VIG'

	IF(@TIPOCONTRATO <> 2)
	BEGIN
		
		SELECT @AFC = RMC.Valor
			   FROM [remuneraciones].[RM_Constantes] RMC WITH (NOLOCK)
			   WHERE RMC.CodigoVariable = 'D007' AND
					 RMC.Estado = 'VIG'

	END

	SET @OBLIGACIONES = (100 - (@PORCAFP + @SALUD + @AFC)) / 100 

	RETURN @OBLIGACIONES

END
