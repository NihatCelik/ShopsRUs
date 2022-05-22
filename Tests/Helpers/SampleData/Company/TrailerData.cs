using Business.Handlers.Trailers.Commands;
using Entities.Concrete;

namespace Tests.Helpers.SampleData
{
    public static class TrailerData
    {
        public static Trailer GetTrailerDifferentFromUpdateTrailerCommand()
        {
            return new Trailer
            {
                Id = 2,
                IsActive = true,
                Plate = "Test"
            };
        }

        public static UpdateTrailerCommand GetUpdateTrailerCommand()
        {
            return new UpdateTrailerCommand
            {
                Id = 1,
                IsActive = true,
                Plate = "Test"
            };
        }
    }
}
