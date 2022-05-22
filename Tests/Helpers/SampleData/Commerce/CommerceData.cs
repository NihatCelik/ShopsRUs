namespace Tests.Helpers.SampleData
{
    public static class CommerceData
    {
        public static ValidateCommerceCommand GetValidateCommerceCommand()
        {
            return new ValidateCommerceCommand
            {
                CustomerId = 1,
                CustomerBranchId = 1,
                EnvironmentEntityId = 1,
                CommerceEntityId = 2,
                CurrencyId = 1
            };
        }
    }
}
