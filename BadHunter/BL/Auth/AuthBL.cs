using System.ComponentModel.DataAnnotations;
using BadHunter.BL.Exceptions;
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
        public async Task<int> Authentificate(string modelEmail, string modelPassword, bool modelRememberMe)
        {
            var user = await _authDal.GetUser(modelEmail);
            if (user.UserId != null &&
                user.Password == _encrypter.HashPassword(modelPassword , user.Salt))
            {
                Login(user.UserId ?? 0);
                return user.UserId ?? 0;
            }
            throw new AuthorizationException();
        }
        public async Task<ValidationResult?> ValidateEmail(string email)
        {
            var user = await _authDal.GetUser(email);
            if (user.UserId.HasValue)
            {
                return new ValidationResult("Email already exists");
            }
            return null;
        }
        private void Login(int id)
        {
            _httpContextAccessor.HttpContext?.Session.SetInt32(AuthConstants.AUTH_SESSION_PARAM_NAME, id);
        }
    }
}