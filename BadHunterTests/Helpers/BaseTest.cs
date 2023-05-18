using BadHunter.BL.Auth;
using BadHunter.DAL;
using Microsoft.AspNetCore.Http;

namespace BadHunterTests.Helpers
{
    public class BaseTest
    {
        protected readonly IAuthDAL AuthDal = new AuthDAL();
        protected readonly IEncrypter Encrypter = new Encrypter();
        protected readonly IHttpContextAccessor HttpContextAccessor = new HttpContextAccessor();
        
        protected readonly IAuthBL AuthBl;

        public BaseTest()
        {
            AuthBl = new AuthBL(AuthDal, Encrypter, HttpContextAccessor);
        }
    }
}