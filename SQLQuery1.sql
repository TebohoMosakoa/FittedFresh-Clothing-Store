USE [dbMyOnlineShopping]
GO

DECLARE	@return_value Int

EXEC	@return_value = [dbo].[GetBySearch]

SELECT	@return_value as 'Return Value'

GO
