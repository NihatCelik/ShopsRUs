using Business.Handlers.Variants.Commands;
using Entities.Concrete;

namespace Tests.Helpers.SampleData
{
    public static class VariantData
    {
        public static Variant GetVariantDifferentFromUpdateVariantCommand()
        {
            return new Variant
            {
                Id = 2,
                IsActive = true,
                VariantName = "Test"
            };
        }

        public static UpdateVariantCommand GetUpdateVariantCommand()
        {
            return new UpdateVariantCommand
            {
                Id = 1,
                IsActive = true,
                VariantName = "Test"
            };
        }
    }
}
