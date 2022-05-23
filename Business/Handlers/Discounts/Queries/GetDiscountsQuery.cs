using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Discounts.Queries
{
    public class GetDiscountsQuery : IRequest<IDataResult<IEnumerable<Discount>>>
    {
    }

    public class GetDiscountsQueryHandler : IRequestHandler<GetDiscountsQuery, IDataResult<IEnumerable<Discount>>>
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMediator _mediator;

        public GetDiscountsQueryHandler(IDiscountRepository discountRepository, IMediator mediator)
        {
            _discountRepository = discountRepository;
            _mediator = mediator;
        }

        [PerformanceAspect(5)]
        [CacheAspect(10)]
        public async Task<IDataResult<IEnumerable<Discount>>> Handle(GetDiscountsQuery request, CancellationToken cancellationToken)
        {
            return new SuccessDataResult<IEnumerable<Discount>>(await _discountRepository.GetListAsync());
        }
    }
}
