CREATE FUNCTION [remuneraciones].[FNUFTopeImponible]
(
	@TYPE VARCHAR(MAX)
)
RETURNS FLOAT
AS
BEGIN
	
	DECLARE @TOPEIMPONIBLEUF FLOAT

	SELECT @TOPEIMPONIBLEUF = RMC.Valor
	       FROM [remuneraciones].[RM_Constantes] RMC WITH (NOLOCK)
		   WHERE RMC.CodigoVariable = CASE WHEN @TYPE = 'IMP' THEN
		                                     'C004'
										   WHEN @TYPE = 'AFC' THEN
										     'C005'
									  END AND
		         RMC.Estado = 'VIG'

	RETURN @TOPEIMPONIBLEUF

END
