CREATE PROCEDURE [remuneraciones].[package_RM_CargoMod](
	@OPCION VARCHAR(MAX),
	@SUELDO VARCHAR(MAX) = '',
	@EMPRESA VARCHAR(MAX) = '',
	@CLIENTE VARCHAR(MAX) = '',
	@CARGOMOD VARCHAR(MAX) = '',
	@NOMBRECARGO VARCHAR(MAX) = '',
	@GRATIFICACION VARCHAR(MAX) = '',
	@TYPEGRATIF VARCHAR(MAX) = '',
	@OBSERVGRATICONV VARCHAR(MAX) = '',
	@USUARIOCREADOR VARCHAR(MAX) = '',
	@PAGINATION VARCHAR(MAX) = '',
	@TYPEFILTER VARCHAR(MAX) = '',
	@DATAFILTER VARCHAR(MAX) = '',
	@CODIGOSOLICITUD VARCHAR(MAX) = '',
	@AFP VARCHAR(MAX) = '',
	@TYPESUELDO VARCHAR(MAX) = '',
	@EXCINC VARCHAR(MAX) = '',
	@CODIGOVAR VARCHAR(MAX) = '',
	@ESTADO VARCHAR(MAX) = '',
	@OBSERVACIONES VARCHAR(MAX) = '',
	@HORARIOS VARCHAR(MAX) = '',
	@VALORDIARIO VARCHAR(MAX) = '',
	@HORASEMANALES VARCHAR(MAX) = '',
	@TYPE VARCHAR(MAX) = '',
	@DIAS VARCHAR(MAX) = ''
)
AS
	
	BEGIN TRY
		
		BEGIN TRANSACTION
			
			DECLARE @CODE VARCHAR(MAX),
			        @CODINE VARCHAR(MAX),
					@MESSAGE VARCHAR(MAX),
					@MESSAGESIMPLE VARCHAR(MAX)

			DECLARE @__CODINE VARCHAR(MAX),
				    @__GLOSA VARCHAR(MAX),
					@__EMPRESA VARCHAR(MAX)

			-- SOLICITUD DE CARGO MOD => KAM A FINANZAS
			IF(@OPCION = 'CrearSolicitud')
			BEGIN
				
				EXEC [remuneraciones].[__SPRM_CargoMod_CrearSolicitud] @EMPRESA, @USUARIOCREADOR, @TYPE, @CODE OUTPUT, @CODINE OUTPUT, @MESSAGE OUTPUT
					
				SELECT @CODE 'Code',
				       @CODINE 'Codine',
					   @MESSAGE 'Message'
				
			END

			IF(@OPCION = 'DeshacerSolicitud')
			BEGIN
				
				EXEC [remuneraciones].[__SPRM_CargoMod_DeshacerSolicitud] @CODIGOSOLICITUD, @CODE OUTPUT, @MESSAGE OUTPUT

				SELECT @CODE 'Code',
					   @MESSAGE 'Message'

			END

			IF(@OPCION = 'Solicitudes')
			BEGIN
				
				EXEC [remuneraciones].[__SPRM_CargoMod_Solicitudes] @USUARIOCREADOR, @PAGINATION, @TYPEFILTER, @DATAFILTER

			END

			IF(@OPCION = 'ValidaSeleccionCliente')
			BEGIN

				EXEC [remuneraciones].[__SPRM_CargoMod_ValidaSeleccionCliente] @USUARIOCREADOR, @CODIGOSOLICITUD, @CODE OUTPUT

				SELECT @CODE 'Code'
				
			END

			IF(@OPCION = 'Clientes')
			BEGIN
				
				EXEC [remuneraciones].[__SPRM_CargoMod_Clientes] @EMPRESA, @TYPEFILTER, @DATAFILTER, @PAGINATION, @USUARIOCREADOR

			END

			IF(@OPCION = 'PaginationClientes')
			BEGIN
				
				EXEC [dbo].[SP_TW_GET_PAGINATIONS] 
				'Base U2VydmljaW8uQXV0aEBTZXJ2aWNpby5BdXRo',
				'AGENT_WEBSERVICE_APP',
				'',
				'Clientes',
				@PAGINATION,
				'',
				@TYPEFILTER,
				@DATAFILTER,
				@EMPRESA

			END

			IF(@OPCION = 'ActualizaCliente')
			BEGIN
				
				EXEC [remuneraciones].[__SPRM_CargoMod_ActualizaCliente] @USUARIOCREADOR, @CODIGOSOLICITUD, @CLIENTE, @CODE OUTPUT

				SELECT @CODE 'Code'

			END

			IF(@OPCION = 'ActualizaNombreCargo')
			BEGIN
				
				EXEC [remuneraciones].[__SPRM_CargoMod_ActualizaNombreCargo] @USUARIOCREADOR, @CODIGOSOLICITUD, @NOMBRECARGO, @TYPE, @CODE OUTPUT, @MESSAGE OUTPUT

				SELECT @CODE 'Code',
				       @MESSAGE 'Message'

			END

			IF(@OPCION = 'ActualizaSueldo')
			BEGIN
				
				EXEC [remuneraciones].[__SPRM_CargoMod_ActualizaSueldo] @USUARIOCREADOR, @CODIGOSOLICITUD, @SUELDO, @TYPESUELDO, @CODE OUTPUT, @MESSAGE OUTPUT

				SELECT @CODE 'Code',
				       @MESSAGE 'Message'

			END

			IF(@OPCION = 'ActualizaSueldoLiquido')
			BEGIN
				
				EXEC [remuneraciones].[__SPRM_CargoMod_ActualizaSueldo] @USUARIOCREADOR, @CODIGOSOLICITUD, @SUELDO, 'SL', @CODE OUTPUT

				SELECT @CODE 'Code'

			END

			IF(@OPCION = 'ActualizaGratificacion')
			BEGIN
				
				EXEC [remuneraciones].[__SPRM_CargoMod_ActualizaGratificacion] @USUARIOCREADOR, @CODIGOSOLICITUD, @GRATIFICACION, @TYPEGRATIF, @OBSERVGRATICONV, @CODE OUTPUT

				SELECT @CODE 'Code'

			END

			IF(@OPCION = 'Dashboard')
			BEGIN
				
				EXEC [remuneraciones].[__SPRM_CargoMod_Dashboard] @USUARIOCREADOR

			END

			IF(@OPCION = 'HeaderEstructura')
			BEGIN
				
				EXEC [remuneraciones].[__SPRM_CargoMod_HeaderEstructura] @CODIGOSOLICITUD

			END

			IF(@OPCION = 'HaberesEstructura')
			BEGIN
				
				EXEC [remuneraciones].[__SPRM_CargoMod_HaberesEstructura] @CODIGOSOLICITUD

			END

			IF(@OPCION = 'DescuentosEstructura')
			BEGIN
				
				EXEC [remuneraciones].[__SPRM_CargoMod_DescuentosEstructura] @CODIGOSOLICITUD

			END

			IF(@OPCION = 'MargenProvisionEstructura')
			BEGIN
				
				EXEC [remuneraciones].[__SPRM_CargoMod_ProvMargenEstructura] @USUARIOCREADOR, @CODIGOSOLICITUD

			END

			IF(@OPCION = 'AFP')
			BEGIN
				
				EXEC [remuneraciones].[__SPRM_CargoMod_AFP] @EMPRESA

			END

			IF(@OPCION = 'ActualizarAFP')
			BEGIN
				
				EXEC [remuneraciones].[__SPRM_CargoMod_ActualizarAFP] @CODIGOSOLICITUD, @AFP, @CODE OUTPUT, @MESSAGE OUTPUT

			END

			IF(@OPCION = 'CambiarInputSueldo')
			BEGIN
				
				EXEC [remuneraciones].[__SPRM_CargoMod_CambiarInputSueldo] @SUELDO, @CODIGOSOLICITUD, @CODE OUTPUT, @MESSAGE OUTPUT

			END

			IF(@OPCION = 'CambiarCalculoTypeSueldo')
			BEGIN
				
				EXEC [remuneraciones].[__SPRM_CargoMod_CambiarCalculoTypeSueldo] @SUELDO, @CODIGOSOLICITUD, @CODE OUTPUT, @MESSAGE OUTPUT

			END

			IF(@OPCION = 'CambiarProvMargGastoExcInc')
			BEGIN
				
				EXEC [remuneraciones].[__SPRM_CargoMod_CambiarProvMargGastoExcInc] @USUARIOCREADOR, @CODIGOSOLICITUD, @CODIGOVAR, @EXCINC, @CODE OUTPUT, @MESSAGE OUTPUT

				SELECT @CODE 'Code'

			END

			IF(@OPCION = 'CambiarTypeJornada')
			BEGIN
				
				EXEC [remuneraciones].[__SPRM_CargoMod_CambiarTypeJornada] @SUELDO, @CODIGOSOLICITUD, @CODE OUTPUT, @MESSAGE OUTPUT

				SELECT @CODE 'Code',
				       @MESSAGE 'Message'

			END

			IF(@OPCION = 'CambioEstadoSolicitud')
			BEGIN

				EXEC [remuneraciones].[__SPRM_CargoMod_CambioEstadoSolicitud] 
				     @USUARIOCREADOR, 
					 @ESTADO, 
					 @CODIGOSOLICITUD, 
					 @OBSERVACIONES,
					 @CODE OUTPUT, 
					 @MESSAGE OUTPUT, 
					 @MESSAGESIMPLE OUTPUT, 
					 @__CODINE OUTPUT,
					 @__GLOSA OUTPUT,
					 @__EMPRESA OUTPUT

				SELECT @CODE 'Code',
				       @MESSAGE 'Message',
					   @MESSAGESIMPLE 'MessageSimple',
					   @__CODINE 'Codine',
					   @__GLOSA 'Glosa',
					   @__EMPRESA 'Empresa',
					   [dbo].[FNBase64Encode](@__CODINE + '-' + @__EMPRESA) 'CodineCodify'

			END

			IF(@OPCION = 'ValidaStageActual')
			BEGIN
				
				EXEC [remuneraciones].[__SPRM_CargoMod_ValidaStageActual] @CODIGOSOLICITUD, @CODE OUTPUT, @MESSAGE OUTPUT

				SELECT @CODE 'Code',
				       @MESSAGE 'Message'

			END

			IF(@OPCION = 'ActualizaValorDiaPT')
			BEGIN
				
				EXEC [remuneraciones].[__SPRM_CargoMod_ActualizaValorDiaPT] @USUARIOCREADOR, @CODIGOSOLICITUD, @VALORDIARIO, @HORASEMANALES, @CODE OUTPUT, @MESSAGE OUTPUT

				SELECT @CODE 'Code',
				       @MESSAGE 'Message'

			END

			IF(@OPCION = 'ActualizaObservaciones')
			BEGIN
				
				EXEC [remuneraciones].[__SPRM_CargoMod_ActualizaObservaciones] @USUARIOCREADOR, @CODIGOSOLICITUD, @OBSERVACIONES, @TYPE, @CODE OUTPUT, @MESSAGE OUTPUT

				SELECT @CODE 'Code',
				       @MESSAGE 'Message'

			END

			IF(@OPCION = 'EliminarObservaciones')
			BEGIN
				
				EXEC [remuneraciones].[__SPRM_CargoMod_EliminarObservaciones] @USUARIOCREADOR, @CODIGOSOLICITUD, @OBSERVACIONES, @CODE OUTPUT, @MESSAGE OUTPUT

				SELECT @CODE 'Code',
				       @MESSAGE 'Message'

			END

			IF(@OPCION = 'ValidaCadenaObservaciones')
			BEGIN
				
				EXEC [remuneraciones].[__SPRM_CargoMod_ValidaCadenaObservaciones] @USUARIOCREADOR, @CODIGOSOLICITUD, @OBSERVACIONES, @CODE OUTPUT, @MESSAGE OUTPUT

				SELECT @CODE 'Code',
				       @MESSAGE 'Message'

			END

			IF(@OPCION = 'ActualizaHorario')
			BEGIN
				
				EXEC [remuneraciones].[__SPRM_CargoMod_ActualizaHorario] @USUARIOCREADOR, @CODIGOSOLICITUD, @HORARIOS, @CODE OUTPUT, @MESSAGE OUTPUT

				SELECT @CODE 'Code',
				       @MESSAGE 'Message'

			END

			IF(@OPCION = 'ActualizaJornadaFullTime')
			BEGIN
				
				EXEC [remuneraciones].[__SPRM_CargoMod_ActualizaJornadaFullTime] @USUARIOCREADOR, @CODIGOSOLICITUD, @HORARIOS, @HORASEMANALES, @CODE OUTPUT, @MESSAGE OUTPUT

				SELECT @CODE 'Code',
				       @MESSAGE 'Message'

			END

			IF(@OPCION = 'ListObservaciones')
			BEGIN

				EXEC [remuneraciones].[__SPRM_CargoMod_ListObservaciones] @CODIGOSOLICITUD

			END

			IF(@OPCION = 'ActualizaWizards')
			BEGIN
				
				EXEC [remuneraciones].[__SPRM_CargoMod_ActualizaWizards] @CODIGOSOLICITUD, @ESTADO, @USUARIOCREADOR

			END

			IF(@OPCION = 'ValidaStageWizards')
			BEGIN
				
				EXEC [remuneraciones].[__SPRM_CargoMod_ValidaStageWizards] @CODIGOSOLICITUD, @ESTADO, @CODE OUTPUT, @MESSAGE OUTPUT

				SELECT @CODE 'Code',
				       @MESSAGE 'Message'

			END

			IF(@OPCION = 'ActualizaDiasSemanales')
			BEGIN
				
				EXEC [remuneraciones].[__SPRM_CargoMod_ActualizaDiasSemanales]
					 @USUARIOCREADOR,
					 @CODIGOSOLICITUD,
					 @DIAS,
					 @CODE OUTPUT, 
					 @MESSAGE OUTPUT

				SELECT @CODE 'Code',
				       @MESSAGE 'Message'

			END

			IF(@OPCION = 'HistorialCargoMod')
			BEGIN
				
				EXEC [remuneraciones].[__HistorialCargoMod]
					 @CODIGOSOLICITUD
					 
			END


		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION

		SELECT '500' 'Code',
		       'Ha ocurrido un error interno en el servidor' 'Message',
			   ERROR_MESSAGE() 'Error'

	END CATCH
