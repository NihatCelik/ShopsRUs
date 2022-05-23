using Business.Constants;
using Business.Handlers.Discounts.Commands;
using Business.Handlers.Discounts.Queries;
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

            _discountRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Discount, bool>>>())).ReturnsAsync(new Discount { Id = 1, UserType = UserType.Employee, DiscountRate = 30, OverYear = 0 });

            var handler = new GetDiscountQueryHandler(_discountRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            x.Data.Id.Should().Be(1);
            x.Data.UserType.Should().Be(UserType.Employee);
            x.Data.DiscountRate.Should().Be(30);
            x.Data.OverYear.Should().Be(0);
        }

        [Test]
        public async Task Discount_GetQueries_Success()
        {
            //Arrange
            var query = new GetDiscountsQuery();

            _discountRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Discount, bool>>>()))
                .ReturnsAsync(new List<Discount> { new Discount(), new Discount() });

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
            //Arrange
            List<Discount> rt = new List<Discount>();
            var command = new CreateDiscountCommand
            {
                DiscountRate = 10,
                OverYear = 0,
                UserType = UserType.Affiliate
            };

            _discountRepository.Setup(x => x.GetQuery(It.IsAny<Expression<Func<Discount, bool>>>())).Returns(rt.AsQueryable().BuildMockDbSet().Object);

            _discountRepository.Setup(x => x.AddAsync(It.IsAny<Discount>()))
                .ReturnsAsync(new Discount { DiscountRate = 10, OverYear = 0, UserType = UserType.Affiliate });

            var handler = new CreateDiscountCommandHandler(_discountRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _discountRepository.Verify(x => x.GetQuery(It.IsAny<Expression<Func<Discount, bool>>>()), Times.Once);
            _discountRepository.Verify(x => x.AddAsync(It.IsAny<Discount>()), Times.Once);
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Discount_CreateCommand_DiscountAlreadyExist()
        {
            //Arrange
            List<Discount> rt = new List<Discount>() { new Discount { DiscountRate = 10, OverYear = 0, UserType = UserType.Affiliate } };

            var command = new CreateDiscountCommand
            {
                DiscountRate = 10,
                OverYear = 0,
                UserType = UserType.Affiliate
            };

            _discountRepository.Setup(x => x.GetQuery(It.IsAny<Expression<Func<Discount, bool>>>())).Returns(rt.AsQueryable().BuildMockDbSet().Object);

            _discountRepository.Setup(x => x.AddAsync(It.IsAny<Discount>()))
                .ReturnsAsync(new Discount { DiscountRate = 10, OverYear = 0, UserType = UserType.Affiliate });

            var handler = new CreateDiscountCommandHandler(_discountRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _discountRepository.Verify(x => x.GetQuery(It.IsAny<Expression<Func<Discount, bool>>>()), Times.Once);
            _discountRepository.Verify(x => x.AddAsync(It.IsAny<Discount>()), Times.Never);
            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.DiscountAlreadyExist);
        }

        [Test]
        public async Task Discount_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateDiscountCommand
            {
                DiscountRate = 10,
                Id = 1,
                OverYear = 0,
                UserType = UserType.Affiliate
            };

            _discountRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Discount, bool>>>()))
                .ReturnsAsync(new Discount { Id = 1, DiscountRate = 5, OverYear = 2, UserType = UserType.Customer });

            _discountRepository.Setup(x => x.UpdateAsync(It.IsAny<Discount>()))
                .ReturnsAsync(new Discount() { Id = 1, DiscountRate = 10, OverYear = 0, UserType = UserType.Affiliate });

            var handler = new UpdateDiscountCommandHandler(_discountRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _discountRepository.Verify(x => x.GetAsync(It.IsAny<Expression<Func<Discount, bool>>>()), Times.Once);
            _discountRepository.Verify(x => x.UpdateAsync(It.IsAny<Discount>()), Times.Once);
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Discount_UpdateCommand_DiscountNotFound()
        {
            //Arrange
            Discount rt = null;
            var command = new UpdateDiscountCommand
            {
                DiscountRate = 10,
                Id = 1,
                OverYear = 0,
                UserType = UserType.Affiliate
            };

            _discountRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Discount, bool>>>())).ReturnsAsync(rt);

            _discountRepository.Setup(x => x.UpdateAsync(It.IsAny<Discount>()))
                .ReturnsAsync(new Discount() { Id = 1, DiscountRate = 10, OverYear = 0, UserType = UserType.Affiliate });

            var handler = new UpdateDiscountCommandHandler(_discountRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _discountRepository.Verify(x => x.GetAsync(It.IsAny<Expression<Func<Discount, bool>>>()), Times.Once);
            _discountRepository.Verify(x => x.UpdateAsync(It.IsAny<Discount>()), Times.Never);
            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.RecordNotFound);
        }

        [Test]
        public async Task Discount_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteDiscountCommand();

            _discountRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Discount, bool>>>())).ReturnsAsync(new Discount());
            _discountRepository.Setup(x => x.DeleteAsync(It.IsAny<Discount>()));

            var handler = new DeleteDiscountCommandHandler(_discountRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _discountRepository.Verify(x => x.GetAsync(It.IsAny<Expression<Func<Discount, bool>>>()), Times.Once);
            _discountRepository.Verify(x => x.DeleteAsync(It.IsAny<Discount>()), Times.Once);
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }

        [Test]
        public async Task Discount_DeleteCommand_RecordNotFound()
        {
            //Arrange
            Discount rt = null;
            var command = new DeleteDiscountCommand();

            _discountRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Discount, bool>>>())).ReturnsAsync(rt);
            _discountRepository.Setup(x => x.DeleteAsync(It.IsAny<Discount>()));

            var handler = new DeleteDiscountCommandHandler(_discountRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _discountRepository.Verify(x => x.GetAsync(It.IsAny<Expression<Func<Discount, bool>>>()), Times.Once);
            _discountRepository.Verify(x => x.UpdateAsync(It.IsAny<Discount>()), Times.Never);
            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.RecordNotFound);
        }
    }
}

