using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        [HttpPost("[action]")]
        public string Authenticate()
        {
            return "Olá Mundo!";
        }

        [HttpPost("[action]")]
        public string ResetPassword()
        {
            return "Olá Mundo!";
        }

        [HttpPost("[action]")]
        public string SignUp()
        {
            return "Olá Mundo!";
        }
    }
}
