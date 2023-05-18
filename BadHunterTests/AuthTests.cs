using System.Transactions;
using BadHunter.BL.Exceptions;
using BadHunter.DAL.Models;
using BadHunterTests.Helpers;

namespace BadHunterTests;

public class AuthTests : BaseTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task BaseAuthTest()
    {
        using (TransactionScope scope = Helper.CreateTransactionScope( ))
        {
            string email = Guid.NewGuid() + "@test.com ";
            
            int userId = await AuthBl.CreateUser(new UserModel()
            {
                Email = email,
                Password = "123"
            });

            Assert.Throws<AuthorizationException>(() => 
                AuthBl.Authentificate("", "", false).GetAwaiter().GetResult());
            Assert.Throws<AuthorizationException>(() => 
                AuthBl.Authentificate("", "123", false).GetAwaiter().GetResult());
            Assert.Throws<AuthorizationException>(() => 
                AuthBl.Authentificate(email, "", false).GetAwaiter().GetResult());
            await AuthBl.Authentificate(email, "123", false);
        }
    }
}