CREATE FUNCTION [remuneraciones].[FNGratificacion]
(
	@CONVENIDA VARCHAR(1),
	@CODIGOCARGOMOD VARCHAR(MAX),
	@SUELDOBASEXDIASTRABAJADOS FLOAT,
	@COSTOCERO VARCHAR(1),
	@TYPESUELDO VARCHAR(5) = ''
)
RETURNS FLOAT
AS
BEGIN
	
	DECLARE @GRATIFICACION FLOAT,
	        @CONSTANTE FLOAT,
			@TOPEGRATIFICACION FLOAT,
			@EBONOS FLOAT,
			@CALCULOGRATIFICACION25 FLOAT,
			@__WITHEXCEPCION NUMERIC,
			@__CLIENTE VARCHAR(MAX),
			@__EMPRESA VARCHAR(MAX)

	IF(@CONVENIDA = 'N')
	BEGIN
		
		IF(@COSTOCERO = 'N')
		BEGIN
			
			SELECT @__CLIENTE = VCMC.Cliente,
			       @__EMPRESA = VCMC.Empresa
			       FROM [remuneraciones].[View_CargoModCliente] VCMC
				   WHERE VCMC.CodigoCargoMod = [dbo].[FNBase64Encode](@CODIGOCARGOMOD)

			SELECT @CONSTANTE = Valor
					FROM [remuneraciones].[RM_Constantes] WITH (NOLOCK)
					WHERE CodigoVariable = 'C013' AND
							Estado = 'VIG'

			SELECT @TOPEGRATIFICACION = Valor
					FROM [remuneraciones].[RM_Constantes] WITH (NOLOCK)
					WHERE CodigoVariable = 'C002' AND
							Estado = 'VIG'

			SELECT @__WITHEXCEPCION = COUNT(1)
			       FROM [remuneraciones].[View_ExcepcionGratificacion] VEG
				   WHERE VEG.Cliente = @__CLIENTE AND
				         VEG.Empresa = @__EMPRESA

			IF(@__WITHEXCEPCION = 0)
			BEGIN

				SELECT @EBONOS = ISNULL(SUM(Valor), 0)
						FROM [remuneraciones].[RM_BonosCargoMod] WITH (NOLOCK)
						WHERE CodigoCargoMod = @CODIGOCARGOMOD AND
								Estado = 'VIG' AND
								CAST(GETDATE() AS DATE) BETWEEN FechaVigenciaDesde AND
																CASE WHEN FechaVigenciaHasta IS NOT NULL THEN
																	FechaVigenciaHasta
																ELSE
																CAST('9999-12-31' AS DATE)
																 END
			
			
				SET @CALCULOGRATIFICACION25 = CASE WHEN @TYPESUELDO = 'SB' OR @TYPESUELDO = 'SBPT' THEN
													  (@SUELDOBASEXDIASTRABAJADOS + @EBONOS) * @CONSTANTE 
												   WHEN @TYPESUELDO = 'SL' OR @TYPESUELDO = 'SLPT'  THEN
													  (@SUELDOBASEXDIASTRABAJADOS * @CONSTANTE) - @EBONOS
											  END

				SET @GRATIFICACION = CASE WHEN @CALCULOGRATIFICACION25 > @TOPEGRATIFICACION THEN
											@TOPEGRATIFICACION
										ELSE
											@CALCULOGRATIFICACION25
										END

			END
			ELSE
			BEGIN
				
				SET @GRATIFICACION = @TOPEGRATIFICACION

			END

		END
		ELSE
		BEGIN
			
			SET @GRATIFICACION = 0

		END

	END
	ELSE
	BEGIN
		
		SELECT @GRATIFICACION = ISNULL(Valor, 0)
		       FROM [remuneraciones].[RM_GratificacionConvenida] WITH (NOLOCK)
			   WHERE CodigoCargoMod = @CODIGOCARGOMOD AND
			         Estado = 'VIG' AND
					 CAST(GETDATE() AS DATE) BETWEEN FechaVigenciaDesde AND
					                                 CASE WHEN FechaVigenciaHasta IS NOT NULL THEN
													     FechaVigenciaHasta
													 ELSE
														CAST('9999-12-31' AS DATE)
													 END

	END

	RETURN ROUND(@GRATIFICACION, 0)

END
