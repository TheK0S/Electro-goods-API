using Electro_goods_API.Helpers;
using FluentAssertions;

namespace Electro_goods_API.Tests
{
    public class HashPasswordHelperTests
    {
        [Fact]
        public void HashPasswordHelper_HashPassword_ReturnString()
        {
            //Arrange
            string password = "password";

            //Act
            string result = HashPasswordHelper.HashPasword(password);

            //Assert
            result.Should().Be("5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8");

        }
    }
}