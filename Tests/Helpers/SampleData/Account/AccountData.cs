using Business.Handlers.Accounts.Commands;
using Entities.Concrete;

namespace Tests.Helpers.SampleData
{
    public static class AccountData
    {
        public static Account GetAccountDifferentFromUpdateAccountCommand()
        {
            return new Account
            {
                Id = 2,
                AccountName = "Test",
            };
        }

        public static UpdateAccountCommand GetUpdateAccountCommand()
        {
            return new UpdateAccountCommand
            {
                Id = 1,
                AccountName = "Test",
            };
        }

        public static ValidateAccountCommand GetValidateAccountCommand()
        {
            return new ValidateAccountCommand
            {
                AccountTypeEntityId = 1,
                BankId = 1,
                CurrencyId = 1
            };
        }
    }
}
