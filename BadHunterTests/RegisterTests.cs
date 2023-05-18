using System.Transactions;
using BadHunter.DAL.Models;
using BadHunterTests.Helpers;

namespace BadHunterTests;

public class RegisterTests : BaseTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task BaseRegistrationTest()
    {
        using (TransactionScope scope = Helper.CreateTransactionScope( ))
        {
            string email = Guid.NewGuid() + "@test.com ";
            var emailValidationResult = await AuthBl.ValidateEmail(email);
            Assert.IsNull(emailValidationResult);
            
            int userId = await AuthBl.CreateUser(new UserModel()
            {
                Email = email,
                Password = "123"
            });

            var userById = await AuthDal.GetUser(userId);
            Assert.Equals(email, userById.Email);
            Assert.IsNotNull(userById.Salt);
            
            var userByEmail = await AuthDal.GetUser(email);
            Assert.Equals(email, userByEmail.Email);
            Assert.IsNotNull(userByEmail .Salt);
            
            emailValidationResult = await AuthBl.ValidateEmail(email);
            Assert.Greater(userId, 0);
            Assert.IsNotNull(emailValidationResult);

            var hashPassword = Encrypter.HashPassword("123", userByEmail.Salt);
            Assert.Equals(userByEmail.Password, hashPassword); 
        }
    }
}