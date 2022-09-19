using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IdentityServer.Common.Validators
{
    public class CustomUserValidator : IUserValidator<AppUser>
    {
        public async Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            List<IdentityError> errors = new List<IdentityError>();

            string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

            if (user.Email.ToLower().EndsWith("@yandex.ru") ||
                (user.Email.ToLower().EndsWith("@mail.ru")) ||
                (user.Email.ToLower().EndsWith("ru")))

            {
                errors.Add(new IdentityError
                {
                    Code = "Incorrect Email Domen",
                    Description = "This domain is in the spam database. Choose another email service"
                });
            }
            if (Regex.IsMatch(user.Email, pattern, RegexOptions.IgnoreCase))
            {
                errors.Add(new IdentityError
                {
                    Code = "Incorrect format Email",
                    Description = "Incorrect format email adress"
                });
            }
            if (user.UserName.Contains("admin"))
            {
                errors.Add(new IdentityError
                {
                    Code = "Incorect user name",
                    Description = "User nickname must not contain the word 'admin'"
                });
            }
            if (await manager.FindByNameAsync(user.UserName) != null)
            {
                errors.Add(new IdentityError
                {
                    Code = "Incorect user name",
                    Description = "This name is already in use"
                });
            }
            if (await manager.FindByLoginAsync(user.UserName, "Incorect user name") != null)
            {
                errors.Add(new IdentityError
                {
                    Code = "Incorect user name",
                    Description = "This name is already in use"
                });
            }
            if (user.BirthDay.Year > (DateTime.Now.Year - Constants.MIN_AGE_TO_ACCESS) || 
                user.BirthDay.Year < (DateTime.Now.Year - Constants.MAX_AGE_TO_ACCESS))
            {
                errors.Add(new IdentityError
                {
                    Code = "Incorect birthdate",
                    Description = $"User birthdate must be upper than {DateTime.Now.Year - Constants.MAX_AGE_TO_ACCESS} " +
                    $"and lower than {DateTime.Now.Year - Constants.MIN_AGE_TO_ACCESS}"
                });
            }
            return await Task.FromResult(errors.Count == 0 ?
                IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
        }
    }
}
