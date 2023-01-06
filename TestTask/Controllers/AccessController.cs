using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using TestTask.Application.UseCases.Users.Command;
using TestTask.Models;

namespace TestTask.Controllers
{
    public class AccessController : Controller
    {
        private readonly IMediator _mediator;

        public AccessController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                var identity = await _mediator.Send(new LoginUserCommand()
                    { UserName = model.UserName, Password = model.Password });

                var props = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity), props);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewData["ValidationMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                var identity = await _mediator.Send(new RegisterUserCommand()
                    { UserName = model.UserName, Password = model.Password });

                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ViewData["ValidationMessage"] = ex.Message;
                return View();
            }
        }
    }
}
