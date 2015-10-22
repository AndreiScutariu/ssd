using NHibernate;
using NUnit.Framework;
using Ssd.Repository.Entities;
using Ssd.Repository.Managers;
using Ssd.Repository.Security.Encryptions;
using Ssd.Repository.Tests.Builder;
using Ssd.Repository.Utils;

namespace Ssd.Repository.Tests
{
    public class UserManagerTests
    {
        protected readonly UserManager UserManager = new UserManager();

        public abstract class WhenAddingAnUser : UserManagerTests
        {
            protected const string Password = "somepassword";
            protected int SavedUserId;
            protected readonly ISession Session = Database.OpenSession();
            protected ITransactionManager TransactionManager = new NHiberbateTransactionManager();

            [TestFixtureSetUp]
            public abstract void TestFixtureSetUp();

            [Test]
            public void ThenUserIsSaved()
            {
                Assert.IsTrue(SavedUserId > 0);
            }

            [Test]
            public void ThenPasswordIsEncrypted()
            {
                var user = Session.Get<User>(SavedUserId);
                Assert.AreEqual(StringEncrypter.DoHash(Password), user.Password);
            }
        }

        [TestFixture]
        public class WhenAddingUserWithoutGenerics : WhenAddingAnUser
        {
            public override void TestFixtureSetUp()
            {
                var user = new UserBuilder().WithPassword(Password).Build();
                TransactionManager.RunInTransaction(() =>
                {
                    SavedUserId = (int)Session.Save(user);
                });
            }
        }

        [TestFixture]
        public class WhenAddingUserWithGenerics : WhenAddingAnUser
        {
            public override void TestFixtureSetUp()
            {
                var user = new UserBuilder().WithPassword(Password).Build();
                SavedUserId = UserManager.Save(user);
            }
        }
    }
}