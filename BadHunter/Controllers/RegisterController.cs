using BadHunter.BL.Auth;
using BadHunter.ViewMapper;
using BadHunter.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BadHunter.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IAuthBL _authBl;

        public RegisterController(IAuthBL authBl)
        {
            _authBl = authBl;
        }

        [HttpGet]
        [Route("/register")]
        public IActionResult Index()
        {
            return View(new RegisterViewModel());
        }
        
        [HttpPost]
        [Route("/register")]
        public async Task<IActionResult> IndexSave(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var errorModel = await _authBl.ValidateEmail(model.Email!);
                if (errorModel != null)
                {
                    ModelState.AddModelError(nameof(model.Email), errorModel.ErrorMessage!);
                }
            }
            
            if (ModelState.IsValid)
            {
                await _authBl.CreateUser(AuthMapper.MapRegisterViewModelToUserModel(model));
                return Redirect("/"); 
            }
            return View("Index", new RegisterViewModel());
        }
    }
}