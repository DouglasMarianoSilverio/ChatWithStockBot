using CWSB.WebApp.MVC.Models;
using CWSB.WebApp.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CWSB.WebApp.MVC.Controllers
{
    public class IdentityController : Controller
    {
        private readonly IUserAuthenticationService _authenticationService;

        public IdentityController(IUserAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet]
        [Route("new-account")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("new-account")]
        public async Task<IActionResult> Register(UserRegister userRegister)
        {
            if (!ModelState.IsValid) return View(userRegister);

            var result = await _authenticationService.Register(userRegister);

            //if (ResponsePossuiErros(resposta.ResponseResult)) return View(usuarioRegistro);

            //await RealizarLogin(resposta);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLogin userLogin, string returnUrl = null)
        {
            //ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid) return View(userLogin);

            var result = await _authenticationService.Login(userLogin);

            //if (ResponsePossuiErros(resposta.ResponseResult)) return View(usuarioLogin);

            //await RealizarLogin(resposta);

            if (string.IsNullOrEmpty(returnUrl)) return RedirectToAction("Index", "Home");

            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        [Route("exit")]
        public async Task<IActionResult> Logout()
        {
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        private async Task DoLogin(UserLoginResponse response)
        {
            //var token = ObterTokenFormatado(resposta.AccessToken);

            //var claims = new List<Claim>();
            //claims.Add(new Claim("JWT", resposta.AccessToken));
            //claims.AddRange(token.Claims);

            //var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            //var authProperties = new AuthenticationProperties
            //{
            //    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
            //    IsPersistent = true
            //};

            //await HttpContext.SignInAsync(
            //    CookieAuthenticationDefaults.AuthenticationScheme,
            //    new ClaimsPrincipal(claimsIdentity),
            //    authProperties);
        }

        //private static JwtSecurityToken ObterTokenFormatado(string jwtToken)
        //{
        //    return new JwtSecurityTokenHandler().ReadToken(jwtToken) as JwtSecurityToken;
        //}
    }
}
