using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Invoices.Queries
{
    public class GetInvoiceByInvoiceNumberQuery : IRequest<IDataResult<Invoice>>
    {
        public string InvoiceNumber { get; set; }
    }

    public class GetInvoiceByInvoiceNumberQueryHandler : IRequestHandler<GetInvoiceByInvoiceNumberQuery, IDataResult<Invoice>>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMediator _mediator;

        public GetInvoiceByInvoiceNumberQueryHandler(IInvoiceRepository invoiceRepository, IMediator mediator)
        {
            _invoiceRepository = invoiceRepository;
            _mediator = mediator;
        }

        public async Task<IDataResult<Invoice>> Handle(GetInvoiceByInvoiceNumberQuery request, CancellationToken cancellationToken)
        {
            var invoice = await _invoiceRepository.GetAsync(p => p.InvoiceNumber == request.InvoiceNumber);
            return new SuccessDataResult<Invoice>(invoice);
        }
    }
}
