using Business.Handlers.Products.Commands;
using Entities.Concrete;

namespace Tests.Helpers.SampleData
{
    public static class ProductData
    {
        public static CreateProductCommand GetCreateProductCommand()
        {
            return new CreateProductCommand
            {
                Category1Id = 1,
                Category2Id = 1,
                Description1 = "Description1",
                Description2 = "Description1",
                IsActive = true,
                IsDomesticProduction = true,
                Origin = "Origin",
                ProductCode = "ProductCode",
                ProductName = "ProductName",
                TrademarkId = 1
            };
        }

        public static Product GetProductCommand()
        {
            return new Product
            {
                Id = 1,
                Category1Id = 1,
                Category2Id = 1,
                Description1 = "Description1",
                Description2 = "Description1",
                IsActive = true,
                IsDomesticProduction = true,
                Origin = "Origin",
                ProductCode = "ProductCode",
                ProductName = "ProductName",
                TrademarkId = 1,
            };
        }


        public static UpdateProductCommand GetUpdateProductCommand()
        {
            return new UpdateProductCommand
            {
                Id = 1,
                Category1Id = 1,
                Category2Id = 1,
                Description1 = "Description1",
                Description2 = "Description1",
                IsActive = true,
                IsDomesticProduction = true,
                Origin = "Origin",
                ProductCode = "ProductCode",
                ProductName = "ProductName",
                TrademarkId = 1
            };
        }

        public static ValidateProductCommand GetValidateProductCommand()
        {
            return new ValidateProductCommand
            {
                Category1Id = 1,
                Category2Id = 1,
                TrademarkId = 1
            };
        }

        public static Product GetProductDifferentFromUpdateProductCommand()
        {
            return new Product
            {
                Id = 2,
                Category1Id = 1,
                Category2Id = 1,
                Description1 = "Description1",
                Description2 = "Description1",
                IsActive = true,
                IsDomesticProduction = true,
                Origin = "Origin",
                ProductCode = "ProductCode",
                ProductName = "ProductName",
                TrademarkId = 1
            };
        }
    }
}
