using Business.Constants;
using Business.Handlers.Discounts.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Enums;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Discounts.Commands
{
    public class UpdateDiscountCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int OverYear { get; set; }
        public int DiscountRate { get; set; }
        public UserType UserType { get; set; }
    }

    public class UpdateDiscountCommandHandler : IRequestHandler<UpdateDiscountCommand, IResult>
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMediator _mediator;

        public UpdateDiscountCommandHandler(IDiscountRepository discountRepository, IMediator mediator)
        {
            _discountRepository = discountRepository;
            _mediator = mediator;
        }

        [ValidationAspect(typeof(UpdateDiscountValidator), Priority = 1)]
        [CacheRemoveAspect("Get")]
        public async Task<IResult> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
        {
            var isThereDiscountRecord = await _discountRepository.GetAsync(u => u.Id == request.Id);
            if (isThereDiscountRecord == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }

            isThereDiscountRecord.OverYear = request.OverYear;
            isThereDiscountRecord.DiscountRate = request.DiscountRate;
            isThereDiscountRecord.UserType = request.UserType;

            await _discountRepository.UpdateAsync(isThereDiscountRecord);
            return new SuccessResult(Messages.Updated);
        }
    }
}
