using Core.Exceptions;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using TodoList.Models.Dtos.Users.Requests;
using TodoList.Models.Entities;
using TodoList.Service.Concretes;

namespace Tests.Services;

public class UserServiceTest
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<UserManager<User>> _userManagerMock;
        private UserService _userService;

        [SetUp]
        public void SetUp()
        {
            _userManagerMock = new Mock<UserManager<User>>(
                new Mock<IUserStore<User>>().Object,
                null, null, null, null, null, null, null, null
            );
            _userService = new UserService(_userManagerMock.Object);
        }

        [Test]
        public async Task RegisterAsync_ShouldRegisterUserAndReturnUser_WhenDataIsValid()
        {
            // Arrange
            var registerDto = new RegisterRequestDto(
                "John",
                "Doe",
                "john.doe@example.com",
                "New York",
                "johndoe",
                "Password123!");

            _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            _userManagerMock.Setup(um => um.AddToRoleAsync(It.IsAny<User>(), "User"))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _userService.RegisterAsync(registerDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(registerDto.Email, result.Email);
        }

        [Test]
        public void RegisterAsync_ShouldThrowBusinessException_WhenIdentityResultFails()
        {
            // Arrange
            var registerDto = new RegisterRequestDto(
                "John",
                "Doe",
                "john.doe@example.com",
                "New York",
                "johndoe",
                "Password123!");

            _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Error" }));

            // Act & Assert
            var ex = Assert.ThrowsAsync<BusinessException>(() => _userService.RegisterAsync(registerDto));
            Assert.AreEqual("Error", ex.Message);
        }

        [Test]
        public async Task GetByEmailAsync_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var user = new User { Email = "john.doe@example.com" };

            _userManagerMock.Setup(um => um.FindByEmailAsync("john.doe@example.com"))
                .ReturnsAsync(user);

            // Act
            var result = await _userService.GetByEmailAsync("john.doe@example.com");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("john.doe@example.com", result.Email);
        }

        [Test]
        public void GetByEmailAsync_ShouldThrowNotFoundException_WhenUserDoesNotExist()
        {
            // Arrange
            _userManagerMock.Setup(um => um.FindByEmailAsync("john.doe@example.com"))
                .ReturnsAsync((User)null);

            // Act & Assert
            var ex = Assert.ThrowsAsync<NotFoundException>(() => _userService.GetByEmailAsync("john.doe@example.com"));
            Assert.AreEqual("Kullanıcı bulunamadı.", ex.Message);
        }

        [Test]
        public async Task ChangePasswordAsync_ShouldChangePassword_WhenDataIsValid()
        {
            // Arrange
            var user = new User { Id = "123" };
            var changePasswordDto = new ChangePasswordRequestDto("OldPassword123!",
                "NewPassword123!",
                "NewPassword123!");

            _userManagerMock.Setup(um => um.FindByIdAsync("123"))
                .ReturnsAsync(user);
            _userManagerMock.Setup(um =>
                    um.ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _userService.ChangePasswordAsync("123", changePasswordDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(user.Id, result.Id);
        }

        [Test]
        public async Task ChangePasswordAsync_ShouldThrowBusinessException_WhenPasswordsDoNotMatch()
        {
            // Arrange
            var changePasswordDto = new ChangePasswordRequestDto("OldPassword123!",
                "NewPassword123!",
                "NewPassword123!");

            // Act & Assert
            var ex = Assert.ThrowsAsync<BusinessException>(() =>
                _userService.ChangePasswordAsync("64bc7198-d5ca-45a6-ae6e-8ffbfee81fea", changePasswordDto));
            Assert.AreEqual("Parola Uyuşmuyor.", ex.Message);
        }

        [Test]
        public async Task DeleteAsync_ShouldReturnSuccessMessage_WhenUserExists()
        {
            // Arrange
            var user = new User { Id = "123" };

            _userManagerMock.Setup(um => um.FindByIdAsync("123"))
                .ReturnsAsync(user);
            _userManagerMock.Setup(um => um.DeleteAsync(user))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _userService.DeleteAsync("123");

            // Assert
            Assert.AreEqual("Kullanıcı Silindi.", result);
        }

        [Test]
        public void DeleteAsync_ShouldThrowNotFoundException_WhenUserDoesNotExist()
        {
            // Arrange
            _userManagerMock.Setup(um => um.FindByIdAsync("123"))
                .ReturnsAsync((User)null);

            // Act & Assert
            var ex = Assert.ThrowsAsync<NotFoundException>(() => _userService.DeleteAsync("123"));
            Assert.AreEqual("Kullanıcı bulunamadı.", ex.Message);
        }
    }
}