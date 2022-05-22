
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.Users.ValidationRules;


namespace Business.Handlers.Users.Commands
{


    public class UpdateUserCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.Collections.Generic.ICollection<Invoice> Invoices { get; set; }

        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, IResult>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;

            public UpdateUserCommandHandler(IUserRepository userRepository, IMediator mediator)
            {
                _userRepository = userRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateUserValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                var isThereUserRecord = await _userRepository.GetAsync(u => u.Id == request.Id);


                isThereUserRecord.FirstName = request.FirstName;
                isThereUserRecord.LastName = request.LastName;
                isThereUserRecord.Email = request.Email;
                isThereUserRecord.PhoneNumber = request.PhoneNumber;
                isThereUserRecord.Address = request.Address;
                isThereUserRecord.CreatedDate = request.CreatedDate;
                isThereUserRecord.Invoices = request.Invoices;


                _userRepository.Update(isThereUserRecord);
                await _userRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

