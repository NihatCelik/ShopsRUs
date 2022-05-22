using Business.Handlers.Invoices.Commands;
using FluentValidation;

namespace Business.Handlers.Invoices.ValidationRules
{
    public class CreateInvoiceValidator : AbstractValidator<CreateInvoiceCommand>
    {
        public CreateInvoiceValidator()
        {
            RuleFor(x => x.InvoiceNumber).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.InvoiceDetails).NotEmpty();
        }
    }

    public class UpdateInvoiceValidator : AbstractValidator<UpdateInvoiceCommand>
    {
        public UpdateInvoiceValidator()
        {
            RuleFor(x => x.InvoiceNumber).NotEmpty();
            RuleFor(x => x.SubTotal).NotEmpty();
            RuleFor(x => x.Total).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.InvoiceDetails).NotEmpty();
        }
    }
}