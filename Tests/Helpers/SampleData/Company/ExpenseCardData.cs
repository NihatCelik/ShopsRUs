using Business.Handlers.ExpenseCards.Commands;
using Entities.Concrete;

namespace Tests.Helpers.SampleData
{
    public static class ExpenseCardData
    {
        public static ExpenseCard GetExpenseCardDifferentFromUpdateExpenseCardCommand()
        {
            return new ExpenseCard
            {
                Id = 2,
                IsActive = true,
                Name = "Test"
            };
        }

        public static UpdateExpenseCardCommand GetUpdateExpenseCardCommand()
        {
            return new UpdateExpenseCardCommand
            {
                Id = 1,
                IsActive = true,
                Name = "Test"
            };
        }
    }
}
