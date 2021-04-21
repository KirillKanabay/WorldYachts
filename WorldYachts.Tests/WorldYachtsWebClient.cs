using System;
using System.Threading.Tasks;
using WorldYachts.Services;
using WorldYachts.Services.Authenticate;
using Xunit;

namespace WorldYachts.Tests
{
    public class WorldYachtsWebClient
    {
        [Fact]
        public async Task Authenticate()
        {
            //Arrange
            var request = new AuthenticateRequest() {Username = "admin", Password = "admin"};
            var webClient = new WebClientService();
            var authUser = new AuthUser();
            
            //Act
            var response =
                await webClient.PostAsync<AuthenticateRequest, AuthenticateResponse>("users/authenticate", request);
            authUser.Authenticate(response);
            
            //Assert
            Assert.Equal("admin@gmail.com", authUser.Email);
        }
    }
}
