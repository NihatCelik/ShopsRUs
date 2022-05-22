using Business.Constants;
using Business.Handlers.Users.Commands;
using Business.Handlers.Users.Queries;
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

            _userRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(new User()
//propertyler buraya yazılacak
//{																		
//UserId = 1,
//UserName = "Test"
//}
);

            var handler = new GetUserQueryHandler(_userRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.UserId.Should().Be(1);

        }

        [Test]
        public async Task User_GetQueries_Success()
        {
            //Arrange
            var query = new GetUsersQuery();

            _userRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<User, bool>>>()))
                        .ReturnsAsync(new List<User> { new User() { /*TODO:propertyler buraya yazılacak UserId = 1, UserName = "test"*/ } });

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
            User rt = null;
            //Arrange
            var command = new CreateUserCommand();
            //propertyler buraya yazılacak
            //command.UserName = "deneme";

            _userRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
                        .ReturnsAsync(rt);

            _userRepository.Setup(x => x.Add(It.IsAny<User>())).Returns(new User());

            var handler = new CreateUserCommandHandler(_userRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _userRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task User_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateUserCommand();
            //propertyler buraya yazılacak 
            //command.UserName = "test";

            _userRepository.Setup(x => x.Query())
                                           .Returns(new List<User> { new User() { /*TODO:propertyler buraya yazılacak UserId = 1, UserName = "test"*/ } }.AsQueryable());

            _userRepository.Setup(x => x.Add(It.IsAny<User>())).Returns(new User());

            var handler = new CreateUserCommandHandler(_userRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task User_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateUserCommand();
            //command.UserName = "test";

            _userRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
                        .ReturnsAsync(new User() { /*TODO:propertyler buraya yazılacak UserId = 1, UserName = "deneme"*/ });

            _userRepository.Setup(x => x.Update(It.IsAny<User>())).Returns(new User());

            var handler = new UpdateUserCommandHandler(_userRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _userRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task User_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteUserCommand();

            _userRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
                        .ReturnsAsync(new User() { /*TODO:propertyler buraya yazılacak UserId = 1, UserName = "deneme"*/});

            _userRepository.Setup(x => x.Delete(It.IsAny<User>()));

            var handler = new DeleteUserCommandHandler(_userRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _userRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

