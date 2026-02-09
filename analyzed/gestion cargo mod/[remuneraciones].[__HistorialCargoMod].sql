CREATE PROCEDURE [remuneraciones].[__HistorialCargoMod](
	@CODIGOCARGOMOD VARCHAR(MAX)
)
AS
	BEGIN TRY

		BEGIN TRANSACTION

			SELECT ISNULL(FORMAT(VCML.Fecha, 'dd-MM-yyyy HH:mm') + ' horas.', '-') 'Fecha',
				  CASE WHEN VCML.Estado ='CREATE' OR VCML.Estado = 'SOLIC' THEN 
							'Solicitud creada'
						WHEN VCML.Estado = 'PDAPROB' THEN
							'Pendiente validación finanzas'
						WHEN VCML.Estado = 'APROB' THEN
							'Pendiente creación en Softland'
						WHEN VCML.Estado = 'FD' THEN
							'Pendiente de creación en firma digital'
						WHEN VCML.Estado = 'TER' THEN
							'Solicitud Terminada'

				  ELSE 
					'Estado Desconocido'
				  END 'Estado',
				  VCML.Descripcion,
				  CASE WHEN VCML.Usuario != 'Sistema' THEN
						(SELECT VU.Nombre + ' ' + VU.Apellido
								FROM [cargamasiva].[View_Usuarios] VU
								WHERE (VU.NombreUsuario = CASE WHEN VCML.Usuario != 'Sistema' THEN 
																[dbo].[FNBase64Decode](VCML.Usuario) 
														  ELSE
																VCML.Usuario 
														  END OR
										VU.Correo = CASE WHEN VCML.Usuario != 'Sistema' THEN 
															[dbo].[FNBase64Decode](VCML.Usuario) 
													ELSE
															VCML.Usuario 
													END))
				   ELSE
						VCML.Usuario
				   END 'Usuario'
				   FROM [remuneraciones].[View_CargosModLog] VCML
				   WHERE VCML.CodigoCargoMod = [dbo].[FNBase64Decode](@CODIGOCARGOMOD)
				   ORDER BY VCML.Fecha DESC


		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION

	END CATCH