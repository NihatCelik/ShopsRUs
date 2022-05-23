using Business.Constants;
using Business.Handlers.Users.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Enums;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

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
        public UserType UserType { get; set; }
    }

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
        public async Task<IResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var isThereUserRecord = await _userRepository.GetAsync(u => u.Id == request.Id);
            if (isThereUserRecord == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }

            isThereUserRecord.FirstName = request.FirstName;
            isThereUserRecord.LastName = request.LastName;
            isThereUserRecord.Email = request.Email;
            isThereUserRecord.PhoneNumber = request.PhoneNumber;
            isThereUserRecord.Address = request.Address;
            isThereUserRecord.UserType = request.UserType;

            await _userRepository.UpdateAsync(isThereUserRecord);
            return new SuccessResult(Messages.Updated);
        }
    }
}