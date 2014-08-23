using System;
using System.Linq;
using Moq;
using Ninject;
using PropertyManager.DTO.Domain;
using PropertyManager.Web.Controllers;
using PropertyManager.WebShared.Interfaces.Controllers;
using Xunit.Extensions;

namespace PropertyManager.Web.Tests
{
    public class WebServiceTests
    {

        

        public WebServiceTests()
        {
            
        }

        [Theory]
        [InlineData("Admin", "P@$$DG@$$")]
        public void LoginTest(string username, string password)
        {
            LoginController loginControllerMock = new LoginController();
            GetClaimsRequest getClaimsRequestMock = Mock.Of<GetClaimsRequest>();
            getClaimsRequestMock.UserName = username;
            getClaimsRequestMock.Password = password;

            var claims = loginControllerMock.GetClaims(getClaimsRequestMock);
        }

        #region private methods

        

        #endregion
    }
}
