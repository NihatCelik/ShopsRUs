
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.Users.ValidationRules;

namespace Business.Handlers.Users.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateUserCommand : IRequest<IResult>
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.Collections.Generic.ICollection<Invoice> Invoices { get; set; }


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
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var isThereUserRecord = _userRepository.Query().Any(u => u.FirstName == request.FirstName);

                if (isThereUserRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedUser = new User
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    Address = request.Address,
                    CreatedDate = request.CreatedDate,
                    Invoices = request.Invoices,

                };

                _userRepository.Add(addedUser);
                await _userRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}