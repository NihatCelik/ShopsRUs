using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.Invoices.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Invoices.Commands
{
    public class CreateInvoiceCommand : IRequest<IResult>
    {
        public string InvoiceNumber { get; set; }
        public int UserId { get; set; }
        public StoreType StoreType { get; set; }
        public List<InvoiceDetail> InvoiceDetails { get; set; }
    }

    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, IResult>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMediator _mediator;
        public CreateInvoiceCommandHandler(IInvoiceRepository invoiceRepository, IMediator mediator)
        {
            _invoiceRepository = invoiceRepository;
            _mediator = mediator;
        }

        [ValidationAspect(typeof(CreateInvoiceValidator), Priority = 1)]
        [CacheRemoveAspect("Get")]
        [SecuredOperation(Priority = 1)]
        public async Task<IResult> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var isThereInvoiceRecord = await _invoiceRepository.GetQuery().AnyAsync(u => u.InvoiceNumber == request.InvoiceNumber);

            if (isThereInvoiceRecord)
            {
                return new ErrorResult(Messages.NameAlreadyExist);
            }

            decimal total = request.InvoiceDetails.Sum(u => u.Total);
            var addedInvoice = new Invoice
            {
                InvoiceNumber = request.InvoiceNumber,
                SubTotal = total,
                DiscountRate = 0,
                DiscountPrice = 0,
                Total = total,
                UserId = request.UserId,
                InvoiceDetails = request.InvoiceDetails,
                StoreType = request.StoreType,
            };

            await _invoiceRepository.AddAsync(addedInvoice);
            return new SuccessResult(Messages.Added);
        }
    }
}
