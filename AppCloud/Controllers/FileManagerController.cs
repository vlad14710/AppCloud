using Microsoft.AspNetCore.Mvc;
using AppCloud.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;


namespace AppCloud.Controllers
{
    
        [ApiController]
        public class FileManagerController : ControllerBase
        {
            private readonly IManageImage _iManageImage;
            private readonly UserManager<AppUser> _userManager;
            public FileManagerController(IManageImage iManageImage, UserManager<AppUser> userManager)
            {
                _iManageImage = iManageImage;
                _userManager = userManager;
            }

            [HttpPost]
            [Route("uploadfile"),Authorize]
            public async Task<IActionResult> UploadFile(IFormFile _IFormFile)
            {
                var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var user = _userManager.FindByIdAsync(userId).Result;
                var result = await _iManageImage.UploadFile(_IFormFile);
                string name = "defult";
                name = user.UserName;
                return Ok(name);
            }

            [HttpGet]
            [Route("downloadfile")]
            public async Task<IActionResult> DownloadFile(string FileName)
            {
                var result = await _iManageImage.DownloadFile(FileName);
                return File(result.Item1, result.Item2, result.Item2);
            }
        }
    
}
