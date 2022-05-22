
using Business.Handlers.Users.Commands;
using FluentValidation;

namespace Business.Handlers.Users.ValidationRules
{

    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.CreatedDate).NotEmpty();
            RuleFor(x => x.Invoices).NotEmpty();

        }
    }
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.CreatedDate).NotEmpty();
            RuleFor(x => x.Invoices).NotEmpty();

        }
    }
}