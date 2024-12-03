
using AccessControl.Infrastructure.Repositories;
using AccessControl.Test.TestHelpers.TestContexts;

namespace AccessControl.Test.Repository
{

    [TestFixture]
    public class UserRepositoryTests
    {

        private UserRepository _userRepository;
        private AccessControlTestContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new AccessControlTestContext();
            _userRepository = new UserRepository(_context.Context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }
        [Test]
        public async Task AddUserAsync_ShouldAddUserToDatabase()
        {
            // Arrange
            var user = _context.User.DefaultUser;

            // Act
            await _userRepository.AddUserAsync(user);

            // Assert
            var addedUser = await _context.Context.Users.FindAsync(user.Id);
            Assert.NotNull(addedUser);
            Assert.AreEqual(user.UserName, addedUser.UserName);
        }

        [Test]
        public async Task UpdateAsync_ShouldUpdateUserDetails()
        {
            // Arrange
            var user = await _context.User.CreateDefaultUser();

            // Act
            var newEmail = "new@example.com";
            user.Email = newEmail;
            await _userRepository.UpdateAsync(user);

            // Assert
            var updatedUser = await _context.Context.Users.FindAsync(user.Id);
            Assert.NotNull(updatedUser);
            Assert.AreEqual(newEmail, updatedUser.Email);
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var user = await _context.User.CreateDefaultUser();

            // Act
            var result = await _userRepository.GetByIdAsync(user.Id);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(user.UserName, result.UserName);
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {

            // Act
            var result = await _userRepository.GetByIdAsync(999999);
            
            //Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetByUserNameAsync_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var user =await _context.User.CreateDefaultUser();

            // Act
            var result = await _userRepository.GetByUserNameAsync(user.UserName);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(user.Id, result.Id);
        }

        [Test]
        public async Task GetByUserName_ShouldReturnNull_WhenUserDoesNotExist()
        {


            // Act
            var result = await _userRepository.GetByUserNameAsync("nonexistent");

            // Assert
            Assert.Null(result);
        }
    }
}
