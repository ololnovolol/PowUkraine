using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IdentityServer.Common.Validators
{
    //// TODO Move error messages to resources
    public class CustomUserValidator : IUserValidator<AppUser>
    {
        private List<IdentityError> errors;

        public CustomUserValidator()
        {
            errors = new List<IdentityError>();
        }

        public async Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            ValidateEmail(user);
            await ValidateUserName(manager, user);
            ValidateBirthDate(user);

            return await Task.FromResult(errors.Count == 0 ?
                IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
        }

        private void ValidateBirthDate(AppUser user)
        {
            if (user.BirthDay.Year > (DateTime.Now.Year - Constants.MIN_AGE_TO_ACCESS) ||
                user.BirthDay.Year < (DateTime.Now.Year - Constants.MAX_AGE_TO_ACCESS))
            {
                AddErrorsToResult("Incorect birthdate",
                    $"User birthdate must be upper than {DateTime.Now.Year - Constants.MAX_AGE_TO_ACCESS} " +
                    $"and lower than {DateTime.Now.Year - Constants.MIN_AGE_TO_ACCESS}");
            }
        }

        private async Task ValidateUserName(UserManager<AppUser> manager, AppUser user)
        {
            if (user.UserName.Contains("admin"))
            {
                AddErrorsToResult("Incorect user name",
                    "User nickname must not contain the word 'admin'");
            }
            if (await manager.FindByNameAsync(user.UserName) != null)
            {
                AddErrorsToResult("Incorect user name",
                    "This name is already in use");
            }
            if (await manager.FindByLoginAsync(user.UserName, "Incorect user name") != null)
            {
                AddErrorsToResult("Incorect user name",
                    "This name is already in use");
            }
        }

        private void ValidateEmail(AppUser user)
        {
            string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

            if (user.Email.ToLower().EndsWith("@yandex.ru") ||
                (user.Email.ToLower().EndsWith("@mail.ru")) ||
                (user.Email.ToLower().EndsWith("ru")))

            {
                AddErrorsToResult("Incorrect Email Domen",
                    "This domain is in the spam database. Choose another email service");
            }
            if (!Regex.IsMatch(user.Email, pattern, RegexOptions.IgnoreCase))
            {
                AddErrorsToResult("Incorrect format Email",
                    "Incorrect format email adress");
            }
        }

        private void AddErrorsToResult(string code, string description)
        {
            errors.Add(new IdentityError
            {
                Code = code,
                Description = description
            });
        }
    }
}
