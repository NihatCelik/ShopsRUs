using Business.Constants;
using Business.Handlers.Users.Commands;
using Business.Handlers.Users.Queries;
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
    public class UserHandlerTests
    {
        Mock<IUserRepository> _userRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _userRepository = new Mock<IUserRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task User_GetQuery_Success()
        {
            //Arrange
            var query = new GetUserQuery();
            _userRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(new User
                {
                    Id = 1,
                    UserType = UserType.Employee,
                    Address = "İstanbul",
                    Email = "info@gmail.com",
                    FirstName = "Nihat",
                    LastName = "Çelik",
                    PhoneNumber = "90 538 0000"
                });

            var handler = new GetUserQueryHandler(_userRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            x.Data.Id.Should().Be(1);
            x.Data.UserType.Should().Be(UserType.Employee);
            x.Data.Address.Should().Be("İstanbul");
            x.Data.Email.Should().Be("info@gmail.com");
            x.Data.FirstName.Should().Be("Nihat");
            x.Data.LastName.Should().Be("Çelik");
            x.Data.PhoneNumber.Should().Be("90 538 0000");
        }

        [Test]
        public async Task User_GetQueries_Success()
        {
            //Arrange
            var query = new GetUsersQuery();

            _userRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(new List<User> { new User(), new User() });

            var handler = new GetUsersQueryHandler(_userRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<User>)x.Data).Count.Should().BeGreaterThan(1);
        }

        [Test]
        public async Task User_CreateCommand_Success()
        {
            //Arrange
            List<User> rt = new List<User>();
            var command = new CreateUserCommand
            {
                UserType = UserType.Employee,
                Address = "İstanbul",
                Email = "info@gmail.com",
                FirstName = "Nihat",
                LastName = "Çelik",
                PhoneNumber = "90 538 0000"
            };

            _userRepository.Setup(x => x.GetQuery(It.IsAny<Expression<Func<User, bool>>>())).Returns(rt.AsQueryable().BuildMockDbSet().Object);

            _userRepository.Setup(x => x.AddAsync(It.IsAny<User>()))
                .ReturnsAsync(new User
                {
                    UserType = UserType.Employee,
                    Address = "İstanbul",
                    Email = "info@gmail.com",
                    FirstName = "Nihat",
                    LastName = "Çelik",
                    PhoneNumber = "90 538 0000"
                });

            var handler = new CreateUserCommandHandler(_userRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _userRepository.Verify(x => x.GetQuery(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
            _userRepository.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task User_CreateCommand_UserAlreadyExist()
        {
            //Arrange
            List<User> rt = new List<User>() { new User { UserType = UserType.Employee, Address = "İstanbul", Email = "info@gmail.com", FirstName = "Nihat", LastName = "Çelik", PhoneNumber = "90 538 0000" } };
            var command = new CreateUserCommand
            {
                UserType = UserType.Employee,
                Address = "İstanbul",
                Email = "info@gmail.com",
                FirstName = "Nihat",
                LastName = "Çelik",
                PhoneNumber = "90 538 0000"
            };

            _userRepository.Setup(x => x.GetQuery(It.IsAny<Expression<Func<User, bool>>>())).Returns(rt.AsQueryable().BuildMockDbSet().Object);

            _userRepository.Setup(x => x.AddAsync(It.IsAny<User>()))
                    .ReturnsAsync(new User
                    {
                        UserType = UserType.Employee,
                        Address = "İstanbul",
                        Email = "info@gmail.com",
                        FirstName = "Nihat",
                        LastName = "Çelik",
                        PhoneNumber = "90 538 0000"
                    });

            var handler = new CreateUserCommandHandler(_userRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _userRepository.Verify(x => x.GetQuery(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
            _userRepository.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Never);
            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.UserAlreadyExist);
        }

        [Test]
        public async Task User_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateUserCommand
            {
                Id = 1,
                UserType = UserType.Employee,
                Address = "İstanbul",
                Email = "info@gmail.com",
                FirstName = "Nihat",
                LastName = "Çelik",
                PhoneNumber = "90 538 0000"
            };

            _userRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(new User
                {
                    Id = 1,
                    Address = "Ankara",
                    Email = "info@gmail.com",
                    FirstName = "Nihat",
                    LastName = "Çelik",
                    PhoneNumber = "90 538 642 3033",
                    UserType = UserType.Affiliate
                });

            _userRepository.Setup(x => x.UpdateAsync(It.IsAny<User>()))
                .ReturnsAsync(new User
                {
                    Id = 1,
                    UserType = UserType.Employee,
                    Address = "İstanbul",
                    Email = "info@gmail.com",
                    FirstName = "Nihat",
                    LastName = "Çelik",
                    PhoneNumber = "90 538 0000"
                });

            var handler = new UpdateUserCommandHandler(_userRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _userRepository.Verify(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
            _userRepository.Verify(x => x.UpdateAsync(It.IsAny<User>()), Times.Once);
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task User_UpdateCommand_RecordNotFound()
        {
            //Arrange
            User rt = null;
            var command = new UpdateUserCommand
            {
                Id = 1,
                UserType = UserType.Employee,
                Address = "İstanbul",
                Email = "info@gmail.com",
                FirstName = "Nihat",
                LastName = "Çelik",
                PhoneNumber = "90 538 0000"
            };

            _userRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(rt);

            _userRepository.Setup(x => x.UpdateAsync(It.IsAny<User>()))
                .ReturnsAsync(new User
                {
                    Id = 1,
                    UserType = UserType.Employee,
                    Address = "İstanbul",
                    Email = "info@gmail.com",
                    FirstName = "Nihat",
                    LastName = "Çelik",
                    PhoneNumber = "90 538 0000"
                });

            var handler = new UpdateUserCommandHandler(_userRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _userRepository.Verify(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
            _userRepository.Verify(x => x.UpdateAsync(It.IsAny<User>()), Times.Never);
            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.RecordNotFound);
        }

        [Test]
        public async Task User_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteUserCommand();

            _userRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(new User());
            _userRepository.Setup(x => x.DeleteAsync(It.IsAny<User>()));

            var handler = new DeleteUserCommandHandler(_userRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _userRepository.Verify(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
            _userRepository.Verify(x => x.DeleteAsync(It.IsAny<User>()), Times.Once);
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }

        [Test]
        public async Task User_DeleteCommand_RecordNotFound()
        {
            //Arrange
            User rt = null;
            var command = new DeleteUserCommand();

            _userRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(rt);
            _userRepository.Setup(x => x.DeleteAsync(It.IsAny<User>()));

            var handler = new DeleteUserCommandHandler(_userRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _userRepository.Verify(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
            _userRepository.Verify(x => x.DeleteAsync(It.IsAny<User>()), Times.Never);
            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.RecordNotFound);
        }
    }
}

