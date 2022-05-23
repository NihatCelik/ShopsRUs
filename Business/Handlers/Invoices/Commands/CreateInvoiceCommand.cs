using Business.Constants;
using Business.Handlers.Discounts.Queries;
using Business.Handlers.Invoices.ValidationRules;
using Business.Handlers.Users.Queries;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Invoices.Commands
{
    public class CreateInvoiceCommand : IRequest<IDataResult<Invoice>>
    {
        public string InvoiceNumber { get; set; }
        public int UserId { get; set; }
        public StoreType StoreType { get; set; }
        public List<InvoiceDetail> InvoiceDetails { get; set; }
    }

    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, IDataResult<Invoice>>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMediator _mediator;
        public CreateInvoiceCommandHandler(IInvoiceRepository invoiceRepository, IMediator mediator)
        {
            _invoiceRepository = invoiceRepository;
            _mediator = mediator;
        }

        [ValidationAspect(typeof(CreateInvoiceValidator), Priority = 1)]
        [CacheRemoveAspect("Get")]
        public async Task<IDataResult<Invoice>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var isThereInvoiceRecord = await _invoiceRepository.GetQuery().AnyAsync(u => u.InvoiceNumber == request.InvoiceNumber);

            if (isThereInvoiceRecord)
            {
                return new ErrorDataResult<Invoice>(Messages.InvoiceAlreadyExist);
            }

            decimal total = request.InvoiceDetails.Sum(u => u.Total);
            var addedInvoice = new Invoice
            {
                InvoiceNumber = request.InvoiceNumber,
                SubTotal = total,
                DiscountRate = 0,
                DiscountPrice = 0,
                Total = total,
                UserId = request.UserId,
                InvoiceDetails = request.InvoiceDetails,
                StoreType = request.StoreType,
            };

            if (request.StoreType == StoreType.Store)
            {
                var user = (await _mediator.Send(new GetUserQuery { Id = request.UserId })).Data;
                if (user == null)
                {
                    return new ErrorDataResult<Invoice>(Messages.UserNotFound);
                }

                var discount = (await _mediator.Send(new GetDiscountByUserTypeQuery { UserType = user.UserType })).Data;
                if (discount != null)
                {
                    ApplyDiscount(addedInvoice, user, discount);
                }
            }

            addedInvoice.DiscountPrice = (Convert.ToInt32(addedInvoice.SubTotal) / 100) * 5;
            addedInvoice.Total -= addedInvoice.DiscountPrice;

            await _invoiceRepository.AddAsync(addedInvoice);
            return new SuccessDataResult<Invoice>(addedInvoice);
        }

        private void ApplyDiscount(Invoice invoice, User user, Discount discount)
        {
            int userRegisterYear = Convert.ToInt32(DateTime.Now.Subtract(user.CreatedDate).TotalDays) / 365;
            if (userRegisterYear >= discount.OverYear)
            {
                invoice.DiscountRate = discount.DiscountRate;
                var discountValue = invoice.SubTotal * (discount.DiscountRate / 100m);
                invoice.Total -= discountValue;
            }
        }
    }
}
