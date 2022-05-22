using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.Users.ValidationRules;
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

namespace Business.Handlers.Users.Commands
{
    public class CreateUserCommand : IRequest<IResult>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public UserType UserType { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, IResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;
        public CreateUserCommandHandler(IUserRepository userRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }

        [ValidationAspect(typeof(CreateUserValidator), Priority = 1)]
        [CacheRemoveAspect("Get")]
        [SecuredOperation(Priority = 1)]
        public async Task<IResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var isThereUserRecord = await _userRepository.GetQuery().AnyAsync(u => u.Email == request.Email);

            if (isThereUserRecord)
            {
                return new ErrorResult(Messages.UserAlreadyExist);
            }

            var addedUser = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserType = request.UserType,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
            };

            await _userRepository.AddAsync(addedUser);
            return new SuccessResult(Messages.Added);
        }
    }
}
