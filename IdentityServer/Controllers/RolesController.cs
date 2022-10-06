using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace IdentityServer.Controllers
{
    [Route("api/[controller]/[action]")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RolesController(
            RoleManager<IdentityRole> roleManager,
            UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            IQueryable<AppUser> users = _userManager.Users;
            IList<AppUser> roles = await _userManager.GetUsersInRoleAsync("Admin");

            List<UserVm> result = new List<UserVm>();

            foreach (AppUser user in users)
            {
                result.Add(
                    new UserVm
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        BirthDay = user.BirthDay.ToShortDateString(),
                        Role = roles.Contains(user) ? "Admin" : "User",
                    });
            }

            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetUser([FromBody] StringParameter id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            AppUser user = await _userManager.FindByIdAsync(id.Data);

            if (user is null)
            {
                return BadRequest();
            }

            UserVm result = new UserVm
            {
                FirstName = user.FirstName ?? string.Empty,
                LastName = user.LastName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                BirthDay = user.BirthDay.ToShortDateString(),
            };

            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserVm updateUser)
        {
            if (updateUser is null)
            {
                return BadRequest();
            }

            AppUser user = await _userManager.FindByIdAsync(updateUser.UserId);

            if (user is null)
            {
                return BadRequest();
            }

            user.FirstName = updateUser.FirstName ?? string.Empty;
            user.LastName = updateUser.LastName ?? string.Empty;
            user.Email = updateUser.Email ?? string.Empty;
            user.PhoneNumber = updateUser.PhoneNumber ?? string.Empty;
            DateTime.TryParse(updateUser.BirthDay, out DateTime parseDate);

            if (parseDate != DateTime.MinValue)
            {
                user.BirthDay = parseDate;
            }

            IdentityResult result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser([FromBody] StringParameter email)
        {
            if (email.Data is null) return BadRequest();

            var user = await _userManager.FindByEmailAsync(email.Data);

            if (user is null) return BadRequest();

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            bool isRole = await _userManager.IsInRoleAsync(user, "Admin");

            if (isRole)
            {
                await _userManager.RemoveFromRoleAsync(user, "Admin");
            }
            else
            {
                await _userManager.RemoveFromRoleAsync(user, "User");
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRole([FromBody] StringParameter email)
        {
            if (email.Data is null) return BadRequest();

            var user = await _userManager.FindByEmailAsync(email.Data);

            if (user is null) return BadRequest();

            bool isRole = await _userManager.IsInRoleAsync(user, "Admin");

            if (isRole)
            {
                var resultRemove = await _userManager.RemoveFromRoleAsync(user, "Admin");
                var resultUpdate = await _userManager.AddToRoleAsync(user, "User");
            }
            else
            {
                // var resultRemove = await _userManager.RemoveFromRoleAsync(user, "User");
                var resultUpdate = await _userManager.AddToRoleAsync(user, "Admin");
            }

            return Ok();
        }

        public async Task<IActionResult> Edit(string userId)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            IList<string> userRoles = await _userManager.GetRolesAsync(user);
            List<IdentityRole> allRoles = _roleManager.Roles.ToList();

            ChangeRoleViewModel model = new ChangeRoleViewModel
            {
                UserId = user.Id,
                UserEmail = user.Email,
                UserRoles = userRoles,
                AllRoles = allRoles,
            };

            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            IList<string> userRoles = await _userManager.GetRolesAsync(user);

            IEnumerable<string> addedRoles = roles.Except(userRoles);

            IEnumerable<string> removedRoles = userRoles.Except(roles);

            await _userManager.AddToRolesAsync(user, addedRoles);

            await _userManager.RemoveFromRolesAsync(user, removedRoles);

            return Ok();
        }
    }
}
