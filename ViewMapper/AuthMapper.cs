using BadHunter.DAL.Models;
using BadHunter.ViewModels;

namespace BadHunter.ViewMapper
{
    public static class AuthMapper
    {
        public static UserModel MapRegisterViewModelToUserModel(RegisterViewModel viewModel)
        {
            return new UserModel()
            {
                Email = viewModel.Email!,
                Password = viewModel.Password!
            };
        }
    }
}