using Business.Handlers.Drivers.Commands;
using Entities.Concrete;

namespace Tests.Helpers.SampleData
{
    public static class DriverData
    {
        public static Driver GetDriverDifferentFromUpdateDriverCommand()
        {
            return new Driver
            {
                Id = 2,
                IsActive = true,
                Name = "Test",
                Surname = "Test"
            };
        }

        public static UpdateDriverCommand GetUpdateDriverCommand()
        {
            return new UpdateDriverCommand
            {
                Id = 1,
                IsActive = true,
                Name = "Test",
                Surname = "Test"
            };
        }
    }
}
