using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services.Implementations;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace BarberBook.Tests
{
    public class LoginTests
    {
        private readonly Mock<IAdminService> _adminServiceMock;
        private readonly Mock<IBarberService> _barberServiceMock;
        private readonly Mock<IClientService> _clientServiceMock;
        private readonly LoginService _loginService;

        public LoginTests()
        {
            _adminServiceMock = new Mock<IAdminService>();
            _barberServiceMock = new Mock<IBarberService>();
            _clientServiceMock = new Mock<IClientService>();
            _loginService = new LoginService(_clientServiceMock.Object, _barberServiceMock.Object, _adminServiceMock.Object);

        }

        [Fact]
        public async Task NonExistentUserLogin()
        {
            // User that does not exists in our database
            string email = "johndoe@gmail.com";
            string password = "Non-existent14#";

            var result = await _loginService.Login(email, password);

            Assert.Equal(result.ErrorMsg, $"No user with email {email} was found");
        }

        [Fact]
        public async Task UserValidLogin()
        {
            //Login as barber
            string email = "barber1@meil.com";
            string password = "1234@Qqqq";

            _barberServiceMock.Setup(x => x.GetBarberByEmail(email)).ReturnsAsync(new BarberDto
            {
                Email = email
            });

            LoginService loginService = new LoginService(_clientServiceMock.Object, _barberServiceMock.Object, _adminServiceMock.Object);
            

            UserExtDto result = await loginService.Login(email, password);

            Assert.Equal(email, result.Email);
        }

        //[Fact]
        //public async Task DeletedUserLogin()
        //{
        //    string email = "user1todelete@gmail.com";
        //    string password = "Qwerty123321#";

        //    _clientServiceMock.Setup(service => service.GetClientByEmail(email))
        //                  .ReturnsAsync(new ClientDto { Email = email});

        //    LoginService loginService = new LoginService(_clientServiceMock.Object, _barberServiceMock.Object, _adminServiceMock.Object);

        //    UserExtDto result = await loginService.Login(email, password);

        //    Assert.Equal(email, result.Email);

        //    ApplicationUser applicationUser = new ApplicationUser() { Email = email};
        //    new DataAccessLayer.Data.DataContext().ApplicationUsers.Remove(applicationUser);
        //    await new DataAccessLayer.Data.DataContext().SaveChangesAsync();

        //    _clientServiceMock.Setup(service => service.GetClientByEmail(email))
        //                  .ReturnsAsync((ClientDto?)null);

        //    result = await loginService.Login(email, password);

        //    Assert.Equal($"No user with email {email} was found", result.ErrorMsg);
        //}
    }
}