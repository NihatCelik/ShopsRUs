using System.Threading.Tasks;
using Business.Handlers.Discounts.Commands;
using Business.Handlers.Users.Commands;
using Core.Utilities.IoC;
using Entities.Enums;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Helpers
{
    public static class FakeDataMiddleware
    {
        public static async Task UseDbFakeDataCreator(this IApplicationBuilder app)
        {
            var mediator = ServiceTool.ServiceProvider.GetService<IMediator>();

            await mediator.Send(new CreateDiscountCommand { DiscountRate = 5, OverYear = 2, UserType = UserType.Customer });
            await mediator.Send(new CreateDiscountCommand { DiscountRate = 10, OverYear = 0, UserType = UserType.Affiliate });
            await mediator.Send(new CreateDiscountCommand { DiscountRate = 30, OverYear = 0, UserType = UserType.Employee });

            await mediator.Send(new CreateUserCommand
            {
                Address = "İstanbul",
                Email = "info@gmail.com",
                FirstName = "Nihat",
                LastName = "Çelik",
                PhoneNumber = "0538 600 0000",
                UserType = UserType.Employee
            });

            await mediator.Send(new CreateUserCommand
            {
                Address = "Ankara",
                Email = "info@hotmail.com",
                FirstName = "Ali",
                LastName = "Çelik",
                PhoneNumber = "0538 500 0000",
                UserType = UserType.Affiliate
            });

            await mediator.Send(new CreateUserCommand
            {
                Address = "Elazığ",
                Email = "info@yahoo.com",
                FirstName = "Kerem",
                LastName = "Çelik",
                PhoneNumber = "0538 400 0000",
                UserType = UserType.Customer
            });
        }
    }
}
