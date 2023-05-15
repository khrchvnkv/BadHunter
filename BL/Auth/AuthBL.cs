using BadHunter.DAL;
using BadHunter.DAL.Models;

namespace BadHunter.BL.Auth
{
    public class AuthBL : IAuthBL
    {
        private readonly IAuthDAL _authDal;

        public AuthBL(IAuthDAL authDal)
        {
            _authDal = authDal;
        }
        public async Task<int> CreateUser(UserModel model) => 
            await _authDal.CreateUser(model);
    }
}