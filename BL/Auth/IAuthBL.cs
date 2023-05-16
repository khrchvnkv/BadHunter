using System.ComponentModel.DataAnnotations;
using BadHunter.DAL.Models;

namespace BadHunter.BL.Auth
{
    public interface IAuthBL
    {
        Task<int> CreateUser(UserModel model);
        Task<int>  Authentificate(string modelEmail, string modelPassword, bool modelRememberMe);
        Task<ValidationResult?> ValidateEmail(string email);
    }
}