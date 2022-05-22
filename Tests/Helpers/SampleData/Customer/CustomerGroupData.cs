namespace Tests.Helpers.SampleData
{
    public static class CustomerGroupData
    {
        public static CustomerGroup GetCustomerGroupDifferentFromUpdateCustomerGroupCommand()
        {
            return new CustomerGroup
            {
                Id = 2,
                IsActive = true,
                GroupName = "Test"
            };
        }

        public static UpdateCustomerGroupCommand GetUpdateCustomerGroupCommand()
        {
            return new UpdateCustomerGroupCommand
            {
                Id = 1,
                IsActive = true,
                GroupName = "Test"
            };
        }
    }
}
