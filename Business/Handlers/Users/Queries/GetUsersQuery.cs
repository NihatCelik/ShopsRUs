using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Users.Queries
{
    public class GetUsersQuery : IRequest<IDataResult<IEnumerable<User>>>
    {

    }

    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IDataResult<IEnumerable<User>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;

        public GetUsersQueryHandler(IUserRepository userRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }

        [PerformanceAspect(5)]
        [CacheAspect(10)]
        public async Task<IDataResult<IEnumerable<User>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return new SuccessDataResult<IEnumerable<User>>(await _userRepository.GetListAsync());
        }
    }
}
