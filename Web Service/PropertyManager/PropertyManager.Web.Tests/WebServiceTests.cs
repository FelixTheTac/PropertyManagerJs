using System;
using System.Linq;
using Moq;
using PropertyManager.DTO.Domain;
using PropertyManager.Web.Controllers;
using Xunit.Extensions;

namespace PropertyManager.Web.Tests
{
    public class WebServiceTests
    {
        [Theory]
        [InlineData("Admin", "P@$$DG@$$")]
        public void LoginTest(string username, string password)
        {
            LoginController loginControllerMock = Mock.Of<LoginController>();
            GetClaimsRequest getClaimsRequestMock = Mock.Of<GetClaimsRequest>();
            getClaimsRequestMock.UserName = username;
            getClaimsRequestMock.Password = password;

            var claims = loginControllerMock.GetClaims(getClaimsRequestMock);
        }
    }
}
