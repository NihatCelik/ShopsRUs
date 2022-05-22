namespace Tests.Helpers.SampleData
{
    public static class ProductPriceData
    {
        public static CreateOrUpdateProductPriceCommand GetCreateOrUpdateProductPriceCommand()
        {
            return new CreateOrUpdateProductPriceCommand
            {
                ProductId = 1,
                ProfitRate = 1,
                ProfitValue = 1,
                PurchasePrice = 1,
                PurchasePriceCurrencyId = 1,
                PurchaseVatExcluded = true,
                PurchaseVatRate = 1,
                SalePrice = 1,
                SalePriceCurrencyId = 1,
                SaleVatExcluded = true,
                SaleVatRate = 1
            };
        }

        public static ProductPrice GetProductPriceCommand()
        {
            return new ProductPrice
            {
                ProductId = 1,
                ProfitRate = 1,
                ProfitValue = 1,
                PurchasePrice = 1,
                PurchasePriceCurrencyId = 1,
                PurchaseVatExcluded = true,
                PurchaseVatRate = 1,
                SalePrice = 1,
                SalePriceCurrencyId = 1,
                SaleVatExcluded = true,
                SaleVatRate = 1
            };
        }

        public static ValidateProductPriceCommand GetValidateProductPriceCommand()
        {
            return new ValidateProductPriceCommand
            {
                ProductId = 1,
                PurchasePriceCurrencyId = 1,
                SalePriceCurrencyId = 1
            };
        }
    }
}
