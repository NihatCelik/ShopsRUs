using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Enums;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Discounts.Queries
{
    public class GetDiscountByUserTypeQuery : IRequest<IDataResult<Discount>>
    {
        public UserType UserType { get; set; }
    }

    public class GetDiscountByUserTypeQueryHandler : IRequestHandler<GetDiscountByUserTypeQuery, IDataResult<Discount>>
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMediator _mediator;

        public GetDiscountByUserTypeQueryHandler(IDiscountRepository discountRepository, IMediator mediator)
        {
            _discountRepository = discountRepository;
            _mediator = mediator;
        }

        public async Task<IDataResult<Discount>> Handle(GetDiscountByUserTypeQuery request, CancellationToken cancellationToken)
        {
            var discount = await _discountRepository.GetAsync(p => p.UserType == request.UserType);
            return new SuccessDataResult<Discount>(discount);
        }
    }
}
