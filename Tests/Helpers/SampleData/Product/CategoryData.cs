using Business.Handlers.Categories.Commands;
using Entities.Concrete;

namespace Tests.Helpers.SampleData
{
    public static class CategoryData
    {
        public static Category GetCategoryDifferentFromUpdateCategoryCommand()
        {
            return new Category
            {
                Id = 2,
                IsActive = true,
                CategoryName = "Test"
            };
        }

        public static UpdateCategoryCommand GetUpdateCategoryCommand()
        {
            return new UpdateCategoryCommand
            {
                Id = 1,
                IsActive = true,
                CategoryName = "Test"
            };
        }
    }
}
