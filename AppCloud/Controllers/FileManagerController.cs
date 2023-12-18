using Microsoft.AspNetCore.Mvc;
using AppCloud.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace AppCloud.Controllers
{
    
        [ApiController]
        public class FileManagerController : ControllerBase
        {
            private readonly IManageImage _iManageImage;
            private readonly UserManager<AppUser> _userManager;
            private readonly AppDbContext _context;
            public FileManagerController(AppDbContext context ,IManageImage iManageImage, UserManager<AppUser> userManager)
            {
                _context = context;
                _iManageImage = iManageImage;
                _userManager = userManager;

            }

            [HttpPost]
            [Route("uploadfile"),Authorize]
            public async Task<IActionResult> UploadFile( IFormFile _IFormFile,[FromForm] FileModelTemporary filedata)
            {
                var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var user = _userManager.FindByIdAsync(userId).Result;

                var result = await _iManageImage.UploadFile(_IFormFile, filedata);

                var fileModel = new FileModel
                {
                    Name = result,
                    Info = filedata.Info,
                    User = user.UserName,  
                    AppUser = user
                };
                _context.Attach(fileModel);
                _context.Entry(fileModel).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                _context.SaveChanges();

                return Ok(user.UserName);
            }

            [HttpGet]
            [Route("getallfiles/{FileName}"), Authorize]
            public async Task<IActionResult> DownloadFile(string FileName)
            {
                var result = await _iManageImage.DownloadFile(FileName);
                return File(result.Item1, result.Item2, result.Item2);
            }

            [HttpGet]
            [Route("getallfiles"), Authorize]
            public async Task<IActionResult> GetAllFiles()
            {
            
            
                var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var user = _userManager.FindByIdAsync(userId).Result;

               
                var files = _context.Files.Where(fm => fm.User == user.Id).ToList();


                var jsonOptions = new JsonSerializerOptions
                {
                ReferenceHandler = ReferenceHandler.Preserve,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All)
                };

                return Ok(JsonSerializer.Serialize(files, jsonOptions));

            }


        
            [HttpDelete("{fileName}")]
            [Authorize] 
            public IActionResult DeleteFile(string fileName)
            {
                try
                {
                    var filePath = Helper.Common.GetFilePath(fileName);

                   
                    if (System.IO.File.Exists(filePath))
                    {

                        System.IO.File.Delete(filePath);
                        var file = _context.Files.SingleOrDefault(fm => fm.Name == fileName);
                        _context.Files.Remove(file);
                        _context.SaveChanges();
                    return Ok($"Файл {fileName} успішно видалено");
                    }
                    else
                    {
                        return NotFound($"Файл {fileName} не знайдено");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Помилка видалення файлу: {ex.Message}");
                }
            }
        






    }   
    
}
