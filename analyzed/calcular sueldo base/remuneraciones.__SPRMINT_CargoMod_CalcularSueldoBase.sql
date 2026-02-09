811865959	SQL_STORED_PROCEDURE	[remuneraciones].[__SPRMINT_CargoMod_CalcularSueldoBase]	"CREATE PROCEDURE [remuneraciones].[__SPRMINT_CargoMod_CalcularSueldoBase](
	@SUELDOESPERADO FLOAT,
	@OTROIMPONIBLEINPUT FLOAT,
	@CODIGOCARGOMOD VARCHAR(50),
	@OUTGRATIFICACION FLOAT OUTPUT,
	@OUTTOTALES FLOAT OUTPUT,
	@OUTTOTALESIMP FLOAT OUTPUT,
	@OUTOTROSHABERES FLOAT OUTPUT,
	@OUTHABERES FLOAT OUTPUT,
	@OUTSALUD FLOAT OUTPUT,
	@OUTAFP FLOAT OUTPUT,
	@OUTAFC FLOAT OUTPUT,
	@OUTIMPUESTO FLOAT OUTPUT,
	@OUTDESCUENTOS FLOAT OUTPUT,
	@OUTLIQUIDO FLOAT OUTPUT
)
AS
	
	BEGIN TRY

		BEGIN TRANSACTION
			
			DECLARE @INGRESOMINIMOMENSUAL FLOAT,
					@TYPESUELDOCONSTANTE FLOAT,
			        @GRATIFICACION FLOAT,
					@EBONOS FLOAT,
					@EANIS FLOAT,
					@TOPEIMPONIBLEUF FLOAT,
					@TOPEIMPONIBLEAFCUF FLOAT,
					@VALORUF FLOAT,
					@PORCSALUD FLOAT,
					@PORCAFP FLOAT,
					@PORCAFC FLOAT,
					@EMPRESA VARCHAR(MAX),
					@TYPECONTRATO INT

			DECLARE @TOTALES FLOAT,
			        @TOTALESIMP FLOAT

			DECLARE @T1 FLOAT,
			        @T2 FLOAT,
					@T3 FLOAT,
					@T4 FLOAT,
					@T5 FLOAT,
					@T6 FLOAT,
					@T7 FLOAT,
					@T8 FLOAT,
					@T9 FLOAT,
					@T10 FLOAT,
					@T11 FLOAT

			DECLARE @TOPEAFP FLOAT,
			        @TOPEAFC FLOAT

			SET @T1 = 0
			SET @T2 = 0
			SET @T3 = 0
			SET @T4 = 0
			SET @T5 = 0
			SET @T6 = 0
			SET @T7 = 0
			SET @T8 = 0
			SET @T9 = 0
			SET @T10 = 0
			SET @T11 = 0

			SELECT @INGRESOMINIMOMENSUAL = VC.Valor
			       FROM [remuneraciones].[View_Constantes] VC
				   WHERE VC.CodigoVariable = 'C001'

			SELECT @TYPESUELDOCONSTANTE = RMTS.Valor
			       FROM [remuneraciones].[RM_CargosMod] RMC WITH (NOLOCK)
				   INNER JOIN [remuneraciones].[RM_TypeSueldo] RMTS WITH (NOLOCK)
				   ON RMTS.Codigo = RMC.TypeSueldo
				   WHERE RMC.CodigoCargoMod = @CODIGOCARGOMOD

			SET @INGRESOMINIMOMENSUAL = @INGRESOMINIMOMENSUAL / @TYPESUELDOCONSTANTE

			/** T1 => SUELDO MINIMO + OTROS IMPONIBLES ? INPUT PARAMETER : 0, PRIMERA VEZ */
			SET @T1 = @INGRESOMINIMOMENSUAL + @OTROIMPONIBLEINPUT

			/** T2 => GRATIFICACION */
			SELECT @T2 = [remuneraciones].[FNGratificacion](
							RMCM.GratificacionPactada,
							RMCM.CodigoCargoMod,
							@INGRESOMINIMOMENSUAL,
							RMCM.GratifCC,
							RMCM.TypeSueldoInput
			             ),
				   @EBONOS = 0,
				   @EANIS = [remuneraciones].[FNEANI](
								RMCM.CodigoCargoMod
				            ),
				   @TOPEIMPONIBLEUF = [remuneraciones].[FNUFTopeImponible](
										 'IMP'
				                      ),
				   @TOPEIMPONIBLEAFCUF = [remuneraciones].[FNUFTopeImponible](
										    'AFC'
				                         ),
				   @PORCAFP = [remuneraciones].[FNPorcentajeAFP](
				                 RMCM.AFP,
								 RMCM.Empresa
				              ),
				   @EMPRESA = RMCM.Empresa,
				   @TYPECONTRATO = RMCM.TipoContrato
			       FROM [remuneraciones].[RM_CargosMod] RMCM WITH (NOLOCK)
				   WHERE RMCM.CodigoCargoMod = @CODIGOCARGOMOD

			/** OBTENCION DE CONSTANTE AFC */
			SELECT @PORCAFC = CASE WHEN @TYPECONTRATO = 1 THEN
								 VC.Valor
			                  ELSE
								 0
							  END
			       FROM [remuneraciones].[View_Constantes] VC
			       WHERE VC.CodigoVariable = 'C006'


			/** SE DEVUELVE GRATIFICACION, PARA SER UTILIZADA DE MANERA GLOBAL */
			SET @OUTGRATIFICACION = @T2
			
			/** T3 => TOTALES IMPONIBLES => SUELDO BASE + GRATIFICACION */
			SET @T3 = @T1 + @T2

			SET @OUTTOTALES = @T1
			SET @OUTTOTALESIMP = @T3

			/** SE ASIGNAN SUMATORIA DE BONOS Y SUMATORIA DE ASIGNACIONES NO IMPONIBLES */
			SET @OUTOTROSHABERES = @EBONOS + @EANIS

			/** SE ASIGNAN HABERES, T3 Y OTROS HABERES */
			SET @OUTHABERES = @T3 + (@EBONOS + @EANIS)

			/** CALCULAR SALUD */
			SET @TOPEAFP = @T3

			SELECT @VALORUF = ValorUF
	               FROM [remuneraciones].[View_UltimaUF]

			SET @TOPEAFP = CASE WHEN @T3 > (@VALORUF * @TOPEIMPONIBLEUF) THEN
								(@VALORUF * @TOPEIMPONIBLEUF)
						   ELSE
								@TOPEAFP
			               END

			SELECT @PORCSALUD = VC.Valor / 100
			       FROM [remuneraciones].[View_Constantes] VC
				   WHERE VC.CodigoVariable = 'D005'

			/** T4 => SALUD */
			SET @T4 = @TOPEAFP * @PORCSALUD

			/** T5 => PLAN SALUD */
			SET @T5 = @VALORUF * 0

			SET @T4 = CASE WHEN @T5 > @T4 THEN
						 @T5
					  ELSE
						 @T4
					  END

			SET @T4 = ROUND(@T4, 0)

			SET @OUTSALUD = @T4	

			/** CALCULAR AFP */

			/** T6 => AFP */
			SET @T6 = @TOPEAFP * (@PORCAFP / 100)
			SET @T6 = ROUND(@T6, 0)

			SET @OUTAFP = @T6

			/** CALCULAR AFC */
			
			SET @TOPEAFC = @T3
			SET @TOPEAFC = CASE WHEN @T3 > (@TOPEAFC * @VALORUF) THEN
							 (@TOPEAFC * @VALORUF)
						   ELSE
							 @T3
						   END

			/** T7 => AFC */
			SET @T7 = @TOPEAFC * @PORCAFC

			SET @T7 = ROUND(@T7, 0)

			SET @OUTAFC = 0

			/** CALCULAR TRIBUTARIO */
			
			/** T8 => TRIBUTARIO */
			SET @T8 = @T3 - @T4	- @T6 - @T8 - @T7

			/** CALCULAR IMPUESTO UNICO */

			/** T9 => IMPUESTO */
			SET @T9 = [remuneraciones].[FNImpuestoUnicoFromLiquido](@T8, @EMPRESA)

			SET @T9 = CASE WHEN @T9 < 0 THEN
						 0
					  ELSE
						 @T9
					  END

			SET @OUTIMPUESTO = @T9

			/** CALCULAR LIQUIDO */

			/** T10 => LIQUIDO */
			SET @T10 = ROUND(@T3 - @T4 - @T6 - @T7 - @T9 + (@EBONOS + @EANIS), 0)
			SET @OUTLIQUIDO = @T10

			/** CALCULAR DESCUENTOS */

			/** T11 => DESCUENTOS */
			SET @T11 = @T4 + @T6 + @T7 + @T9

			SET @OUTDESCUENTOS = @T11

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION

		SELECT '500' 'Code',
		       'Ha ocurrido un problema SP INTERNAL',
			   ERROR_MESSAGE() 'Error'

	END CATCH
"	2021-04-09 12:28:58.743	2021-05-27 13:41:57.410