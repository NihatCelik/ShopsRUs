using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Invoices.Queries
{
    public class GetInvoicesQuery : IRequest<IDataResult<IEnumerable<Invoice>>>
    {
    }

    public class GetInvoicesQueryHandler : IRequestHandler<GetInvoicesQuery, IDataResult<IEnumerable<Invoice>>>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMediator _mediator;

        public GetInvoicesQueryHandler(IInvoiceRepository invoiceRepository, IMediator mediator)
        {
            _invoiceRepository = invoiceRepository;
            _mediator = mediator;
        }

        [PerformanceAspect(5)]
        [CacheAspect(10)]
        public async Task<IDataResult<IEnumerable<Invoice>>> Handle(GetInvoicesQuery request, CancellationToken cancellationToken)
        {
            return new SuccessDataResult<IEnumerable<Invoice>>(await _invoiceRepository.GetListAsync());
        }
    }
}
