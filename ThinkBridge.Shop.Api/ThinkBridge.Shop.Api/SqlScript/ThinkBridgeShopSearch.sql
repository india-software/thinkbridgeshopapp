USE [ThinkBridgeShopDB]
GO
/****** Object:  StoredProcedure [dbo].[ThinkBridgeProductSearch]    Script Date: 15-06-2021 02:15:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[ThinkBridgeProductSearch]
(
    @PageIndex			int = 0, 
	@PageSize			int = 2147483644,
	@CategoryIds     nvarchar(MAX) = null,	--a list of category IDs (comma-separated list). e.g. 1,2,3
	@ManufacturerId		int = 0,
	@Keywords			nvarchar(400) = null,
	@TotalRecords		int = null OUTPUT
)
AS
BEGIN

DECLARE
@sql nvarchar(max),
@sqlcondition nvarchar(max)


SET @sql = '
	SELECT p.*
	FROM
		Product p'
		
Set @sqlcondition=' where p.Deleted=0  and p.published=1 '
	--filter by keywords bases on their name but we can use for full text search
	IF (@Keywords is not null And @Keywords!='')
	BEGIN
		SET @sqlcondition = @sqlcondition + ' And p.Name like ''%' + @Keywords + '%'''
		
	END
	--filter by category
	IF (@CategoryIds is not null And @CategoryIds!='')
	BEGIN
	SET @sql=@sql+'(NOLOCK) INNER JOIN ProductCategory pcm with (NOLOCK) ON p.Id = pcm.ProductId '
		SET  @sqlcondition = @sqlcondition + '
		AND pcm.CategoryId IN ('
		SET @sqlcondition = @sqlcondition + + CAST(@CategoryIds AS nvarchar(max))
		SET @sqlcondition = @sqlcondition + ')'
	END
	

	---filter by manufacturer
	IF @ManufacturerId > 0
	BEGIN
		SET @sql = @sql + '
		INNER JOIN ProductManufacturer pmm with (NOLOCK)
			ON p.Id = pmm.ProductId'
		Set @sqlcondition=@sqlcondition+' AND pmm.ManufacturerId = ' + CAST(@ManufacturerId AS nvarchar(max))
	END
	
	--PRINT (@sql)
	SET @sql=@sql +' '+@sqlcondition+' order by p.id OFFSET '+ Cast((@PageSize * @PageIndex)  as nvarchar(Max))
	set @sql=@sql+' ROWS FETCH NEXT '+Cast(@PageSize as nvarchar(max))
	set @sql=@sql+' ROWS ONLY'
	EXEC sp_executesql  @sql
	
	SET @TotalRecords = @@rowcount
	PRINT (@sql)
	
	END
