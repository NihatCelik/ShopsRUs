namespace Tests.Helpers.SampleData
{
    public static class UnitData
    {
        public static CreateUnitCommand GetCreateUnitCommand()
        {
            return new CreateUnitCommand
            {
                ETransformationUnitCodeId = 1,
                IsActive = true,
                OrderNumber = 1,
                UnitName = "Test"
            };
        }

        public static CreateUnitCommand GetCreateUnitCommandWithoutETransformationUnitCodeId()
        {
            return new CreateUnitCommand
            {
                IsActive = true,
                OrderNumber = 1,
                UnitName = "Test"
            };
        }

        public static Unit GetUnitDifferentFromUpdateUnitCommand()
        {
            return new Unit
            {
                Id = 2,
                ETransformationUnitCodeId = 1,
                IsActive = true,
                OrderNumber = 1,
                UnitName = "Test"
            };
        }

        public static UpdateUnitCommand GetUpdateUnitCommand()
        {
            return new UpdateUnitCommand
            {
                Id = 1,
                ETransformationUnitCodeId = 1,
                IsActive = true,
                OrderNumber = 1,
                UnitName = "Test"
            };
        }

        public static UpdateUnitCommand GetUpdateUnitCommandWithoutETransformationUnitCodeId()
        {
            return new UpdateUnitCommand
            {
                Id = 1,
                IsActive = true,
                OrderNumber = 1,
                UnitName = "Test"
            };
        }
    }
}
