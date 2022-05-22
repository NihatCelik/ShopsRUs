namespace Tests.Helpers.SampleData
{
    public static class CustomerAccountData
    {
        public static CustomerAccount GetCustomerAccountDifferentFromUpdateCustomerAccountCommand()
        {
            return new CustomerAccount
            {
                Id = 2,
                DocumentNumber = "Test",
            };
        }

        public static UpdateCustomerAccountCommand GetUpdateCustomerAccountCommand()
        {
            return new UpdateCustomerAccountCommand
            {
                Id = 1,
                DocumentNumber = "Test",
            };
        }

        public static ValidateCustomerAccountCommand GetValidateCustomerAccountCommand()
        {
            return new ValidateCustomerAccountCommand
            {
                AccountId = 1,
                CustomerAccountEntityId = 1,
                CustomerId = 1,
                EnvironmentEntityId = 2,
                Debt = 100,
                Receivable = 0,
                LastCustomerBalance = 0,
            };
        }
    }
}
