using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ViewModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate([FromBody]LoginModel login)
        {
            if (login.Username == "123" && login.Password == "123")
            {
                var identity = new ClaimsIdentity("password");
                identity.AddClaim(new Claim(ClaimTypes.Role, "User"));
                HttpContext.Authentication.SignInAsync("ApiAuth", new ClaimsPrincipal(identity)).Wait();
            }
            else
            {
                return Unauthorized();
            }
            return Ok(true);
        }
        // GET api/values
        [HttpGet]
        [Route("get")]
        [Authorize(Roles = "User")]
        public IEnumerable<LoginModel> Get()
        {
            return new LoginModel[] { new LoginModel { Password = "1", Username = "1" }, new LoginModel { Username = "2", Password = "2" } };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
