using Core.Exceptions;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using TodoList.Models.Dtos.Users.Requests;
using TodoList.Models.Entities;
using TodoList.Service.Concretes;

namespace Tests.Services;

public class RoleServiceTest
{
    [TestFixture]
    public class RoleServiceTests
    {
        private Mock<UserManager<User>> _mockUserManager;
        private Mock<RoleManager<IdentityRole>> _mockRoleManager;
        private RoleService _roleService;

        [SetUp]
        public void Setup()
        {
            // Mock UserManager and RoleManager using Mock.Of<T> for simpler mocks
            _mockUserManager = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null,
                null, null, null);
            _mockRoleManager =
                new Mock<RoleManager<IdentityRole>>(Mock.Of<IRoleStore<IdentityRole>>(), null, null, null, null);

            _roleService = new RoleService(_mockUserManager.Object, _mockRoleManager.Object);
        }

        [Test]
        public async Task AddRoleToUser_WhenRoleDoesNotExist_ShouldThrowBusinessException()
        {
            // Arrange
            var roleName = "Admin";
            var userId = "user123";
            var dto = new RoleAddToUserRequestDto(roleName, userId);

            _mockRoleManager.Setup(r => r.FindByNameAsync(roleName))
                .ReturnsAsync((IdentityRole)null); // Simulate role does not exist

            // Act & Assert
            var ex = Assert.ThrowsAsync<BusinessException>(() => _roleService.AddRoleToUser(dto));
            Assert.That(ex.Message, Is.EqualTo("rol bulunamadı."));
        }

        [Test]
        public async Task AddRoleToUser_WhenUserDoesNotExist_ShouldThrowNotFoundException()
        {
            // Arrange
            var roleName = "Admin";
            var userId = "invalidUserId";
            var dto = new RoleAddToUserRequestDto(userId,roleName);

            // Mock the role retrieval to return a valid role
            var role = new IdentityRole { Name = roleName };
            _mockRoleManager.Setup(r => r.FindByNameAsync(roleName))
                .ReturnsAsync(role); // Simulate role exists

            // Mock user retrieval to return null (simulating user not found)
            _mockUserManager.Setup(u => u.FindByIdAsync(userId))
                .ReturnsAsync((User)null); // Simulate user does not exist

            // Act & Assert
            var exception = Assert.ThrowsAsync<NotFoundException>(async () =>
                await _roleService.AddRoleToUser(dto));

            Assert.That(exception.Message, Is.EqualTo("Kulllanıcı bulunamadı."));
        }

        [Test]
        public async Task AddRoleToUser_WhenAddToRoleFails_ShouldThrowBusinessException()
        {
            // Arrange
            var roleName = "Admin";
            var userId = "user123";
            var dto = new RoleAddToUserRequestDto (roleName, userId);

            var role = new IdentityRole { Name = roleName };
            _mockRoleManager.Setup(r => r.FindByNameAsync(roleName))
                .ReturnsAsync(role); // Simulate role exists

            var user = new User { Id = userId };
            _mockUserManager.Setup(u => u.FindByIdAsync(userId))
                .ReturnsAsync(user); // Simulate user exists

            _mockUserManager.Setup(u => u.AddToRoleAsync(user, roleName))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError
                    { Description = "Role add failed" })); // Simulate failure

            // Act & Assert
            var ex = Assert.ThrowsAsync<BusinessException>(() => _roleService.AddRoleToUser(dto));
            Assert.That(ex.Message, Is.EqualTo("rol bulunamadı."));
        }

        [Test]
        public async Task AddRoleToUser_WhenRoleAddedSuccessfully_ShouldReturnSuccessMessage()
        {
            // Arrange
            var roleName = "Admin";
            var userId = "user123";
            var dto = new RoleAddToUserRequestDto (userId,roleName);

            var role = new IdentityRole { Name = roleName };

            // Mock the role retrieval to return a valid role (not null)
            _mockRoleManager.Setup(r => r.FindByNameAsync(roleName))
                .ReturnsAsync(role); // Simulate role exists

            var user = new User { Id = userId };
            // Mock user retrieval
            _mockUserManager.Setup(u => u.FindByIdAsync(userId))
                .ReturnsAsync(user); // Simulate user exists

            // Mock role addition to the user (success scenario)
            _mockUserManager.Setup(u => u.AddToRoleAsync(user, roleName))
                .ReturnsAsync(IdentityResult.Success); // Simulate successful addition

            // Act
            var result = await _roleService.AddRoleToUser(dto);

            // Assert
            Assert.That(result, Is.EqualTo("Kulllanıcıya rol eklendi : Admin"));
        }

        [Test]
        public async Task AddRoleAsync_WhenRoleAlreadyExists_ShouldThrowBusinessException()
        {
            // Arrange
            var roleName = "Admin";

            _mockRoleManager.Setup(r => r.FindByNameAsync(roleName))
                .ReturnsAsync(new IdentityRole { Name = roleName }); // Simulate role already exists

            // Act & Assert
            var ex = Assert.ThrowsAsync<BusinessException>(() => _roleService.AddRoleAsync(roleName));
            Assert.That(ex.Message, Is.EqualTo("Eklemek istediğiniz rol benzerseiz olmalıdır!"));
        }

        [Test]
        public async Task AddRoleAsync_WhenRoleAddedSuccessfully_ShouldReturnSuccessMessage()
        {
            // Arrange
            var roleName = "Admin";
            _mockRoleManager.Setup(r => r.FindByNameAsync(roleName))
                .ReturnsAsync((IdentityRole)null); // Simulate role does not exist

            _mockRoleManager.Setup(r => r.CreateAsync(It.IsAny<IdentityRole>()))
                .ReturnsAsync(IdentityResult.Success); // Simulate successful role creation

            // Act
            var result = await _roleService.AddRoleAsync(roleName);

            // Assert
            Assert.That(result, Is.EqualTo("Rol eklendi Admin"));
        }
    }
}