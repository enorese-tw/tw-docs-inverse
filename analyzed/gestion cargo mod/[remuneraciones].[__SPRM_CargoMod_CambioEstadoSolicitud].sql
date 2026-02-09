CREATE PROCEDURE [remuneraciones].[__SPRM_CargoMod_CambioEstadoSolicitud](
	@USUARIOCREADOR VARCHAR(MAX),
	@ESTADO VARCHAR(MAX),
	@CODIGOCARGOMOD VARCHAR(MAX),
	@OBSERVACION VARCHAR(MAX),
	@CODE VARCHAR(MAX) OUTPUT,
	@MESSAGE VARCHAR(MAX) OUTPUT,
	@MESSAGESIMPLE VARCHAR(MAX) OUTPUT,
	@CODINE VARCHAR(MAX) OUTPUT,
	@GLOSA VARCHAR(MAX) OUTPUT,
	@EMPRESA VARCHAR(MAX) OUTPUT
)
AS
	
	BEGIN TRY
		
		BEGIN TRANSACTION
			
			DECLARE @PROFILE VARCHAR(MAX),
			        @HTMLEMAIL VARCHAR(MAX)

			DECLARE @__USERNAME VARCHAR(MAX),
					@__CODIGOSOLICITUD VARCHAR(MAX),
					@__NAMECARGOMOD VARCHAR(MAX),
					@__ASUNTOEMAIL VARCHAR(MAX),
					@__CORREOUSER VARCHAR(MAX),
					@__CC VARCHAR(MAX)

			DECLARE @__SQL NVARCHAR(MAX),
					@__CODINE VARCHAR(MAX),
					@__CODINESEQ NUMERIC,
					@__GLOSAVALIDAR VARCHAR(MAX),
					@__EMPRESA VARCHAR(MAX),
					@__EMPRESASOFTLAND VARCHAR(MAX),
					@__CODINEASIGN VARCHAR(MAX),
					@__CLIENTE VARCHAR(MAX),
					@__MOTIVORECHAZO VARCHAR(MAX)

			DECLARE @__CODIGOCARGOMOD VARCHAR(MAX),
			        @__CREADOR VARCHAR(MAX),
					@__CODIGOSOFTLAND VARCHAR(MAX),
					@__COPY VARCHAR(MAX),
					@__USUARIOCREADOR VARCHAR(MAX),
					@__TYPE VARCHAR(MAX),
					@__CODCLI VARCHAR(MAX)

			DECLARE @__ANALISTAREM VARCHAR(MAX)
			
			DECLARE @CODINEOUT VARCHAR(MAX)

			SELECT @PROFILE = UPPER(TWP.Nombre)
				   FROM [TW_GENERAL_TEAMWORK].[dbo].[TW_Usuarios] TWU WITH (NOLOCK)
				   INNER JOIN [TW_GENERAL_TEAMWORK].[dbo].[TW_Auth] TWA WITH (NOLOCK)
				   ON TWA.Usuario = TWU.Id
				   INNER JOIN [TW_GENERAL_TEAMWORK].[dbo].[TW_AuthProfiles] TWAP WITH (NOLOCK)
				   ON TWAP.Auth = TWA.Id
				   INNER JOIN [TW_GENERAL_TEAMWORK].[dbo].[TW_Profiles] TWP WITH (NOLOCK)
				   ON TWP.Id = TWAP.Profile 
				   WHERE TWU.Correo = [TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](@USUARIOCREADOR)
				   or  TWU.NombreUsuario = [TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](@USUARIOCREADOR)
				   or  TWU.NombreUsuario = @USUARIOCREADOR

			IF(@ESTADO = 'APROB' OR @ESTADO = 'PDAPROB')
			BEGIN
				
				DECLARE @SUELDO FLOAT,
				        @NOMBRECARGO VARCHAR(MAX),
						@ORIGEN VARCHAR(MAX)

				SELECT @SUELDO = RMCM.SueldoBase,
				       @NOMBRECARGO = CASE WHEN LTRIM(RTRIM(REPLACE(RMCM.CargoMod, RMCM.Cliente, ''))) <> '' THEN
					                     RMCM.CargoMod
									  ELSE
									     NULL
									  END,
					   @ORIGEN = RMCM.Type
				       FROM [remuneraciones].[RM_CargosMod] RMCM WITH (NOLOCK)
					   WHERE RMCM.CodigoCargoMod = [TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](@CODIGOCARGOMOD)

				IF(@SUELDO IS NOT NULL AND @NOMBRECARGO IS NOT NULL)
				BEGIN

					UPDATE [remuneraciones].[RM_CargosMod]
						   SET Estado = @ESTADO,
						       EstadoRechazo = NULL,
							   FechaUltimaActualizacion = GETDATE(),
							   UsuarioUltimaActualizacion = @USUARIOCREADOR,
							   UltimoComentario = CASE WHEN  @PROFILE = 'KAM' THEN
													CASE WHEN Type = 'E' THEN
														'Se ha enviado a validación por parte de finanzas de la solicitud de cargo mod'
													ELSE
														'Se ha enviado a validación por parte de finanzas la cotización'
													END
												  ELSE
													CASE WHEN Type = 'E' THEN
														'Se ha validado, aprobado y fue  enviado a creación de softland por parte de remuneraciones de la solicitud de cargo mod.'
													ELSE
														'Se ha validado la cotización y se encuentra aprobada.'
													END
												  END
						   WHERE CodigoCargoMod = [TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](@CODIGOCARGOMOD)

					
					SET @CODE = '200'

					SET @MESSAGESIMPLE = CASE WHEN @PROFILE = 'KAM' THEN
											'Solicitud enviada al área de finanzas, Para su respectiva validación de estructura de renta (Nota: si desea volver a editar, 
											 deberá ser rechazada la solicitud para que vuelva a su poder.)
											'
									     ELSE
											CASE WHEN @ORIGEN = 'E' THEN
													'Solicitud enviada al área de remuneraciones, Para su respectiva creación en softland (Nota: si desea volver a editar, 
													 deberá ser rechazada la solicitud para que vuelva a su poder.)
													'
												WHEN @ORIGEN = 'S' THEN
													'La simulación se ha guardado (Si requiere que esta simulación se convierta en Solicitud de cargo mod, deberá buscarla y moverla para que se solicite al area de remuneraciones.)
													'
											END
										 END

					SET @MESSAGE = 
					CASE WHEN @PROFILE <> 'KAM' THEN
						CASE WHEN @ORIGEN = 'E' THEN
								'
								 <h1 class="color-150x3 family-teamwork">Solicitud enviada al área de remuneraciones</h1>
								 <h3 style="color: rgb(100, 100, 100)" class="family-teamwork">Para su respectiva creación en softland</h3>
								 <h4 class="color-150x3 family-teamwork">Nota: si desea volver a editar, deberá ser rechazada la solicitud para que vuelva a su poder.</h4> 
								'
							WHEN @ORIGEN = 'S' THEN
								'
								 <h1 class="color-150x3 family-teamwork">La simulación se ha guardado</h1>
								 <h3 style="color: rgb(100, 100, 100)" class="family-teamwork">
									Si requiere que esta simulación se convierta en Solicitud de cargo mod, deberá buscarla y moverla para que se solicite al area de remuneraciones.
								 </h3>
								 <h4 class="color-150x3 family-teamwork">Nota: si desea volver a editar, deberá ser rechazada la solicitud para que vuelva a su poder.</h4> 
								'
						END
					END


					-- EMITE CORREO SEGUN PERFILAMIENTO.
					
					SELECT @__CODIGOSOLICITUD = VS.CodigoCargoMod,
						   @__NAMECARGOMOD = VS.NombreCargo,
						   @__TYPE = VS.Type,
						   @__EMPRESA = VS.Empresa,
						   @__CODCLI = VS.Cliente
						   FROM [remuneraciones].[View_Solicitudes] VS
						   WHERE VS.CodigoSolicitud = @CODIGOCARGOMOD

					IF(CHARINDEX('@', [dbo].[FNBase64Decode](@USUARIOCREADOR)) > 0)
					BEGIN

						SELECT @__USERNAME = VU.Nombre,
							   @__CORREOUSER = VU.Correo
							   FROM [cargamasiva].[View_Usuarios] VU
							   WHERE VU.Correo = [dbo].[FNBase64Decode](@USUARIOCREADOR)

					END
					ELSE
					BEGIN
					
						SELECT @__USERNAME = VU.Nombre,
							   @__CORREOUSER = VU.Correo
							   FROM [cargamasiva].[View_Usuarios] VU
							   WHERE VU.NombreUsuario = [dbo].[FNBase64Decode](@USUARIOCREADOR)

					END

					IF(@PROFILE = 'KAM')
					BEGIN
						
						;WITH Correos AS
						(
							SELECT VKC.Correo
									FROM [cargamasiva].[View_KamClientes] VKC
									WHERE VKC.IdCliente IN (SELECT VC.Id
																	FROM [cargamasiva].[View_Clientes] VC
																	WHERE VC.Codigo = @__CODCLI AND
																		VC.Empresa = @__EMPRESA)
							UNION
							SELECT VAKC.Correo
									FROM [cargamasiva].[View_AsistenteKamCliente] VAKC
									WHERE VAKC.KamCliente IN (SELECT VKC.IdKamCliente
																	FROM [cargamasiva].[View_KamClientes] VKC
																	WHERE VKC.IdCliente IN (SELECT VC.Id
																									FROM [cargamasiva].[View_Clientes] VC
																									WHERE VC.Codigo = @__CODCLI AND
																											VC.Empresa = @__EMPRESA))
						)

						(SELECT @__CC = STUFF(
									(SELECT DISTINCT '; ' + Correo
											FROM Correos
									FOR XML PATH ('')),
						1,2, ''))

						IF(@__TYPE = 'E')
						BEGIN

							SET @__ASUNTOEMAIL = 
							'Solicitud Cargo Mod : ' + @__NAMECARGOMOD + ' - ' + @__CODIGOSOLICITUD + ' (Enviado a Validación)'

							EXEC [remuneraciones].[__SPRM_CargoMod_Email_EnvioValidacion]
							@__CODIGOSOLICITUD,
							@CODIGOCARGOMOD,
							@__NAMECARGOMOD,
							@__USERNAME,
							@HTMLEMAIL OUTPUT

							EXEC msdb.dbo.sp_send_dbmail
								@profile_name = 'NoReply',
								@recipients = @__CORREOUSER,
								@copy_recipients = @__CC,
								@blind_copy_recipients = 'sebastian.salas@team-work.cl',
								@body = @HTMLEMAIL,
								@subject = @__ASUNTOEMAIL,
								@body_format = 'HTML'

							EXEC [remuneraciones].[__SPRM_CargoMod_Email_PendienteValidacion]
								@__CODIGOSOLICITUD,
								@CODIGOCARGOMOD,
								@__NAMECARGOMOD,
								'Finanzas',
								@HTMLEMAIL OUTPUT

							SET @__ASUNTOEMAIL = 'Solicitud Cargo Mod : ' + @__NAMECARGOMOD + ' - ' + @__CODIGOSOLICITUD + ' (Pendiente de Validación)'

							EXEC msdb.dbo.sp_send_dbmail
								@profile_name = 'NoReply',
								@recipients = 'marco.vidal@team-work.cl;natalia.ormeno@team-work.cl',
								@blind_copy_recipients = 'sebastian.salas@team-work.cl',
								@body = @HTMLEMAIL,
								@subject = @__ASUNTOEMAIL,
								@body_format = 'HTML'

						END

						IF(@__TYPE = 'S')
						BEGIN
							
							SET @__ASUNTOEMAIL = 
							'Cotización : ' + @__NAMECARGOMOD + ' - ' + @__CODIGOSOLICITUD + ' (Enviado a Validación)'

							EXEC [remuneraciones].[__SPRM_CargoMod_Email_EnvioValCotizacion]
								 @__CODIGOSOLICITUD,
								 @CODIGOCARGOMOD,
								 @__NAMECARGOMOD,
								 @__USERNAME,
								 @HTMLEMAIL OUTPUT

								EXEC msdb.dbo.sp_send_dbmail
									@profile_name = 'NoReply',
									@recipients = @__CORREOUSER,
									@copy_recipients = @__CC,
									@blind_copy_recipients = 'sebastian.salas@team-work.cl',
									@body = @HTMLEMAIL,
									@subject = @__ASUNTOEMAIL,
									@body_format = 'HTML'

							EXEC [remuneraciones].[__SPRM_CargoMod_Email_PendienteValCotizacion]
								@__CODIGOSOLICITUD,
								@CODIGOCARGOMOD,
								@__NAMECARGOMOD,
								'Finanzas',
								@HTMLEMAIL OUTPUT

							SET @__ASUNTOEMAIL = 'Cotización : ' + @__NAMECARGOMOD + ' - ' + @__CODIGOSOLICITUD + ' (Pendiente de Validación)'

							EXEC msdb.dbo.sp_send_dbmail
								@profile_name = 'NoReply',
								@recipients = 'marco.vidal@team-work.cl;natalia.ormeno@team-work.cl',
								@blind_copy_recipients = 'sebastian.salas@team-work.cl',
								@body = @HTMLEMAIL,
								@subject = @__ASUNTOEMAIL,
								@body_format = 'HTML'

						END

					END

					IF(@PROFILE = 'FINANZAS')
					BEGIN
						IF(@__TYPE = 'E')
						BEGIN
							
							SET @__ANALISTAREM =
							(SELECT STUFF(
									(SELECT DISTINCT '; ' + Correo
										   FROM [cargamasiva].[View_Usuarios] VU
										   WHERE VU.Id IN (SELECT VAA.UserId
																  FROM [remuneraciones].[View_AsignAnalista] VAA
																  WHERE VAA.Empresa = @__EMPRESA)
									FOR XML PATH ('')),
							1,2, ''))

							SET @__ASUNTOEMAIL = 
							'Solicitud Cargo Mod : ' + @__NAMECARGOMOD + ' - ' + @__CODIGOSOLICITUD + ' (Enviado a Creación Softland)'

							EXEC [remuneraciones].[__SPRM_CargoMod_Email_EnvioCreacionSoftland]
							@__CODIGOSOLICITUD,
							@CODIGOCARGOMOD,
							@__NAMECARGOMOD,
							'Finanzas',
							@HTMLEMAIL OUTPUT

							EXEC msdb.dbo.sp_send_dbmail
								@profile_name = 'NoReply',
								@recipients = 'marco.vidal@team-work.cl;natalia.ormeno@team-work.cl',
								@blind_copy_recipients = 'sebastian.salas@team-work.cl',
								@body = @HTMLEMAIL,
								@subject = @__ASUNTOEMAIL,
								@body_format = 'HTML'

							EXEC [remuneraciones].[__SPRM_CargoMod_Email_PendienteCreacionSoftland]
							@__CODIGOSOLICITUD,
							@CODIGOCARGOMOD,
							@__NAMECARGOMOD,
							'Remuneraciones',
							@HTMLEMAIL OUTPUT

							SET @__ASUNTOEMAIL = 'Solicitud Cargo Mod : ' + @__NAMECARGOMOD + ' - ' + @__CODIGOSOLICITUD + ' (Pendiente de Creación Softland)'

							EXEC msdb.dbo.sp_send_dbmail
								@profile_name = 'NoReply',
								@recipients = @__ANALISTAREM,
								@blind_copy_recipients = 'sebastian.salas@team-work.cl',
								@body = @HTMLEMAIL,
								@subject = @__ASUNTOEMAIL,
								@body_format = 'HTML'

						END
						
						IF(@__TYPE = 'S')
						BEGIN
							
							DECLARE @__USERCREADOR VARCHAR(MAX)

							SELECT @__USERCREADOR = VS.UsuarioCreador
								   FROM [remuneraciones].[View_Solicitudes] VS
								   WHERE CodigoCargoMod = @__CODIGOSOLICITUD

							IF(CHARINDEX('@', [dbo].[FNBase64Decode](@__USERCREADOR)) > 0)
							BEGIN

								SELECT @__USERNAME = VU.Nombre,
									   @__CORREOUSER = VU.Correo
									   FROM [cargamasiva].[View_Usuarios] VU
									   WHERE VU.Correo = [dbo].[FNBase64Decode](@__USERCREADOR)

							END
							ELSE
							BEGIN
					
								SELECT @__USERNAME = VU.Nombre,
									   @__CORREOUSER = VU.Correo
									   FROM [cargamasiva].[View_Usuarios] VU
									   WHERE VU.NombreUsuario = [dbo].[FNBase64Decode](@__USERCREADOR)

							END
							
							SET @__ASUNTOEMAIL = 
							'Cotización : ' + @__NAMECARGOMOD + ' - ' + @__CODIGOSOLICITUD + ' (Aprobada)'

							EXEC [remuneraciones].[__SPRM_CargoMod_Email_EnvioCotizacionAprobada]
								 @__CODIGOSOLICITUD,
								 @CODIGOCARGOMOD,
								 @__NAMECARGOMOD,
								 @__USERNAME,
								 @HTMLEMAIL OUTPUT

							EXEC msdb.dbo.sp_send_dbmail
								@profile_name = 'NoReply',
								@recipients = @__CORREOUSER,
								@copy_recipients = 'marco.vidal@team-work.cl;natalia.ormeno@team-work.cl',
								@blind_copy_recipients = 'sebastian.salas@team-work.cl',
								@body = @HTMLEMAIL,
								@subject = @__ASUNTOEMAIL,
								@body_format = 'HTML'

						END

					END

					SET @CODINE = @CODIGOCARGOMOD

				END
				ELSE
				BEGIN
					
					SET @CODE = '300'

					SET @MESSAGE = 'Corregir los siguientes incovenientes <br/> <ul>'

					IF(@SUELDO IS NULL)
					BEGIN
						SET @MESSAGE = @MESSAGE + '<li>Debe indicar el sueldo del cargo mod</li>'
					END

					IF(@NOMBRECARGO IS NULL)
					BEGIN
						SET @MESSAGE = @MESSAGE + '<li>Debe indicar el nombre del cargo mod</li>'
					END

					SET @MESSAGE = @MESSAGE + '</ul>'

				END


			END

			IF(@ESTADO = 'RZ')
			BEGIN
				
				UPDATE [remuneraciones].[RM_CargosMod]
						SET Estado = CASE WHEN @ESTADO = 'RZ' THEN
											 'CREATE'
						                  WHEN @ESTADO = 'RZR' THEN
										     'PDAPROB'
									 END,
						    EstadoRechazo = @ESTADO,
							FechaUltimaActualizacion = GETDATE(),
							UsuarioUltimaActualizacion = @USUARIOCREADOR,
							Wizards = 1,
							UltimoComentario =  'Se rechaza cargo mod, por el siguiente motivo: ' + @OBSERVACION
						WHERE CodigoCargoMod = [TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](@CODIGOCARGOMOD)

				SET @CODE = '200'

				SET @MESSAGESIMPLE = CASE WHEN @PROFILE = 'KAM' THEN
										'La solicitud ha sido rechazada exitosamente, ha vuelto a su poder para ser modificada de acuerdo a lo que estime conveniente.
										'
									 ELSE
										'La solicitud ha sido rechazada exitosamente, ha vuelto a poder del creador para ser modificada de acuerdo a lo que él estime conveniente.
										'
									 END

				SET @MESSAGE = 
				CASE WHEN @PROFILE <> 'KAM' THEN
					'
						<h1 class="color-150x3 family-teamwork">La solicitud ha sido rechazada exitosamente</h1>
						<h3 style="color: rgb(100, 100, 100)" class="family-teamwork">
							ha vuelto a poder del creador para ser modificada de acuerdo a lo que él estime conveniente.
						</h3>
					'
				END

				SELECT @__CODIGOSOLICITUD = VS.CodigoCargoMod,
					   @__NAMECARGOMOD = VS.NombreCargo,
					   @__MOTIVORECHAZO = VS.Comentarios,
					   @__USUARIOCREADOR = VS.UsuarioCreador,
					   @__EMPRESA = VS.Empresa,
					   @__CODCLI = VS.Cliente
					   FROM [remuneraciones].[View_Solicitudes] VS
					   WHERE VS.CodigoSolicitud = @CODIGOCARGOMOD
				
				IF(CHARINDEX('@', [dbo].[FNBase64Decode](@__USUARIOCREADOR)) > 0)
				BEGIN

					SELECT @__USERNAME = VU.Nombre,
						   @__CORREOUSER = VU.Correo
						   FROM [cargamasiva].[View_Usuarios] VU
						   WHERE VU.Correo = [dbo].[FNBase64Decode](@__USUARIOCREADOR)

				END
				ELSE
				BEGIN
					
					SELECT @__USERNAME = VU.Nombre,
						   @__CORREOUSER = VU.Correo
						   FROM [cargamasiva].[View_Usuarios] VU
						   WHERE VU.NombreUsuario = [dbo].[FNBase64Decode](@__USUARIOCREADOR)

				END

				;WITH Correos AS
				(
					SELECT VKC.Correo
							FROM [cargamasiva].[View_KamClientes] VKC
							WHERE VKC.IdCliente IN (SELECT VC.Id
															FROM [cargamasiva].[View_Clientes] VC
															WHERE VC.Codigo = @__CODCLI AND
																VC.Empresa = @__EMPRESA)
					UNION
					SELECT VAKC.Correo
							FROM [cargamasiva].[View_AsistenteKamCliente] VAKC
							WHERE VAKC.KamCliente IN (SELECT VKC.IdKamCliente
															FROM [cargamasiva].[View_KamClientes] VKC
															WHERE VKC.IdCliente IN (SELECT VC.Id
																							FROM [cargamasiva].[View_Clientes] VC
																							WHERE VC.Codigo = @__CODCLI AND
																									VC.Empresa = @__EMPRESA))
				)

				(SELECT @__CC = STUFF(
							(SELECT DISTINCT '; ' + Correo
									FROM Correos
							FOR XML PATH ('')),
				1,2, ''))

				IF(@__TYPE = 'E')
				BEGIN

					EXEC [remuneraciones].[__SPRM_CargoMod_Email_Rechazo]
					@__CODIGOSOLICITUD,
					@CODIGOCARGOMOD,
					@__NAMECARGOMOD,
					@__USERNAME,
					@__MOTIVORECHAZO,
					@HTMLEMAIL OUTPUT

					SET @__ASUNTOEMAIL = 'Solicitud Cargo Mod : ' + @__NAMECARGOMOD + ' - ' + @__CODIGOSOLICITUD + ' (Solicitud Rechazada)'

					EXEC msdb.dbo.sp_send_dbmail
						@profile_name = 'NoReply',
						@recipients = @__CORREOUSER,
						@copy_recipients = @__CC,
						@blind_copy_recipients = 'sebastian.salas@team-work.cl',
						@body = @HTMLEMAIL,
						@subject = @__ASUNTOEMAIL,
						@body_format = 'HTML'

					EXEC [remuneraciones].[__SPRM_CargoMod_Email_Rechazo]
					@__CODIGOSOLICITUD,
					@CODIGOCARGOMOD,
					@__NAMECARGOMOD,
					'Finanzas',
					@__MOTIVORECHAZO,
					@HTMLEMAIL OUTPUT

					SET @__ASUNTOEMAIL = 'Solicitud Cargo Mod : ' + @__NAMECARGOMOD + ' - ' + @__CODIGOSOLICITUD + ' (Solicitud Rechazada)'

					EXEC msdb.dbo.sp_send_dbmail
						@profile_name = 'NoReply',
						@recipients = 'marco.vidal@team-work.cl;natalia.ormeno@team-work.cl',
						@blind_copy_recipients = 'sebastian.salas@team-work.cl',
						@body = @HTMLEMAIL,
						@subject = @__ASUNTOEMAIL,
						@body_format = 'HTML'

				END

				IF(@__TYPE = 'S')
				BEGIN
					
					EXEC [remuneraciones].[__SPRM_CargoMod_Email_RechazoCotizacion]
					@__CODIGOSOLICITUD,
					@CODIGOCARGOMOD,
					@__NAMECARGOMOD,
					@__USERNAME,
					@__MOTIVORECHAZO,
					@HTMLEMAIL OUTPUT

					SET @__ASUNTOEMAIL = 'Cotización : ' + @__NAMECARGOMOD + ' - ' + @__CODIGOSOLICITUD + ' (Rechazada)'

					EXEC msdb.dbo.sp_send_dbmail
						@profile_name = 'NoReply',
						@recipients = @__CORREOUSER,
						@copy_recipients = 'marco.vidal@team-work.cl;natalia.ormeno@team-work.cl',
						@blind_copy_recipients = 'sebastian.salas@team-work.cl',
						@body = @HTMLEMAIL,
						@subject = @__ASUNTOEMAIL,
						@body_format = 'HTML'

				END

				SET @CODINE = @CODIGOCARGOMOD

			END

			-- Rechazo de remuneraciones
			IF(@ESTADO = 'RZR')
			BEGIN
				
				SET @__CODIGOCARGOMOD = [dbo].[FNBase64Decode](@CODIGOCARGOMOD)
				
				UPDATE [remuneraciones].[RM_CargosMod]
				       SET Estado = 'PDAPROB',
					       EstadoRechazo = 'RZR',
						   UltimoComentario = 'Se rechaza cargo mod por parte de remuneraciones, por el siguiente motivo: ' + @OBSERVACION,
						   FechaUltimaActualizacion = GETDATE(),
						   UsuarioUltimaActualizacion = @USUARIOCREADOR
					   WHERE CodigoCargoMod = @__CODIGOCARGOMOD

				DELETE FROM [remuneraciones].[RM_EstructuraRentaBase]
				       WHERE CodigoCargoMod = @__CODIGOCARGOMOD

				DELETE FROM [remuneraciones].[RM_EstructuraBaseD]
				       WHERE CodigoCargoMod = @__CODIGOCARGOMOD

				DELETE FROM [remuneraciones].[RM_EstructuraBaseH]
				       WHERE CodigoCargoMod = @__CODIGOCARGOMOD

				DELETE FROM [remuneraciones].[RM_EstructuraBaseMargenProv]
				       WHERE CodigoCargoMod = @__CODIGOCARGOMOD

				SELECT @__CODIGOSOLICITUD = VS.CodigoCargoMod,
					   @__NAMECARGOMOD = VS.NombreCargo,
					   @__MOTIVORECHAZO = VS.Comentarios,
					   @__USUARIOCREADOR = VS.UsuarioCreador,
					   @__EMPRESA = VS.Empresa,
					   @__CODCLI = VS.Cliente
					   FROM [remuneraciones].[View_Solicitudes] VS
					   WHERE VS.CodigoCargoMod = @__CODIGOCARGOMOD

				IF(CHARINDEX('@', [dbo].[FNBase64Decode](@__USUARIOCREADOR)) > 0)
				BEGIN

					SELECT @__USERNAME = VU.Nombre,
						   @__CORREOUSER = VU.Correo
						   FROM [cargamasiva].[View_Usuarios] VU
						   WHERE VU.Correo = [dbo].[FNBase64Decode](@__USUARIOCREADOR)

				END
				ELSE
				BEGIN
					
					SELECT @__USERNAME = VU.Nombre,
						   @__CORREOUSER = VU.Correo
						   FROM [cargamasiva].[View_Usuarios] VU
						   WHERE VU.NombreUsuario = [dbo].[FNBase64Decode](@__USUARIOCREADOR)

				END

				EXEC [remuneraciones].[__SPRM_CargoMod_Email_Rechazo]
				@__CODIGOSOLICITUD,
				@CODIGOCARGOMOD,
				@__NAMECARGOMOD,
				@__USERNAME,
				@__MOTIVORECHAZO,
				@HTMLEMAIL OUTPUT

				SET @__ASUNTOEMAIL = 'Solicitud Cargo Mod : ' + @__NAMECARGOMOD + ' - ' + @__CODIGOSOLICITUD + ' (Solicitud Rechazada Remuneraciones)'

				EXEC msdb.dbo.sp_send_dbmail
					@profile_name = 'NoReply',
					@recipients = @__CORREOUSER,
					@blind_copy_recipients ='sebastian.salas@team-work.cl',
					@body = @HTMLEMAIL,
					@subject = @__ASUNTOEMAIL,
					@body_format = 'HTML'

				IF(CHARINDEX('@', [dbo].[FNBase64Decode](@USUARIOCREADOR)) > 0)
				BEGIN

					SELECT @__USERNAME = VU.Nombre,
						   @__CORREOUSER = VU.Correo
						   FROM [cargamasiva].[View_Usuarios] VU
						   WHERE VU.Correo = [dbo].[FNBase64Decode](@USUARIOCREADOR)

				END
				ELSE
				BEGIN
					
					SELECT @__USERNAME = VU.Nombre,
						   @__CORREOUSER = VU.Correo
						   FROM [cargamasiva].[View_Usuarios] VU
						   WHERE VU.NombreUsuario = [dbo].[FNBase64Decode](@USUARIOCREADOR)

				END

				EXEC [remuneraciones].[__SPRM_CargoMod_Email_Rechazo]
				@__CODIGOSOLICITUD,
				@CODIGOCARGOMOD,
				@__NAMECARGOMOD,
				@__USERNAME,
				@__MOTIVORECHAZO,
				@HTMLEMAIL OUTPUT

				SET @__ASUNTOEMAIL = 'Solicitud Cargo Mod : ' + @__NAMECARGOMOD + ' - ' + @__CODIGOSOLICITUD + ' (Solicitud Rechazada Remuneraciones)'

				EXEC msdb.dbo.sp_send_dbmail
					@profile_name = 'NoReply',
					@recipients = @__CORREOUSER,
					@blind_copy_recipients ='sebastian.salas@team-work.cl',
					@body = @HTMLEMAIL,
					@subject = @__ASUNTOEMAIL,
					@body_format = 'HTML'

				EXEC [remuneraciones].[__SPRM_CargoMod_Email_Rechazo]
				@__CODIGOSOLICITUD,
				@CODIGOCARGOMOD,
				@__NAMECARGOMOD,
				'Finanzas',
				@__MOTIVORECHAZO,
				@HTMLEMAIL OUTPUT

				SET @__ASUNTOEMAIL = 'Solicitud Cargo Mod : ' + @__NAMECARGOMOD + ' - ' + @__CODIGOSOLICITUD + ' (Solicitud Rechazada Remuneraciones)'

				EXEC msdb.dbo.sp_send_dbmail
					@profile_name = 'NoReply',
					@recipients = 'marco.vidal@team-work.cl;natalia.ormeno@team-work.cl',
					@blind_copy_recipients = 'sebastian.salas@team-work.cl',
					@body = @HTMLEMAIL,
					@subject = @__ASUNTOEMAIL,
					@body_format = 'HTML'
					
				SET @CODE = '200'
				SET @MESSAGESIMPLE = 'La solicitud ha sido rechazada exitosamente, ha vuelto a poder de finanzas para ser modificada de acuerdo a lo que él estime conveniente.'

			END
			
			
			
			
			
			
			
			
			
			
			----------------------------------------------------------------------------------------
			----------------------------------------------------------------------------------------
			-- Envio a Firma digital (creacion en softland de cargo mod)
			IF(@ESTADO = 'FD')
			BEGIN
				
				SET @__CODIGOCARGOMOD = [dbo].[FNBase64Decode](@CODIGOCARGOMOD)

				SELECT @GLOSA = VS.NombreCargoMod,
				       @__CLIENTE = VS.Cliente,
				       @__EMPRESA = VS.Empresa,
					   @__EMPRESASOFTLAND = CASE WHEN VS.Empresa = 'TWRRHH' THEN
													'TEAMRRHH'
												 WHEN VS.Empresa = 'TWC' THEN
												    'TEAMWORK'
					                        ELSE
												VS.Empresa
											END
				       FROM [remuneraciones].[View_Solicitudes] VS
					   WHERE VS.CodigoCargoMod = @__CODIGOCARGOMOD
				/*
				SELECT @__GLOSAVALIDAR = VCMI.CargoMod
					   FROM [cargamasiva].[View_CargosModInternos] VCMI
					   WHERE VCMI.IdCargo = (SELECT MAX(VCMII.IdCargo)
												    FROM [cargamasiva].[View_CargosModInternos] VCMII
												    WHERE VCMII.Empresa = @__EMPRESA) AND
							 VCMI.Empresa = @__EMPRESA
				

				SET @__SQL =
				'
					SELECT @CODINEOUT = SWC.Codine
					       FROM [conectorsoftland.team-work.cl\SQL2017].[' + @__EMPRESASOFTLAND + '].[SOFTLAND].[SW_CODINE] SWC WITH (NOLOCK)
						   WHERE SWC.Glosa = ''' + @__GLOSAVALIDAR + ''' 
				'

				PRINT @__SQL

				EXEC sys.sp_executesql 
					 @__SQL,
					 N'@CODINEOUT VARCHAR(MAX) OUTPUT',
					 @CODINEOUT = @__CODINE OUTPUT

				SET @__CODINESEQ = CAST(@__CODINE AS NUMERIC) + 1
				*/
				
				
				DECLARE @Result VARCHAR(MAX)
				EXEC [remuneraciones].[__SPRM_CargoMod_Code_AlfaNum] 
				@CODINEOUT = @Result OUTPUT, 
				@__EMPRESACODE = @__EMPRESA;
				
				SET @__CODINEASIGN = @Result
				
				----------------------------------------------------------------------------------------
				----------------------------------------------------------------------------------------

				UPDATE [remuneraciones].[RM_CargosMod]
						SET Estado = @ESTADO,
						    EstadoRechazo = NULL,
							FechaUltimaActualizacion = GETDATE(),
							UsuarioUltimaActualizacion = @USUARIOCREADOR,
							UltimoComentario = 'Se ha creado en softland cargo mod, pendiente de creación en plataforma de firma digital',
							Codigo = @__CODINEASIGN,
							CodigoCargoMod = @__CODINEASIGN + '-' + Empresa
						WHERE CodigoCargoMod = @__CODIGOCARGOMOD

				UPDATE [remuneraciones].[RM_CargosModLog]
				       SET CodigoCargoMod = @__CODINEASIGN + '-' + @__EMPRESA
					   WHERE CodigoCargoMod = @__CODIGOCARGOMOD

				UPDATE [remuneraciones].[RM_BonosCargoMod]
				       SET CodigoCargoMod = @__CODINEASIGN + '-' + @__EMPRESA
					   WHERE CodigoCargoMod = @__CODIGOCARGOMOD

				UPDATE [remuneraciones].[RM_ANICargoMod]
				       SET CodigoCargoMod = @__CODINEASIGN + '-' + @__EMPRESA
					   WHERE CodigoCargoMod = @__CODIGOCARGOMOD

				UPDATE [remuneraciones].[RM_ProvMargen]
				       SET CodigoCargoMod = @__CODINEASIGN + '-' + @__EMPRESA
					   WHERE CodigoCargoMod = [dbo].[FNBase64Encode](@__CODIGOCARGOMOD)

				UPDATE [remuneraciones].[RM_EstructuraRentaBase]
				       SET Codigo = @__CODINEASIGN,
					       Estado = @ESTADO,
						   EstadoRechazo = NULL,
						   FechaUltimaActualizacion = GETDATE(),
						   UsuarioUltimaActualizacion = @USUARIOCREADOR,
						   UltimoComentario = 'Se ha creado en softland cargo mod, pendiente de creación en plataforma de firma digital',
						   CodigoCargoMod = @__CODINEASIGN + '-' + Empresa
					   WHERE CodigoCargoMod = @__CODIGOCARGOMOD

				UPDATE [remuneraciones].[RM_EstructuraBaseH]
				       SET CodigoCargoMod = @__CODINEASIGN + '-' + @__EMPRESA
					   WHERE CodigoCargoMod = @__CODIGOCARGOMOD
				     
				UPDATE [remuneraciones].[RM_EstructuraBaseD]
				       SET CodigoCargoMod = @__CODINEASIGN + '-' + @__EMPRESA
					   WHERE CodigoCargoMod = @__CODIGOCARGOMOD

				UPDATE [remuneraciones].[RM_EstructuraBaseMargenProv]
				       SET CodigoCargoMod = @__CODINEASIGN + '-' + @__EMPRESA
					   WHERE CodigoCargoMod = @__CODIGOCARGOMOD

				UPDATE [remuneraciones].[RM_GratificacionConvenida]
				       SET CodigoCargoMod = @__CODINEASIGN + '-' + @__EMPRESA
					   WHERE CodigoCargoMod = @__CODIGOCARGOMOD

				UPDATE [remuneraciones].[RM_CargosModLog]
				       SET CodigoCargoMod = @__CODINEASIGN + '-' + @__EMPRESA
					   WHERE CodigoCargoMod = @__CODIGOCARGOMOD
				     
				IF(@__EMPRESA = 'TWEST')
				BEGIN
					
					INSERT INTO [TW_ACCESS_EST].[dbo].[cargomod] (Idcargo, cargomod, cliente, areaneg)
						   VALUES((SELECT MAX(IdCargo) + 1 
						                  FROM [cargamasiva].[View_CargosModInternos] VCMI
						                  WHERE VCMI.Empresa = @__EMPRESA),
						          @GLOSA,
								  @__CLIENTE,
								  @__CLIENTE)

				END

				IF(@__EMPRESA != 'TWEST')
				BEGIN
					
					INSERT INTO [TW_ACCESS_OUT].[dbo].[cargomod] (Idcargo, cargo, cliente, empresa, areanegocio)
						   VALUES((SELECT MAX(IdCargo) + 1 
						                  FROM [cargamasiva].[View_CargosModInternos] VCMI
						                  WHERE VCMI.Empresa = @__EMPRESA),
						          @GLOSA,
								  @__CLIENTE,
								  @__EMPRESA,
								  @__CLIENTE)

				END

				INSERT INTO [dbo].[KM_CargosBPO]
				       VALUES((SELECT MAX(Id) + 1
					                  FROM [cargamasiva].[View_CargosFirmaDigital] VCFD),
					          @__CODINEASIGN + '-' + @__EMPRESA,
							  @GLOSA,
							  'S',
							  @__EMPRESA)

				SET @MESSAGE = ''
				SET @MESSAGESIMPLE = ''
				SET @CODE = '200'

				SET @CODINE = @__CODINEASIGN
				SET @EMPRESA = @__EMPRESA

				DECLARE @__NEWCODIGOCARGOMOD VARCHAR(MAX)

				SET @__NEWCODIGOCARGOMOD = [dbo].[FNBase64Encode](@__CODINEASIGN + '-' + @__EMPRESA)

				SET @__ANALISTAREM =
					(SELECT STUFF(
							(SELECT DISTINCT '; ' + Correo
									FROM [cargamasiva].[View_Usuarios] VU
									WHERE VU.Id IN (SELECT VAA.UserId
															FROM [remuneraciones].[View_AsignAnalista] VAA
															WHERE VAA.Empresa = @__EMPRESA)
							FOR XML PATH ('')),
					1,2, ''))

				SELECT @__CREADOR = [dbo].[FNBase64Decode](VHE.UsuarioCreador),
				       @__CODIGOCARGOMOD = VHE.CodigoFirmaDigital,
					   @__CODIGOSOFTLAND = VHE.CodigoSoftland,
					   @__CODIGOSOLICITUD = VHE.CodigoSolicitud,
					   @__NAMECARGOMOD = VHE.NombreCargoMod
				       FROM [remuneraciones].[View_HeaderEstructura] VHE
					   WHERE VHE.CodigoCargoMod = @__NEWCODIGOCARGOMOD
				
				IF(CHARINDEX('@', @__CREADOR) > 0)
				BEGIN

					SELECT @__USERNAME = VU.Nombre,
						   @__CORREOUSER = VU.Correo
						   FROM [cargamasiva].[View_Usuarios] VU
						   WHERE VU.Correo = @__CREADOR

				END
				ELSE
				BEGIN
					
					SELECT @__USERNAME = VU.Nombre,
						   @__CORREOUSER = VU.Correo
						   FROM [cargamasiva].[View_Usuarios] VU
						   WHERE VU.NombreUsuario = @__CREADOR

				END

				EXEC [remuneraciones].[__SPRM_CargoMod_Email_PendienteFD]
				     @__NEWCODIGOCARGOMOD,
					 @__CODIGOCARGOMOD,
					 @__CODIGOSOFTLAND,
					 @__NAMECARGOMOD,
					 'T.I.',
					 @HTMLEMAIL OUTPUT

				;WITH Correos AS
				(
					SELECT VKC.Correo
							FROM [cargamasiva].[View_KamClientes] VKC
							WHERE VKC.IdCliente IN (SELECT VC.Id
															FROM [cargamasiva].[View_Clientes] VC
															WHERE VC.Codigo = @__CLIENTE AND
																VC.Empresa = @__EMPRESA)
					UNION
					SELECT VAKC.Correo
							FROM [cargamasiva].[View_AsistenteKamCliente] VAKC
							WHERE VAKC.KamCliente IN (SELECT VKC.IdKamCliente
															FROM [cargamasiva].[View_KamClientes] VKC
															WHERE VKC.IdCliente IN (SELECT VC.Id
																							FROM [cargamasiva].[View_Clientes] VC
																							WHERE VC.Codigo = @__CLIENTE AND
																									VC.Empresa = @__EMPRESA))
				)

				(SELECT @__CC = STUFF(
							(SELECT DISTINCT '; ' + Correo
									FROM Correos
							FOR XML PATH ('')),
				1,2, ''))
				
				SET @__ASUNTOEMAIL = 'Solicitud Cargo Mod : ' + @__NAMECARGOMOD + ' - ' + @__CODIGOCARGOMOD + ' (Solicitud Pendiente Firma Digital)'
				SET @__COPY = @__CORREOUSER + CASE WHEN @__ANALISTAREM IS NOT NULL THEN ';' ELSE '' END + ISNULL(@__ANALISTAREM, '')
				SET @__COPY = @__COPY + CASE WHEN @__COPY IS NOT NULL THEN ';' ELSE '' END + ISNULL(@__CC, '')

				EXEC msdb.dbo.sp_send_dbmail
					@profile_name = 'NoReply',
					@recipients = 'sistemas@team-work.cl',
					@copy_recipients = @__COPY,
					@body = @HTMLEMAIL,
					@subject = @__ASUNTOEMAIL,
					@body_format = 'HTML'

			END

			IF(@ESTADO = 'TER')
			BEGIN
				SET @__CODIGOCARGOMOD = [dbo].[FNBase64Decode](@CODIGOCARGOMOD)

				SELECT @__EMPRESA = VS.Empresa
				       FROM [remuneraciones].[View_Solicitudes] VS
					   WHERE VS.CodigoCargoMod = @__CODIGOCARGOMOD

				SELECT @__CREADOR = [dbo].[FNBase64Decode](VHE.UsuarioCreador),
				       @__CODIGOCARGOMOD = VHE.CodigoFirmaDigital,
					   @__CODIGOSOFTLAND = VHE.CodigoSoftland,
					   @__CODIGOSOLICITUD = VHE.CodigoSolicitud,
					   @__NAMECARGOMOD = VHE.NombreCargoMod,
					   @__EMPRESA = VHE.Empresa,
					   @__CODCLI = VHE.Cliente
				       FROM [remuneraciones].[View_HeaderEstructura] VHE
					   WHERE VHE.CodigoCargoMod = @CODIGOCARGOMOD
				
				IF(CHARINDEX('@', @__CREADOR) > 0)
				BEGIN

					SELECT @__USERNAME = VU.Nombre,
						   @__CORREOUSER = VU.Correo
						   FROM [cargamasiva].[View_Usuarios] VU
						   WHERE VU.Correo = @__CREADOR

				END
				ELSE
				BEGIN
					
					SELECT @__USERNAME = VU.Nombre,
						   @__CORREOUSER = VU.Correo
						   FROM [cargamasiva].[View_Usuarios] VU
						   WHERE VU.NombreUsuario = @__CREADOR

				END

				SET @__ANALISTAREM =
					(SELECT STUFF(
							(SELECT DISTINCT '; ' + Correo
									FROM [cargamasiva].[View_Usuarios] VU
									WHERE VU.Id IN (SELECT VAA.UserId
															FROM [remuneraciones].[View_AsignAnalista] VAA
															WHERE VAA.Empresa = @__EMPRESA)
							FOR XML PATH ('')),
					1,2, ''))

				EXEC [remuneraciones].[__SPRM_CargoMod_Email_Terminado]
				     @CODIGOCARGOMOD,
					 @__CODIGOCARGOMOD,
					 @__CODIGOSOFTLAND,
					 @__NAMECARGOMOD,
					 @__USERNAME,
					 @HTMLEMAIL OUTPUT

				;WITH Correos AS
				(
					SELECT VKC.Correo
							FROM [cargamasiva].[View_KamClientes] VKC
							WHERE VKC.IdCliente IN (SELECT VC.Id
															FROM [cargamasiva].[View_Clientes] VC
															WHERE VC.Codigo = @__CODCLI AND
																VC.Empresa = @__EMPRESA)
					UNION
					SELECT VAKC.Correo
							FROM [cargamasiva].[View_AsistenteKamCliente] VAKC
							WHERE VAKC.KamCliente IN (SELECT VKC.IdKamCliente
															FROM [cargamasiva].[View_KamClientes] VKC
															WHERE VKC.IdCliente IN (SELECT VC.Id
																							FROM [cargamasiva].[View_Clientes] VC
																							WHERE VC.Codigo = @__CODCLI AND
																									VC.Empresa = @__EMPRESA))
				)

				(SELECT @__CC = STUFF(
							(SELECT DISTINCT '; ' + Correo
									FROM Correos
							FOR XML PATH ('')),
				1,2, ''))
				
				SET @__ASUNTOEMAIL = 'Solicitud Cargo Mod : ' + @__NAMECARGOMOD + ' - ' + @__CODIGOCARGOMOD + ' (Solicitud Terminada)'
				SET @__COPY = @__CC + CASE WHEN @__ANALISTAREM IS NOT NULL THEN ';' ELSE '' END + ISNULL(@__ANALISTAREM, '')

				EXEC msdb.dbo.sp_send_dbmail
					@profile_name = 'NoReply',
					@recipients = @__CORREOUSER,
					@copy_recipients = @__COPY,
					@blind_copy_recipients = 'sistemas@team-work.cl',
					@body = @HTMLEMAIL,
					@subject = @__ASUNTOEMAIL,
					@body_format = 'HTML'
					
				UPDATE [remuneraciones].[RM_EstructuraRentaBase]
				       SET Estado = @ESTADO,
					       UltimoComentario = 'Se termina cargo mod, creado en softland y firma digital',
						   FechaUltimaActualizacion = GETDATE(),
						   UsuarioUltimaActualizacion = @USUARIOCREADOR
					   WHERE CodigoCargoMod = [dbo].[FNBase64Decode](@CODIGOCARGOMOD)

				SET @CODE = '200'
				SET @MESSAGE = 'Se ha terminado el cargo mod'

			END

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION

		SELECT ERROR_MESSAGE()

	END CATCH
