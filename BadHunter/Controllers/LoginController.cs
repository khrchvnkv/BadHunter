using BadHunter.BL.Auth;
using BadHunter.BL.Exceptions;
using BadHunter.ViewMapper;
using BadHunter.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BadHunter.Controllers
{
    public class LoginController : Controller
    { 
        private readonly IAuthBL _authBl;

        public LoginController(IAuthBL authBl)
        {
            _authBl = authBl;
        }
        
        [HttpGet]
        [Route("/login")]
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }
        
        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> IndexSave(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _authBl.Authentificate(model.Email!, model.Password!, model.RememberMe == true);
                    return Redirect("/");
                }
                catch (AuthorizationException)
                {
                    ModelState.AddModelError(nameof(model.Email), "Email or password incorrect");
                }
                 
            }
            return View("Index", new LoginViewModel());
        }
    }
}