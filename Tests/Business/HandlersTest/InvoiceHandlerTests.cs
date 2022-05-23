using Business.Handlers.Discounts.Queries;
using Business.Handlers.Invoices.Commands;
using Business.Handlers.Invoices.Queries;
using Business.Handlers.Users.Queries;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Enums;
using FluentAssertions;
using MediatR;
using MockQueryable.Moq;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class InvoiceHandlerTests
    {
        Mock<IInvoiceRepository> _invoiceRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _invoiceRepository = new Mock<IInvoiceRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Invoice_GetQuery_Success()
        {
            //Arrange
            var query = new GetInvoiceQuery();

            var rt = new Invoice
            {
                Id = 1,
                DiscountPrice = 0,
                DiscountRate = 10,
                InvoiceDetails = new List<InvoiceDetail> { new InvoiceDetail() },
                InvoiceNumber = "001",
                StoreType = StoreType.Grocer,
                SubTotal = 10,
                Total = 9,
                UserId = 1
            };

            _invoiceRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Invoice, bool>>>())).ReturnsAsync(rt);

            var handler = new GetInvoiceQueryHandler(_invoiceRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            x.Data.Id.Should().Be(1);
            x.Data.DiscountPrice.Should().Be(0);
            x.Data.DiscountRate.Should().Be(10);
            x.Data.InvoiceDetails.Should().NotBeEmpty();
            x.Data.InvoiceNumber.Should().Be("001");
            x.Data.Total.Should().Be(9);
            x.Data.SubTotal.Should().Be(10);
        }

        [Test]
        public async Task Invoice_GetQueries_Success()
        {
            //Arrange
            var query = new GetInvoicesQuery();

            _invoiceRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Invoice, bool>>>()))
                        .ReturnsAsync(new List<Invoice> { new Invoice(), new Invoice() });

            var handler = new GetInvoicesQueryHandler(_invoiceRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Invoice>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Invoice_CreateCommand_Grocer_ForEmployee_Success()
        {
            //Arrange
            List<Invoice> rt = new List<Invoice>();

            var invoiceDetails = new List<InvoiceDetail>();
            invoiceDetails.Add(new InvoiceDetail { Id = 1, InvoiceId = 1, ProductId = 1, ProductName = "Apple", Quantity = 10, Total = 10, UnitPrice = 1 });
            invoiceDetails.Add(new InvoiceDetail { Id = 2, InvoiceId = 1, ProductId = 2, ProductName = "Banana", Quantity = 5, Total = 10, UnitPrice = 2 });
            var command = new CreateInvoiceCommand
            {
                InvoiceDetails = invoiceDetails,
                InvoiceNumber = "001",
                StoreType = StoreType.Grocer,
                UserId = 1
            };

            _invoiceRepository.Setup(x => x.GetQuery(It.IsAny<Expression<Func<Invoice, bool>>>())).Returns(rt.AsQueryable().BuildMockDbSet().Object);
            _mediator.Setup(x => x.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SuccessDataResult<User>(new User
                {
                    UserType = UserType.Employee
                }));

            _mediator.Setup(x => x.Send(It.IsAny<GetDiscountByUserTypeQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SuccessDataResult<Discount>(new Discount
                {
                    DiscountRate = 30,
                    OverYear = 0,
                    UserType = UserType.Employee
                }));

            _invoiceRepository.Setup(x => x.AddAsync(It.IsAny<Invoice>())).ReturnsAsync(new Invoice());

            var handler = new CreateInvoiceCommandHandler(_invoiceRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _invoiceRepository.Verify(x => x.GetQuery(It.IsAny<Expression<Func<Invoice, bool>>>()), Times.Once);
            _mediator.Verify(x => x.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()), Times.Never);
            _mediator.Verify(x => x.Send(It.IsAny<GetDiscountByUserTypeQuery>(), It.IsAny<CancellationToken>()), Times.Never);
            _invoiceRepository.Verify(x => x.AddAsync(It.IsAny<Invoice>()), Times.Once);

            x.Success.Should().BeTrue();
            x.Data.SubTotal.Should().Be(20);
            x.Data.Total.Should().Be(20);
            x.Data.DiscountPrice.Should().Be(0);
            x.Data.DiscountRate.Should().Be(0);
        }

        [Test]
        public async Task Invoice_CreateCommand_Grocer_ForAffiliate_Success()
        {
            //Arrange
            List<Invoice> rt = new List<Invoice>();

            var invoiceDetails = new List<InvoiceDetail>();
            invoiceDetails.Add(new InvoiceDetail { Id = 1, InvoiceId = 1, ProductId = 1, ProductName = "Apple", Quantity = 10, Total = 10, UnitPrice = 1 });
            invoiceDetails.Add(new InvoiceDetail { Id = 2, InvoiceId = 1, ProductId = 2, ProductName = "Banana", Quantity = 5, Total = 10, UnitPrice = 2 });
            var command = new CreateInvoiceCommand
            {
                InvoiceDetails = invoiceDetails,
                InvoiceNumber = "001",
                StoreType = StoreType.Grocer,
                UserId = 1
            };

            _invoiceRepository.Setup(x => x.GetQuery(It.IsAny<Expression<Func<Invoice, bool>>>())).Returns(rt.AsQueryable().BuildMockDbSet().Object);
            _mediator.Setup(x => x.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SuccessDataResult<User>(new User
                {
                    UserType = UserType.Affiliate
                }));

            _mediator.Setup(x => x.Send(It.IsAny<GetDiscountByUserTypeQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SuccessDataResult<Discount>(new Discount
                {
                    DiscountRate = 10,
                    OverYear = 0,
                    UserType = UserType.Affiliate
                }));

            _invoiceRepository.Setup(x => x.AddAsync(It.IsAny<Invoice>())).ReturnsAsync(new Invoice());

            var handler = new CreateInvoiceCommandHandler(_invoiceRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _invoiceRepository.Verify(x => x.GetQuery(It.IsAny<Expression<Func<Invoice, bool>>>()), Times.Once);
            _mediator.Verify(x => x.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()), Times.Never);
            _mediator.Verify(x => x.Send(It.IsAny<GetDiscountByUserTypeQuery>(), It.IsAny<CancellationToken>()), Times.Never);
            _invoiceRepository.Verify(x => x.AddAsync(It.IsAny<Invoice>()), Times.Once);

            x.Success.Should().BeTrue();
            x.Data.SubTotal.Should().Be(20);
            x.Data.Total.Should().Be(20);
            x.Data.DiscountPrice.Should().Be(0);
            x.Data.DiscountRate.Should().Be(0);
        }

        [Test]
        public async Task Invoice_CreateCommand_Grocer_ForCustomer_Success()
        {
            //Arrange
            List<Invoice> rt = new List<Invoice>();

            var invoiceDetails = new List<InvoiceDetail>();
            invoiceDetails.Add(new InvoiceDetail { Id = 1, InvoiceId = 1, ProductId = 1, ProductName = "Apple", Quantity = 10, Total = 10, UnitPrice = 1 });
            invoiceDetails.Add(new InvoiceDetail { Id = 2, InvoiceId = 1, ProductId = 2, ProductName = "Banana", Quantity = 5, Total = 10, UnitPrice = 2 });
            var command = new CreateInvoiceCommand
            {
                InvoiceDetails = invoiceDetails,
                InvoiceNumber = "001",
                StoreType = StoreType.Grocer,
                UserId = 1
            };

            _invoiceRepository.Setup(x => x.GetQuery(It.IsAny<Expression<Func<Invoice, bool>>>())).Returns(rt.AsQueryable().BuildMockDbSet().Object);
            _mediator.Setup(x => x.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SuccessDataResult<User>(new User
                {
                    UserType = UserType.Customer
                }));

            _mediator.Setup(x => x.Send(It.IsAny<GetDiscountByUserTypeQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SuccessDataResult<Discount>(new Discount
                {
                    DiscountRate = 5,
                    OverYear = 2,
                    UserType = UserType.Customer
                }));

            _invoiceRepository.Setup(x => x.AddAsync(It.IsAny<Invoice>())).ReturnsAsync(new Invoice());

            var handler = new CreateInvoiceCommandHandler(_invoiceRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _invoiceRepository.Verify(x => x.GetQuery(It.IsAny<Expression<Func<Invoice, bool>>>()), Times.Once);
            _mediator.Verify(x => x.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()), Times.Never);
            _mediator.Verify(x => x.Send(It.IsAny<GetDiscountByUserTypeQuery>(), It.IsAny<CancellationToken>()), Times.Never);
            _invoiceRepository.Verify(x => x.AddAsync(It.IsAny<Invoice>()), Times.Once);

            x.Success.Should().BeTrue();
            x.Data.SubTotal.Should().Be(20);
            x.Data.Total.Should().Be(20);
            x.Data.DiscountPrice.Should().Be(0);
            x.Data.DiscountRate.Should().Be(0);
        }

        [Test]
        public async Task Invoice_CreateCommand_Grocer_ForCashDiscount_Success()
        {
            //Arrange
            List<Invoice> rt = new List<Invoice>();

            var invoiceDetails = new List<InvoiceDetail>();
            invoiceDetails.Add(new InvoiceDetail { Id = 1, InvoiceId = 1, ProductId = 1, ProductName = "Tomato", Quantity = 20, Total = 100, UnitPrice = 5 });
            invoiceDetails.Add(new InvoiceDetail { Id = 2, InvoiceId = 1, ProductId = 2, ProductName = "Patato", Quantity = 20, Total = 80, UnitPrice = 4 });
            var command = new CreateInvoiceCommand
            {
                InvoiceDetails = invoiceDetails,
                InvoiceNumber = "001",
                StoreType = StoreType.Grocer,
                UserId = 1
            };

            _invoiceRepository.Setup(x => x.GetQuery(It.IsAny<Expression<Func<Invoice, bool>>>())).Returns(rt.AsQueryable().BuildMockDbSet().Object);
            _mediator.Setup(x => x.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SuccessDataResult<User>(new User
                {
                    UserType = UserType.Customer
                }));

            _mediator.Setup(x => x.Send(It.IsAny<GetDiscountByUserTypeQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SuccessDataResult<Discount>(new Discount
                {
                    DiscountRate = 5,
                    OverYear = 2,
                    UserType = UserType.Customer
                }));

            _invoiceRepository.Setup(x => x.AddAsync(It.IsAny<Invoice>())).ReturnsAsync(new Invoice());

            var handler = new CreateInvoiceCommandHandler(_invoiceRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _invoiceRepository.Verify(x => x.GetQuery(It.IsAny<Expression<Func<Invoice, bool>>>()), Times.Once);
            _mediator.Verify(x => x.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()), Times.Never);
            _mediator.Verify(x => x.Send(It.IsAny<GetDiscountByUserTypeQuery>(), It.IsAny<CancellationToken>()), Times.Never);
            _invoiceRepository.Verify(x => x.AddAsync(It.IsAny<Invoice>()), Times.Once);

            x.Success.Should().BeTrue();
            x.Data.SubTotal.Should().Be(180);
            x.Data.Total.Should().Be(175);
            x.Data.DiscountPrice.Should().Be(5);
            x.Data.DiscountRate.Should().Be(0);
        }

        [Test]
        public async Task Invoice_CreateCommand_Store_ForEmployee_Success()
        {
            //Arrange
            List<Invoice> rt = new List<Invoice>();

            var invoiceDetails = new List<InvoiceDetail>();
            invoiceDetails.Add(new InvoiceDetail { Id = 1, InvoiceId = 1, ProductId = 1, ProductName = "Tomato", Quantity = 20, Total = 100, UnitPrice = 5 });
            invoiceDetails.Add(new InvoiceDetail { Id = 2, InvoiceId = 1, ProductId = 2, ProductName = "Banana", Quantity = 20, Total = 80, UnitPrice = 4 });
            var command = new CreateInvoiceCommand
            {
                InvoiceDetails = invoiceDetails,
                InvoiceNumber = "001",
                StoreType = StoreType.Store,
                UserId = 1
            };

            _invoiceRepository.Setup(x => x.GetQuery(It.IsAny<Expression<Func<Invoice, bool>>>())).Returns(rt.AsQueryable().BuildMockDbSet().Object);
            _mediator.Setup(x => x.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SuccessDataResult<User>(new User
                {
                    UserType = UserType.Employee
                }));

            _mediator.Setup(x => x.Send(It.IsAny<GetDiscountByUserTypeQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SuccessDataResult<Discount>(new Discount
                {
                    DiscountRate = 30,
                    OverYear = 0,
                    UserType = UserType.Employee
                }));

            _invoiceRepository.Setup(x => x.AddAsync(It.IsAny<Invoice>())).ReturnsAsync(new Invoice());

            var handler = new CreateInvoiceCommandHandler(_invoiceRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _invoiceRepository.Verify(x => x.GetQuery(It.IsAny<Expression<Func<Invoice, bool>>>()), Times.Once);
            _mediator.Verify(x => x.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            _mediator.Verify(x => x.Send(It.IsAny<GetDiscountByUserTypeQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            _invoiceRepository.Verify(x => x.AddAsync(It.IsAny<Invoice>()), Times.Once);

            x.Success.Should().BeTrue();
            x.Data.SubTotal.Should().Be(180);
            x.Data.Total.Should().Be(121);
            x.Data.DiscountPrice.Should().Be(5);
            x.Data.DiscountRate.Should().Be(30);
        }

        [Test]
        public async Task Invoice_CreateCommand_Store_ForAffiliate_Success()
        {
            //Arrange
            List<Invoice> rt = new List<Invoice>();

            var invoiceDetails = new List<InvoiceDetail>();
            invoiceDetails.Add(new InvoiceDetail { Id = 1, InvoiceId = 1, ProductId = 1, ProductName = "Tomato", Quantity = 20, Total = 100, UnitPrice = 5 });
            invoiceDetails.Add(new InvoiceDetail { Id = 2, InvoiceId = 1, ProductId = 2, ProductName = "Banana", Quantity = 20, Total = 80, UnitPrice = 4 });
            var command = new CreateInvoiceCommand
            {
                InvoiceDetails = invoiceDetails,
                InvoiceNumber = "001",
                StoreType = StoreType.Store,
                UserId = 1
            };

            _invoiceRepository.Setup(x => x.GetQuery(It.IsAny<Expression<Func<Invoice, bool>>>())).Returns(rt.AsQueryable().BuildMockDbSet().Object);
            _mediator.Setup(x => x.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SuccessDataResult<User>(new User
                {
                    UserType = UserType.Affiliate
                }));

            _mediator.Setup(x => x.Send(It.IsAny<GetDiscountByUserTypeQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SuccessDataResult<Discount>(new Discount
                {
                    DiscountRate = 10,
                    OverYear = 0,
                    UserType = UserType.Affiliate
                }));

            _invoiceRepository.Setup(x => x.AddAsync(It.IsAny<Invoice>())).ReturnsAsync(new Invoice());

            var handler = new CreateInvoiceCommandHandler(_invoiceRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _invoiceRepository.Verify(x => x.GetQuery(It.IsAny<Expression<Func<Invoice, bool>>>()), Times.Once);
            _mediator.Verify(x => x.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            _mediator.Verify(x => x.Send(It.IsAny<GetDiscountByUserTypeQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            _invoiceRepository.Verify(x => x.AddAsync(It.IsAny<Invoice>()), Times.Once);

            x.Success.Should().BeTrue();
            x.Data.SubTotal.Should().Be(180);
            x.Data.Total.Should().Be(157);
            x.Data.DiscountPrice.Should().Be(5);
            x.Data.DiscountRate.Should().Be(10);
        }

        [Test]
        public async Task Invoice_CreateCommand_Store_ForCustomer_Success()
        {
            //Arrange
            List<Invoice> rt = new List<Invoice>();

            var invoiceDetails = new List<InvoiceDetail>();
            invoiceDetails.Add(new InvoiceDetail { Id = 1, InvoiceId = 1, ProductId = 1, ProductName = "Tomato", Quantity = 20, Total = 100, UnitPrice = 5 });
            invoiceDetails.Add(new InvoiceDetail { Id = 2, InvoiceId = 1, ProductId = 2, ProductName = "Banana", Quantity = 20, Total = 80, UnitPrice = 4 });
            var command = new CreateInvoiceCommand
            {
                InvoiceDetails = invoiceDetails,
                InvoiceNumber = "001",
                StoreType = StoreType.Store,
                UserId = 1
            };

            _invoiceRepository.Setup(x => x.GetQuery(It.IsAny<Expression<Func<Invoice, bool>>>())).Returns(rt.AsQueryable().BuildMockDbSet().Object);
            _mediator.Setup(x => x.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SuccessDataResult<User>(new User
                {
                    UserType = UserType.Customer
                }));

            _mediator.Setup(x => x.Send(It.IsAny<GetDiscountByUserTypeQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SuccessDataResult<Discount>(new Discount
                {
                    DiscountRate = 5,
                    OverYear = 2,
                    UserType = UserType.Customer
                }));

            _invoiceRepository.Setup(x => x.AddAsync(It.IsAny<Invoice>())).ReturnsAsync(new Invoice());

            var handler = new CreateInvoiceCommandHandler(_invoiceRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _invoiceRepository.Verify(x => x.GetQuery(It.IsAny<Expression<Func<Invoice, bool>>>()), Times.Once);
            _mediator.Verify(x => x.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            _mediator.Verify(x => x.Send(It.IsAny<GetDiscountByUserTypeQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            _invoiceRepository.Verify(x => x.AddAsync(It.IsAny<Invoice>()), Times.Once);

            x.Success.Should().BeTrue();
            x.Data.SubTotal.Should().Be(180);
            x.Data.Total.Should().Be(175);
            x.Data.DiscountPrice.Should().Be(5);
            x.Data.DiscountRate.Should().Be(0);
        }

        [Test]
        public async Task Invoice_CreateCommand_Store_ForCustomer_Over_Year_Success()
        {
            //Arrange
            List<Invoice> rt = new List<Invoice>();

            var invoiceDetails = new List<InvoiceDetail>();
            invoiceDetails.Add(new InvoiceDetail { Id = 1, InvoiceId = 1, ProductId = 1, ProductName = "Tomato", Quantity = 20, Total = 100, UnitPrice = 5 });
            invoiceDetails.Add(new InvoiceDetail { Id = 2, InvoiceId = 1, ProductId = 2, ProductName = "Banana", Quantity = 20, Total = 80, UnitPrice = 4 });
            var command = new CreateInvoiceCommand
            {
                InvoiceDetails = invoiceDetails,
                InvoiceNumber = "001",
                StoreType = StoreType.Store,
                UserId = 1
            };

            _invoiceRepository.Setup(x => x.GetQuery(It.IsAny<Expression<Func<Invoice, bool>>>())).Returns(rt.AsQueryable().BuildMockDbSet().Object);
            _mediator.Setup(x => x.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SuccessDataResult<User>(new User
                {
                    CreatedDate = new DateTime(2010, 1, 1),
                    UserType = UserType.Customer
                }));

            _mediator.Setup(x => x.Send(It.IsAny<GetDiscountByUserTypeQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SuccessDataResult<Discount>(new Discount
                {
                    DiscountRate = 5,
                    OverYear = 2,
                    UserType = UserType.Customer
                }));

            _invoiceRepository.Setup(x => x.AddAsync(It.IsAny<Invoice>())).ReturnsAsync(new Invoice());

            var handler = new CreateInvoiceCommandHandler(_invoiceRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _invoiceRepository.Verify(x => x.GetQuery(It.IsAny<Expression<Func<Invoice, bool>>>()), Times.Once);
            _mediator.Verify(x => x.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            _mediator.Verify(x => x.Send(It.IsAny<GetDiscountByUserTypeQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            _invoiceRepository.Verify(x => x.AddAsync(It.IsAny<Invoice>()), Times.Once);

            x.Success.Should().BeTrue();
            x.Data.SubTotal.Should().Be(180);
            x.Data.Total.Should().Be(166);
            x.Data.DiscountPrice.Should().Be(5);
            x.Data.DiscountRate.Should().Be(5);
        }
    }
}
