using System.Security.Cryptography;
using BadHunter.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BadHunter.Controllers
{
    public class ProfileController : Controller
    {
        [HttpGet]
        [Route("/profile")]
        public IActionResult Index()
        {
            return View(new ProfileViewModel());
        }
        
        [HttpPost] 
        [Route("/profile")]
        public async Task<IActionResult> IndexSave()
        {
            string fileName = string.Empty;
            var imageData = Request.Form.Files[0];
            if (imageData != null)
            {
                MD5 md5Hash = MD5.Create();
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(imageData.FileName);
                byte[] hashBytes = md5Hash.ComputeHash(inputBytes);

                string hash = Convert.ToHexString(hashBytes);
                var dir = "./wwwroot/images/" + hash.Substring(0, 2) + "/" +
                          hash.Substring(0, 4);

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                fileName = dir + "/" + imageData.FileName;
                using (var stream = System.IO.File.Create(fileName))
                {
                    await imageData.CopyToAsync(stream);
                }
            }

            return View("Index", new ProfileViewModel());
        }
    }
}