using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DaaApp.API.Data;
using DaaApp.API.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace DaaApp.API.Tests.Data
{
    public class AuthRepositoryTests
    {
        [Fact]
        public void GivenCredentials_WhenCorrect_ReturnsUser()
        {
            //Given
            List<User> users = new List<User> {
                new User {
                    Id = 1,
                    Username = "userA"
                    },
                new User {
                    Id = 1,
                    Username = "userA"
                    }
            };

            var contextMock = new Mock<DataContext>();
            var userDbSetMock = new Mock<DbSet<User>>();

            userDbSetMock.Setup(z => z.FirstOrDefaultAsync(It.IsAny<string>())).Returns<User>(users[0]);

            contextMock.SetupProperty(users => users.Users, userDbSetMock.Object);

            AuthRepository _sut = new AuthRepository(contextMock.Object);

            //When
            var actual = _sut.Login("admin", "password");

            var notActual = actual;
            //Then
        }
    }
}