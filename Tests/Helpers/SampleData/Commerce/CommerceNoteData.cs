using Business.Handlers.CommerceNotes.Commands;
using Entities.Concrete;

namespace Tests.Helpers.SampleData
{
    public static class CommerceNoteData
    {
        public static CommerceNote GetCommerceNoteDifferentFromUpdateCommerceNoteCommand()
        {
            return new CommerceNote
            {
                Id = 2,
                Note = "Test"
            };
        }

        public static UpdateCommerceNoteCommand GetUpdateCommerceNoteCommand()
        {
            return new UpdateCommerceNoteCommand
            {
                Id = 1,
                Note = "Test"
            };
        }

        public static ValidateCommerceNoteCommand GetValidateCommerceNoteCommand()
        {
            return new ValidateCommerceNoteCommand
            {
                CommerceId = 1
            };
        }
    }
}
