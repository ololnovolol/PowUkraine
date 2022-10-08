using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using IdentityServer.Models;
using IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace IdentityServer.Common.Validators
{
    public class CustomUserValidator : IUserValidator<AppUser>
    {
        private readonly List<IdentityError> _errors;

        public CustomUserValidator(IConfiguration configuration) => _errors = new List<IdentityError>();

        public async Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            ValidateEmail(user);

            return await Task.FromResult(
                _errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(_errors.ToArray()));
        }

        private void ValidateEmail(AppUser user)
        {
            const string pattern =
                @"^([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$";

            if (user.Email.EndsWith("@yandex.ru", StringComparison.OrdinalIgnoreCase) ||
                user.Email.EndsWith("@mail.ru", StringComparison.OrdinalIgnoreCase) ||
                user.Email.EndsWith("ru", StringComparison.OrdinalIgnoreCase))
            {
                AddErrorsToResult(
                    "incorrect_email_domen",
                    ResourceReader.GetExceptionMessage("incorrect_email_domen"));
            }

            if (!Regex.IsMatch(user.Email, pattern, RegexOptions.IgnoreCase))
            {
                AddErrorsToResult(
                    "incorrect_email_format",
                    ResourceReader.GetExceptionMessage("incorrect_email_format"));
            }
        }

        private void AddErrorsToResult(string code, string description)
            => _errors.Add(new IdentityError { Code = code, Description = description });
    }
}
