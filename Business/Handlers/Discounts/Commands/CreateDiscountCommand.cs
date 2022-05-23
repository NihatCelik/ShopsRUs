using Business.Constants;
using Business.Handlers.Discounts.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

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
        public async Task<IResult> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var isThereDiscountRecord = await _discountRepository.GetQuery().AnyAsync(u => u.DiscountRate == request.DiscountRate && u.UserType == request.UserType);

            if (isThereDiscountRecord)
            {
                return new ErrorResult(Messages.DiscountAlreadyExist);
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