CREATE PROCEDURE [remuneraciones].[__SPRM_CargoMod_Dashboard](
	@USUARIOCREADOR VARCHAR(MAX)
)
AS
	
	BEGIN TRY
		
		BEGIN TRANSACTION
			
			DECLARE @PROFILE VARCHAR(MAX),
			        @SQL VARCHAR(MAX)
					
			DECLARE @__USERID VARCHAR(MAX),
			        @__FILTERCUENTA VARCHAR(MAX)

			IF(CHARINDEX('@', [dbo].[FNBase64Decode](@USUARIOCREADOR)) > 0)
			BEGIN

				SELECT @PROFILE = UPPER(TWP.Nombre),
					   @__USERID = CAST(TWU.Id AS VARCHAR(MAX))
					   FROM [TW_GENERAL_TEAMWORK].[dbo].[TW_Usuarios] TWU WITH (NOLOCK)
					   INNER JOIN [TW_GENERAL_TEAMWORK].[dbo].[TW_Auth] TWA WITH (NOLOCK)
					   ON TWA.Usuario = TWU.Id
					   INNER JOIN [TW_GENERAL_TEAMWORK].[dbo].[TW_AuthProfiles] TWAP WITH (NOLOCK)
					   ON TWAP.Auth = TWA.Id
					   INNER JOIN [TW_GENERAL_TEAMWORK].[dbo].[TW_Profiles] TWP WITH (NOLOCK)
					   ON TWP.Id = TWAP.Profile 
					   WHERE TWU.Correo = [dbo].[FNBase64Decode](@USUARIOCREADOR)

			END
			ELSE
			BEGIN
				
				SELECT @PROFILE = UPPER(TWP.Nombre),
					   @__USERID = CAST(TWU.Id AS VARCHAR(MAX))
					   FROM [TW_GENERAL_TEAMWORK].[dbo].[TW_Usuarios] TWU WITH (NOLOCK)
					   INNER JOIN [TW_GENERAL_TEAMWORK].[dbo].[TW_Auth] TWA WITH (NOLOCK)
					   ON TWA.Usuario = TWU.Id
					   INNER JOIN [TW_GENERAL_TEAMWORK].[dbo].[TW_AuthProfiles] TWAP WITH (NOLOCK)
					   ON TWAP.Auth = TWA.Id
					   INNER JOIN [TW_GENERAL_TEAMWORK].[dbo].[TW_Profiles] TWP WITH (NOLOCK)
					   ON TWP.Id = TWAP.Profile 
					   WHERE TWU.NombreUsuario = [dbo].[FNBase64Decode](@USUARIOCREADOR)

			END


			SET @__FILTERCUENTA = 
			'
				(VCME.Cliente + ''-'' + VCME.Empresa IN (SELECT VC.Codigo + ''-'' + VC.Empresa
									                            FROM [cargamasiva].[View_Clientes] VC
																INNER JOIN [kam].[View_KamCliente] VKC
																ON VKC.IdCliente = VC.Id
																WHERE VKC.Kam = ' + @__USERID + ') OR
				VCME.Cliente + ''-'' + VCME.Empresa IN (SELECT VC.Codigo + ''-'' + VC.Empresa
																FROM [cargamasiva].[View_Clientes] VC
																INNER JOIN [kam].[View_KamCliente] VKC
																ON VKC.IdCliente = VC.Id
																INNER JOIN [kam].[View_AsistenteKamCliente] VAKC
																ON VAKC.KamCliente = VKC.Id
																WHERE VAKC.Asistente = ' + @__USERID + '))
			'

			
			SET @SQL = 
			CASE WHEN @PROFILE = 'KAM' THEN
					'
						SELECT (SELECT COUNT(1)
						               FROM [remuneraciones].[View_CargosModEstructura] VCME
									   WHERE VCME.Estado IN (''CREATE'') AND
									         VCME.Type = ''E'' AND
											 ' + @__FILTERCUENTA + ') ''Creaciones'',
						      (SELECT COUNT(1)
						               FROM [remuneraciones].[View_CargosModEstructura] VCME
									   WHERE VCME.Estado IN (''CREATE'') AND
									         VCME.Type = ''E'' AND
											 ' + @__FILTERCUENTA + ' AND
											 VCME.EstadoRechazo = ''RZ'') ''RechazadosKam'',
					          ''NA'' ''RechazadosRem'',
							   (SELECT COUNT(1)
						               FROM [remuneraciones].[View_CargosModEstructura] VCME
									   WHERE VCME.Estado IN (''PDAPROB'') AND
									         VCME.Type = ''E'' AND
											 ' + @__FILTERCUENTA + ') ''PendientesFinanzas'',
							   (SELECT COUNT(1)
						               FROM [remuneraciones].[View_EstructuraRentaBase] VCME
									   WHERE VCME.Estado IN (''APROB'') AND
									         VCME.Type = ''E'' AND
											 ' + @__FILTERCUENTA + ') ''PendientesRemuneraciones'',
							   (SELECT COUNT(1)
						               FROM [remuneraciones].[View_EstructuraRentaBase] VCME
									   WHERE VCME.Estado IN (''FD'') AND
									         VCME.Type = ''E'' AND
											 ' + @__FILTERCUENTA + ') ''PendientesFirmaDigital'',
							   (SELECT COUNT(1)
						               FROM [remuneraciones].[View_EstructuraRentaBase] VCME
									   WHERE VCME.Estado IN (''TER'') AND
									         VCME.Type = ''E'' AND
											 ' + @__FILTERCUENTA + ') ''Terminados'',
							   (SELECT COUNT(1)
						               FROM [remuneraciones].[View_CargosModEstructura] VCME
									   WHERE VCME.Estado IN (''CREATE'') AND
									         VCME.Type = ''S'' AND
											 ' + @__FILTERCUENTA + ') ''Simulaciones'',
							   (SELECT COUNT(1)
						               FROM [remuneraciones].[View_CargosModEstructura] VCME
									   WHERE VCME.Estado IN (''PDAPROB'') AND
									         VCME.Type = ''S'' AND
											 ' + @__FILTERCUENTA + ') ''ValidacionCotizaciones'',
							   (SELECT COUNT(1)
						               FROM [remuneraciones].[View_CargosModEstructura] VCME
									   WHERE VCME.Estado IN (''APROB'') AND
									         VCME.Type = ''S'' AND
											 ' + @__FILTERCUENTA + ') ''CotizacionesAprobadas'',
							   (SELECT COUNT(1)
						               FROM [remuneraciones].[View_CargosModEstructura] VCME
									   WHERE VCME.Estado IN (''CREATE'') AND
									         VCME.Type = ''S'' AND
											 ' + @__FILTERCUENTA + ' AND
											 VCME.EstadoRechazo = ''RZ'') ''CotizacionesRechazadas'',
							   ''' + @PROFILE + ''' ''Profile''
					'
				 WHEN @PROFILE = 'FINANZAS' OR @PROFILE = 'ADMINISTRADOR' THEN
					'
					SELECT ''NA'' ''Creaciones'',
						   ''NA'' ''RechazadosKam'',
						   SUM(VD.RechazadosRem) ''RechazadosRem'',
						   ''NA'' ''Simulaciones'',
						   SUM(VD.PendientesFinanzas) ''PendientesFinanzas'',
						   SUM(VD.PendientesRemuneraciones) ''PendientesRemuneraciones'',
						   SUM(VD.PendientesFirmaDigital) ''PendientesFirmaDigital'',
						   SUM(VD.Terminados) ''Terminados'',
						   SUM(VD.ValidacionCotizaciones) ''ValidacionCotizaciones'',
						   SUM(VD.CotizacionesAprobadas) ''CotizacionesAprobadas'',
						   ''NA'' ''CotizacionesRechazadas'',
						   ''' + @PROFILE + ''' ''Profile''
						   FROM [remuneraciones].[View_Dashboard] VD
					'
				WHEN @PROFILE = 'ANALISTA DE REMUNERACIONES' OR @PROFILE = 'COORDINADOR CONTRATOS' THEN
				    '
					SELECT ''NA'' ''Creaciones'',
						   ''NA'' ''RechazadosRem'',
						   ''NA'' ''RechazadosKam'',
						   ''NA'' ''Simulaciones'',
						   ''NA'' ''PendientesFinanzas'',
						   ''NA'' ''ValidacionCotizaciones'',
						   ''NA'' ''CotizacionesAprobadas'',
						   ''NA'' ''CotizacionesRechazadas'',
						   (SELECT COUNT(1)
						           FROM [remuneraciones].[View_EstructuraRentaBase] VERB
								   WHERE VERB.Estado = ''APROB'' AND
								         VERB.Empresa IN (SELECT VAA.Empresa
										                         FROM [remuneraciones].[View_AsignAnalista] VAA
														         WHERE VAA.UserId = ' + @__USERID + ')) ''PendientesRemuneraciones'',
						   (SELECT COUNT(1)
						           FROM [remuneraciones].[View_EstructuraRentaBase] VERB
								   WHERE VERB.Estado = ''FD'' AND
								         VERB.Empresa IN (SELECT VAA.Empresa
										                         FROM [remuneraciones].[View_AsignAnalista] VAA
														         WHERE VAA.UserId = ' + @__USERID + ')) ''PendientesFirmaDigital'',
						   (SELECT COUNT(1)
						           FROM [remuneraciones].[View_EstructuraRentaBase] VERB
								   WHERE VERB.Estado = ''TER'' AND
								         VERB.Empresa IN (SELECT VAA.Empresa
										                         FROM [remuneraciones].[View_AsignAnalista] VAA
														         WHERE VAA.UserId = ' + @__USERID + ')) ''Terminados'',
						   ''' + @PROFILE + ''' ''Profile''
					'
				WHEN @PROFILE = 'ANALISTA CONTRATOS' THEN
				    '
					SELECT ''NA'' ''Creaciones'',
						   ''NA'' ''RechazadosRem'',
						   ''NA'' ''RechazadosKam'',
						   ''NA'' ''Simulaciones'',
						   ''NA'' ''PendientesFinanzas'',
						   ''NA'' ''PendientesRemuneraciones'',
						   ''NA'' ''PendientesFirmaDigital'',
						   ''NA'' ''ValidacionCotizaciones'',
						   ''NA'' ''CotizacionesAprobadas'',
						   ''NA'' ''CotizacionesRechazadas'',
						   (SELECT COUNT(1)
						           FROM [remuneraciones].[View_EstructuraRentaBase] VERB
								   WHERE VERB.Estado = ''TER'') ''Terminados'',
						   ''' + @PROFILE + ''' ''Profile''
					'
			ELSE
				''
			END
			
			EXEC(@SQL)

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION

	END CATCH
