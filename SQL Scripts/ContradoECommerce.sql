USE [ECommerceDemo]
GO

/****** Object:  StoredProcedure [dbo].[usp_product_get_all]    Script Date: 11/19/2019 08:07:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_product_get_all]
AS
BEGIN
	SET NOCOUNT ON
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	
	SELECT P.[ProductId], P.[ProdName], P.[ProdDescription], PC.[CategoryName] FROM dbo.[Product] P
	INNER JOIN dbo.[ProductCategory] PC ON P.ProdCatId = PC.ProdCatId

	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
END

GO

/****** Object:  StoredProcedure [dbo].[usp_productattributelookup_get_all]    Script Date: 11/19/2019 08:07:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_productattributelookup_get_all]
AS
BEGIN
	SET NOCOUNT ON
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	
	SELECT [AttributeId], [ProdCatId], [AttributeName] FROM dbo.[ProductAttributeLookup]

	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
END

GO
