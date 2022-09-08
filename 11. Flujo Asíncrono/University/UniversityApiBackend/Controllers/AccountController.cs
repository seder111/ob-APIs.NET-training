using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Helpers;
using UniversityApiBackend.Models.DataModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using Microsoft.Extensions.Localization;

namespace UniversityApiBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly JwtSettings _jwtSettings;
        private readonly UniversityDbContext _context;
        private readonly IStringLocalizer<AccountController> _stringLocalizer;
        
        public AccountController(UniversityDbContext context, JwtSettings jwtSettings, IStringLocalizer<AccountController> stringLocalizer)
        {
            _jwtSettings = jwtSettings;
            _context = context;
            _stringLocalizer = stringLocalizer;
        }

        private IEnumerable<User> Login = new List<User>()
        {
            new User()
            {
                Id = 1,
                Email = "sergiogonzalezmerino@gmail.com",
                Name = "Admin",
                Password = "123456"
            },
            new User()
            {
                Id = 2,
                Email = "user@gmail.com",
                Name = "User 1",
                Password = "123456"
            }
        };
        

        [HttpPost]
        public async Task<ActionResult> GetToken (UserLogin userLogin)
        {
            try
            {

                var Token = new UserToken();
                var Valid = await _context.Users.AnyAsync(user => user.Email.Equals(userLogin.Username.ToLower()));
                var saludo = string.Empty;

                if (Valid)
                {
                    var user = await _context.Users.FirstOrDefaultAsync(user => user.Email.Equals(userLogin.Username.ToLower()) && user.Password.Equals(userLogin.Password));

                    Token = JwtHelpers.GenTokenKey(new UserToken()
                    {
                        EmailId = user.Email,
                        UserName = user.Name,
                        Id = user.Id,
                        GuidId = Guid.NewGuid(),
                    }, _jwtSettings);

                    saludo = string.Format(_stringLocalizer.GetString("Welcome"), user.Name);

                    
                } else
                {
                    return BadRequest("Error credenciales");
                }

                

                return Ok(new
                {
                    Saludo = saludo,
                    Token,
                });

                
            } catch (Exception e)
            {
                throw new Exception("Error al obtener el token", e);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> GetUserList()
        {
            var UserList = await _context.Users.ToListAsync();

            return Ok(UserList);
        }

        [HttpPost("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult<User>> PostModifyRols(int id,User req)
        {
            if (id != req.Id)
            {
                return BadRequest();
            }

            var userDB = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);

            if (userDB == null)
            {
                return BadRequest();
            }

            userDB.Rol = req.Rol;
            await _context.SaveChangesAsync();

            return Ok(userDB);
        }
    }
}
