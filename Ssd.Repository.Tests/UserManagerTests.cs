using NUnit.Framework;
using Ssd.Repository.Managers;
using Ssd.Repository.Security.Encryptions;
using Ssd.Repository.Tests.Builder;

namespace Ssd.Repository.Tests
{
    public class UserManagerTests
    {
        protected UserManager UserManager;

        public UserManagerTests()
        {
            UserManager = new UserManager();
        }

        [TestFixture]
        public class WhenAddingAnUser : UserManagerTests
        {
            private const string Password = "somepassword";
            private int _savedUserId;

            [TestFixtureSetUp]
            public void TestFixtureSetUp()
            {
                var user = new UserBuilder().WithPassword(Password).Build();
                _savedUserId = UserManager.Save(user);
            }

            [Test]
            public void ThenUserIsSaved()
            {
                Assert.IsTrue(_savedUserId > 0);
            }

            [Test]
            public void ThenPasswordIsEncrypted()
            {
                var user = UserManager.Get(_savedUserId);
                Assert.AreEqual(user.Password, StringEncrypter.DoHash(Password));
            }
        }
    }
}
