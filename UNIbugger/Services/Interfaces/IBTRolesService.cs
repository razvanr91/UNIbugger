using System.Collections.Generic;
using System.Threading.Tasks;
using UNIbugger.Models;

namespace UNIbugger.Services.Interfaces
{
    public interface IBTRolesService
    {
        public Task<bool> IsUserInRoleAsync(BTUser user, string role);

        public Task<IEnumerable<string>> GetUserRolesAsync(BTUser user);

        public Task<bool> AddUserToRoleAsync(BTUser user, string role);

        public Task<bool> RemoveUserFromRoleAsync(BTUser user, string role);

        public Task<bool> RemoveUserFromMultipleRolesAsync(BTUser user, IEnumerable<string> roles);

        public Task<List<BTUser>> GetUsersInRoleAsync(string role, string companyId);

        public Task<List<BTUser>> GetUsersNotInRoleAsync(string role, string companyId);

        public Task<string> GetRoleNameByIdAsync(string roleId);
    }
}
