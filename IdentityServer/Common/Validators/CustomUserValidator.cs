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
        private readonly IConfiguration _configuration;
        private readonly List<IdentityError> _errors;

        public CustomUserValidator(IConfiguration configuration)
        {
            _configuration = configuration;
            _errors = new List<IdentityError>();
        }

        public async Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            ValidateEmail(user);
            await ValidateUserName(manager, user);
            ValidateBirthDate(user);

            return await Task.FromResult(
                _errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(_errors.ToArray()));
        }

        private void ValidateBirthDate(AppUser user)
        {
            if (user.BirthDay.Year > DateTime.Now.Year - int.Parse(_configuration["constants:minAge"]) ||
                user.BirthDay.Year < DateTime.Now.Year - int.Parse(_configuration["constants:maxAge"]))
            {
                AddErrorsToResult(
                    "Incorrect_birthdate",
                    ResourceReader.GetExceptionMessage("Incorrect_birthdate"));
            }
        }

        private async Task ValidateUserName(UserManager<AppUser> manager, AppUser user)
        {
            if (user.UserName.ToLower().Contains("admin"))
            {
                AddErrorsToResult(
                    "incorrect_username",
                    ResourceReader.GetExceptionMessage("incorrect_username"));
            }

            if (await manager.FindByNameAsync(user.UserName) != null)
            {
                AddErrorsToResult(
                    "already_use_name",
                    ResourceReader.GetExceptionMessage("already_use_name"));
            }

            if (await manager.FindByLoginAsync(user.UserName, "Incorrect user name") != null)
            {
                AddErrorsToResult(
                    "already_use_name",
                    ResourceReader.GetExceptionMessage("already_use_name"));
            }
        }

        private void ValidateEmail(AppUser user)
        {
            const string pattern =
                @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])
                @))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

            if (user.Email.ToLower().EndsWith("@yandex.ru") ||
                user.Email.ToLower().EndsWith("@mail.ru") ||
                user.Email.ToLower().EndsWith("ru"))

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
        {
            _errors.Add(new IdentityError { Code = code, Description = description });
        }
    }
}