using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CPMS.Models;
using CPMS.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CPMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepo _IAdminRepo;
        private readonly IHostingEnvironment _hostEnvironment;
        private IConfiguration _config;
        public AdminController(IAdminRepo _IAdminRepo, IHostingEnvironment hostingEnvironment, IConfiguration config)
        {
            this._IAdminRepo = _IAdminRepo;
            _hostEnvironment = hostingEnvironment;
            _config = config;
        }

        [HttpPost("create-admin")]
        public async Task<IActionResult> CreateAdmin([FromForm]Admin admin)
        {
            admin.ProfileImageName = await SaveFile(admin.ProfileImageFile);
            var res = await _IAdminRepo.CreateAdmin(admin);
            if(res == false)
            {
                return StatusCode(501);
            }

            return Ok(new { message = "Admin created successfully" });
        }

        [HttpGet("admin-details/{id}")]
        public async Task<IActionResult> GetAdminById(int id)
        {
            var admin = await _IAdminRepo.GetAdminById(id);
            if(admin == null)
            {
                return NotFound();
            }
            admin.ProfileImageSrc = String.Format("{0}://{1}{2}/UploadFiles/{3}", Request.Scheme, Request.Host, Request.PathBase, admin.ProfileImageName);
            return Ok(admin);
        }

        [AllowAnonymous]
        [HttpPost("admin-signin")]
        public async Task<IActionResult> AdminSignIn(string email, string password)
        {
            var user = await Authenticate(email, password);
            if (user != null)
            {
                var token = Generate(user);
                return Ok(new
                {
                    message = "SignIn successfull",
                    UserId = user.Id,
                    Role = "Admin",
                    Token = token
                });
            }

            return NotFound(new { message = "User Not Found" });
        }

        [NonAction]
        private string Generate(Admin user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                 new Claim(ClaimTypes.NameIdentifier, user.Name),
                 new Claim(ClaimTypes.Email, user.Email),
                 new Claim(ClaimTypes.Role, "Admin")
             };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [NonAction]
        private async Task<Admin> Authenticate(string email, string password)
        {
            var admin = await _IAdminRepo.AdminSignIn(email, password);
            return admin;
        }

        [NonAction]
        public async Task<string> SaveFile(IFormFile file)
        {
            string fileName = new String(Path.GetFileNameWithoutExtension(file.FileName).Take(10).ToArray()).Replace(' ', '-');
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(_hostEnvironment.ContentRootPath, "UploadFiles", fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return fileName;
        }

        [NonAction]
        public void DeleteFile(string fileName)
        {
            var filePath = Path.Combine(_hostEnvironment.ContentRootPath, "UploadFiles", fileName);
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);
        }



    }
}