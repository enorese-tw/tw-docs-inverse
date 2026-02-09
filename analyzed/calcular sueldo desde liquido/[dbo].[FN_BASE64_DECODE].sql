
CREATE FUNCTION [dbo].[FN_BASE64_DECODE]
(
	@encoded_text varchar(8000)
)
RETURNS VARCHAR(MAX)
AS
BEGIN

	DECLARE
		@output           VARCHAR(8000),
		@block_start      INT,
		@encoded_length   INT,
		@decoded_length   INT,
		@mapr             BINARY(122)

	SET @encoded_text = REPLACE(REPLACE(REPLACE(REPLACE(@encoded_text, ' ',''),CHAR(9),''),CHAR(10),''),CHAR(13),'')
	
	IF @encoded_text COLLATE LATIN1_GENERAL_BIN
	   LIKE '%[^ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=]%'
	   COLLATE LATIN1_GENERAL_BIN
	BEGIN
		RETURN CAST('Invalid Base64 data found in ' + @encoded_text AS INT)
	END

	--IF LEN(@encoded_text) & 3 > 0
	--  RETURN NULL
	SET @output   = ''
	-- The nth byte of @mapr contains the base64 value
	-- of the character with an ASCII value of n.
	-- EG, 65th byte = 0x00 = 0 = value of 'A'
	SET @mapr = 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF -- 1-33
			  + 0xFFFFFFFFFFFFFFFFFFFF3EFFFFFF3F3435363738393A3B3C3DFFFFFF00FFFFFF -- 33-64
			  + 0x000102030405060708090A0B0C0D0E0F10111213141516171819FFFFFFFFFFFF -- 65-96
			  + 0x1A1B1C1D1E1F202122232425262728292A2B2C2D2E2F30313233 -- 97-122
	
	--get the number of blocks to be decoded
	SET @encoded_length = LEN(@encoded_text)
	SET @decoded_length = @encoded_length / 4 * 3
	--for each block
	SET @block_start = 1
	WHILE @block_start < @encoded_length BEGIN
		--decode the block and add to output
		--BINARY values between 1 and 4 bytes can be implicitly cast to INT
		SET @output = @output + CAST(CAST(CAST(
								SUBSTRING( @mapr, ASCII( SUBSTRING( @encoded_text, @block_start    , 1) ), 1) * 262144
							  + SUBSTRING( @mapr, ASCII( SUBSTRING( @encoded_text, @block_start + 1, 1) ), 1) * 4096
							  + SUBSTRING( @mapr, ASCII( SUBSTRING( @encoded_text, @block_start + 2, 1) ), 1) * 64
							  + SUBSTRING( @mapr, ASCII( SUBSTRING( @encoded_text, @block_start + 3, 1) ), 1) 
							  AS INTEGER) AS BINARY(3)) AS VARCHAR(3))

		SET @block_start = @block_start + 4
	END
	
	IF RIGHT(@encoded_text, 2) = '=='
		SET @decoded_length = @decoded_length - 2
	ELSE IF RIGHT(@encoded_text, 1) = '='
		SET @decoded_length = @decoded_length - 1
		--IF SUBSTRING(@output, @decoded_length, 1) = CHAR(0)
		-- SET @decoded_length = @decoded_length - 1
		--return the decoded string

	RETURN LEFT(@output, @decoded_length)

END
