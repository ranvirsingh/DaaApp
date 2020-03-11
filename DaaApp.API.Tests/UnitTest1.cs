using System;
using Xunit;
using DaaApp.API.Models;

namespace DaaApp.API.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Value value = new Value();
            value.Id = 1;
            value.Name = "Value";

            Assert.Equal(1, value.Id);
            Assert.Equal("Value", value.Name);
        }
    }
}
