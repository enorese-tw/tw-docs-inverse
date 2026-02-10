CREATE PROCEDURE [finiquitos].[__PdfFiniquitoCaratula](
	@IDFINIQUITO NUMERIC
)
AS
	
	BEGIN TRY
		
		BEGIN TRANSACTION
			
			DECLARE @__DATA VARCHAR(MAX),
				    @__HTML VARCHAR(MAX),
				    @__TITLE VARCHAR(MAX),
				    @__BARCODE VARCHAR(MAX)

			DECLARE @__EXISTEPDF NUMERIC
				
			DECLARE @__SQL NVARCHAR(MAX),
					@__PDFDOCUMENTO VARCHAR(MAX)

			SET @__DATA = [dbo].[FNBase64Encode](@IDFINIQUITO)

			SELECT @__EXISTEPDF = COUNT(1)
				   FROM [finiquitos].[View_Pdf] VP
				   WHERE VP.IdFiniquito = @IDFINIQUITO AND
						 VP.TipoPdf = 'Caratula'

			IF(@__EXISTEPDF = 0)
			BEGIN
						
				SELECT @__PDFDOCUMENTO = VPDF.Pdf
					   FROM [finiquitos].[View_PdfDocumentosFiniquito] VPDF
					   WHERE VPDF.IdFiniquito = @IDFINIQUITO AND
							 VPDF.TipoDoc = 'Caratula'

				SET @__SQL =
				'
				EXEC [pdf].[__' + @__PDFDOCUMENTO + '] 
						''' + @__DATA + ''',
						@HTMLOUT OUTPUT,
						@BARCODEOUT OUTPUT,
						@TITLEOUT OUTPUT
				'

				EXEC sys.sp_executesql 
					 @__SQL,
					 N'@HTMLOUT VARCHAR(MAX) OUTPUT, @BARCODEOUT VARCHAR(MAX) OUTPUT, @TITLEOUT VARCHAR(MAX) OUTPUT',
					 @HTMLOUT = @__HTML OUTPUT,
					 @BARCODEOUT = @__BARCODE OUTPUT,
					 @TITLEOUT = @__TITLE OUTPUT

				INSERT INTO [finiquitos].[FN_Pdf]
					   VALUES(@IDFINIQUITO,
						      @__HTML,
						      'Caratula')

			END

			SELECT VP.Pdf 'html',
				   'FNQ-' + CAST(VP.IdFiniquito AS VARCHAR(MAX)) 'barcode'
				   FROM [finiquitos].[View_Pdf] VP
				   WHERE VP.IdFiniquito = @IDFINIQUITO AND
						 VP.TipoPdf = 'Caratula'

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION

		SELECT ERROR_MESSAGE() 'ERROR'

	END CATCH