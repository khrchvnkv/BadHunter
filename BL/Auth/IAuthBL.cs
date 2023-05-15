using BadHunter.DAL.Models;

namespace BadHunter.BL.Auth
{
    public interface IAuthBL
    {
        Task<int> CreateUser(UserModel model);
    }
}