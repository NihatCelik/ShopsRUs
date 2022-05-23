using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Users.Commands
{
    public class DeleteUserCommand : IRequest<IResult>
    {
        public int Id { get; set; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, IResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;

        public DeleteUserCommandHandler(IUserRepository userRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }

        [CacheRemoveAspect("Get")]
        public async Task<IResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userToDelete = await _userRepository.GetAsync(p => p.Id == request.Id);
            if (userToDelete == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }

            await _userRepository.DeleteAsync(userToDelete);
            return new SuccessResult(Messages.Deleted);
        }
    }
}
