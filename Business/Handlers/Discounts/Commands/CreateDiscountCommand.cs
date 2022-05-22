using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.Discounts.ValidationRules;
using Entities.Enums;

namespace Business.Handlers.Discounts.Commands
{
    public class CreateDiscountCommand : IRequest<IResult>
    {
        public int OverYear { get; set; }
        public int DiscountRate { get; set; }
        public UserType UserType { get; set; }
    }

    public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, IResult>
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMediator _mediator;
        public CreateDiscountCommandHandler(IDiscountRepository discountRepository, IMediator mediator)
        {
            _discountRepository = discountRepository;
            _mediator = mediator;
        }

        [ValidationAspect(typeof(CreateDiscountValidator), Priority = 1)]
        [CacheRemoveAspect("Get")]
        [SecuredOperation(Priority = 1)]
        public async Task<IResult> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var isThereDiscountRecord = _discountRepository.GetQuery().Any(u => u.OverYear == request.OverYear);

            if (isThereDiscountRecord)
            {
                return new ErrorResult(Messages.NameAlreadyExist);
            }

            var addedDiscount = new Discount
            {
                UserType = request.UserType,
                OverYear = request.OverYear,
                DiscountRate = request.DiscountRate,
            };

            await _discountRepository.AddAsync(addedDiscount);
            return new SuccessResult(Messages.Added);
        }
    }
}