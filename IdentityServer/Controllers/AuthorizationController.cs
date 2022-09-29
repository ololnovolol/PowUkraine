using System;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer.Models;
using IdentityServer.Services;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace IdentityServer.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly IIdentityServerInteractionService _interactionService;

        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AuthorizationController(
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            IIdentityServerInteractionService interactionService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _interactionService = interactionService;
        }

        #region StandartAuth

        // authorization/login
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var externalProviders = await _signInManager.GetExternalAuthenticationSchemesAsync();

            return View(new LoginViewModel { ReturnUrl = returnUrl, ExternalProviders = externalProviders });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            viewModel.ExternalProviders = await _signInManager.GetExternalAuthenticationSchemesAsync();

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = await _userManager.FindByEmailAsync(viewModel.Email);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, ResourceReader.GetExceptionMessage("user_not_found"));

                return View(viewModel);
            }

            var result = await _signInManager.PasswordSignInAsync(
                user.UserName,
                viewModel.Password,
                viewModel.RememberMe,
                false);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(viewModel.ReturnUrl) && Url.IsLocalUrl(viewModel.ReturnUrl))
                {
                    return Redirect(viewModel.ReturnUrl);
                }

                return BadRequest();
            }

            ModelState.AddModelError(string.Empty, ResourceReader.GetExceptionMessage("Incorrect_psw_email"));

            return View(viewModel);
        }

        // authorization/register
        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            return View(new RegisterViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if (!viewModel.AgreeAllStatements)
            {
                ModelState.AddModelError(string.Empty, ResourceReader.GetExceptionMessage("false_agree"));

                return View(viewModel);
            }

            var user = new AppUser
            {
                UserName = viewModel.UserName, Email = viewModel.Email, BirthDay = viewModel.BirthDay
            };

            var result = await _userManager.CreateAsync(user, viewModel.Password);

            if (result.Succeeded)
            {
                var a = await _userManager.AddToRoleAsync(user, "User");
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
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();
            var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);

            return Redirect(logoutRequest.PostLogoutRedirectUri);
        }

        #endregion

        #region ExternalAuth

        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUri = Url.Action(nameof(ExternalLoginCallback), "Authorization", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUri);

            return Challenge(properties, provider);
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();

            if (info == null)
            {
                return RedirectToAction("Login");
            }

            var result = await _signInManager
                .ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);

            if (result.Succeeded)
            {
                return Redirect(returnUrl);
            }

            return View(
                "ExternalRegister",
                new RegisterViewModel
                {
                    UserName = info.Principal.FindFirstValue(ClaimTypes.Name),
                    Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                    ReturnUrl = returnUrl
                });
        }

        [HttpPost]
        public async Task<IActionResult> ExternalRegister(RegisterViewModel viewModel)
        {
            if (viewModel.AgreeAllStatements)
            {
                ModelState.AddModelError(string.Empty, ResourceReader.GetExceptionMessage("false_agree"));

                return View(viewModel);
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();

            if (info == null)
            {
                return RedirectToAction("Login");
            }

            var user = new AppUser { UserName = viewModel.UserName, Email = viewModel.Email};

            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                await _userManager.AddLoginAsync(user, info);
                await _userManager.AddToRoleAsync(user, "User");

                await _signInManager.SignInAsync(user, false);

                return Redirect(viewModel.ReturnUrl);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return View(viewModel);
        }

        #endregion
    }
}