using Business.Handlers.Parties.Commands;
using Entities.Concrete;

namespace Tests.Helpers.SampleData
{
    public static class PartyData
    {
        public static Party GetPartyDifferentFromUpdatePartyCommand()
        {
            return new Party
            {
                Id = 2,
                PartyName = "Test",
            };
        }

        public static UpdatePartyCommand GetUpdatePartyCommand()
        {
            return new UpdatePartyCommand
            {
                Id = 1,
                PartyName = "Test",
            };
        }

        public static ValidatePartyCommand GetValidatePartyCommand()
        {
            return new ValidatePartyCommand
            {
                CountryId = 1,
                CityId = 1,
                DistrictId = 1
            };
        }
    }
}
