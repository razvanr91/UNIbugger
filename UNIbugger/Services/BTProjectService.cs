using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using UNIbugger.Data;
using UNIbugger.Models;
using UNIbugger.Services.Interfaces;

namespace UNIbugger.Services
{
    public class BTProjectService : IBTProjectService
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<BTUser> _userManager;

        public BTProjectService(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<BTUser> userManager )
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public Task AddNewProjectAsync(Project project)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> AddProjectManagerAsync(string userId, string projectId)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> AddUserToProjectAsync(string userId, string projectId)
        {
            throw new System.NotImplementedException();
        }

        public Task ArchiveProjectAsync(Project project)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<BTUser>> GetAllProjectMembersExceptPMAsync(string projectId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Project>> GetAllProjectsByCompany(string companyId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Project>> GetAllProjectsByPriority(string companyId, string priority)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Project>> GetArchivedProjectsByCompany(string companyId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<BTUser>> GetDevelopersOnProjectAsync(string projectId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Project> GetProjectByIdAsync(string projectId, string companyId)
        {
            throw new System.NotImplementedException();
        }

        public Task<BTUser> GetProjectManagerAsync(string projectId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<BTUser>> GetProjectMembersByRoleAsync(string projectId, string role)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<BTUser>> GetSubmittersOnProjectAsync(string projectId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Project>> GetUserProjectAsync(string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<BTUser>> GetUsersNotOnProjectAsync(string projectId, string companyId)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> IsUserOnProject(string userId, string projectId)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> LookupProjectPriorityId(string priorityName)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveProjectManagerAsync(string projectId)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveUserFromProjectAsync(string userId, string projectId)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveUsersFromProjectByRoleAsync(string role, string projectId)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateProjectAsync(Project project)
        {
            throw new System.NotImplementedException();
        }
    }
}
