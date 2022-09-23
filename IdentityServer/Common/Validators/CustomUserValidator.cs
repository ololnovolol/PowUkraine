using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace IdentityServer.Common.Validators
{
    //// TODO Move error messages to resources
    public class CustomUserValidator : IUserValidator<AppUser>
    {
        private readonly IConfiguration _config;
        private readonly List<IdentityError> _errors;

        public CustomUserValidator(IConfiguration config)
        {
            _config = config;
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
            if (user.BirthDay.Year > (DateTime.Now.Year - Constants.MinAgeToAccess) ||
                user.BirthDay.Year < (DateTime.Now.Year - Constants.MaxAgeToAccess))
            {
                AddErrorsToResult(
                    "Incorrect birthdate",
                    $"User birthdate must be upper than {DateTime.Now.Year - Constants.MaxAgeToAccess} " +
                    $"and lower than {DateTime.Now.Year - Constants.MinAgeToAccess}");
            }
        }

        private async Task ValidateUserName(UserManager<AppUser> manager, AppUser user)
        {
            if (user.UserName.Contains("admin"))
            {
                AddErrorsToResult(
                    "Incorrect user name",
                    "User nickname must not contain the word 'admin'");
            }

            if (await manager.FindByNameAsync(user.UserName) != null)
            {
                AddErrorsToResult(
                    "Incorect user name",
                    "This name is already in use");
            }

            if (await manager.FindByLoginAsync(user.UserName, "Incorrect user name") != null)
            {
                AddErrorsToResult(
                    "Incorect user name",
                    "This name is already in use");
            }
        }

        private void ValidateEmail(AppUser user)
        {
            var pattern =
                @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

            if (user.Email.ToLower().EndsWith("@yandex.ru") ||
                user.Email.ToLower().EndsWith("@mail.ru") ||
                user.Email.ToLower().EndsWith("ru"))

            {
                AddErrorsToResult(
                    "Incorrect Email Domen",
                    "This domain is in the spam database. Choose another email service");
            }

            if (!Regex.IsMatch(user.Email, pattern, RegexOptions.IgnoreCase))
            {
                AddErrorsToResult(
                    "Incorrect format Email",
                    "Incorrect format email adress");
            }
        }

        private void AddErrorsToResult(string code, string description)
        {
            _errors.Add(new IdentityError { Code = code, Description = description });
        }
    }
}