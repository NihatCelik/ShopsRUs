
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.Users.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteUserCommand : IRequest<IResult>
    {
        public int Id { get; set; }

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
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                var userToDelete = _userRepository.Get(p => p.Id == request.Id);

                _userRepository.Delete(userToDelete);
                await _userRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

