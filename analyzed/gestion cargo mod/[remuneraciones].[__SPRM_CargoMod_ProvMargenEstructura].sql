
CREATE PROCEDURE [remuneraciones].[__SPRM_CargoMod_ProvMargenEstructura](
	@USUARIOCREADOR VARCHAR(MAX),
	@CODIGOCARGOMOD VARCHAR(MAX)
)
AS
	
	BEGIN TRY
		
		BEGIN TRANSACTION
				
			 DECLARE @CLIENTE VARCHAR(MAX),
			         @__EMPRESA VARCHAR(MAX),
			         @MONTOCALCULO FLOAT,
					 @MONTOCALCULOCEMP FLOAT,
					 @PROVISION FLOAT,
					 @COSTOEMPRESA FLOAT,
					 @GASTOS FLOAT,
					 @TOTALHABERES FLOAT,
					 @COSTODIARIO FLOAT,
					 @SQL NVARCHAR(MAX)

			 DECLARE @DESCRIPCION VARCHAR(MAX),
			         @CODIGOVARIABLE VARCHAR(MAX),
					 @VALORCONCEPTO FLOAT,
					 @TYPECALCULO VARCHAR(MAX),
					 @__TYPESUELDO VARCHAR(MAX),
					 @__VALORDIARIOCONST NUMERIC

			 DECLARE @__COLUMN VARCHAR(MAX),
			         @__TABLE VARCHAR(MAX),
					 @__DESCRIPCION VARCHAR(MAX),
					 @__EXC VARCHAR(MAX),
					 @__CODIGOREF VARCHAR(MAX),
					 @__ESTADOACTUAL VARCHAR(MAX)

			 DECLARE @__COLUMNTOPE VARCHAR(MAX),
			         @__TABLETOPE VARCHAR(MAX),
					 @__DESCRIPCIONTOPE VARCHAR(MAX),
					 @__CODIGOREFTOPE VARCHAR(MAX),
			         @__MONTOCALCULOTOPE FLOAT
					 

			 DECLARE @__EXISTETOPEPROV NUMERIC,
			         @__USERID NUMERIC,
					 @__PROVMARGENRESTRINGED NUMERIC

			 CREATE TABLE #ProvMargen (
				ConceptoReal VARCHAR(MAX),
				Concepto VARCHAR(MAX),
				Porcentaje VARCHAR(MAX),
				MontoCLP VARCHAR(MAX),
				PorcentajeReal FLOAT,
				Monto FLOAT,
				TypeConcepto VARCHAR(MAX),
				CodigoVariable VARCHAR(MAX),
				OptWithExcluir VARCHAR(MAX),
				CodigoCargoMod VARCHAR(MAX),
				ConstanteGlosa VARCHAR(MAX),
				CodigoConstante VARCHAR(MAX)
			 )
			
			 SELECT @CLIENTE = VCMC.Cliente,
			        @__EMPRESA = VCMC.Empresa
			        FROM [remuneraciones].[View_CargoModCliente] VCMC
					WHERE VCMC.CodigoCargoMod = @CODIGOCARGOMOD

			SELECT @__TYPESUELDO = VHE.TypeCalculoSueldo
			       FROM [remuneraciones].[View_HeaderEstructura] VHE
				   WHERE VHE.CodigoCargoMod = @CODIGOCARGOMOD

			 -- Obtencion dinamica de provisiones, tipo de calculo (P [porcentaje], M [monto]), valor asociado a la constante de provisión.


			 DECLARE CURSOR_PROVISIONES CURSOR
			         FOR SELECT VCAP.Descripcion,
								VCAP.CodigoVariable,
								VCAP.PorcConcepto,
								VCAP.TypeCalculo
								FROM [remuneraciones].[View_ConstAsignProv] VCAP
								WHERE VCAP.TypeConcepto = 'Provision' AND
									  VCAP.Cliente = @CLIENTE AND
									  VCAP.Empresa = @__EMPRESA

			 OPEN CURSOR_PROVISIONES

			 FETCH CURSOR_PROVISIONES INTO @DESCRIPCION, @CODIGOVARIABLE, @VALORCONCEPTO, @TYPECALCULO

			 WHILE @@FETCH_STATUS = 0
			 BEGIN
				
				 SET @__DESCRIPCION = ''

				 IF(@TYPECALCULO = 'P')
				 BEGIN

					 SELECT @__COLUMN = VCAC.ColumnName,
							@__TABLE = VCAC.TableViewName,
							@__DESCRIPCION = VCAC.Descripcion,
							@__CODIGOREF = VCAC.CodigoRef
							FROM [remuneraciones].[View_ConstAsignCalculo] VCAC
							WHERE VCAC.CodigoConcepto = @CODIGOVARIABLE AND
								  VCAC.Cliente = @CLIENTE AND
								  VCAC.Empresa = @__EMPRESA
					
						print @__COLUMN
						print @__TABLE
						print @__DESCRIPCION
						print @__CODIGOREF

					 SET @SQL = 
					 '
					 SELECT @MONTOCALCULOOUT = CONST.' + @__COLUMN + '
							FROM ' + @__TABLE + ' CONST 
							WHERE CONST.CodigoCargoMod = ''' + @CODIGOCARGOMOD + '''
					 '

					 EXEC sys.sp_executesql 
					 @SQL,
					 N'@MONTOCALCULOOUT NUMERIC OUTPUT',
					 @MONTOCALCULOOUT = @MONTOCALCULO OUTPUT

					 --/* Validacion de consideración de Tope para Calculo de monto */
					 SELECT @__EXISTETOPEPROV = COUNT(1)
					        FROM [remuneraciones].[View_ConstCalculoTopeProv] VCCTP
							WHERE VCCTP.Cliente = @CLIENTE AND
							      VCCTP.CodigoConcepto = @CODIGOVARIABLE AND
								  VCCTP.Empresa = @__EMPRESA
					 
					 IF(@__EXISTETOPEPROV > 0)
					 BEGIN
						
						SELECT @__COLUMNTOPE = VCCTP.ColumnName,
							   @__TABLETOPE = VCCTP.TableViewName,
							   @__DESCRIPCIONTOPE = VCCTP.Descripcion,
							   @__CODIGOREFTOPE = VCCTP.CodigoRef
							   FROM [remuneraciones].[View_ConstCalculoTopeProv] VCCTP
							   WHERE VCCTP.CodigoConcepto = @CODIGOVARIABLE AND
								     VCCTP.Cliente = @CLIENTE AND
									 VCCTP.Empresa = @__EMPRESA

						SET @SQL = 
						 '
						 SELECT @MONTOCALCULOOUT = CONST.' + @__COLUMNTOPE + '
								FROM ' + @__TABLETOPE + ' CONST 
								WHERE CONST.CodigoCargoMod = ''' + @CODIGOCARGOMOD + '''
						 '

						 EXEC sys.sp_executesql 
						 @SQL,
						 N'@MONTOCALCULOOUT NUMERIC OUTPUT',
						 @MONTOCALCULOOUT = @__MONTOCALCULOTOPE OUTPUT

						 IF(@MONTOCALCULO > @__MONTOCALCULOTOPE)
						 BEGIN
							
							SET @__DESCRIPCION = @__DESCRIPCIONTOPE
							SET @__TABLE = @__TABLETOPE
							SET @__COLUMN = @__COLUMNTOPE
							SET @__CODIGOREF = @__CODIGOREFTOPE
							SET @MONTOCALCULO = @__MONTOCALCULOTOPE

						 END

					 END

				END
				ELSE
				BEGIN
					
					SET @__DESCRIPCION = 'None'

				END

				IF(@__DESCRIPCION <> '')
				BEGIN

					INSERT INTO #ProvMargen
					VALUES(@DESCRIPCION,
					       CASE WHEN @TYPECALCULO = 'P' THEN
								  @DESCRIPCION
								  + 
								  '<br/> <p class="alert alert-info pdl-5 pdr-5 pdb-5 pdt-5" style="font-size: 12px;">El porcentaje de esta provisión se esta calculando sobre el ' + @__DESCRIPCION + '</p>'
						   ELSE
								@DESCRIPCION
								+ 
								'<br/> <p class="alert alert-warning  pdl-5 pdr-5 pdb-5 pdt-5" style="font-size: 12px;">El monto de esta povisión es fijo.</p>'
						   END,
						   CASE WHEN @TYPECALCULO = 'P' THEN
							  CAST(@VALORCONCEPTO AS VARCHAR(MAX)) + '%'
						   ELSE
								''
						   END,
						   CASE WHEN @TYPECALCULO = 'P' THEN
							   '$ ' + CAST([remuneraciones].[FNConvertMoney](ROUND(((@MONTOCALCULO / 100) * @VALORCONCEPTO), 0)) AS VARCHAR(MAX))
						   ELSE
							   '$ ' +  CAST([remuneraciones].[FNConvertMoney](@VALORCONCEPTO) AS VARCHAR(MAX))
						   END,
						   CASE WHEN @TYPECALCULO = 'P' THEN
							   @VALORCONCEPTO
						   ELSE
							   NULL
						   END,
						   CASE WHEN @TYPECALCULO = 'P' THEN
							   ROUND(((@MONTOCALCULO / 100) * @VALORCONCEPTO), 0)
						   ELSE
							   @VALORCONCEPTO
						   END,
						   '',
						   @CODIGOVARIABLE,
						   '',
						   @CODIGOCARGOMOD,
						   CASE WHEN @TYPECALCULO = 'P' THEN
							   @__DESCRIPCION
						   ELSE
							   NULL
						   END,
						   CASE WHEN @TYPECALCULO = 'P' THEN
							   @__CODIGOREF
						   ELSE
							   NULL
						   END)

				 END

				 FETCH CURSOR_PROVISIONES INTO @DESCRIPCION, @CODIGOVARIABLE, @VALORCONCEPTO, @TYPECALCULO

			 END

			 CLOSE CURSOR_PROVISIONES
			 DEALLOCATE CURSOR_PROVISIONES

			INSERT INTO #ProvMargen
			SELECT 'Provisión',
			       'Provisión',
			       '',
				   '$ '+ [TW_GENERAL_TEAMWORK].[dbo].[FN_CONVERTMONEY](
					   (SELECT SUM(Monto)
							   FROM #ProvMargen)
				   ),
				   NULL,
				   (SELECT SUM(Monto)
						   FROM #ProvMargen),
				   'Provision',
					'',
					'',
					@CODIGOCARGOMOD,
					'',
					''

			-- Se aginan gastos asociados al area de negocio => se incluye opcion de exclusion o no
			DECLARE CURSOR_MARGENES CURSOR
			         FOR SELECT VCAP.Descripcion,
								VCAP.CodigoVariable,
								VCAP.Valor,
								VCAP.TypeCalculo
								FROM [remuneraciones].[View_ConstAsignMargen] VCAP
								WHERE VCAP.Cliente = @CLIENTE AND
								      VCAP.Empresa = @__EMPRESA

			OPEN CURSOR_MARGENES
			
			FETCH CURSOR_MARGENES INTO @DESCRIPCION, @CODIGOVARIABLE, @VALORCONCEPTO, @TYPECALCULO

			WHILE @@FETCH_STATUS = 0
			BEGIN
				
				-- Validacion de gasto excluido del calculo

				SELECT @__EXC = CASE WHEN COUNT(1) > 0 THEN
				                    'S'
				                ELSE
								    'N'
								END
					   FROM [remuneraciones].[View_GastosMargenExc] VGME
					   WHERE VGME.CodigoCargoMod = [TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](@CODIGOCARGOMOD) AND 
							 VGME.CodigoGasto = @CODIGOVARIABLE

				IF(@TYPECALCULO = 'P')
				 BEGIN

					 SELECT @__COLUMN = VCAC.ColumnName,
							@__TABLE = VCAC.TableViewName,
							@__DESCRIPCION = VCAC.Descripcion,
							@__CODIGOREF = VCAC.CodigoRef
							FROM [remuneraciones].[View_ConstAsignCalculo] VCAC
							WHERE VCAC.CodigoConcepto = @CODIGOVARIABLE AND
								  VCAC.Cliente = @CLIENTE AND
								  VCAC.Empresa = @__EMPRESA

					 SET @SQL = 
					 CASE WHEN @__CODIGOREF != 'R005' THEN
						 '
						 SELECT @MONTOCALCULOOUT = CONST.' + @__COLUMN + '
								FROM ' + @__TABLE + ' CONST 
								WHERE CONST.CodigoCargoMod = ''' + @CODIGOCARGOMOD + '''
						 '
					ELSE
						'
						 SELECT @MONTOCALCULOOUT = CONST.' + @__COLUMN + '
								FROM ' + @__TABLE + ' CONST 
						 '
					END

					 EXEC sys.sp_executesql 
					 @SQL,
					 N'@MONTOCALCULOOUT NUMERIC OUTPUT',
					 @MONTOCALCULOOUT = @MONTOCALCULO OUTPUT

				END
				ELSE
				BEGIN
					
					SET @__DESCRIPCION = 'None'

				END

				IF(@__DESCRIPCION <> '')
				BEGIN

					INSERT INTO #ProvMargen
					VALUES(@DESCRIPCION,
					       CASE WHEN @__EXC = 'N' THEN
							   CASE WHEN @TYPECALCULO = 'P' THEN
									  @DESCRIPCION
									  + 
									  '<br/> <p class="alert alert-info pdl-5 pdr-5 pdb-5 pdt-5" style="font-size: 12px;">El porcentaje de este margen se esta calculando sobre el ' + @__DESCRIPCION + '</p>'
							   ELSE
									@DESCRIPCION
									+ 
									'<br/> <p class="alert alert-warning pdl-5 pdr-5 pdb-5 pdt-5" style="font-size: 12px;">El monto de esta margen es fijo.</p>'
							   END
						   ELSE
								@DESCRIPCION
								+ 
								'<br/> <p class="alert alert-danger pdl-5 pdr-5 pdb-5 pdt-5" style="font-size: 12px;">Este concepto fue excluido de la estructura de renta.</p>'
						   END,
						   CASE WHEN @__EXC = 'N' THEN
							   CASE WHEN @TYPECALCULO = 'P' THEN
								  CAST(@VALORCONCEPTO AS VARCHAR(MAX)) + '%'
							   ELSE
									''
							   END
						   ELSE
							  ''
						   END,
						   CASE WHEN @__EXC = 'N' THEN
							   CASE WHEN @TYPECALCULO = 'P' THEN
									CASE WHEN @__CODIGOREF != 'R005' THEN
								      '$ ' + CAST([remuneraciones].[FNConvertMoney](ROUND(((@MONTOCALCULO / 100) * @VALORCONCEPTO), 0)) AS VARCHAR(MAX))
								   ELSE	
								      '$ ' + CAST([remuneraciones].[FNConvertMoney](ROUND((@MONTOCALCULO * @VALORCONCEPTO), 0)) AS VARCHAR(MAX))
								   END
							   ELSE
								   '$ ' +  CAST([remuneraciones].[FNConvertMoney](@VALORCONCEPTO) AS VARCHAR(MAX))
							   END
						   ELSE
							  '$ 0'
						   END,
						   CASE WHEN @__EXC = 'N' THEN
							  @VALORCONCEPTO
						   ELSE
							  NULL
						   END,
						   CASE WHEN @__EXC = 'N' THEN
							   CASE WHEN @TYPECALCULO = 'P' THEN
									
									CASE WHEN @__CODIGOREF != 'R005' THEN
										ROUND(((@MONTOCALCULO / 100) * @VALORCONCEPTO), 0)
									ELSE
										ROUND((@MONTOCALCULO * @VALORCONCEPTO), 0)
									END
							   ELSE
								   @VALORCONCEPTO
							   END
						   ELSE
							  0
						   END,
						   'Gasto',
						   @CODIGOVARIABLE,
						   'S',
						   @CODIGOCARGOMOD,
						   CASE WHEN @TYPECALCULO = 'P' THEN
							   @__DESCRIPCION
						   ELSE
							   NULL
						   END,
						   CASE WHEN @TYPECALCULO = 'P' THEN
							   @__CODIGOREF
						   ELSE
							   NULL
						   END)

				END

				FETCH CURSOR_MARGENES INTO @DESCRIPCION, @CODIGOVARIABLE, @VALORCONCEPTO, @TYPECALCULO

			END
			
			CLOSE CURSOR_MARGENES
			DEALLOCATE CURSOR_MARGENES
			
			SELECT @PROVISION = SUM(Monto)
			       FROM #ProvMargen
				   WHERE TypeConcepto = 'Provision'

			SELECT @GASTOS = ISNULL(SUM(Monto), 0)
			       FROM #ProvMargen
				   WHERE TypeConcepto = 'Gasto' AND
				         OptWithExcluir = 'S'

			-- Asignacion de costo empresa y margen dinamico.
			DECLARE CURSOR_MARGEN CURSOR
			         FOR SELECT VCAP.Descripcion,
								VCAP.CodigoVariable,
								VCAP.PorcConcepto,
								VCAP.TypeCalculo
								FROM [remuneraciones].[View_ConstAsignProv] VCAP
								WHERE VCAP.TypeConcepto = 'Margen' AND
									  VCAP.Cliente = @CLIENTE AND
									  VCAP.Empresa = @__EMPRESA

			 OPEN CURSOR_MARGEN

			 FETCH CURSOR_MARGEN INTO @DESCRIPCION, @CODIGOVARIABLE, @VALORCONCEPTO, @TYPECALCULO

			 WHILE @@FETCH_STATUS = 0
			 BEGIN
				 
				 IF(@TYPECALCULO = 'P')
				 BEGIN

					 SELECT @__COLUMN = VCAC.ColumnName,
							@__TABLE = VCAC.TableViewName,
							@__DESCRIPCION = VCAC.Descripcion,
							@__CODIGOREF = VCAC.CodigoRef
							FROM [remuneraciones].[View_ConstAsignCalculo] VCAC
							WHERE VCAC.CodigoConcepto = 'M001' AND
								  VCAC.Cliente = @CLIENTE AND
								  VCAC.Empresa = @__EMPRESA

					PRINT @__CODIGOREF

					 IF(@__CODIGOREF != 'R006')
					 BEGIN

						 SET @SQL = 
						 '
						 SELECT @MONTOCALCULOOUT = CONST.' + @__COLUMN + '
								FROM ' + @__TABLE + ' CONST 
								WHERE CONST.CodigoCargoMod = ''' + @CODIGOCARGOMOD + '''
						 '
						 

					 END
					 
					EXEC sys.sp_executesql 
					@SQL,
					N'@MONTOCALCULOOUT NUMERIC OUTPUT',
					@MONTOCALCULOOUT = @MONTOCALCULO OUTPUT

				END
				ELSE
				BEGIN
					
					SET @__DESCRIPCION = 'None'

				END

				IF(@__DESCRIPCION <> '')
				BEGIN
					PRINT @PROVISION
					PRINT @GASTOS
					PRINT @TYPECALCULO
					SELECT @MONTOCALCULOCEMP = VHE.TotalHaberes
					       FROM [remuneraciones].[View_HaberesEstructura] VHE
						   WHERE VHE.CodigoCargoMod = @CODIGOCARGOMOD

					PRINT @MONTOCALCULOCEMP

					INSERT INTO #ProvMargen
					VALUES('Costo Empresa',
					       'Costo Empresa',
						   '',
						   '$ ' + CAST([remuneraciones].[FNConvertMoney](@PROVISION + @GASTOS + @MONTOCALCULOCEMP) AS VARCHAR(MAX)),
						   NULL,
						   @PROVISION + @GASTOS + @MONTOCALCULOCEMP,
						   'CostoEmpresa',
						   'C023',
						   '',
						   @CODIGOCARGOMOD,
						   CASE WHEN @TYPECALCULO = 'P' THEN
							   @__DESCRIPCION
						   ELSE
							   NULL
						   END,
						   CASE WHEN @TYPECALCULO = 'P' THEN
							   @__CODIGOREF
						   ELSE
							   NULL
						   END)

					 SELECT @COSTOEMPRESA = ISNULL(Monto, 0)
							FROM #ProvMargen
							WHERE TypeConcepto = 'CostoEmpresa'

					 IF(@__CODIGOREF = 'R006')
					 BEGIN
						
						SET @MONTOCALCULO = @COSTOEMPRESA

					 END

					 INSERT INTO #ProvMargen
					 VALUES(@DESCRIPCION,
					        CASE WHEN @TYPECALCULO = 'P' THEN
								  @DESCRIPCION
								  + 
								  '<br/> <p class="alert alert-info pdl-5 pdr-5 pdb-5 pdt-5" style="font-size: 12px;">El margen se esta calculando sobre el ' + @__DESCRIPCION + '</p>'
						   ELSE
								@DESCRIPCION
								+ 
								'<br/> <p class="alert alert-warning pdl-5 pdr-5 pdb-5 pdt-5" style="font-size: 12px;">El monto del margen es fijo.</p>'
						   END,
						   CASE WHEN @TYPECALCULO = 'P' THEN
							  CAST(@VALORCONCEPTO AS VARCHAR(MAX)) + '%'
						   ELSE
								''
						   END,
						   CASE WHEN @TYPECALCULO = 'P' THEN
							   '$ ' + CAST([remuneraciones].[FNConvertMoney](ROUND(((@MONTOCALCULO / 100) * @VALORCONCEPTO), 0)) AS VARCHAR(MAX))
						   ELSE
							   '$ ' +  CAST([remuneraciones].[FNConvertMoney](@VALORCONCEPTO) AS VARCHAR(MAX))
						   END,
						   CASE WHEN @TYPECALCULO = 'P' THEN
							  @VALORCONCEPTO
						   ELSE
							  NULL
						   END,
						   CASE WHEN @TYPECALCULO = 'P' THEN
							   ROUND(((@MONTOCALCULO / 100) * @VALORCONCEPTO), 0)
						   ELSE
							   @VALORCONCEPTO
						   END,
						   'Margen',
						   '',
						   '',
						   @CODIGOCARGOMOD,
						   CASE WHEN @TYPECALCULO = 'P' THEN
							   @__DESCRIPCION
						   ELSE
							   NULL
						   END,
						   CASE WHEN @TYPECALCULO = 'P' THEN
							   @__CODIGOREF
						   ELSE
							   NULL
						   END)

				END

				 FETCH CURSOR_MARGEN INTO @DESCRIPCION, @CODIGOVARIABLE, @VALORCONCEPTO, @TYPECALCULO

			 END

			 CLOSE CURSOR_MARGEN
			 DEALLOCATE CURSOR_MARGEN
			 
			 /** Aqui se indicaran valores excepcionales posterior al calculo del margen, pero sumando al costo a facturar */
			 
			 DECLARE @_SUMVARIABLESCOSTOAFACTURAR FLOAT
			 
			 SET @_SUMVARIABLESCOSTOAFACTURAR = 0
			 
			 SELECT @_SUMVARIABLESCOSTOAFACTURAR = ISNULL(SUM(Valor), 0)
			        FROM [remuneraciones].[VariablesCostoAFacturar] WITH (NOLOCK)
			        WHERE Cliente = @CLIENTE AND
						  Empresa = @__EMPRESA
						  
			INSERT INTO #ProvMargen
			       SELECT RMC.Descripcion,
					      RMC.Descripcion + '<br/> <p class="alert alert-warning pdl-5 pdr-5 pdb-5 pdt-5" style="font-size: 12px;">El monto de esta margen es fijo.</p>',
					      '',
					      '$ ' + [dbo].[FNConvertMoney](VCAF.Valor),
					      NULL,
					      VCAF.Valor,
					      'Gasto',
					      RMC.CodigoVariable,
					      'S',
					      @CODIGOCARGOMOD,
					      NULL,
					      NULL
			              FROM [remuneraciones].[VariablesCostoAFacturar] VCAF WITH (NOLOCK)
			              JOIN [remuneraciones].[RM_Constantes] RMC WITH (NOLOCK)
			              ON VCAF.CodigoVariable = RMC.CodigoVariable
					      WHERE VCAF.Cliente = @CLIENTE AND
								VCAF.Empresa = @__EMPRESA

			INSERT INTO #ProvMargen
			       SELECT 'Costo a facturar',
				          'Costo a facturar',
				          '',
						  '$ ' + [TW_GENERAL_TEAMWORK].[dbo].[FN_CONVERTMONEY](SUM(Monto) + @_SUMVARIABLESCOSTOAFACTURAR),
						  NULL,
						  SUM(Monto) + @_SUMVARIABLESCOSTOAFACTURAR,
						  'CostoFacturar',
						  'C024',
						  '',
					      @CODIGOCARGOMOD,
						  '',
						  ''
				          FROM #ProvMargen
						  WHERE TypeConcepto IN ('Margen', 'CostoEmpresa')

			SELECT @__VALORDIARIOCONST = VTS.ConstSueldo
			       FROM [remuneraciones].[View_TypeSueldo] VTS
				   WHERE VTS.Codigo = @__TYPESUELDO

			SELECT @COSTODIARIO = Monto / @__VALORDIARIOCONST
			       FROM #ProvMargen
				   WHERE TypeConcepto = 'CostoFacturar'

			INSERT INTO #ProvMargen
			       VALUES('Valor diario',
				          'Valor diario',
				          '',
						  '$ ' + [TW_GENERAL_TEAMWORK].[dbo].[FN_CONVERTMONEY](@COSTODIARIO),
						  NULL,
						  @COSTODIARIO,
						  '',
						  'C022',
						  '',
						  @CODIGOCARGOMOD,
						  '',
						  '')
			
			SELECT @__ESTADOACTUAL = VS.EstadoReal
			       FROM [remuneraciones].[View_Solicitudes] VS
				   WHERE VS.CodigoSolicitud = @CODIGOCARGOMOD

			IF(CHARINDEX('@', [dbo].[FNBase64Decode](@USUARIOCREADOR)) > 0)
			BEGIN
				
				SELECT @__USERID = VU.Id
					   FROM [cargamasiva].[View_Usuarios] VU
					   WHERE VU.Correo = [dbo].[FNBase64Decode](@USUARIOCREADOR)

			END
			ELSE
			BEGIN
				
				SELECT @__USERID = VU.Id
					   FROM [cargamasiva].[View_Usuarios] VU
					   WHERE VU.NombreUsuario = [dbo].[FNBase64Decode](@USUARIOCREADOR)

			END

			SELECT @__PROVMARGENRESTRINGED = COUNT(1)
			       FROM [remuneraciones].[View_UserRestringido] VU
				   WHERE VU.UserId = @__USERID

			CREATE TABLE #TempOrderBy (Descripcion VARCHAR(MAX), OrderBy NUMERIC)

			/** Creacion de order de provisiones */
			;WITH OrderBy AS
			(
				SELECT *
					   FROM [remuneraciones].[View_OrderByProvMargen] VOBPM
					   WHERE VOBPM.OrderBy BETWEEN 1 AND 5
				UNION
				SELECT Descripcion,
					   6
					   FROM [remuneraciones].[View_Constantes] VC
					   WHERE VC.Type = 'Gastos'
				UNION
				SELECT *
					   FROM [remuneraciones].[View_OrderByProvMargen] VOBPM
					   WHERE VOBPM.OrderBy BETWEEN 7 AND 10
			)
			INSERT INTO #TempOrderBy 
			       SELECT * 
						  FROM OrderBy OB
						  ORDER BY OB.OrderBy ASC

			IF(@__ESTADOACTUAL != 'APROB' AND @__ESTADOACTUAL != 'FD' AND @__ESTADOACTUAL != 'TER')
			BEGIN
				
				UPDATE [remuneraciones].[RM_CargosMod]
					   SET ValorDiario = @COSTODIARIO
					   WHERE CodigoCargoMod = [TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](@CODIGOCARGOMOD)
			       
				DELETE FROM [remuneraciones].[RM_ProvMargen]
					   WHERE CodigoCargoMod = @CODIGOCARGOMOD

				INSERT INTO [remuneraciones].[RM_ProvMargen]
					   SELECT *
							  FROM #ProvMargen

				IF(@__PROVMARGENRESTRINGED = 0)
				BEGIN

					SELECT *
						   FROM #ProvMargen PM
						   ORDER BY (SELECT OB.OrderBy
										    FROM #TempOrderBy OB
										    WHERE OB.Descripcion COLLATE SQL_Latin1_General_CP1_CI_AS = PM.ConceptoReal) ASC
						   
				END
				ELSE
				BEGIN
					
					SELECT *
					       FROM #ProvMargen PM
						   WHERE PM.Concepto COLLATE SQL_Latin1_General_CP1_CI_AS IN (SELECT VU.Concepto
																							 FROM [remuneraciones].[View_UserRestringido] VU
																							 WHERE VU.UserId = @__USERID)
						   ORDER BY (SELECT OB.OrderBy
										    FROM #TempOrderBy OB
										    WHERE OB.Descripcion COLLATE SQL_Latin1_General_CP1_CI_AS = PM.ConceptoReal) ASC
													
				END

			END
			ELSE
			BEGIN
				
				IF(@__PROVMARGENRESTRINGED = 0)
				BEGIN

					SELECT *
						   FROM [remuneraciones].[View_ProvMargen] VPM
					       WHERE VPM.CodigoCargoMod = @CODIGOCARGOMOD
						   ORDER BY (SELECT OB.OrderBy
										    FROM #TempOrderBy OB
										    WHERE OB.Descripcion COLLATE SQL_Latin1_General_CP1_CI_AS = VPM.ConceptoReal) ASC

				END
				ELSE
				BEGIN
					
					SELECT *
						   FROM [remuneraciones].[View_ProvMargen] VPM
					       WHERE VPM.CodigoCargoMod = @CODIGOCARGOMOD AND
						         VPM.Concepto IN (SELECT VU.Concepto
														 FROM [remuneraciones].[View_UserRestringido] VU
														 WHERE VU.UserId = @__USERID)
						   ORDER BY (SELECT OB.OrderBy
										    FROM #TempOrderBy OB
										    WHERE OB.Descripcion COLLATE SQL_Latin1_General_CP1_CI_AS = VPM.ConceptoReal) ASC

				END

			END

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION

		SELECT '500' 'Code',
		       'Ha ocurrido algo' 'Message',
			   ERROR_MESSAGE() 'Error',
			   ERROR_PROCEDURE() 'Procedure',
			   ERROR_LINE() 'ErrorLine',
			   @CODIGOCARGOMOD 'CargoMod'

	END CATCH
