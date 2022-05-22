namespace Tests.Helpers.SampleData
{
    public static class BankData
    {
        public static Bank GetBankDifferentFromUpdateBankCommand()
        {
            return new Bank
            {
                Id = 2,
                IsActive = true,
                Name = "Test"
            };
        }

        public static UpdateBankCommand GetUpdateBankCommand()
        {
            return new UpdateBankCommand
            {
                Id = 1,
                IsActive = true,
                Name = "Test"
            };
        }
    }
}
