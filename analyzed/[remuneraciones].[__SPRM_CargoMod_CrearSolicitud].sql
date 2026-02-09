CREATE PROCEDURE [remuneraciones].[__SPRM_CargoMod_CrearSolicitud](
	@EMPRESA VARCHAR(MAX),
	@USUARIOCREADOR VARCHAR(MAX),
	@TYPE VARCHAR(MAX),
	@CODE VARCHAR(MAX) OUTPUT,
	@CODINE VARCHAR(MAX) OUTPUT,
	@MESSAGE VARCHAR(MAX) OUTPUT
)
AS
	
	BEGIN TRY
		
		BEGIN TRANSACTION
			
			DECLARE @TEMPORALCODINE VARCHAR(MAX)

			DECLARE @PROFILE VARCHAR(MAX)

			DECLARE @__PREFIX VARCHAR(MAX)

			IF(CHARINDEX('@', [TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](@USUARIOCREADOR)) > 0)
			BEGIN

				SELECT @PROFILE = UPPER(TWP.Nombre)
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
				
				SELECT @PROFILE = UPPER(TWP.Nombre)
					   FROM [TW_GENERAL_TEAMWORK].[dbo].[TW_Usuarios] TWU WITH (NOLOCK)
					   INNER JOIN [TW_GENERAL_TEAMWORK].[dbo].[TW_Auth] TWA WITH (NOLOCK)
					   ON TWA.Usuario = TWU.Id
					   INNER JOIN [TW_GENERAL_TEAMWORK].[dbo].[TW_AuthProfiles] TWAP WITH (NOLOCK)
					   ON TWAP.Auth = TWA.Id
					   INNER JOIN [TW_GENERAL_TEAMWORK].[dbo].[TW_Profiles] TWP WITH (NOLOCK)
					   ON TWP.Id = TWAP.Profile 
					   WHERE TWU.NombreUsuario = [TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE](@USUARIOCREADOR)

			END

			SET @__PREFIX = CASE WHEN [dbo].[FNBase64Decode](@TYPE) = 'E' THEN
			                   'SLC'
							ELSE
							   'CTZ'
							END

			SELECT @TEMPORALCODINE = @__PREFIX + CAST(SecuenciaSgte AS VARCHAR(MAX))
			       FROM [remuneraciones].[RM_Codigo] WITH (NOLOCK)
				   WHERE Concepto = 'Codine-' + [dbo].[FNBase64Decode](@TYPE)

			UPDATE [remuneraciones].[RM_Codigo]
				   SET SecuenciaSgte = SecuenciaSgte + 1
				   WHERE Concepto = 'Codine-' + [dbo].[FNBase64Decode](@TYPE)


			-- Creacion de la solicitud de cargo mod con codine temporal
			-- Ej. SLC1-TWEST
			INSERT INTO [remuneraciones].[RM_CargosMod] (CodigoCargoMod,
			                                             Codigo, 
														 Empresa, 
														 TipoContrato,
														 UsuarioCreador,
														 Estado,
														 FechaCreacion,
														 UltimoComentario,
														 GratificacionPactada,
														 DiasTrabajados,
														 AFP,
														 IsapreFonosa,
														 HH,
														 DescuentoAtrasos,
														 PACIsaprePorc,
														 GratifCC,
														 TypeSueldoInput,
														 TypeSueldo,
														 TypeJornada,
														 Type,
														 Wizards,
														 NHorasSemanales)
			VALUES(@TEMPORALCODINE + '-' + @EMPRESA,
			       @TEMPORALCODINE,
			       @EMPRESA,
				   CASE WHEN @EMPRESA = 'TWEST' THEN
				        2
				   ELSE
						1
				   END,
				   @USUARIOCREADOR,
				   CASE WHEN @PROFILE = 'KAM' THEN
						'CREATE'
				   ELSE
						'PDAPROB'
				   END,
				   GETDATE(),
				   'Creado ambiente para solicitud',
				   'N',
				   30,
				   [remuneraciones].[FNAFPDefault](@EMPRESA),
				   'FONASA',
				   0,
				   0,
				   0,
				   'N',
				   'SB',
				   'M',
				   'F',
				   [dbo].[FNBase64Decode](@TYPE),
				   1,
				   45)

			INSERT INTO [remuneraciones].[RM_ANICargoMod]
			       VALUES(@TEMPORALCODINE + '-' + @EMPRESA,
				          'ANI00001',
						  0,
						  CAST(GETDATE() AS DATE),
						  NULL,
						  GETDATE(),
						  @USUARIOCREADOR,
						  NULL,
						  NULL,
						  'VIG',
						  '',
						  0,
						  NULL)

			INSERT INTO [remuneraciones].[RM_ANICargoMod]
			       VALUES(@TEMPORALCODINE + '-' + @EMPRESA,
				          'ANI00002',
						  0,
						  CAST(GETDATE() AS DATE),
						  NULL,
						  GETDATE(),
						  @USUARIOCREADOR,
						  NULL,
						  NULL,
						  'VIG',
						  '',
						  0,
						  NULL)

			SET @CODE = '200'
			SET @CODINE = [dbo].[FNBase64Encode](@TEMPORALCODINE + '-' + @EMPRESA)
			SET @MESSAGE = 'Se ha creado el ambiente para solicitar cargo mod'

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION

	END CATCH
