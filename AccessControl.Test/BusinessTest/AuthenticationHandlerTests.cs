using AccessControl.Application.Features.Authentication;
using AccessControl.Application.Persistence;
using AccessControl.Application.ViewModels;
using AccessControl.Domain.Entities;
using AutoMapper;
using Moq;

namespace AccessControl.Test.BusinessTest
{
    [TestFixture]
    public class AuthenticationHandlerTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IPrincipalContextWrapper> _contextWrapper;
        private IAuthenticationHandler _authenticationHandler;

        [SetUp]
        public void SetUp()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _contextWrapper = new Mock<IPrincipalContextWrapper>();
            _authenticationHandler =
                new AuthenticationHandler(_userRepositoryMock.Object, _mapperMock.Object, _contextWrapper.Object);
        }

        [Test]
        public async Task IsLoginSuccessfulAsync_LoginSuccessful_UserAdded()
        {
            // Arrange
            var ldapHost = "ldap://test-server";
            var domain = "test-domain";
            var username = "testuser";
            var password = "testpassword";

            var response = new UserDetailsVM
            {
                IsAuthenticated = true,
                UserName = username
            };


            _contextWrapper
                .Setup(s => s.ValidateCredentialsAndGetUserDetailsAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(response);

            _userRepositoryMock.Setup(r => r.GetByUserNameAsync(username)).ReturnsAsync((User)null!);

            var mappedUser = new User();
            _mapperMock.Setup(m => m.Map<User>(response)).Returns(mappedUser);

            // Act
            var result = await _authenticationHandler.IsLoginSuccessfulAsync(ldapHost, domain, username, password);

            // Assert
            Assert.IsTrue(result);
            _userRepositoryMock.Verify(r => r.UpdateAsync(mappedUser), Times.Never);
            _userRepositoryMock.Verify(r => r.AddUserAsync(It.IsAny<User>()), Times.Once);
        }
        [Test]
        public async Task IsLoginSuccessfulAsync_LoginSuccessful_UserUpdated()
        {
            // Arrange
            var ldapHost = "ldap://test-server";
            var domain = "test-domain";
            var username = "testuser";
            var password = "testpassword";

            var response = new UserDetailsVM
            {
                IsAuthenticated = true,
                UserName = username
            };


            _contextWrapper
                .Setup(s => s.ValidateCredentialsAndGetUserDetailsAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(response);

            var existingUser = new User();
            _userRepositoryMock.Setup(r => r.GetByUserNameAsync(username)).ReturnsAsync(existingUser);

            var mappedUser = new User();
            _mapperMock.Setup(m => m.Map<User>(response)).Returns(mappedUser);

            // Act
            var result = await _authenticationHandler.IsLoginSuccessfulAsync(ldapHost, domain, username, password);

            // Assert
            Assert.IsTrue(result);
            _userRepositoryMock.Verify(r => r.UpdateAsync(mappedUser), Times.Once);
            _userRepositoryMock.Verify(r => r.AddUserAsync(It.IsAny<User>()), Times.Never);
        }

        [Test]
        public async Task IsLoginSuccessfulAsync_LoginFailed_ReturnsFalse()
        {
            // Arrange
            var ldapHost = "ldap://test-server";
            var domain = "test-domain";
            var username = "testuser";
            var password = "testpassword";

            var response = new UserDetailsVM()
            {
                IsAuthenticated = false,
                UserName = null
            };

            _contextWrapper.Setup(s => s.ValidateCredentialsAndGetUserDetailsAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(response);

            // Act
            var result = await _authenticationHandler.IsLoginSuccessfulAsync(ldapHost, domain, username, password);

            // Assert
            Assert.IsFalse(result);
            _userRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<User>()), Times.Never);
            _userRepositoryMock.Verify(r => r.AddUserAsync(It.IsAny<User>()), Times.Never);
        }

    }
}
