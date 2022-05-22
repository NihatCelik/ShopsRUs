using Business.Handlers.Invoices.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Entities.Concrete;
using Business.Handlers.Invoices.Commands;
using Business.Constants;
using MediatR;
using System.Linq;
using FluentAssertions;

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

            _invoiceRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Invoice, bool>>>())).ReturnsAsync(new Invoice()
//propertyler buraya yazılacak
//{																		
//InvoiceId = 1,
//InvoiceName = "Test"
//}
);

            var handler = new GetInvoiceQueryHandler(_invoiceRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.InvoiceId.Should().Be(1);

        }

        [Test]
        public async Task Invoice_GetQueries_Success()
        {
            //Arrange
            var query = new GetInvoicesQuery();

            _invoiceRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Invoice, bool>>>()))
                        .ReturnsAsync(new List<Invoice> { new Invoice() { /*TODO:propertyler buraya yazılacak InvoiceId = 1, InvoiceName = "test"*/ } });

            var handler = new GetInvoicesQueryHandler(_invoiceRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Invoice>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Invoice_CreateCommand_Success()
        {
            Invoice rt = null;
            //Arrange
            var command = new CreateInvoiceCommand();
            //propertyler buraya yazılacak
            //command.InvoiceName = "deneme";

            _invoiceRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Invoice, bool>>>()))
                        .ReturnsAsync(rt);

            _invoiceRepository.Setup(x => x.AddAsync(It.IsAny<Invoice>())).ReturnsAsync(new Invoice());

            var handler = new CreateInvoiceCommandHandler(_invoiceRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _invoiceRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Invoice_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateInvoiceCommand();
            //propertyler buraya yazılacak 
            //command.InvoiceName = "test";

            _invoiceRepository.Setup(x => x.GetQuery())
                                           .Returns(new List<Invoice> { new Invoice() { /*TODO:propertyler buraya yazılacak InvoiceId = 1, InvoiceName = "test"*/ } }.AsQueryable());

            _invoiceRepository.Setup(x => x.Add(It.IsAny<Invoice>())).Returns(new Invoice());

            var handler = new CreateInvoiceCommandHandler(_invoiceRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Invoice_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateInvoiceCommand();
            //command.InvoiceName = "test";

            _invoiceRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Invoice, bool>>>()))
                        .ReturnsAsync(new Invoice() { /*TODO:propertyler buraya yazılacak InvoiceId = 1, InvoiceName = "deneme"*/ });

            _invoiceRepository.Setup(x => x.Update(It.IsAny<Invoice>())).Returns(new Invoice());

            var handler = new UpdateInvoiceCommandHandler(_invoiceRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _invoiceRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Invoice_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteInvoiceCommand();

            _invoiceRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Invoice, bool>>>()))
                        .ReturnsAsync(new Invoice() { /*TODO:propertyler buraya yazılacak InvoiceId = 1, InvoiceName = "deneme"*/});

            _invoiceRepository.Setup(x => x.Delete(It.IsAny<Invoice>()));

            var handler = new DeleteInvoiceCommandHandler(_invoiceRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _invoiceRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

