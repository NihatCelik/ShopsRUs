using Business.Handlers.Discounts.Commands;
using FluentValidation;

namespace Business.Handlers.Discounts.ValidationRules
{
    public class CreateDiscountValidator : AbstractValidator<CreateDiscountCommand>
    {
        public CreateDiscountValidator()
        {
            RuleFor(x => x.OverYear).NotEmpty();
            RuleFor(x => x.DiscountRate).NotEmpty();
            RuleFor(x => x.UserType).NotEmpty();
        }
    }

    public class UpdateDiscountValidator : AbstractValidator<UpdateDiscountCommand>
    {
        public UpdateDiscountValidator()
        {
            RuleFor(x => x.OverYear).NotEmpty();
            RuleFor(x => x.DiscountRate).NotEmpty();
            RuleFor(x => x.UserType).NotEmpty();
        }
    }
}