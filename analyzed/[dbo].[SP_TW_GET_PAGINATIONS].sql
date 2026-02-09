CREATE PROCEDURE [dbo].[SP_TW_GET_PAGINATIONS](
	@AUTHENTICATE VARCHAR(MAX),
	@AGENTAPP VARCHAR(MAX),
	@TRABAJADOR VARCHAR(MAX),
	@TYPEPAGINATION VARCHAR(MAX),
	@PAGINATION VARCHAR(MAX),
	@HASBAJA VARCHAR(MAX),
	@TYPEFILTER VARCHAR(MAX),
	@DATAFILTER VARCHAR(MAX)
)
AS

	BEGIN TRY
		
		BEGIN TRANSACTION

			-- AUTENTIFICACION DE TOKEN DE SEGURIDAD PARA ACCESO A DATOS

			DECLARE @AUTHENTICATION VARCHAR(1),
					@CODE			VARCHAR(500),
					@MESSAGE		VARCHAR(MAX),
					@CONT			NUMERIC

			EXEC [TW_GENERAL_TEAMWORK].[dbo].[SP_GET_AUTHENTICATIONTOKENSECURITY] @AUTHENTICATE, @AGENTAPP, @AUTHENTICATION OUTPUT, @CODE OUTPUT, @MESSAGE OUTPUT

			-- END AUTENTIFICACION DE TOKEN DE SEGURIDAD PARA ACCESO A DATOS

			IF(@AUTHENTICATION = 'S' AND @CODE = '200' AND @MESSAGE = 'OK')
			BEGIN
				
				DECLARE @SQL VARCHAR(MAX)

				DECLARE @PROFILE VARCHAR(MAX)
				
				SELECT @PROFILE = UPPER(TWP.Nombre)
				       FROM [TW_GENERAL_TEAMWORK].[dbo].[TW_Usuarios] TWU WITH (NOLOCK)
					   INNER JOIN [TW_GENERAL_TEAMWORK].[dbo].[TW_Auth] TWA WITH (NOLOCK)
					   ON TWA.Usuario = TWU.Id
					   INNER JOIN [TW_GENERAL_TEAMWORK].[dbo].[TW_AuthProfiles] TWAP WITH (NOLOCK)
					   ON TWAP.Auth = TWA.Id
					   INNER JOIN [TW_GENERAL_TEAMWORK].[dbo].[TW_Profiles] TWP WITH (NOLOCK)
					   ON TWP.Id = TWAP.Profile 
					   WHERE TWU.Nombre + ' ' + TWU.Apellido = @TRABAJADOR

				SET @SQL =
				'DECLARE @INIT		VARCHAR(MAX), ' +
						'@END		VARCHAR(MAX), ' +
						'@RANGE		NUMERIC, ' +
						'@RANGEGET	VARCHAR(MAX),' +
						'@TOPERANGE  NUMERIC,' +
						'@TOPERANGEINIT NUMERIC ' +

				'DECLARE @ROWS NUMERIC,' +
				        '@ROUNDS NUMERIC,' +
						'@PAGETOTAL NUMERIC ' +

				'DECLARE @DIVIDE NUMERIC,' +
				        '@RESTO NUMERIC ' +

				'SET @INIT = SUBSTRING([TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](''' + @PAGINATION + '''), 1, CHARINDEX(''-'', [TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](''' +@PAGINATION + ''')) - 1) ' +
			    'SET @END = SUBSTRING([TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](''' + @PAGINATION + '''), CHARINDEX(''-'', [TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](''' + @PAGINATION + ''')) + 1, LEN([TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](''' + @PAGINATION + '''))) ' +
				
				'CREATE TABLE #Pagination(' +
					'NumeroPagina VARCHAR(MAX),' +
					'Rango VARCHAR(MAX),' +
					'Class VARCHAR(MAX),' +
					'Properties VARCHAR(MAX), ' +
					'TypeFilter VARCHAR(MAX), ' +
					'Filter VARCHAR(MAX), ' +
					'HasBaja VARCHAR(MAX) ' +
				') ' +

				'SET @ROUNDS = 0 ' +
				'SET @RANGE = 5 ' 
				+
				CASE WHEN @TYPEPAGINATION = 'SolicitudBaja' THEN
						'SELECT @ROWS = COUNT(*) ' +
							   'FROM [dbo].[View_SolicitudesBajaNew] VSB WITH (NOLOCK) ' +
							   ' ' +
									  CASE WHEN @PROFILE = 'KAM' OR @PROFILE = 'ASISTENTE KAM' THEN
											'WHERE (VSB.IdCliente IN (SELECT VKC.IdCliente 
										                                 FROM [cargamasiva].[View_KamClientes] VKC 
										                                 WHERE VKC.Kam = ''' + @TRABAJADOR + ''')  
												    OR Idcliente IN (SELECT VMC.IdCliente
								                           FROM [kam].[View_MultiCuentas] VMC
														   WHERE VMC.Asignacion = ''' + @TRABAJADOR + ''')
										            OR UsuarioCreador = ''' + @TRABAJADOR + ''') ' 
										   WHEN @PROFILE = 'ANALISTA FINIQUITOS' OR @PROFILE = 'ANALISTA FINIQUITOS Y BAJAS' THEN
											'WHERE ' +
											'HasBaja IN (''S'', ''N'') ' 
										   WHEN @PROFILE = 'COORDINADOR PROCESOS' THEN
											'WHERE  ' +
											'HasBaja IN (''S'', ''N'') ' 
									  ELSE
										''
									  END
									 +
									 CASE WHEN @TYPEFILTER = 'UnV0VHJhYmFqYWRvcg==' THEN
											'AND (Rut LIKE ''%' + @DATAFILTER + '%'' OR RutSoftland LIKE ''%' + @DATAFILTER + '%'') '
										  WHEN @TYPEFILTER = 'RW1wcmVzYQ==' THEN
											'AND Empresa = ''' + @DATAFILTER + ''' '
										  WHEN @TYPEFILTER = 'Tm9tYnJlIFRyYWJhamFkb3I=' THEN
											'AND NombreTrabajador LIKE ''%' + @DATAFILTER + '%'' '
										  WHEN @TYPEFILTER = 'Q2F1c2FsIERlc3ZpbmN1bGFjacOzbg==' THEN
											'AND Causal = ''' + @DATAFILTER + ''' '
										  WHEN @TYPEFILTER = 'QXJlYSBOZWdvY2lv' THEN
											'AND Cliente LIKE ''%' + @DATAFILTER + '%'' '
										  WHEN @TYPEFILTER = 'RmVjaGFz' THEN
											'AND FechaBaja BETWEEN [TW_GENERAL_TEAMWORK].[dbo].[FN_CONVERTDATEAAAAMMDD](SUBSTRING(''' + @DATAFILTER + ''', 1, CHARINDEX(''@'', ''' + @DATAFILTER + ''') - 1)) AND [TW_GENERAL_TEAMWORK].[dbo].[FN_CONVERTDATEAAAAMMDD](SUBSTRING(''' + @DATAFILTER + ''', CHARINDEX(''@'', ''' + @DATAFILTER + ''') + 1, LEN(''' + @DATAFILTER + ''')))'
										  WHEN @TYPEFILTER = 'RXN0YWRv' THEN
											'AND Estado = ''' + @DATAFILTER + ''' '
									 ELSE
										''
									 END
					 WHEN @TYPEPAGINATION = 'Proveedores' THEN
						'SELECT @ROWS = COUNT(*) ' + 
							   'FROM [dbo].[FZ_Proveedores] WITH (NOLOCK) ' +
							   'WHERE Vigente = ''S'' '
							   +
							   CASE WHEN @TYPEFILTER = 'Nombre' THEN
										'AND Nombre LIKE ''%' + @DATAFILTER + '%'' '
									WHEN @TYPEFILTER = 'Rut' THEN
										'AND Rut LIKE ''%' + @DATAFILTER + '%'' '
							   ELSE
									''
							   END
					WHEN @TYPEPAGINATION = 'Periodo' THEN
						'SELECT @ROWS = COUNT(*) ' + 
							   'FROM [dbo].[FZ_Periodo] WITH (NOLOCK) ' +
							   'WHERE Vigente = ''S'' '
							   +
							   CASE WHEN @TYPEFILTER = 'Empresa' THEN
										'AND Empresa LIKE ''%' + @DATAFILTER + '%'' '
									WHEN @TYPEFILTER = 'Periodo' THEN
										'AND Periodo LIKE ''%' + @DATAFILTER + '%'' '
							   ELSE
									''
							   END
					 WHEN @TYPEPAGINATION = 'Finiquitos' THEN
						'SELECT @ROWS = COUNT(*) ' +
							   'FROM [dbo].[View_Finiquitos] WITH (NOLOCK) ' +
							   'WHERE ' +
									 CASE WHEN @PROFILE = 'ANALISTA FINIQUITOS' OR @PROFILE = 'ANALISTA FINIQUITOS Y BAJAS' THEN
										   'CreadoPor LIKE ''%%'' '
										  WHEN @PROFILE = 'ADMINISTRADOR MURAL' THEN
											'(Estado = ''SENDPP'' AND MontoFiniquito < 500000) OR (Estado = ''VER'' AND MontoFiniquito >= 500000) '
										  WHEN @PROFILE = 'AUDITOR' THEN
										    'Estado = ''SENDPP'' AND MontoFiniquito >= 500000 '
									 ELSE
									   'CreadoPor LIKE ''%%'' '
									 END
									 +
									 CASE WHEN @TYPEFILTER = 'bm9uZQ==' THEN
								            'AND Folio LIKE ''%' + @DATAFILTER + '%'' '
									      WHEN @TYPEFILTER = 'UnV0VHJhYmFqYWRvcg==' THEN
											'AND (Rut LIKE ''%' + @DATAFILTER + '%'' OR RutNormal LIKE ''%' + @DATAFILTER + '%'') '
										  WHEN @TYPEFILTER = 'RW1wcmVzYQ==' THEN
											'AND Empresa = ''' + @DATAFILTER + ''' '
										  WHEN @TYPEFILTER = 'Tm9tYnJlIFRyYWJhamFkb3I=' THEN
											'AND Nombre LIKE ''%' + @DATAFILTER + '%'' '
										  WHEN @TYPEFILTER = 'Q2F1c2FsIERlc3ZpbmN1bGFjacOzbg==' THEN
											'AND Causal LIKE ''' + @DATAFILTER + '%'' '
										  WHEN @TYPEFILTER = 'QXJlYSBOZWdvY2lv' THEN
											'AND AreaNegocio LIKE ''%' + @DATAFILTER + '%'' '
										  WHEN @TYPEFILTER = 'RmVjaGFz' THEN
											'AND FechaTransaccion BETWEEN [TW_GENERAL_TEAMWORK].[dbo].[FN_CONVERTDATEAAAAMMDD](SUBSTRING(''' + @DATAFILTER + ''', 1, CHARINDEX(''@'', ''' + @DATAFILTER + ''') - 1)) AND [TW_GENERAL_TEAMWORK].[dbo].[FN_CONVERTDATEAAAAMMDD](SUBSTRING(''' + @DATAFILTER + ''', CHARINDEX(''@'', ''' + @DATAFILTER + ''') + 1, LEN(''' + @DATAFILTER + ''')))'
										  WHEN @TYPEFILTER = 'RXN0YWRv' THEN
											CASE WHEN @DATAFILTER = 'PPL90' THEN
													'AND Folio IN (SELECT FNF.IdFiniquito
																		   FROM [dbo].[FN_Finiquito] FNF WITH (NOLOCK)
																		   LEFT JOIN [dbo].[FN_TrackingFiniquito] FNTF WITH (NOLOCK)
																		   ON FNTF.IdFiniquito = FNF.IdFiniquito
																		   WHERE (FNF.Estado = ''EMI'' AND FNTF.Pago = ''CHQ'') AND
																				 DATEDIFF(DAY, (SELECT CAST(MAX(FNHF.FechaRegistro) AS DATE)
																									   FROM [dbo].[FN_HistoryFiniquitos] FNHF WITH (NOLOCK)
																									   WHERE FNHF.Estado = ''EMI'' AND
																											 FNHF.Finiquito = FNF.IdFiniquito), CAST(GETDATE() AS DATE)) < 90) '
												 WHEN @DATAFILTER = 'PPT90' THEN
													'AND Folio IN (SELECT FNF.IdFiniquito
																		   FROM [dbo].[FN_Finiquito] FNF WITH (NOLOCK)
																		   LEFT JOIN [dbo].[FN_TrackingFiniquito] FNTF WITH (NOLOCK)
																		   ON FNTF.IdFiniquito = FNF.IdFiniquito
																		   WHERE (FNF.Estado = ''EMI'' AND FNTF.Pago = ''CHQ'') AND
																				 DATEDIFF(DAY, (SELECT CAST(MAX(FNHF.FechaRegistro) AS DATE)
																									   FROM [dbo].[FN_HistoryFiniquitos] FNHF WITH (NOLOCK)
																									   WHERE FNHF.Estado = ''EMI'' AND
																											 FNHF.Finiquito = FNF.IdFiniquito), CAST(GETDATE() AS DATE)) >= 90) '
												 WHEN @DATAFILTER = 'TEFP' THEN
													'AND Folio IN (SELECT FNF.IdFiniquito
																		   FROM [dbo].[FN_Finiquito] FNF WITH (NOLOCK)
																		   LEFT JOIN [dbo].[FN_TrackingFiniquito] FNTF WITH (NOLOCK)
																		   ON FNTF.IdFiniquito = FNF.IdFiniquito
																		   WHERE (FNF.Estado = ''EMI'' AND FNTF.Pago = ''TEF'') OR
																				 (FNF.Estado = ''NOT'' AND FNTF.Pago = ''TEF'')) '
												WHEN @DATAFILTER = 'CAL' THEN
												   'AND Estado = ''' + @DATAFILTER + ''' AND MontoFiniquito > 0 '
												WHEN @DATAFILTER = 'VERPCONF' THEN
												   'AND Estado = ''SENDPP'' OR Estado = ''VER'' '
											ELSE
												'AND Estado = ''' + @DATAFILTER + ''' '
											END
										  WHEN @TYPEFILTER = 'Q29zdG9DZXJv' THEN
										    'AND MontoFiniquito = 0 '
									 ELSE
										''
									 END
					WHEN @TYPEPAGINATION = 'Simulacion' THEN
						'SELECT @ROWS = COUNT(*) ' +
							   'FROM [dbo].[View_Simulaciones] WITH (NOLOCK) ' +
							   'WHERE ' +
									 CASE WHEN @PROFILE = 'ANALISTA FINIQUITOS' OR @PROFILE = 'ANALISTA FINIQUITOS Y BAJAS' THEN
										   'CreadoPor = ''' + @TRABAJADOR + ''' '
									 ELSE
									   'CreadoPor LIKE ''%%'' '
									 END
									 +
									 CASE WHEN @TYPEFILTER = 'bm9uZQ==' THEN
								            'AND Folio LIKE ''%' + @DATAFILTER + '%'' '
									      WHEN @TYPEFILTER = 'UnV0VHJhYmFqYWRvcg==' THEN
											'AND (Rut LIKE ''%' + @DATAFILTER + '%'' OR RutNormal LIKE ''%' + @DATAFILTER + '%'') '
										  WHEN @TYPEFILTER = 'RW1wcmVzYQ==' THEN
											'AND Empresa = ''' + @DATAFILTER + ''' '
										  WHEN @TYPEFILTER = 'Tm9tYnJlIFRyYWJhamFkb3I=' THEN
											'AND Nombre LIKE ''%' + @DATAFILTER + '%'' '
										  WHEN @TYPEFILTER = 'Q2F1c2FsIERlc3ZpbmN1bGFjacOzbg==' THEN
											'AND Causal LIKE ''' + @DATAFILTER + '%'' '
										  WHEN @TYPEFILTER = 'QXJlYSBOZWdvY2lv' THEN
											'AND AreaNegocio LIKE ''%' + @DATAFILTER + '%'' '
										  WHEN @TYPEFILTER = 'RmVjaGFz' THEN
											'AND FechaTransaccion BETWEEN [TW_GENERAL_TEAMWORK].[dbo].[FN_CONVERTDATEAAAAMMDD](SUBSTRING(''' + @DATAFILTER + ''', 1, CHARINDEX(''@'', ''' + @DATAFILTER + ''') - 1)) AND [TW_GENERAL_TEAMWORK].[dbo].[FN_CONVERTDATEAAAAMMDD](SUBSTRING(''' + @DATAFILTER + ''', CHARINDEX(''@'', ''' + @DATAFILTER + ''') + 1, LEN(''' + @DATAFILTER + ''')))'
										  WHEN @TYPEFILTER = 'RXN0YWRv' THEN
											'AND Estado = ''' + @DATAFILTER + ''' '
										  WHEN @TYPEFILTER = 'Q29zdG9DZXJv' THEN
										    'AND MontoFiniquito = 0 '
									 ELSE
										''
									 END
					WHEN @TYPEPAGINATION = 'Gastos' THEN
						'SELECT @ROWS = COUNT(*) ' +
							   'FROM [dbo].[View_Gastos] '
							   +
							   CASE WHEN @TYPEFILTER = 'Code' THEN
										'WHERE Codigo LIKE ''%' + @DATAFILTER + '%'' '
									WHEN @TYPEFILTER = 'NumDoc' THEN
										'WHERE NumeroDocumento LIKE ''%' + @DATAFILTER + '%'' '
									WHEN @TYPEFILTER = 'Periodo' THEN
									    'WHERE Periodo LIKE ''%' + @DATAFILTER + '%'' '
							   ELSE
									''
							   END
					WHEN @TYPEPAGINATION = 'CuentasOrigen' THEN
					    'SELECT @ROWS = COUNT(*) ' +
							   'FROM [dbo].[View_CuentasOrigen] '
							   +
							   CASE WHEN @TYPEFILTER = 'NumeroCuenta' THEN
										'WHERE NumeroCuenta LIKE ''%' + @DATAFILTER + '%'' '
									WHEN @TYPEFILTER = 'Banco' THEN
										'WHERE Banco LIKE ''%' + @DATAFILTER + '%'' '
									WHEN @TYPEFILTER = 'RutCuenta' THEN
										'WHERE RutCuenta LIKE ''%' + @DATAFILTER + '%'' '
							   ELSE
								''
							   END
					WHEN @TYPEPAGINATION = 'ProcesosPago' THEN
					    'SELECT @ROWS = COUNT(1) ' +
							   'FROM [dbo].[View_ProcesosPago] '
					WHEN @TYPEPAGINATION = 'Complementos' THEN
					   'SELECT @ROWS = COUNT(1)
					           FROM [dbo].[View_Complementos] '
								+
								CASE WHEN @TYPEFILTER = 'CodigoFiniquito' THEN
										'WHERE CodigoFiniquito = ''' + @DATAFILTER + ''''
										WHEN @TYPEFILTER = 'CodigoComplemento' THEN
										'WHERE CodigoComplemento = ''' + @DATAFILTER +  ''''
										WHEN @TYPEFILTER = 'Estado' THEN
										'WHERE Estado = ''' + @DATAFILTER + ''' '
								ELSE
									''
								END
								+
								' '
					WHEN @TYPEPAGINATION = 'ValorDiario' THEN
					   'SELECT @ROWS = COUNT(1)
					           FROM [finanzas].[View_ValorDiario]
						       '
						       +
						       CASE WHEN @TYPEFILTER = 'Empresa' THEN
									  'WHERE Empresa = ''' + @DATAFILTER + ''' '
								    WHEN @TYPEFILTER = 'CargoMod' THEN
									  'WHERE CargoMod LIKE ''%' + @DATAFILTER + '%'' '
								    WHEN @TYPEFILTER = 'Cliente' THEN
									  'WHERE Cliente LIKE ''%' + @DATAFILTER + '%'' '
						       ELSE
							      ''
						       END
						       + 
							   ''
				ELSE
					''
				END
				+
				'IF(CHARINDEX(''.'', CAST((@ROWS / @RANGE) AS VARCHAR(MAX))) = 0) ' +
				'BEGIN ' +
					'SET @DIVIDE = (@ROWS / @RANGE) ' +
				'END ' +
				'ELSE ' +
				'BEGIN ' +
					'SET @DIVIDE = CAST(SUBSTRING(CAST((@ROWS / @RANGE) AS VARCHAR(MAX)), 1, CHARINDEX(''.'', CAST((@ROWS / @RANGE) AS VARCHAR(MAX))) - 1) AS NUMERIC) ' +
				'END ' +

				'SET @RESTO = (@ROWS % @RANGE) ' +

				'SET @PAGETOTAL = @DIVIDE + CASE WHEN @RESTO > 0 THEN ' +
												'1 ' +
										  'ELSE ' +
												'0 ' +
										  'END ' +

				'IF(@ROWS > 0) ' +
				'BEGIN ' +

					'INSERT INTO #Pagination ' +
					       'VALUES(''&laquo; Primero'', ' +
								  '[TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_ENCODE](''1-'' + CAST(@RANGE AS VARCHAR(MAX))), ' +
								  ''''', ' +
								  'CASE WHEN ''1-'' + CAST(@RANGE AS VARCHAR(MAX)) = [TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](''' + @PAGINATION + ''') THEN ' +
								    '''disabled="disabled" '' ' +
								  'ELSE ' +
									''''' ' +
								 'END, ' +
								 '''' + @TYPEFILTER + ''', ' +
								 '''' + @DATAFILTER + ''', ' +
								 '''' + @HASBAJA + ''') ' +

					'INSERT INTO #Pagination ' +
						   'VALUES(''&lsaquo; Anterior'', ' +
								  '[TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_ENCODE](CAST(CAST(@INIT AS NUMERIC) - @RANGE AS VARCHAR(MAX)) + ''-'' + CAST(CAST(@END AS NUMERIC) - @RANGE AS VARCHAR(MAX))), ' +
								  ''''', ' +
								  'CASE WHEN ''1-'' + CAST(@RANGE AS VARCHAR(MAX)) = [TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](''' + @PAGINATION + ''') THEN ' +
								    '''disabled="disabled" '' ' +
								  'ELSE ' +
									''''' ' +
								  'END, ' +
								  '''' + @TYPEFILTER + ''', ' +
								  '''' + @DATAFILTER + ''', ' +
								  '''' + @HASBAJA + ''') ' +
				'END ' +

				'IF(CAST(@END AS NUMERIC) / @RANGE < 9) ' +
				'BEGIN ' +
					
					'IF((CAST(@END AS NUMERIC) / @RANGE) + 11 > @PAGETOTAL) ' +
					'BEGIN ' +
						'SET @TOPERANGE = @PAGETOTAL ' +
					'END ' +
					'ELSE ' +
					'BEGIN ' +
						'SET @TOPERANGE = 11 ' +
					'END ' +

					'WHILE (@ROUNDS + 1) < @TOPERANGE + 1 ' +
					'BEGIN ' +
						
						'SET @RANGEGET = CAST((@RANGE * @ROUNDS) + 1 AS VARCHAR(MAX)) + ''-'' + CAST((@RANGE * (@ROUNDS + 1)) AS VARCHAR(MAX)) ' +

						'INSERT INTO #Pagination ' +
							   'VALUES(CAST(@ROUNDS + 1 AS VARCHAR(MAX)), ' +
									  '[TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_ENCODE](@RANGEGET), ' +
									  'CASE WHEN @RANGEGET = [TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](''' + @PAGINATION + ''') THEN ' +
										'''active'' ' +
									  'ELSE ' +
										''''' ' +
									  'END, ' +
									  ''''', ' +
									  '''' + @TYPEFILTER + ''', ' +
									  '''' + @DATAFILTER + ''', ' +
									  '''' + @HASBAJA + ''') ' +
					
						'SET @ROUNDS = @ROUNDS + 1 ' +

					'END ' +

				'END ' +
				'ELSE ' +
				'BEGIN ' +
					
					'IF((CAST(@END AS NUMERIC) / @RANGE) + 5 > @PAGETOTAL) ' +
					'BEGIN ' +
						'SET @TOPERANGE = @PAGETOTAL ' +
						'SET @TOPERANGEINIT = 9 ' +
					'END ' +
					'ELSE ' +
					'BEGIN ' +
						'SET @TOPERANGE = (CAST(@END AS NUMERIC) / @RANGE) + 4 ' +
						'SET @TOPERANGEINIT = 5 ' +
					'END ' +

					'SET @ROUNDS = ((CAST(@END AS NUMERIC) / @RANGE) - @TOPERANGEINIT) ' +

					'WHILE @ROUNDS < CAST(@END AS NUMERIC) / @RANGE ' +
					'BEGIN ' +
						
						'SET @RANGEGET = CAST((@RANGE * @ROUNDS) + 1 AS VARCHAR(MAX)) + ''-'' + CAST((@RANGE * (@ROUNDS + 1)) AS VARCHAR(MAX)) ' +

						'INSERT INTO #Pagination ' +
							   'VALUES(CAST((@ROUNDS + 1) AS VARCHAR(MAX)), ' +
									  '[TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_ENCODE](@RANGEGET), ' +
									  'CASE WHEN @RANGEGET = [TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](''' + @PAGINATION + ''') THEN ' +
										'''active'' ' +
									  'ELSE ' +
										''''' ' +
									  'END, ' +
									  ''''', ' +
									  '''' + @TYPEFILTER + ''', ' +
									  '''' + @DATAFILTER + ''', ' +
									  '''' + @HASBAJA + ''') ' +
					
						'SET @ROUNDS = @ROUNDS + 1 ' +

					'END ' +

					'SET @ROUNDS = (CAST(@END AS NUMERIC) / @RANGE) ' +

					'WHILE @ROUNDS < @TOPERANGE ' +
					'BEGIN ' +
						
						'SET @RANGEGET = CAST((@RANGE * @ROUNDS) + 1 AS VARCHAR(MAX)) + ''-'' + CAST((@RANGE * (@ROUNDS + 1)) AS VARCHAR(MAX)) ' +

						'INSERT INTO #Pagination ' +
							   'VALUES(CAST((@ROUNDS + 1) AS VARCHAR(MAX)), ' +
									  '[TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_ENCODE](@RANGEGET), ' +
									  'CASE WHEN @RANGEGET = [TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](''' + @PAGINATION + ''') THEN ' +
										'''active'' ' +
									  'ELSE ' +
										''''' ' +
									  'END, ' +
									  ''''', ' +
									  '''' + @TYPEFILTER + ''', ' +
									  '''' + @DATAFILTER + ''', ' +
									  '''' + @HASBAJA + ''') ' +
					
						'SET @ROUNDS = @ROUNDS + 1 ' +
					'END ' +
				'END ' +

				'IF(@ROWS > 0) ' +
				'BEGIN ' +
					
					'INSERT INTO #Pagination ' +
					       'VALUES(''Siguiente &rsaquo;'', ' +
								  '[TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_ENCODE](CAST(CAST(@INIT AS NUMERIC) + @RANGE AS VARCHAR(MAX)) + ''-'' + CAST(CAST(@END AS NUMERIC) + @RANGE AS VARCHAR(MAX))), ' +
								  ''''', ' +
								  'CASE WHEN CAST((@PAGETOTAL * @RANGE) - @RANGE + 1 AS VARCHAR(MAX)) + ''-'' + CAST(@PAGETOTAL * @RANGE AS VARCHAR(MAX)) = [TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](''' + @PAGINATION + ''') THEN ' +
								    '''disabled="disabled" '' ' +
								  'ELSE ' +
									''''' ' +
								  'END, ' +
								  '''' + @TYPEFILTER + ''', ' +
								  '''' + @DATAFILTER + ''', ' +
								  '''' + @HASBAJA + ''') ' +

					'INSERT INTO #Pagination ' + 
						   'VALUES(''Ultimo &raquo;'', ' +
								  '[TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_ENCODE](CAST((@PAGETOTAL * @RANGE) - @RANGE + 1 AS VARCHAR(MAX)) + ''-'' + CAST(@PAGETOTAL * @RANGE AS VARCHAR(MAX))), ' +
								  ''''', ' +
								  'CASE WHEN CAST((@PAGETOTAL * @RANGE) - @RANGE + 1 AS VARCHAR(MAX)) + ''-'' + CAST(@PAGETOTAL * @RANGE AS VARCHAR(MAX)) = [TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](''' + @PAGINATION + ''') THEN ' +
								    '''disabled="disabled" '' ' +
								  'ELSE ' +
									''''' ' +
								  'END, ' +
								  '''' + @TYPEFILTER + ''', ' +
								  '''' + @DATAFILTER + ''', ' +
								  '''' + @HASBAJA + ''') ' +

				'END ' +

				'SELECT * ' +
				       'FROM #Pagination '

				EXEC(@SQL)

			END
			ELSE
			BEGIN

				SELECT @CODE 'Code',
					   @MESSAGE 'Message'

			END	

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH

		ROLLBACK TRANSACTION

		SELECT '500' 'Code',
		       @SQL 'sql',
		       ERROR_LINE() 'ErrorLine',
		       ERROR_MESSAGE() 'ErrorMessage',
			   ERROR_NUMBER() 'ErrorNumber',
			   ERROR_PROCEDURE() 'ErrorProcedure',
			   ERROR_SEVERITY() 'ErrorSeverity',
			   ERROR_STATE() 'ErrorState'

	END CATCH