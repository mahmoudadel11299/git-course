using LapShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LapShop.Apicontroller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        IConfiguration _config;
        public LoginController(IConfiguration config)
        {
            _config = config;
        }
        // GET: api/<LoginController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LoginController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LoginController>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] UserModel value)
        {
            var responce = Unauthorized();
            UserModel user= AuthorizeUser(value);
            if (user != null) 
            {
                var token = GenerateToken(user);
                return Ok(new {token=token});
            }
            return responce;
        }

        string GenerateToken(UserModel user)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var credentials=new SigningCredentials(securitykey,SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["JWT:Issuer"],
            _config["JWT:Issuer"],
            null,
            expires:DateTime.Now.AddMinutes(120),
            signingCredentials:credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        UserModel AuthorizeUser(UserModel user)
        {
            if(user.FirstName=="ali" && user.Password == "123")
            {
                return new UserModel()
                {
                    FirstName="ali",
                    Email="info@ali.com"
                };
            }
            return null;
        }
    }
}
