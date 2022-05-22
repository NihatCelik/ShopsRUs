using Business.Handlers.CustomerBranches.Commands;
using Entities.Concrete;

namespace Tests.Helpers.SampleData
{
    public static class CustomerBranchData
    {
        public static CustomerBranch GetCustomerBranchDifferentFromUpdateCustomerBranchCommand()
        {
            return new CustomerBranch
            {
                Id = 2,
                IsActive = true,
                BranchName = "Test"
            };
        }

        public static UpdateCustomerBranchCommand GetUpdateCustomerBranchCommand()
        {
            return new UpdateCustomerBranchCommand
            {
                Id = 1,
                IsActive = true,
                BranchName = "Test"
            };
        }
    }
}
