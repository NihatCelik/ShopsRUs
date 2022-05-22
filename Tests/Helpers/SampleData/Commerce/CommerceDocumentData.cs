using Business.Handlers.CommerceDocuments.Commands;
using Entities.Concrete;

namespace Tests.Helpers.SampleData
{
    public static class CommerceDocumentData
    {
        public static CommerceDocument GetCommerceDocumentDifferentFromUpdateCommerceDocumentCommand()
        {
            return new CommerceDocument
            {
                Id = 2,
                SerialNumber = "Test",
                DocumentNumber = "Test"
            };
        }

        public static UpdateCommerceDocumentCommand GetUpdateCommerceDocumentCommand()
        {
            return new UpdateCommerceDocumentCommand
            {
                Id = 1,
                SerialNumber = "Test",
                DocumentNumber = "Test"
            };
        }

        public static ValidateCommerceDocumentCommand GetValidateCommerceDocumentCommand()
        {
            return new ValidateCommerceDocumentCommand
            {
                CommerceId = 1,
                DocumentEntityId = 1,
                CountryId = 1,
                CityId = 1,
                DistrictId = 1
            };
        }
    }
}
