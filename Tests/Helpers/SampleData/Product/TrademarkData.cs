namespace Tests.Helpers.SampleData
{
    public static class TrademarkData
    {
        public static Trademark GetTrademarkDifferentFromUpdateTrademarkCommand()
        {
            return new Trademark
            {
                Id = 2,
                IsActive = true,
                TrademarkName = "Test"
            };
        }

        public static UpdateTrademarkCommand GetUpdateTrademarkCommand()
        {
            return new UpdateTrademarkCommand
            {
                Id = 1,
                IsActive = true,
                TrademarkName = "Test"
            };
        }
    }
}
