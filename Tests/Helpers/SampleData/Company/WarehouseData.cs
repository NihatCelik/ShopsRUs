namespace Tests.Helpers.SampleData
{
    public static class WarehouseData
    {
        public static Warehouse GetWarehouseDifferentFromUpdateWarehouseCommand()
        {
            return new Warehouse
            {
                Id = 2,
                IsActive = true,
                Name = "Test"
            };
        }

        public static UpdateWarehouseCommand GetUpdateWarehouseCommand()
        {
            return new UpdateWarehouseCommand
            {
                Id = 1,
                IsActive = true,
                Name = "Test"
            };
        }
    }
}
