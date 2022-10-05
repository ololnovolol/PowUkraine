using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer.Models;
using IdentityServer.Services;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

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
            IEnumerable<AuthenticationScheme> externalProviders =
                await _signInManager.GetExternalAuthenticationSchemesAsync();

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

            AppUser user = await _userManager.FindByEmailAsync(viewModel.Email);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, ResourceReader.GetExceptionMessage("user_not_found"));

                return View(viewModel);
            }

            SignInResult result = await _signInManager.PasswordSignInAsync(
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
        public IActionResult Register(string returnUrl) => View(new RegisterViewModel { ReturnUrl = returnUrl });

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

            AppUser user = new AppUser
            {
                UserName = viewModel.UserName,
                Email = viewModel.Email,
                BirthDay = viewModel.BirthDay,
            };

            IdentityResult result = await _userManager.CreateAsync(user, viewModel.Password);

            if (result.Succeeded)
            {
                IdentityResult a = await _userManager.AddToRoleAsync(user, "User");
                await _signInManager.SignInAsync(user, false);

                return Redirect(viewModel.ReturnUrl);
            }

            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();
            LogoutRequest logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);

            return Redirect(logoutRequest.PostLogoutRedirectUri);
        }

        #endregion

        #region ExternalAuth

        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            string redirectUri = Url.Action(nameof(ExternalLoginCallback), "Authorization", new { returnUrl });

            AuthenticationProperties properties =
                _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUri);

            return Challenge(properties, provider);
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl)
        {
            ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();

            if (info == null)
            {
                return RedirectToAction("Login");
            }

            SignInResult result = await _signInManager
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
                    ReturnUrl = returnUrl,
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

            ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();

            if (info == null)
            {
                return RedirectToAction("Login");
            }

            AppUser user = new AppUser { UserName = viewModel.UserName, Email = viewModel.Email };

            IdentityResult result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                await _userManager.AddLoginAsync(user, info);
                await _userManager.AddToRoleAsync(user, "User");

                await _signInManager.SignInAsync(user, false);

                return Redirect(viewModel.ReturnUrl);
            }

            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return View(viewModel);
        }

        #endregion
    }
}
