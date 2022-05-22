using Business.Handlers.CustomerAddresses.Commands;
using Entities.Concrete;

namespace Tests.Helpers.SampleData
{
    public static class CustomerAddressData
    {
        public static CreateCustomerAddressCommand GetCreateCustomerAddressCommand()
        {
            return new CreateCustomerAddressCommand
            {
                Address = "Adress",
                AddressName = "AddressName",
                CityId = 1,
                CountryId = 1,
                CustomerAddressEntityId = 1,
                CustomerBranchId = 1,
                CustomerId = 1,
                DistrictId = 1,
                Email = "EMail",
                Phone1 = "Phone1",
                Phone2 = "Phone2",
            };
        }

        public static CustomerAddress GetCustomerAddressCommand()
        {
            return new CustomerAddress
            {
                Address = "Adress",
                AddressName = "AddressName",
                CityId = 1,
                CountryId = 1,
                CustomerAddressEntityId = 1,
                CustomerBranchId = 1,
                CustomerId = 1,
                DistrictId = 1,
                Email = "EMail",
                Phone1 = "Phone1",
                Phone2 = "Phone2",
            };
        }


        public static UpdateCustomerAddressCommand GetUpdateCustomerAddressCommand()
        {
            return new UpdateCustomerAddressCommand
            {
                Id = 1,
                Address = "Adress",
                AddressName = "AddressName",
                CityId = 1,
                CountryId = 1,
                CustomerAddressEntityId = 1,
                CustomerBranchId = 1,
                CustomerId = 1,
                DistrictId = 1,
                Email = "EMail",
                Phone1 = "Phone1",
                Phone2 = "Phone2",
            };
        }

        public static ValidateCustomerAddressCommand GetValidateCustomerAddressCommand()
        {
            return new ValidateCustomerAddressCommand
            {
                CityId = 1,
                CountryId = 1,
                CustomerAddressEntityId = 1,
                CustomerBranchId = 1,
                CustomerId = 1,
                DistrictId = 1
            };
        }

        public static CustomerAddress GetCustomerAddressDifferentFromUpdateCustomerAddressCommand()
        {
            return new CustomerAddress
            {
                Id = 2,
                Address = "Adress",
                AddressName = "AddressName",
                CityId = 1,
                CountryId = 1,
                CustomerAddressEntityId = 1,
                CustomerBranchId = 1,
                CustomerId = 1,
                DistrictId = 1,
                Email = "EMail",
                Phone1 = "Phone1",
                Phone2 = "Phone2",
            };
        }
    }
}
