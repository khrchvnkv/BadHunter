using BadHunter.DAL;
using BadHunter.DAL.Models;

namespace BadHunter.BL.Auth
{
    public class AuthBL : IAuthBL
    {
        private readonly IAuthDAL _authDal;
        private readonly IEncrypter _encrypter;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthBL(IAuthDAL authDal, 
            IEncrypter encrypter,
            IHttpContextAccessor contextAccessor)
        {
            _authDal = authDal;
            _encrypter = encrypter;
            _httpContextAccessor = contextAccessor;
        }
        public async Task<int> CreateUser(UserModel model)
        {
            model.Salt = Guid.NewGuid().ToString();
            model.Password = _encrypter.HashPassword(model.Password, model.Salt);
            int id = await _authDal.CreateUser(model);
            Login(id);
            return id;
        }
        public void Login(int id)
        {
            _httpContextAccessor.HttpContext?.Session.SetInt32(AuthConstants.AUTH_SESSION_PARAM_NAME, id);
        }
    }
}