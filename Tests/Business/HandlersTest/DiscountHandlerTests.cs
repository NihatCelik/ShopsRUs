using Business.Constants;
using Business.Handlers.Discounts.Commands;
using Business.Handlers.Discounts.Queries;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentAssertions;
using MediatR;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class DiscountHandlerTests
    {
        Mock<IDiscountRepository> _discountRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _discountRepository = new Mock<IDiscountRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Discount_GetQuery_Success()
        {
            //Arrange
            var query = new GetDiscountQuery();

            _discountRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Discount, bool>>>())).ReturnsAsync(new Discount()
//propertyler buraya yazılacak
//{																		
//DiscountId = 1,
//DiscountName = "Test"
//}
);

            var handler = new GetDiscountQueryHandler(_discountRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.DiscountId.Should().Be(1);

        }

        [Test]
        public async Task Discount_GetQueries_Success()
        {
            //Arrange
            var query = new GetDiscountsQuery();

            _discountRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Discount, bool>>>()))
                        .ReturnsAsync(new List<Discount> { new Discount() { /*TODO:propertyler buraya yazılacak DiscountId = 1, DiscountName = "test"*/ } });

            var handler = new GetDiscountsQueryHandler(_discountRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Discount>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Discount_CreateCommand_Success()
        {
            Discount rt = null;
            //Arrange
            var command = new CreateDiscountCommand();
            //propertyler buraya yazılacak
            //command.DiscountName = "deneme";

            _discountRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Discount, bool>>>()))
                        .ReturnsAsync(rt);

            _discountRepository.Setup(x => x.Add(It.IsAny<Discount>())).Returns(new Discount());

            var handler = new CreateDiscountCommandHandler(_discountRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _discountRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Discount_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateDiscountCommand();
            //propertyler buraya yazılacak 
            //command.DiscountName = "test";

            _discountRepository.Setup(x => x.Query())
                                           .Returns(new List<Discount> { new Discount() { /*TODO:propertyler buraya yazılacak DiscountId = 1, DiscountName = "test"*/ } }.AsQueryable());

            _discountRepository.Setup(x => x.Add(It.IsAny<Discount>())).Returns(new Discount());

            var handler = new CreateDiscountCommandHandler(_discountRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Discount_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateDiscountCommand();
            //command.DiscountName = "test";

            _discountRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Discount, bool>>>()))
                        .ReturnsAsync(new Discount() { /*TODO:propertyler buraya yazılacak DiscountId = 1, DiscountName = "deneme"*/ });

            _discountRepository.Setup(x => x.Update(It.IsAny<Discount>())).Returns(new Discount());

            var handler = new UpdateDiscountCommandHandler(_discountRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _discountRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Discount_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteDiscountCommand();

            _discountRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Discount, bool>>>()))
                        .ReturnsAsync(new Discount() { /*TODO:propertyler buraya yazılacak DiscountId = 1, DiscountName = "deneme"*/});

            _discountRepository.Setup(x => x.Delete(It.IsAny<Discount>()));

            var handler = new DeleteDiscountCommandHandler(_discountRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _discountRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

