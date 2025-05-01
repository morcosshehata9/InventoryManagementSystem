using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using InventoryManagementSystem.DTOs.Account;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public AccountController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser user = new ApplicationUser
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email,
            };
            IdentityResult identityResult = await userManager.CreateAsync(user, registerDTO.Password);
            if (!identityResult.Succeeded)
            {
                foreach(var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return BadRequest(ModelState);
            }
            return Ok("Account Created Successfully!");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            ApplicationUser user = await userManager.FindByNameAsync(loginDTO.Username);
            if (user == null)
            {
                return Unauthorized("Invalid Account!");
            }
            bool found = await userManager.CheckPasswordAsync(user, loginDTO.Password);
            if (!found)
            {
                return NotFound();
            }
            IList<string> userRoles = await userManager.GetRolesAsync(user);

            List<Claim> claimsList = new List<Claim>();
            claimsList.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claimsList.Add(new Claim(ClaimTypes.Name, user.UserName));
            claimsList.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            if (userRoles != null)
            {
                foreach (var role in userRoles)
                {
                    {
                        claimsList.Add(new Claim(ClaimTypes.Role, role));
                    }
                }
            }
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"], // provider
                //audience: "", // consumer
                expires: DateTime.UtcNow.AddHours(1),
                claims: claimsList,
                signingCredentials: signingCredentials
            );

            return Ok(new
            {
                expired = DateTime.UtcNow.AddHours(1),
                token = new JwtSecurityTokenHandler().WriteToken(token) // return compact token 
            });  
        }
    }
}
