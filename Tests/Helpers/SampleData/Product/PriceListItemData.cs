using Business.Handlers.PriceListItems.Commands;

namespace Tests.Helpers.SampleData
{
    public static class PriceListItemData
    {
        public static ValidatePriceListItemCommand GetValidatePriceListItemCommand()
        {
            return new ValidatePriceListItemCommand
            {
                PriceListId = 1,
                ProductId = 1,
                ProductVariantId = 1,
                SalePriceCurrencyId = 1
            };
        }
    }
}
