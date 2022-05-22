namespace Tests.Helpers.SampleData
{
    public static class WithholdingData
    {
        public static Withholding GetWithholdingDifferentFromUpdateWithholdingCommand()
        {
            return new Withholding
            {
                Id = 2,
                IsActive = true,
                Name = "Test",
                Code = "032",
                Rate = 10
            };
        }

        public static UpdateWithholdingCommand GetUpdateWithholdingCommand()
        {
            return new UpdateWithholdingCommand
            {
                Id = 1,
                IsActive = true,
                Name = "Test",
                Code = "032",
                Rate = 10
            };
        }
    }
}
