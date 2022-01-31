using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task AddNewProjectAsync(Project project)
        {
            if(project != null)
            {
                project.Id = Guid.NewGuid();
                await _context.AddAsync(project);
                await _context.SaveChangesAsync();
            }
        }

        public Task<bool> AddProjectManagerAsync(string userId, string projectId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> AddUserToProjectAsync(string userId, string projectId)
        {
            BTUser user = await _context.Users.FirstOrDefaultAsync(user => user.Id.ToString() == userId);
            if(user != null)
            {
                Project project = await _context.Projects.FirstOrDefaultAsync(project => project.Id.ToString() == projectId);
                if(!(await IsUserOnProjectAsync(userId, projectId)) && project != null)
                {
                    try
                    {
                        project.Members.Add(user);
                        await _context.SaveChangesAsync();
                        return true;
                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }
                }
                return false;
            }

            return false;
        }

        public async Task ArchiveProjectAsync(Project project)
        {
            project.Archived = true;
            _context.Update(project);
            await _context.SaveChangesAsync();
        }

        public Task<List<BTUser>> GetAllProjectMembersExceptPMAsync(string projectId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Project>> GetAllProjectsByCompany(string companyId)
        {
            List<Project> projects = new();
            projects = await _context.Projects.Where(project => project.CompanyId.ToString() == companyId && project.Archived == false)
                                              .Include(project => project.Members)
                                              .Include(project => project.Tickets)
                                                .ThenInclude(ticket => ticket.Comments)
                                              .Include(project => project.Tickets)
                                                .ThenInclude(ticket => ticket.Attachments)
                                              .Include(project => project.Tickets)
                                                .ThenInclude(ticket => ticket.History)
                                              .Include(project => project.Tickets)
                                                .ThenInclude(ticket => ticket.Notifications)
                                              .Include(project => project.Tickets)
                                                .ThenInclude(ticket => ticket.DeveloperUser)
                                              .Include(project => project.Tickets)
                                                .ThenInclude(ticket => ticket.OwnerUser)
                                              .Include(project => project.Tickets)
                                                .ThenInclude(ticket => ticket.TicketStatus)
                                              .Include(project => project.Tickets)
                                                .ThenInclude(ticket => ticket.TicketPriority)
                                              .Include(project => project.Tickets)
                                                .ThenInclude(ticket => ticket.TicketType)
                                              .Include(project => project.ProjectPriority)
                                              .ToListAsync();

            return projects;
        }

        public async Task<List<Project>> GetAllProjectsByPriority(string companyId, string priority)
        {
            List<Project> projects = await GetAllProjectsByCompany(companyId);
            string priorityId = await LookupProjectPriorityId(priority);

            return projects.Where(project => project.ProjectPriorityId == priorityId).ToList();
        }

        public async Task<List<Project>> GetArchivedProjectsByCompany(string companyId)
        {
            List<Project> projects = await GetAllProjectsByCompany(companyId);

            return projects.Where(project => project.Archived == true).ToList();
        }

        public Task<List<BTUser>> GetDevelopersOnProjectAsync(string projectId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Project> GetProjectByIdAsync(string projectId, string companyId)
        {
            Project project = await _context.Projects
                                            .Include(project => project.Tickets)
                                            .Include(project => project.Members)
                                            .Include(project => project.ProjectPriority)
                                            .FirstOrDefaultAsync(project => project.Id.ToString() == projectId && project.CompanyId.ToString() == companyId);

            return project;
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

        public async Task<bool> IsUserOnProjectAsync(string userId, string projectId)
        {
            Project project = await _context.Projects
                                            .Include(project => project.Members)
                                            .FirstOrDefaultAsync(project => project.Id.ToString() == projectId);
            bool result = false;

            if(project != null)
            {
                result = project.Members.Any(member => member.Id.ToString().Equals(userId));
            }

            return result;
        }

        public async Task<string> LookupProjectPriorityId(string priorityName)
        {
            string priorityId = (await _context.ProjectPriorities.FirstOrDefaultAsync(project => project.Name == priorityName)).Id.ToString();

            return priorityId;
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

        public async Task UpdateProjectAsync(Project project)
        {
            _context.Update(project);
            await _context.SaveChangesAsync();
        }
    }
}
