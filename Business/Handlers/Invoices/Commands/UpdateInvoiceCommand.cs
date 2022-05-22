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
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Invoices.Commands
{
    public class UpdateInvoiceCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public int UserId { get; set; }
        public StoreType StoreType { get; set; }
        public List<InvoiceDetail> InvoiceDetails { get; set; }
    }

    public class UpdateInvoiceCommandHandler : IRequestHandler<UpdateInvoiceCommand, IResult>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMediator _mediator;

        public UpdateInvoiceCommandHandler(IInvoiceRepository invoiceRepository, IMediator mediator)
        {
            _invoiceRepository = invoiceRepository;
            _mediator = mediator;
        }

        [ValidationAspect(typeof(UpdateInvoiceValidator), Priority = 1)]
        [CacheRemoveAspect("Get")]
        [SecuredOperation(Priority = 1)]
        public async Task<IResult> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var isThereInvoiceRecord = await _invoiceRepository.GetAsync(u => u.Id == request.Id);
            if (isThereInvoiceRecord == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }

            var total = request.InvoiceDetails.Sum(u => u.Total);
            isThereInvoiceRecord.InvoiceNumber = request.InvoiceNumber;
            isThereInvoiceRecord.SubTotal = total;
            isThereInvoiceRecord.DiscountRate = 0;
            isThereInvoiceRecord.DiscountPrice = 0;
            isThereInvoiceRecord.Total = total;
            isThereInvoiceRecord.UserId = request.UserId;
            isThereInvoiceRecord.StoreType = request.StoreType;
            isThereInvoiceRecord.InvoiceDetails = request.InvoiceDetails;

            await _invoiceRepository.UpdateAsync(isThereInvoiceRecord);
            return new SuccessResult(Messages.Updated);
        }
    }
}
