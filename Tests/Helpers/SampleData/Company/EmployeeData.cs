namespace Tests.Helpers.SampleData
{
    public static class EmployeeData
    {
        public static Employee GetEmployeeDifferentFromUpdateEmployeeCommand()
        {
            return new Employee
            {
                Id = 2,
                FullName = "Test",
            };
        }

        public static UpdateEmployeeCommand GetUpdateEmployeeCommand()
        {
            return new UpdateEmployeeCommand
            {
                Id = 1,
                FullName = "Test",
            };
        }

        public static ValidateEmployeeCommand GetValidateEmployeeCommand()
        {
            return new ValidateEmployeeCommand
            {
                ImageFileId = 1,
                CurrencyId = 1,
            };
        }
    }
}
