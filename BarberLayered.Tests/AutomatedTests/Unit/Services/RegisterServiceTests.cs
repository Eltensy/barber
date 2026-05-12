using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services.Implementations;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace BarberLayered.Tests.AutomatedTests.Unit.Services;

public class RegisterServiceTests
{
    private readonly Mock<IAdminService> _adminService = new();
    private readonly Mock<IBarberService> _barberService = new();

    [Fact]
    public async Task AdminAddBarber_WhenBarberAlreadyExists_ShouldReturnMinusOne()
    {
        var userStore = CreateUserEmailStore();
        var userManager = CreateUserManager(userStore.Object);
        var signInManager = CreateSignInManager(userManager.Object);
        _barberService.Setup(x => x.GetBarberByEmail("bob@test.com")).ReturnsAsync(new BarberDto { Id = "b1", Name = "Bob", Surname = "Ray", Email = "bob@test.com", Phone = "111", PasswordHash = "hash" });
        var service = new RegisterService(_adminService.Object, _barberService.Object, userManager.Object, userStore.Object, signInManager.Object);

        var result = await service.AdminAddBarber(new RegistrationDto { Email = "bob@test.com", Password = "secret", Name = "Bob", Surname = "Ray", Phone = "111" });

        result.Should().Be(-1);
        userManager.Verify(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task AdminAddBarber_WhenCreateSucceeds_ShouldReturnZero()
    {
        var userStore = CreateUserEmailStore();
        var userManager = CreateUserManager(userStore.Object);
        var signInManager = CreateSignInManager(userManager.Object);
        _barberService.Setup(x => x.GetBarberByEmail("bob@test.com")).ReturnsAsync((BarberDto?)null);
        userManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), "secret")).ReturnsAsync(IdentityResult.Success);
        userManager.Setup(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), "Barber")).ReturnsAsync(IdentityResult.Success);
        var service = new RegisterService(_adminService.Object, _barberService.Object, userManager.Object, userStore.Object, signInManager.Object);

        var result = await service.AdminAddBarber(new RegistrationDto { Email = "bob@test.com", Password = "secret", Name = "Bob", Surname = "Ray", Phone = "111" });

        result.Should().Be(0);
        signInManager.Verify(x => x.SignInAsync(It.IsAny<ApplicationUser>(), false, null), Times.Once);
        userManager.Verify(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), "Barber"), Times.Once);
    }

    [Fact]
    public void Constructor_WhenEmailStoreIsNotSupported_ShouldThrow()
    {
        var plainStore = new Mock<IUserStore<ApplicationUser>>();
        var userManager = CreateUserManager(plainStore.Object);
        var signInManager = CreateSignInManager(userManager.Object);

        var action = () => new RegisterService(_adminService.Object, _barberService.Object, userManager.Object, plainStore.Object, signInManager.Object);

        action.Should().Throw<NotSupportedException>();
    }


    private static Mock<IUserEmailStore<ApplicationUser>> CreateUserEmailStore()
    {
        var userStore = new Mock<IUserEmailStore<ApplicationUser>>();
        userStore.Setup(x => x.SetUserNameAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        userStore.Setup(x => x.SetEmailAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        return userStore;
    }

    private static Mock<UserManager<ApplicationUser>> CreateUserManager(IUserStore<ApplicationUser> store)
    {
        var options = new Mock<IOptions<IdentityOptions>>();
        options.Setup(x => x.Value).Returns(new IdentityOptions());
        var passwordHasher = new Mock<IPasswordHasher<ApplicationUser>>();
        var userValidators = new List<IUserValidator<ApplicationUser>>();
        var passwordValidators = new List<IPasswordValidator<ApplicationUser>>();
        var lookupNormalizer = new Mock<ILookupNormalizer>();
        var identityErrors = new Mock<IdentityErrorDescriber>();
        var services = new Mock<IServiceProvider>();
        var logger = new Mock<ILogger<UserManager<ApplicationUser>>>();

        return new Mock<UserManager<ApplicationUser>>(
            store,
            options.Object,
            passwordHasher.Object,
            userValidators,
            passwordValidators,
            lookupNormalizer.Object,
            identityErrors.Object,
            services.Object,
            logger.Object);
    }

    private static Mock<SignInManager<ApplicationUser>> CreateSignInManager(UserManager<ApplicationUser> userManager)
    {
        var contextAccessor = new Mock<IHttpContextAccessor>();
        contextAccessor.Setup(x => x.HttpContext).Returns(new DefaultHttpContext());
        var claimsFactory = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
        var options = new Mock<IOptions<IdentityOptions>>();
        options.Setup(x => x.Value).Returns(new IdentityOptions());
        var logger = new Mock<ILogger<SignInManager<ApplicationUser>>>();
        var schemes = new Mock<IAuthenticationSchemeProvider>();
        var confirmation = new Mock<IUserConfirmation<ApplicationUser>>();

        return new Mock<SignInManager<ApplicationUser>>(
            userManager,
            contextAccessor.Object,
            claimsFactory.Object,
            options.Object,
            logger.Object,
            schemes.Object,
            confirmation.Object);
    }
}
