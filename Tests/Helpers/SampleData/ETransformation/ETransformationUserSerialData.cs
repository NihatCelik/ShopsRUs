using Business.Handlers.ETransformationUserSerials.Commands;
using Entities.Concrete;

namespace Tests.Helpers.SampleData
{
    public static class ETransformationUserSerialData
    {
        public static ETransformationUserSerial GetETransformationUserSerialDifferentFromUpdateETransformationUserSerialCommand()
        {
            return new ETransformationUserSerial
            {
                Id = 2,
                Value = "Test",
            };
        }

        public static UpdateETransformationUserSerialCommand GetUpdateETransformationUserSerialCommand()
        {
            return new UpdateETransformationUserSerialCommand
            {
                Id = 1,
                Value = "Test",
            };
        }

        public static ValidateETransformationUserSerialCommand GetValidateETransformationUserSerialCommand()
        {
            return new ValidateETransformationUserSerialCommand
            {
                ETransformationUserSerialTypeEntityId = 1,
                UserId = 1
            };
        }
    }
}
