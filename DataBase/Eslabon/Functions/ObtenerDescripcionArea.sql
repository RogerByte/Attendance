CREATE FUNCTION [dbo].[ObtenerDescripcionArea] 
(
	@RowData VARCHAR(2000),
	@SplitOn VARCHAR(5)
)
RETURNS VARCHAR(4000)
AS
BEGIN
	DECLARE @retValue VARCHAR(4000)
	select @retValue = COALESCE(@retValue + ' ', '') + p.Data from (SELECT Data FROM dbo.Split(@RowData, @SplitOn) WHERE Id > 1) as p;
	--SET @retValue = (SELECT Data FROM dbo.Split(@RowData, @SplitOn) WHERE Id = @index)
	RETURN @retValue
END