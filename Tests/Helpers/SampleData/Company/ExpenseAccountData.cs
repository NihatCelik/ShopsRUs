using Business.Handlers.ExpenseAccounts.Commands;
using Entities.Concrete;

namespace Tests.Helpers.SampleData
{
    public static class ExpenseAccountData
    {
        public static ExpenseAccount GetExpenseAccountDifferentFromUpdateExpenseAccountCommand()
        {
            return new ExpenseAccount
            {
                Id = 2,
                DocumentNumber = "Test",
            };
        }

        public static UpdateExpenseAccountCommand GetUpdateExpenseAccountCommand()
        {
            return new UpdateExpenseAccountCommand
            {
                Id = 1,
                DocumentNumber = "Test",
            };
        }

        public static ValidateExpenseAccountCommand GetValidateExpenseAccountCommand()
        {
            return new ValidateExpenseAccountCommand
            {
                AccountId = 1,
                EnvironmentEntityId = 1,
                ExpenseCardId = 1
            };
        }
    }
}
