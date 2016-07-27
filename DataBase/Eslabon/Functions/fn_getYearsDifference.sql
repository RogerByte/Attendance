CREATE FUNCTION fn_getYearsDifference
(
	@StartDate datetime,
	@EndDate datetime
)
RETURNS DECIMAL(20,2)
AS
BEGIN
	DECLARE @YEARS DECIMAL(20,2)
	SET @YEARS = (SELECT CAST((CAST(s.seconds AS DECIMAL(20,2)) / 3600 / 8760) AS DECIMAL(20,2)) yrs
					FROM (select datediff(second, @StartDate, @EndDate) as seconds) s)
	RETURN @YEARS
END
