using BarberLayered.Controllers;
using BarberLayered.Models;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace BarberLayered.Tests.Controllers
{
    public class LoginResult
    {
        public _UserType UserType { get; set; }
        public string ErrorMsg { get; set; }
    }

    public class AccountControllerTests
    {
        private readonly Mock<ILoginService> _loginServiceMock = new Mock<ILoginService>();
        private readonly Mock<IRegisterService> _registerServiceMock = new Mock<IRegisterService>();

        [Fact]
        public void Index_ReturnsViewResult()
        {
            var controller = new AccountController(_loginServiceMock.Object, _registerServiceMock.Object);
            var result = controller.Index();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Login_ReturnsViewResult()
        {
            var controller = new AccountController(_loginServiceMock.Object, _registerServiceMock.Object);
            var result = controller.Login();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Login_Post_WithValidModel_RedirectsToCorrectAction()
        {
            var loginModel = new LoginViewModel { Email = "test@example.com", Password = "password" };
            var loginResult = new LoginResult { UserType = _UserType.Client };
            _loginServiceMock.Setup(x => x.Login(loginModel.Email, loginModel.Password))
                 .ReturnsAsync(new UserExtDto
                 {
                     UserType = (_UserType)UserType.Client,
                     ErrorMsg = ""
                 });

            var controller = new AccountController(_loginServiceMock.Object, _registerServiceMock.Object);
            var result = await controller.Login(loginModel) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("BarberShop", result.ControllerName);
        }

        [Fact]
        public void Register_ReturnsViewResult()
        {
            var controller = new AccountController(_loginServiceMock.Object, _registerServiceMock.Object);
            var result = controller.Register();
            Assert.IsType<ViewResult>(result);
        }

        /* [Fact]
        public async Task Register_Post_WithMismatchedPasswords_ReturnsViewResultWithErrorMessage()
        {
            var controller = new AccountController(_loginServiceMock.Object, _registerServiceMock.Object);
            var registerViewModel = new RegisterViewModelWithKey
            {
                Password = "password1",
                ConfirmPassword = "password2"
            };

            var result = await controller.Register(registerViewModel) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal("", result.ViewName);
            Assert.NotNull(result.ViewData.Model);
            Assert.IsType<RegisterViewModelWithKey>(result.ViewData.Model);
            Assert.Equal("The password and confirmation password do not match.", controller.TempData["ErrorMessage"]);
        } */

        [Fact]
        public async Task Register_Post_WithValidModel_RedirectsToCorrectAction()
        {
            var registerViewModel = new RegisterViewModelWithKey
            {
                Password = "password",
                ConfirmPassword = "password",
            };

            var registerResult = new UserExtDto { UserType = _UserType.Client };
            _registerServiceMock.Setup(x => x.Register(It.IsAny<RegistrationDto>())).ReturnsAsync(registerResult);

            var controller = new AccountController(_loginServiceMock.Object, _registerServiceMock.Object);
            var result = await controller.Register(registerViewModel) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("BarberShop", result.ControllerName);
        }

        [Fact]
        public void Logout_RedirectsToCorrectAction()
        {
            var controller = new AccountController(_loginServiceMock.Object, _registerServiceMock.Object);
            var result = controller.Logout() as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("BarberShop", result.ControllerName);
        }
    }
}
