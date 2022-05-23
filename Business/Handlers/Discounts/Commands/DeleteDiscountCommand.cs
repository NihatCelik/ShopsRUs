using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Discounts.Commands
{
    public class DeleteDiscountCommand : IRequest<IResult>
    {
        public int Id { get; set; }
    }

    public class DeleteDiscountCommandHandler : IRequestHandler<DeleteDiscountCommand, IResult>
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMediator _mediator;

        public DeleteDiscountCommandHandler(IDiscountRepository discountRepository, IMediator mediator)
        {
            _discountRepository = discountRepository;
            _mediator = mediator;
        }

        [CacheRemoveAspect("Get")]
        public async Task<IResult> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
        {
            var discountToDelete = await _discountRepository.GetAsync(p => p.Id == request.Id);
            if (discountToDelete == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }

            await _discountRepository.DeleteAsync(discountToDelete);
            return new SuccessResult(Messages.Deleted);
        }
    }
}
