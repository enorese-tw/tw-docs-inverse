CREATE PROCEDURE [dbo].[SP_API_SIGNIN](
	@USERNAME VARCHAR(MAX),
	@PASSWORD VARCHAR(MAX)
)
AS
	
	BEGIN TRY
		
		BEGIN TRANSACTION
			
			IF((SELECT COUNT(1)
			           FROM [dbo].[TW_APIUsuarios]
					   WHERE Username = @USERNAME) > 0)
			BEGIN
				
				IF((SELECT COUNT(1)
			               FROM [dbo].[TW_APIUsuarios]
					       WHERE Username = @USERNAME AND
						         GETDATE() BETWEEN ValidoDesde AND
								                   CASE WHEN ValidoHasta IS NOT NULL THEN
												      ValidoHasta
												   ELSE
													  CAST('9999-31-12' AS DATETIME)
												   END) > 0)
				BEGIN
					
					IF((SELECT COUNT(1)
							   FROM [dbo].[TW_APIUsuarios]
							   WHERE Username = @USERNAME AND
							         Password = HASHBYTES('SHA2_512', [dbo].[FN_BASE64_DECODE](@PASSWORD)) AND
									 GETDATE() BETWEEN ValidoDesde AND
													   CASE WHEN ValidoHasta IS NOT NULL THEN
														  ValidoHasta
													   ELSE
														  CAST('9999-31-12' AS DATETIME)
													   END) > 0)
					BEGIN
						
						SELECT '200' 'Code',
						       'Autorizado' 'Message'
							   
					END
					ELSE
					BEGIN
						
						SELECT '401' 'Code',
				               'Usuario no autorizado, debido a contraseña incorrecta' 'Message',
					           'Unauthorized' 'Exception'

					END

				END
				ELSE
				BEGIN
					
					SELECT '401' 'Code',
				           'Usuario no autorizado, debido a fechas de autorización de acceso a API' 'Message',
					       'Unauthorized' 'Exception'

				END

			END
			ELSE
			BEGIN
				
				SELECT '401' 'Code',
				       'Usuario no autorizado' 'Message',
					   'Unauthorized' 'Exception'

			END

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION

		SELECT '500' 'Code',
		       'Ha ocurrido un problema inesperado en el servidor, intentelo nuevamente más tarde' 'Message',
			   ERROR_MESSAGE() 'Exception'

	END CATCH