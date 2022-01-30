using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNIbugger.Data;
using UNIbugger.Models;
using UNIbugger.Services.Interfaces;

namespace UNIbugger.Services
{
    public class BTRolesService : IBTRolesService
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<BTUser> _userManager;

        public BTRolesService(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<BTUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<bool> AddUserToRoleAsync(BTUser user, string role)
        {
            bool result = (await _userManager.AddToRoleAsync(user, role)).Succeeded;

            return result;  
        }

        public async Task<string> GetRoleNameByIdAsync(string roleId)
        {
            IdentityRole role = await _context.Roles.FindAsync(roleId);
            string result = await _roleManager.GetRoleNameAsync(role);

            return result;
        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(BTUser user)
        {
            IEnumerable<string> result = await _userManager.GetRolesAsync(user);

            return result;
        }

        public async Task<List<BTUser>> GetUsersInRoleAsync(string role, string companyId)
        {
            List<BTUser> users = (await _userManager.GetUsersInRoleAsync(role)).ToList();
            List<BTUser> result = users.Where(user => user.CompanyId == companyId).ToList();

            return result;
        }

        public async Task<List<BTUser>> GetUsersNotInRoleAsync(string role, string companyId)
        {
            List<string> userIds = (await _userManager.GetUsersInRoleAsync(role)).Select(user => user.Id).ToList();
            List<BTUser> roleUsers =  _context.Users.Where(user => !userIds.Contains(user.Id)).ToList();

            List<BTUser> result = roleUsers.Where(user => user.CompanyId == companyId).ToList();

            return result;
        }

        public async Task<bool> IsUserInRoleAsync(BTUser user, string role)
        {
            bool result = await _userManager.IsInRoleAsync(user, role);

            return result;
        }

        public async Task<bool> RemoveUserFromMultipleRolesAsync(BTUser user, IEnumerable<string> roles)
        {
            bool result = (await _userManager.RemoveFromRolesAsync(user, roles)).Succeeded;

            return result;
        }

        public async Task<bool> RemoveUserFromRoleAsync(BTUser user, string role)
        {
            bool result = (await _userManager.RemoveFromRoleAsync(user, role)).Succeeded;
            
            return result;
        }
    }
}
