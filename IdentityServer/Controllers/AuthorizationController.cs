using IdentityServer.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IdentityServer.Controllers
{
    public class AuthorizationController : Controller
    {

        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IIdentityServerInteractionService _interactionService;

        public AuthorizationController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager,
            IIdentityServerInteractionService interactionService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _interactionService = interactionService;
        }

        // authorization/login
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = await _userManager.FindByEmailAsync(viewModel.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found");
                return View(viewModel);
            }

            var result = await _signInManager.PasswordSignInAsync(viewModel.Email,
                viewModel.Password, viewModel.RememberMe, false);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(viewModel.ReturnUrl) && Url.IsLocalUrl(viewModel.ReturnUrl))
                {
                    return Redirect(viewModel.ReturnUrl);
                }
            }

            ModelState.AddModelError(string.Empty, "Incorrect email (or) password");
            return View(viewModel);
        }

        // authorization/register
        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            return View(new RegisterViewModel() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if (!viewModel.agreeAllStatements)
            {
                ModelState.AddModelError(String.Empty, "You must agree all statements");
                return View(viewModel);
            }

            var user = new AppUser
            {
                UserName = viewModel.UserName,
                Email = viewModel.Email,
                BirthDay = viewModel.BirthDay,
            };

            var result = await _userManager.CreateAsync(user, viewModel.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return Redirect(viewModel.ReturnUrl);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
            return View(viewModel);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();
            var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);
            return Redirect(logoutRequest.PostLogoutRedirectUri);
        }
    }
}
