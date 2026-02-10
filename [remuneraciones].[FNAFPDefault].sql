CREATE FUNCTION [remuneraciones].[FNAFPDefault]
(
	@EMPRESA VARCHAR(MAX)
)
RETURNS VARCHAR(MAX)
AS
BEGIN
	
	DECLARE @AFPDEFAULT VARCHAR(MAX),
	        @CODIGOAFP VARCHAR(10)

	SELECT @CODIGOAFP = RIGHT('00' + LTRIM(RTRIM(CAST(RMC.Valor AS VARCHAR(MAX)))), 2)
	       FROM [remuneraciones].[RM_Constantes] RMC WITH (NOLOCK)
		   WHERE RMC.CodigoVariable = CASE WHEN @EMPRESA = 'TWEST' THEN
		                                     'A002'
										   WHEN @EMPRESA = 'TWRRHH' THEN
		                                     'A003'
										   WHEN @EMPRESA = 'TWC' THEN
		                                     'A004' 
									  END AND
		         RMC.Estado = 'VIG'

	IF(@EMPRESA = 'TWEST')
	BEGIN
		
		SELECT @AFPDEFAULT = VAFP.Nombre
		       FROM [remuneraciones].[View_AfpTWEST] VAFP WITH (NOLOCK)
			   WHERE VAFP.CodAFP = @CODIGOAFP

	END
	ELSE IF(@EMPRESA = 'TWRRHH')
	BEGIN
		
		SELECT @AFPDEFAULT = VAFP.Nombre
		       FROM [remuneraciones].[View_AfpTWRRHH] VAFP WITH (NOLOCK)
			   WHERE VAFP.CodAFP = @CODIGOAFP

	END
	ELSE IF(@EMPRESA = 'TWC')
	BEGIN
		
		SELECT @AFPDEFAULT = VAFP.Nombre
		       FROM [remuneraciones].[View_AfpTWC] VAFP WITH (NOLOCK)
			   WHERE VAFP.CodAFP = @CODIGOAFP

	END


	RETURN @AFPDEFAULT

END
