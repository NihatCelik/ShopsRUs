namespace Tests.Helpers.SampleData
{
    public static class PriceListData
    {
        public static PriceList GetPriceListDifferentFromUpdatePriceListCommand()
        {
            return new PriceList
            {
                Id = 2,
                IsActive = true,
                Name = "Test"
            };
        }

        public static UpdatePriceListCommand GetUpdatePriceListCommand()
        {
            return new UpdatePriceListCommand
            {
                Id = 1,
                IsActive = true,
                Name = "Test"
            };
        }
    }
}
