using Core.Utilities.Security.Hashing;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace Tests.Helpers.SampleData
{
    public static class UserData
    {
        public static UserLoginCommand GetUserLogin()
        {
            return new UserLoginCommand
            {
                Email = "info1@hesapizi.com",
                Password = "123456",
            };
        }

        public static User GetUser()
        {
            HashingHelper.CreatePasswordHash("123456", out var passwordHash, out var passwordSalt);

            return new User()
            {
                Id = 1,
                Email = "info@hesapizi.com",
                FullName = "Hesapİzi",
                IsActive = true,
                CreateDate = DateTime.Now,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };
        }

        public static User GetInactiveUser()
        {
            HashingHelper.CreatePasswordHash("123456", out var passwordHash, out var passwordSalt);

            return new User()
            {
                Id = 1,
                Email = "info@hesapizi.com",
                FullName = "Hesapİzi",
                IsActive = false,
                CreateDate = DateTime.Now,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };
        }

        public static User GetUser(string name)
        {
            HashingHelper.CreatePasswordHash("123456", out var passwordHash, out var passwordSalt);

            return new User()
            {
                Id = 1,
                Email = "test@test.com",
                FullName = string.Format("{0} {1} {2}", name, name, name),
                PhoneNumber = "05339262726",
                CreateDate = DateTime.Now,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };
        }

        public static List<User> GetUserList()
        {
            HashingHelper.CreatePasswordHash("123456", out var passwordHash, out var passwordSalt);
            var list = new List<User>();

            for (var i = 1; i <= 5; i++)
            {
                var user = new User()
                {
                    Id = i,
                    Email = $"test@test.com {i}",
                    FullName = $"name {i} name {i} name {i}",
                    PhoneNumber = $"12345678{i}",
                    CreateDate = DateTime.Now,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    IsActive = true,
                };
                list.Add(user);
            }

            return list;
        }

        public static UserResetPasswordCommand GetUserPasswordRequestCommand()
        {
            return new UserResetPasswordCommand
            {
                UserId = 1,
                Token = "7a14",
                NewPassword = "123456",
                ConfirmPassword = "123456"
            };
        }

        public static UserResetPasswordCommand GetUserPasswordRequestDifferentPasswordsCommand()
        {
            return new UserResetPasswordCommand
            {
                UserId = 1,
                Token = "7a14",
                NewPassword = "123456",
                ConfirmPassword = "1234567"
            };
        }
    }
}
