namespace Tests.Helpers.SampleData
{
    public static class CustomerData
    {
        public static CreateCustomerCommand GetCreateCustomerCommand()
        {
            return new CreateCustomerCommand
            {
                ImageFileId = 1,
                CurrencyId = 1,
                CustomerGroup1Id = 1,
                CustomerGroup2Id = 2,
                PriceListId = 1,
                CustomerTypeEntityId = 1,
                CustomerCode = "code",
                CustomerName = "name",
                CustomerSurname = "surname",
                AuthorizedPerson = "authorizedPerson",
                TaxOffice = "taxOffice",
                TaxNumber = "taxNumber",
                Iban = "iban",
                RiskLimit = 1,
                ExpiryDay = 1,
                PercentDiscount = 1,
                Description1 = "",
                Description2 = "",
                IsActive = true,
            };
        }

        public static ValidateCustomerCommand GetValidateCustomerCommand()
        {
            return new ValidateCustomerCommand
            {
                ImageFileId = 1,
                CurrencyId = 1,
                CustomerGroup1Id = 1,
                CustomerGroup2Id = 2,
                PriceListId = 1,
                CustomerTypeEntityId = 1
            };
        }

        public static Customer GetCustomerDifferentFromUpdateCustomerCommand()
        {
            return new Customer
            {
                Id = 2,
                IsActive = true,
                CustomerCode = "11",
                CustomerName = "Test",
                CustomerSurname = "Test"
            };
        }

        public static UpdateCustomerCommand GetUpdateCustomerCommand()
        {
            return new UpdateCustomerCommand
            {
                Id = 1,
                IsActive = true,
                CustomerCode = "11",
                CustomerName = "Test",
                CustomerSurname = "Test"
            };
        }
    }
}
