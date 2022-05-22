using Business.Handlers.ChequeAndBonds.Commands;
using Entities.Concrete;

namespace Tests.Helpers.SampleData
{
    public static class ChequeAndBondData
    {
        public static ChequeAndBond GetChequeAndBondDifferentFromUpdateChequeAndBondCommand()
        {
            return new ChequeAndBond
            {
                Id = 2,
                SerialNumber = "Test",
            };
        }

        public static UpdateChequeAndBondCommand GetUpdateChequeAndBondCommand()
        {
            return new UpdateChequeAndBondCommand
            {
                Id = 1,
                SerialNumber = "Test",
            };
        }

        public static ValidateChequeAndBondCommand GetValidateChequeAndBondCommand()
        {
            return new ValidateChequeAndBondCommand
            {
                AccountId = 1,
                CustomerId = 1,
                RecordTypeEntityId = 1,
                BankId = 1
            };
        }
    }
}
