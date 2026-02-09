CREATE PROCEDURE [remuneraciones].[__SPRM_CargoMod_Clientes](
	@EMPRESA VARCHAR(MAX) = '',
	@TYPEFILTER VARCHAR(MAX) = '',
	@DATAFILTER VARCHAR(MAX) = '',
	@PAGINATION VARCHAR(MAX) = '',
	@USUARIOCREADOR VARCHAR(MAX) = ''
)
AS
	
	BEGIN TRY
		
		BEGIN TRANSACTION
			
			DECLARE @SQL VARCHAR(MAX)
			DECLARE @INIT	VARCHAR(MAX),
					@END	VARCHAR(MAX)

			DECLARE @PROFILE VARCHAR(MAX),
					@__USER VARCHAR(MAX)

			IF(@PAGINATION <> '')
			BEGIN
				SET @INIT = SUBSTRING([TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](@PAGINATION), 1, CHARINDEX('-', [TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](@PAGINATION)) - 1)
				SET @END = SUBSTRING([TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](@PAGINATION), CHARINDEX('-', [TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](@PAGINATION)) + 1, LEN([TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](@PAGINATION)))
			END

			IF(CHARINDEX('@', [TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](@USUARIOCREADOR)) > 0)
			BEGIN

				SELECT @PROFILE = UPPER(TWP.Nombre),
						@__USER = CAST(TWU.Id AS VARCHAR(MAX))
						FROM [TW_GENERAL_TEAMWORK].[dbo].[TW_Usuarios] TWU WITH (NOLOCK)
						INNER JOIN [TW_GENERAL_TEAMWORK].[dbo].[TW_Auth] TWA WITH (NOLOCK)
						ON TWA.Usuario = TWU.Id
						INNER JOIN [TW_GENERAL_TEAMWORK].[dbo].[TW_AuthProfiles] TWAP WITH (NOLOCK)
						ON TWAP.Auth = TWA.Id
						INNER JOIN [TW_GENERAL_TEAMWORK].[dbo].[TW_Profiles] TWP WITH (NOLOCK)
						ON TWP.Id = TWAP.Profile 
						WHERE TWU.Correo = [TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](@USUARIOCREADOR)

			END
			ELSE
			BEGIN
				
				SELECT @PROFILE = UPPER(TWP.Nombre),
						@__USER = CAST(TWU.Id AS VARCHAR(MAX))
						FROM [TW_GENERAL_TEAMWORK].[dbo].[TW_Usuarios] TWU WITH (NOLOCK)
						INNER JOIN [TW_GENERAL_TEAMWORK].[dbo].[TW_Auth] TWA WITH (NOLOCK)
						ON TWA.Usuario = TWU.Id
						INNER JOIN [TW_GENERAL_TEAMWORK].[dbo].[TW_AuthProfiles] TWAP WITH (NOLOCK)
						ON TWAP.Auth = TWA.Id
						INNER JOIN [TW_GENERAL_TEAMWORK].[dbo].[TW_Profiles] TWP WITH (NOLOCK)
						ON TWP.Id = TWAP.Profile 
						WHERE TWU.NombreUsuario = [TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](@USUARIOCREADOR)

			END

			

			SET @SQL = 
			'
			;WITH Clientes AS
			(
				SELECT *,
					   ROW_NUMBER() OVER(ORDER BY Codigo ASC) AS ROW_NUMBER
					   FROM [remuneraciones].[View_Clientes]
					   '
					   +
					   CASE WHEN @TYPEFILTER = 'Codigo' THEN
						   CASE WHEN @EMPRESA <> '' THEN
								'WHERE Codigo LIKE ''%' + @DATAFILTER + '%'' AND Empresa = ''' + @EMPRESA + ''' '
							   WHEN @EMPRESA = '' THEN
							    'WHERE Codigo LIKE ''%' + @DATAFILTER + '%'' '
						  END
					      
					   ELSE
						  CASE WHEN @EMPRESA <> '' THEN
								'WHERE Empresa = ''' + @EMPRESA + ''' '
							   WHEN @EMPRESA = '' THEN
							    ''
						  END
					   END
					   +
					   CASE WHEN @PROFILE = 'KAM' THEN
							'AND Codigo IN (SELECT VKC.Codigo
							                       FROM [remuneraciones].[View_KamCliente] VKC
												   WHERE VKC.IdUsuario = ' + @__USER + ')
								OR 
								Codigo IN (SELECT VKC.Codigo
								                  FROM [remuneraciones].[View_KamCliente] VKC 
												  WHERE VKC.IdKamCliente IN (SELECT VAKC.KamCliente
												                                    FROM [remuneraciones].[View_AsistenteKamCliente] VAKC
																					WHERE VAKC.Asistente = ' + @__USER + '))
							  '
					   ELSE
						    ''
					   END
					   +
					   '
			)
			SELECT DISTINCT Codigo,
			                Value,
							Empresa
			       FROM Clientes
				   WHERE ROW_NUMBER BETWEEN '
				   +
				   CASE WHEN @PAGINATION <> '' THEN
						@INIT + ' AND ' + @END
				   ELSE
						'1 AND 1000'
				   END
				   +
				   '
			'

			EXEC (@SQL)

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION

	END CATCH
