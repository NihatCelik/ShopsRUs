namespace Tests.Helpers.SampleData
{
    public static class CommerceProductData
    {
        public static ValidateCommerceProductCommand GetValidateCommerceProductCommand()
        {
            return new ValidateCommerceProductCommand
            {
                CommerceId = 1,
                ProductId = 1,
                UnitId = 1,
                ProductVariantId = 1,
                WarehouseId = 1,
                CommerceEntityId = 1,
                LastStockValue = 10,
                Quantity = 5,
            };
        }
    }
}
