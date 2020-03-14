using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using System.Linq.Expressions;
using DaaApp.API.Data;
using DaaApp.API.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using EntityFrameworkCore3Mock;
using System.Text;
using System.IO;

namespace DaaApp.API.Tests.Data
{
    public class AuthRepositoryTests
    {
        public DbContextOptions<DataContext> DummyOptions { get; } = new DbContextOptionsBuilder<DataContext>().Options;
        private byte[] GetRandomByteArray()
        {
            return Encoding.ASCII.GetBytes(Path.GetRandomFileName());
        }

        [Fact]
        public void Login_GivenCredentials_WhenCorrect_ReturnsUser()
        {
            //Given
            var passwordSalt = GetRandomByteArray();
            string password = "password";
            byte[] passwordHash;
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

            var users = new List<User> {
                new User {
                    Id = 1,
                    Username = "admin",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                    },
                new User {
                    Id = 2,
                    Username = "unadmin",
                    PasswordHash = GetRandomByteArray(),
                    PasswordSalt = GetRandomByteArray()
                    }
            };

            var dbContextMock = new DbContextMock<DataContext>(DummyOptions);
            var usersDbSetMock = dbContextMock.CreateDbSetMock(x => x.Users, users);
            AuthRepository _sut = new AuthRepository(dbContextMock.Object);

            //When
            var actual = _sut.Login("admin", "password");
            var expectedUser = users.FirstOrDefault(u => u.Username == "admin");
            var actualUser = actual.Result;
            //Then
            actualUser.Should().BeEquivalentTo(expectedUser);
        }

        [Fact]
        public void Login_GivenCredentials_WhenIncorrect_ReturnsNull()
        {
            //Given
            var passwordSalt = GetRandomByteArray();
            string password = "password";
            byte[] passwordHash;
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

            var users = new List<User> {
                new User {
                    Id = 1,
                    Username = "admin",
                    PasswordHash = GetRandomByteArray(),
                    PasswordSalt = GetRandomByteArray()
                    },
                new User {
                    Id = 2,
                    Username = "unadmin",
                    PasswordHash = GetRandomByteArray(),
                    PasswordSalt = GetRandomByteArray()
                    }
            };

            var dbContextMock = new DbContextMock<DataContext>(DummyOptions);
            var usersDbSetMock = dbContextMock.CreateDbSetMock(x => x.Users, users);
            AuthRepository _sut = new AuthRepository(dbContextMock.Object);

            //When
            var actual = _sut.Login("admin", "password");
            var actualUser = actual.Result;
            //Then
            actualUser.Should().BeNull();
        }

        [Fact]
        public void Login_GivenUser_WhenIncorrect_ReturnsNull()
        {
            //Given
            var passwordSalt = GetRandomByteArray();
            string password = "password";
            byte[] passwordHash;
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

            var users = new List<User> {
                new User {
                    Id = 1,
                    Username = "admin",
                    PasswordHash = GetRandomByteArray(),
                    PasswordSalt = GetRandomByteArray()
                    },
                new User {
                    Id = 2,
                    Username = "unadmin",
                    PasswordHash = GetRandomByteArray(),
                    PasswordSalt = GetRandomByteArray()
                    }
            };

            var dbContextMock = new DbContextMock<DataContext>(DummyOptions);
            var usersDbSetMock = dbContextMock.CreateDbSetMock(x => x.Users, users);
            AuthRepository _sut = new AuthRepository(dbContextMock.Object);

            //When
            var actual = _sut.Login("admins", "password");
            var actualUser = actual.Result;
            //Then
            actualUser.Should().BeNull();
        }

        [Fact]
        public void UserExists_WhenCorrectUser_ReturnsTrue()
        {
            var users = new List<User> {
                new User {
                    Id = 1,
                    Username = "admin",
                    },
                new User {
                    Id = 2,
                    Username = "unadmin",
                    }
            };

            var dbContextMock = new DbContextMock<DataContext>(DummyOptions);
            var usersDbSetMock = dbContextMock.CreateDbSetMock(x => x.Users, users);
            AuthRepository _sut = new AuthRepository(dbContextMock.Object);

            //When
            var actual = _sut.UserExists("admin");
            var actualUser = actual.Result;
            //Then
            actualUser.Should().BeTrue();
        }

        [Fact]
        public void UserExists_WhenIncorrectUser_ReturnsFalse()
        {
            var users = new List<User> {
                new User {
                    Id = 1,
                    Username = "admin",
                    },
                new User {
                    Id = 2,
                    Username = "unadmin",
                    }
            };

            var dbContextMock = new DbContextMock<DataContext>(DummyOptions);
            var usersDbSetMock = dbContextMock.CreateDbSetMock(x => x.Users, users);
            AuthRepository _sut = new AuthRepository(dbContextMock.Object);

            //When
            var actual = _sut.UserExists("admins");
            var actualUser = actual.Result;
            //Then
            actualUser.Should().BeFalse();
        }

        [Fact]
        public void Register_WhenUserPasswordProvided_ReturnsRegisteredUser()
        {
            //Given
            var passwordSalt = GetRandomByteArray();
            string password = "password";
            byte[] passwordHash;
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

            var users = new List<User> {
                new User {
                    Id = 1,
                    Username = "admin",
                    PasswordHash = GetRandomByteArray(),
                    PasswordSalt = GetRandomByteArray()
                    },
                new User {
                    Id = 2,
                    Username = "unadmin",
                    PasswordHash = GetRandomByteArray(),
                    PasswordSalt = GetRandomByteArray()
                    }
            };

            var newUser = new User
            {
                Username = "newuser"
            };

            var dbContextMock = new DbContextMock<DataContext>(DummyOptions);
            var usersDbSetMock = dbContextMock.CreateDbSetMock(x => x.Users, users);
            AuthRepository _sut = new AuthRepository(dbContextMock.Object);

            //When
            var actual = _sut.Register(newUser, "password");
            
            var count = dbContextMock.Object.Users.Count();

            //Then
            count.Should().Be(3);
        }
    }
}