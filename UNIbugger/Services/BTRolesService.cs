using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
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

        public Task<string> GetRoleNameByIdAsync(string roleId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<string>> GetUserRolesAsync(BTUser user)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<BTUser>> GetUsersInRoleAsync(string role, string companyId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<BTUser>> GetUsersNotInRoleAsync(string role, string companyId)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> IsUserInRoleAsync(BTUser user, string role)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> RemoveUserFromMultipleRolesAsync(BTUser user, IEnumerable<string> roles)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> RemoveUserFromRoleAsync(BTUser user, string role)
        {
            throw new System.NotImplementedException();
        }
    }
}
