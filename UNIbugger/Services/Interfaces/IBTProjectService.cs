using System.Collections.Generic;
using System.Threading.Tasks;
using UNIbugger.Models;

namespace UNIbugger.Services.Interfaces
{
    public interface IBTProjectService
    {
        public Task AddNewProjectAsync(Project project);

        public Task<bool> AddProjectManagerAsync(string userId, string projectId);

        public Task<bool> AddUserToProjectAsync(string userId, string projectId);

        public Task ArchiveProjectAsync(Project project);

        public Task<List<Project>> GetAllProjectsByCompany(string companyId);

        public Task<List<Project>> GetAllProjectsByPriority(string companyId,string priority);

        public Task<List<BTUser>> GetAllProjectMembersExceptPMAsync(string projectId);

        public Task<List<Project>> GetArchivedProjectsByCompany(string companyId);

        public Task<List<BTUser>> GetDevelopersOnProjectAsync(string projectId);

        public Task<BTUser> GetProjectManagerAsync(string projectId);

        public Task<List<BTUser>> GetProjectMembersByRoleAsync(string projectId, string role);

        public Task<Project> GetProjectByIdAsync(string projectId, string companyId);

        public Task<List<BTUser>> GetSubmittersOnProjectAsync(string projectId);

        public Task<List<BTUser>> GetUsersNotOnProjectAsync(string projectId, string companyId);

        public Task<List<Project>> GetUserProjectAsync(string userId);

        public Task<bool> IsUserOnProject(string userId, string projectId);

        public Task<string> LookupProjectPriorityId(string priorityName);

        public Task RemoveProjectManagerAsync(string projectId);

        public Task RemoveUsersFromProjectByRoleAsync(string role, string projectId);

        public Task RemoveUserFromProjectAsync(string userId, string projectId);

        public Task UpdateProjectAsync(Project project);
    }

}
