namespace Tests.Helpers.SampleData
{
    public static class CustomerAuthorizedUserData
    {
        public static ValidateCustomerAuthorizedUserCommand GetValidateCustomerAuthorizedUserCommand()
        {
            return new ValidateCustomerAuthorizedUserCommand
            {
                CustomerId = 1,
                UserId = 1
            };
        }
    }
}
