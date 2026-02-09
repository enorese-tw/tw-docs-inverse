CREATE PROCEDURE [remuneraciones].[__SPRM_CargoMod_Solicitudes](
	@USUARIOCREADOR VARCHAR(MAX),
	@PAGINATION VARCHAR(MAX),
	@TYPEFILTER VARCHAR(MAX),
	@DATAFILTER VARCHAR(MAX)
)
AS
	
	BEGIN TRY
		
		BEGIN TRANSACTION
			
			DECLARE @INIT	VARCHAR(MAX),
					@END	VARCHAR(MAX)

			DECLARE @SQL VARCHAR(MAX)

			DECLARE @PROFILE VARCHAR(MAX),
					@__USERID VARCHAR(MAX)

			SET @INIT = SUBSTRING([TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](@PAGINATION), 1, CHARINDEX('-', [TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](@PAGINATION)) - 1)
			SET @END = SUBSTRING([TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](@PAGINATION), CHARINDEX('-', [TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](@PAGINATION)) + 1, LEN([TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](@PAGINATION)))
				
			DECLARE @__EXCEPPROVMARG NUMERIC,
			        @__USER NUMERIC
	
			IF(CHARINDEX('@', [dbo].[FNBase64Decode](@USUARIOCREADOR)) > 0)
			BEGIN

				SELECT  @PROFILE = UPPER(TWP.Nombre),
						@__USERID = CAST(TWU.Id AS VARCHAR(MAX))
						FROM [TW_GENERAL_TEAMWORK].[dbo].[TW_Usuarios] TWU WITH (NOLOCK)
						INNER JOIN [TW_GENERAL_TEAMWORK].[dbo].[TW_Auth] TWA WITH (NOLOCK)
						ON TWA.Usuario = TWU.Id
						INNER JOIN [TW_GENERAL_TEAMWORK].[dbo].[TW_AuthProfiles] TWAP WITH (NOLOCK)
						ON TWAP.Auth = TWA.Id
						INNER JOIN [TW_GENERAL_TEAMWORK].[dbo].[TW_Profiles] TWP WITH (NOLOCK)
						ON TWP.Id = TWAP.Profile 
						WHERE TWU.Correo = [dbo].[FNBase64Decode](@USUARIOCREADOR)

				SELECT @__USER = VU.Id
					   FROM [cargamasiva].[View_Usuarios] VU
					   WHERE VU.Correo = [dbo].[FNBase64Decode](@USUARIOCREADOR)

			END
			ELSE
			BEGIN
				
				SELECT  @PROFILE = UPPER(TWP.Nombre),
						@__USERID = CAST(TWU.Id AS VARCHAR(MAX))
						FROM [TW_GENERAL_TEAMWORK].[dbo].[TW_Usuarios] TWU WITH (NOLOCK)
						INNER JOIN [TW_GENERAL_TEAMWORK].[dbo].[TW_Auth] TWA WITH (NOLOCK)
						ON TWA.Usuario = TWU.Id
						INNER JOIN [TW_GENERAL_TEAMWORK].[dbo].[TW_AuthProfiles] TWAP WITH (NOLOCK)
						ON TWAP.Auth = TWA.Id
						INNER JOIN [TW_GENERAL_TEAMWORK].[dbo].[TW_Profiles] TWP WITH (NOLOCK)
						ON TWP.Id = TWAP.Profile 
						WHERE TWU.NombreUsuario = [dbo].[FNBase64Decode](@USUARIOCREADOR)

				SELECT @__USER = VU.Id
					   FROM [cargamasiva].[View_Usuarios] VU
					   WHERE VU.NombreUsuario = [dbo].[FNBase64Decode](@USUARIOCREADOR)

			END

			SELECT @__EXCEPPROVMARG = COUNT(1)
			       FROM [remuneraciones].[View_ExcepcionProvMargen] VEPM
				   WHERE VEPM.Usuario = @__USER

			/** Tipo de visualización por defecto */
			IF(@TYPEFILTER = '')
			BEGIN

				SET @TYPEFILTER = 'Estado '
				SET @DATAFILTER = CASE WHEN @PROFILE = 'KAM' THEN
										'SOLIC'
									   WHEN @PROFILE = 'FINANZAS' THEN
										'PDAPROB'
									   WHEN @PROFILE = 'ANALISTA DE REMUNERACIONES' OR @PROFILE = 'COORDINADOR CONTRATOS' THEN
										'APROB'
									   WHEN @PROFILE = 'ANALISTA CONTRATOS' OR @PROFILE = 'ADMINISTRADOR' THEN
										'FD'
								  ELSE
									''
								  END

			END

			SET @SQL =
			'
				;WITH Solicitudes AS
				(
					SELECT *, 
						   ROW_NUMBER() OVER(ORDER BY FechaCreacionDate DESC) AS ROW_NUMBER 
						   FROM [remuneraciones].[View_Solicitudes] VS 
						   '
						   +
						   CASE WHEN @PROFILE = 'KAM' THEN
								  '
									WHERE ((VS.Cliente + ''-'' + VS.Empresa IN (SELECT VC.Codigo + ''-'' + VC.Empresa
									                                                  FROM [cargamasiva].[View_Clientes] VC
																					  INNER JOIN [kam].[View_KamCliente] VKC
																					  ON VKC.IdCliente = VC.Id
																					  WHERE VKC.Kam = ' + @__USERID + ')) OR
										  (VS.Cliente + ''-'' + VS.Empresa IN (SELECT VC.Codigo + ''-'' + VC.Empresa
									                                                  FROM [cargamasiva].[View_Clientes] VC
																					  INNER JOIN [kam].[View_KamCliente] VKC
																					  ON VKC.IdCliente = VC.Id
																					  INNER JOIN [kam].[View_AsistenteKamCliente] VAKC
																					  ON VAKC.KamCliente = VKC.Id
																					  WHERE VAKC.Asistente = ' + @__USERID + '))) 
								  
								  '
								WHEN @PROFILE = 'FINANZAS' THEN
								  'WHERE EstadoReal IN (''PDAPROB'', ''APROB'', ''SOLIC'', ''TER'', ''TERT'', ''FD'') '
								WHEN @PROFILE = 'ANALISTA DE REMUNERACIONES' OR @PROFILE = 'COORDINADOR CONTRATOS' THEN
								  'WHERE EstadoReal IN (''APROB'', ''FD'', ''TER'')  AND
								         Empresa IN (SELECT VAA.Empresa
										                    FROM [remuneraciones].[View_AsignAnalista] VAA
														    WHERE VAA.UserId = ' + @__USERID + ') AND
										 Type = ''E''
								  '
								WHEN @PROFILE = 'ANALISTA CONTRATOS' THEN
								  'WHERE EstadoReal IN (''TER'') '
								WHEN @PROFILE = 'ADMINISTRADOR' THEN
								  'WHERE EstadoReal IN (''PDAPROB'', ''APROB'', ''SOLIC'', ''TER'', ''TERT'', ''FD'') '
						   ELSE
							  ''
						   END
						   +
						   CASE WHEN @TYPEFILTER = 'CodigoSolicitud' THEN
								  ' AND CodigoSolicitud = ''' + @DATAFILTER + ''' '
								WHEN @TYPEFILTER = 'CodigoCargoMod' THEN
								  ' AND CodigoCargoMod LIKE ''%' + @DATAFILTER + '%'' '
								WHEN @TYPEFILTER = 'NombreCargoMod' THEN
								  ' AND NombreCargoMod LIKE ''%' + @DATAFILTER + '%'' '
								WHEN @TYPEFILTER = 'NombreCargo' THEN
								  ' AND NombreCargo LIKE ''%' + @DATAFILTER + '%'' '
								WHEN @TYPEFILTER = 'Estado ' THEN
								  CASE WHEN @DATAFILTER = 'PDAPROB' THEN
										'AND (EstadoReal = ''PDAPROB'' AND Type = ''E'')'
									   WHEN @DATAFILTER = 'PDAPROB-C' THEN
										'AND (EstadoReal = ''PDAPROB'' AND Type = ''S'')'
									   WHEN @DATAFILTER = 'SOLICS' THEN
										'AND (EstadoReal = ''CREATE'' AND Type = ''S'')'
									   WHEN @DATAFILTER = 'CREATEE' THEN
										'AND (EstadoReal = ''CREATE'' AND Type = ''E'')'
									   WHEN @DATAFILTER = 'RZ' THEN
									    'AND EstadoRechazo = ''' + @DATAFILTER + ''' AND Type = ''E'' '
									   WHEN @DATAFILTER = 'RZR' THEN
									    'AND EstadoRechazo = ''' + @DATAFILTER + ''' AND Type = ''E'' '
									   WHEN @DATAFILTER = 'RZ-C' THEN
									    'AND EstadoRechazo = ''RZ'' AND Type = ''S'' '
									   WHEN @DATAFILTER = 'APROB-C' THEN
										'AND (EstadoReal = ''APROB'' AND Type = ''S'')'
								  ELSE
									 'AND EstadoReal = ''' + @DATAFILTER + ''' AND Type = ''E'' '
								  END
						   ELSE
							   ''
						   END
						   +
						  '
				)
				SELECT TOP(50) CodigoCargoMod,
					   Empresa,
					   NombreSolicitud,
					   Creado,
					   CreadoPor,
					   Cliente,
					   Comentarios,
					   Border,
					   Glyphicon,
					   GlyphiconColor,
					   FechaCreacionDate,
					   CodigoSolicitud,
					   CodigoCliente,
					   NombreCargo,
					   UltimaActualizacion,
					   Estado,
					   '
					   +
					   CASE WHEN @PROFILE = 'KAM' THEN
							   '
								OptDesechar,
								OptPdf,
								CASE WHEN EstadoReal = ''CREATE'' THEN
								   OptEditar
								ELSE
								   ''N''
								END ''OptEditar'',
								CASE WHEN EstadoReal = ''CREATE'' THEN
									OptSolicitar
								ELSE
									''N''
								END ''OptSolicitar'',
								OptRechazar,
								''N'' ''OptPublicar'',
								SectHaber,
								SectDesc,
								CASE WHEN ' + CAST(@__EXCEPPROVMARG AS VARCHAR(MAX)) + ' > 0 THEN
									''S''
								ELSE
									''N''
								END ''SectPorvMargen'',
								OptDescargarReporteFirmaDigital,
								OptRechazarRem,
								''S'' ''OptHistorial'',
								'
							WHEN @PROFILE = 'FINANZAS' THEN
							    '
								OptDesechar,
								OptPdf,
								OptEditar,
								OptSolicitar,
								OptRechazar,
								''N'' ''OptPublicar'',
								SectHaber,
								SectDesc,
								SectPorvMargen,
								OptDescargarReporteFirmaDigital,
								OptRechazarRem,
								''S'' ''OptHistorial'',
								'
							WHEN @PROFILE = 'ANALISTA DE REMUNERACIONES' OR @PROFILE = 'COORDINADOR CONTRATOS' THEN
							    '
								OptDesechar, 
								OptPdf, 
								''N'' ''OptEditar'', 
								''N'' ''OptSolicitar'', 
								OptRechazar, 
								OptPublicar, 
								SectHaber, 
								SectDesc, 
								SectPorvMargen, 
								OptDescargarReporteFirmaDigital,
								OptRechazarRem,
								''S'' ''OptHistorial'',
								'
							WHEN @PROFILE = 'ANALISTA CONTRATOS' THEN
							    '
								OptDesechar,
								OptPdf,
								''N'' ''OptEditar'', 
								''N'' ''OptSolicitar'', 
								OptRechazar, 
								OptPublicar, 
								SectHaber, 
								SectDesc, 
								SectPorvMargen,
								OptDescargarReporteFirmaDigital,
								OptRechazarRem,
								''S'' ''OptHistorial'',
								'
							WHEN @PROFILE = 'ADMINISTRADOR' THEN
								'
								OptDesechar,
								OptPdf,
								OptEditar, 
								OptSolicitar, 
								OptRechazar, 
								OptPublicar, 
								SectHaber, 
								SectDesc, 
								SectPorvMargen,
								OptDescargarReporteFirmaDigital,
								OptRechazarRem,
								''S'' ''OptHistorial'',
								'
					   ELSE
					      ''
					   END
					   +
					   '
					   Wizards,
					   GlosaCodigo,
					   ''' + @PROFILE + ''' ''Profile''
				       FROM Solicitudes
			'

			EXEC (@SQL)

			--select @SQL

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION
		
		SELECT ERROR_MESSAGE() 'Error',
		       ERROR_LINE() 'Line'

	END CATCH
