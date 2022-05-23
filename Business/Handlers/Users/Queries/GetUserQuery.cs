using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Users.Queries
{
    public class GetUserQuery : IRequest<IDataResult<User>>
    {
        public int Id { get; set; }
    }

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, IDataResult<User>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;

        public GetUserQueryHandler(IUserRepository userRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }

        public async Task<IDataResult<User>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(p => p.Id == request.Id);
            return new SuccessDataResult<User>(user);
        }
    }
}
