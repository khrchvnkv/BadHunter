using BadHunter.BL.Auth;
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
                await _authBl.Authentificate(model.Email!, model.Password!, model.RememberMe == true);
                return Redirect("/"); 
            }
            return View("Index", new LoginViewModel());
        }
    }
}