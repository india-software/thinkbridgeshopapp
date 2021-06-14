CREATE PROCEDURE [dbo].[ThinkBridgeProductLoadAllPagedSearch]
(
    @PageIndex			int = 0, 
	@PageSize			int = 2147483644,
	@CategoryNewIds     nvarchar(MAX) = null,	--a list of category IDs (comma-separated list). e.g. 1,2,3	
	@TotalRecords		int = null OUTPUT
)
AS
BEGIN
DECLARE
@sql nvarchar(max)


SET @sql = '
	SELECT p.*
	FROM
		Product p (NOLOCK) INNER JOIN Product_Category_Mapping pcm with (NOLOCK)
			ON p.Id = pcm.ProductId where p.Deleted=0  and p.published=1 '
	
	
	--filter by category
	IF (@CategoryNewIds is not null And @CategoryNewIds!='')
	BEGIN
		SET @sql = @sql + '
		AND pcm.CategoryId IN ('
		SET @sql = @sql + + CAST(@CategoryNewIds AS nvarchar(max))
		SET @sql = @sql + ')'
	END
	--PRINT (@sql)
	EXEC sp_executesql @sql
	SET @TotalRecords = @@rowcount
	END
GO


