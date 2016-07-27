CREATE FUNCTION [dbo].[ScalarSplit] 
(
	@RowData VARCHAR(2000),
	@SplitOn VARCHAR(5),
	@index INT
)
RETURNS VARCHAR(4000)
AS
BEGIN
	DECLARE @retValue VARCHAR(4000)
	select @retValue = COALESCE(@retValue + ' ', '') + p.Data from (SELECT Data FROM dbo.Split(@RowData, @SplitOn) WHERE Id = @index) as p;
	--SET @retValue = (SELECT Data FROM dbo.Split(@RowData, @SplitOn) WHERE Id = @index)
	RETURN @retValue
END