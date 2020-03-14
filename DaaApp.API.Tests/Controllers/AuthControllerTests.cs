using System.Threading.Tasks;
using DaaApp.API.Controllers;
using DaaApp.API.Data;
using DaaApp.API.Dtos;
using DaaApp.API.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace DaaApp.API.Tests.Controllers
{
    public class AuthControllerTests
    {
        [Fact]
        public async void Register_WhenUserExists_ReturnFailure()
        {
            //Given
            var authRepositoryMock = new Mock<IAuthRepository>();
            var configMock = new Mock<IConfiguration>();
            UserForRegisterDto user = new UserForRegisterDto
            {
                Username = "admin",
                Password = "password"
            };

            authRepositoryMock.Setup(x => x.UserExists("admin")).Returns(Task.FromResult(true));

            //When
            AuthController authController = new AuthController(authRepositoryMock.Object, configMock.Object);
            var response = await authController.Register(user) as ObjectResult;

            //Then
            response.Should().BeOfType<BadRequestObjectResult>();
            response.Value.Should().Be("Username already exists");
        }
        [Fact]
        public async void Register_WhenUserDontExists_ReturnOk()
        {
            //Given
            var authRepositoryMock = new Mock<IAuthRepository>();
            var configMock = new Mock<IConfiguration>();
            UserForRegisterDto userForRegisterDto = new UserForRegisterDto
            {
                Username = "admin",
                Password = "password"
            };
            User user = new User
            {
                Id = 1,
                Username = "admin"
            };

            authRepositoryMock.Setup(x => x.UserExists("admin")).Returns(Task.FromResult(false));

            authRepositoryMock.Setup(x => x.Register(user, "password")).Returns(Task.FromResult(new User
            {
                Id = 1,
                Username = "admin"
            }));

            //When
            AuthController authController = new AuthController(authRepositoryMock.Object, configMock.Object);
            IActionResult response = await authController.Register(userForRegisterDto);

            //Then
            response.Should().BeOfType<StatusCodeResult>();
            (response as StatusCodeResult).StatusCode.Should().Be(201);
        }
    }
}