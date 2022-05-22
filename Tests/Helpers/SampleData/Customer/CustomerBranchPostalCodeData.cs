using Business.Handlers.CustomerBranchPostalCodes.Commands;
using Entities.Concrete;

namespace Tests.Helpers.SampleData
{
    public static class CustomerBranchPostalCodeData
    {
        public static ValidateCustomerBranchPostalCodeCommand GetValidateCustomerBranchPostalCodeCommand()
        {
            return new ValidateCustomerBranchPostalCodeCommand
            {
                CustomerId = 1,
                CustomerBranchId = 1,
                PostalCodeTypeEntityId = 1
            };
        }

        public static CustomerBranchPostalCode GetCustomerBranchPostalCodeCommand()
        {
            return new CustomerBranchPostalCode
            {
                Id = 1,
                CustomerId = 1,
                CustomerBranchId = 1,
                PostalCodeTypeEntityId = 1,
                Value = "test"
            };
        }

        public static CustomerBranchPostalCode GetCustomerBranchPostalCodeDifferentFromUpdateCustomerBranchPostalCodeCommand()
        {
            return new CustomerBranchPostalCode
            {
                Id = 2,
                CustomerId = 1,
                CustomerBranchId = 1,
                PostalCodeTypeEntityId = 1,
                Value = "test"
            };
        }
    }
}
