CREATE PROCEDURE [remuneraciones].[__SPRM_CargoMod_ActualizaNombreCargo](
	@USUARIOCREADOR		VARCHAR(MAX),
	@CODIGOSOLICITUD	VARCHAR(MAX),
	@NOMBRECARGO		VARCHAR(MAX),
	@TYPE				VARCHAR(MAX),
	@CODE				VARCHAR(MAX) OUTPUT,
	@MESSAGE			VARCHAR(MAX) OUTPUT
)
AS
	
	BEGIN TRY
		
		BEGIN TRANSACTION
			
			DECLARE @_PT BIT = 0,
			        @_SD BIT = 0,
 					@_CLIENTE VARCHAR(3) = '',
 					@_CODIGOCARGOMOD VARCHAR(100) = '',
 					@_SUGERENCIANOMBRE VARCHAR(30) = ''
 					
 					
 			SET @_CODIGOCARGOMOD = [dbo].[FNBase64Decode](@CODIGOSOLICITUD)
 			
 			-- Actualización de nombre cargo-funcion, más sugerencia de nombre cargo mod.
 			IF(@TYPE = 'CF')
 			BEGIN
	 			
	 			SELECT @_PT = CASE WHEN TypeJornada = 'F' THEN 0 ELSE 1 END,
       				   @_SD = CASE WHEN TypeSueldo = 'M' OR TypeSueldo = 'PTM' THEN 0 ELSE 1 END,
	   				   @_CLIENTE = Cliente
	 			       FROM [remuneraciones].[RM_CargosMod] WITH (NOLOCK)
	 			       WHERE CodigoCargoMod = @_CODIGOCARGOMOD
	 			
	 			       
	 			SET @_SUGERENCIANOMBRE = [remuneraciones].[FNSugerirNombreCargoMod](UPPER(@NOMBRECARGO), @_PT, @_SD, @_CLIENTE)
	 			
	 			UPDATE [remuneraciones].[RM_CargosMod]
					   SET NombreCargo = UPPER(@NOMBRECARGO),
						   CargoMod = CASE WHEN Type = 'E' THEN
											 [dbo].[FNNoTilde](
												[remuneraciones].[FNCrearGlosaCargoMod](
													@_SUGERENCIANOMBRE, 
													Empresa, 
													@CODIGOSOLICITUD
												)
											 )
										   WHEN Type = 'S' THEN
											 [dbo].[FNNoTilde](
												@_SUGERENCIANOMBRE
											 )
									  END,
						   FechaUltimaActualizacion = GETDATE(),
						   UsuarioUltimaActualizacion = @USUARIOCREADOR,
						   UltimoComentario = 'Se actualiza el nombre del cargo-funcion y se sugiere nomre de cargo mod'
					   WHERE CodigoCargoMod = @_CODIGOCARGOMOD
	 			
	 			SET @MESSAGE = 'Se ha actualizado el nombre del cargo-funcion (se da una asignación automatica al nombre del cargo mod, lo puede modificar si es que estima conveniente)' 
	 			
	 		END
	 		
	 		-- Actualización de nombre cargo mod
	 		IF(@TYPE = 'CM')
	 		BEGIN
		 		
		 		UPDATE [remuneraciones].[RM_CargosMod]
						   SET CargoMod = CASE WHEN Type = 'E' THEN
												 [dbo].[FNNoTilde](
													 [remuneraciones].[FNCrearGlosaCargoMod](
														UPPER(@NOMBRECARGO), 
														Empresa, 
														@CODIGOSOLICITUD
													 )
												 )
											   WHEN Type = 'S' THEN
												 [dbo].[FNNoTilde](
													UPPER(@NOMBRECARGO)
												 )
										  END,
							   FechaUltimaActualizacion = GETDATE(),
							   UsuarioUltimaActualizacion = @USUARIOCREADOR,
							   UltimoComentario = 'Se actualiza el nombre del cargo mod'
					   WHERE CodigoCargoMod = @_CODIGOCARGOMOD
		 		
		 		SET @MESSAGE = 'Se ha actualizado el nombre del cargo mod'  
		 		
		 	END
		 	
		 	SET @CODE = '200'
		 	

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION
		
	END CATCH
