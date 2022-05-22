namespace Tests.Helpers.SampleData
{
    public static class EmployeeAccountData
    {
        public static EmployeeAccount GetEmployeeAccountDifferentFromUpdateEmployeeAccountCommand()
        {
            return new EmployeeAccount
            {
                Id = 2,
                DocumentNumber = "Test",
            };
        }

        public static UpdateEmployeeAccountCommand GetUpdateEmployeeAccountCommand()
        {
            return new UpdateEmployeeAccountCommand
            {
                Id = 1,
                DocumentNumber = "Test",
            };
        }

        public static ValidateEmployeeAccountCommand GetValidateEmployeeAccountCommand()
        {
            return new ValidateEmployeeAccountCommand
            {
                AccountId = 1,
                EmployeeAccountEntityId = 1,
                EmployeeId = 1,
                EnvironmentEntityId = 2
            };
        }
    }
}
