using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNIbugger.Data;
using UNIbugger.Models;
using UNIbugger.Models.Enums;
using UNIbugger.Services.Interfaces;

namespace UNIbugger.Services
{
    public class BTProjectService : IBTProjectService
    {
        private readonly ApplicationDbContext _context;
        private readonly IBTRolesService _roleService;

        public BTProjectService(ApplicationDbContext context, IBTRolesService roleService )
        {
            _context = context;
            _roleService = roleService;
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

        public async Task<bool> AddProjectManagerAsync(string userId, string projectId)
        {
            BTUser currentPM = await GetProjectManagerAsync(projectId);

            if(currentPM != null)
            {
                try
                {
                    await RemoveProjectManagerAsync(projectId);
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"*** ERROR *** - Problem removing PM from project --> {ex.Message}");
                    return false;
                }
            }

            try
            {
                await AddProjectManagerAsync(userId, projectId);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"*** ERROR *** - Problem adding PM to project --> {ex.Message}");
                return false;
            }

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
                    catch (Exception)
                    {
                        throw;
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

        public async Task<List<BTUser>> GetAllProjectMembersExceptPMAsync(string projectId)
        {
            List<BTUser> developers = await GetProjectMembersByRoleAsync(projectId, Roles.Developer.ToString());
            List<BTUser> submitter = await GetProjectMembersByRoleAsync(projectId, Roles.Submitter.ToString());
            List<BTUser> admins = await GetProjectMembersByRoleAsync(projectId, Roles.Admin.ToString());

            List<BTUser> projectMembers = developers.Concat(submitter).Concat(admins).ToList();

            return projectMembers;
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

        public async Task<BTUser> GetProjectManagerAsync(string projectId)
        {
            Project project = await _context.Projects.Include(project => project.Members)
                                            .FirstOrDefaultAsync(project => project.Id.ToString().Equals(projectId));
            
            foreach(var member in project?.Members)
            {
                if(await _roleService.IsUserInRoleAsync(member, Roles.ProjectManager.ToString()))
                {
                    return member;
                }
            }

            return null;
        }

        public async Task<List<BTUser>> GetProjectMembersByRoleAsync(string projectId, string role)
        {
            Project project = await _context.Projects
                                            .Include(project => project.Members)
                                            .FirstOrDefaultAsync(project => project.Id.ToString() == projectId);

            List<BTUser> projectMembers = new();
            
            foreach(var member in project.Members)
            {
                if(await _roleService.IsUserInRoleAsync(member, role))
                {
                    projectMembers.Add(member);
                }
            }

            return projectMembers;
        }

        public Task<List<BTUser>> GetSubmittersOnProjectAsync(string projectId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Project>> GetUserProjectsAsync(string userId)
        {
            try
            {
                List<Project> userProjects = (await _context.Users
                                            .Include(user => user.Projects)
                                                .ThenInclude(project => project.Company)
                                            .Include(user => user.Projects)
                                                .ThenInclude(project => project.Members)
                                            .Include(user => user.Projects)
                                                .ThenInclude(project => project.Tickets)
                                            .Include(user => user.Projects)
                                                .ThenInclude(project => project.Tickets)
                                                    .ThenInclude(ticket => ticket.DeveloperUser)
                                            .Include(user => user.Projects)
                                                .ThenInclude(project => project.Tickets)
                                                    .ThenInclude(ticket => ticket.OwnerUser)
                                            .Include(user => user.Projects)
                                                .ThenInclude(project => project.Tickets)
                                                    .ThenInclude(ticket => ticket.TicketPriority)
                                            .Include(user => user.Projects)
                                                .ThenInclude(project => project.Tickets)
                                                    .ThenInclude(ticket => ticket.TicketStatus)
                                            .Include(user => user.Projects)
                                                .ThenInclude(project => project.Tickets)
                                                    .ThenInclude(ticket => ticket.TicketType)
                                            .FirstOrDefaultAsync(user => user.Id.ToString() == userId)).Projects.ToList();

                return userProjects;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"*** ERROR *** - Problem getting user projects list --> {ex.Message}");
                throw;
            }
        }

        public async Task<List<BTUser>> GetUsersNotOnProjectAsync(string projectId, string companyId)
        {
            List<BTUser> users = await _context.Users.Where(user => user.Projects.All(project => project.Id.ToString() != projectId)).ToListAsync();

            return users.Where(user => user.CompanyId.ToString() == companyId).ToList();
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

        public async Task RemoveProjectManagerAsync(string projectId)
        {
            Project project = await _context.Projects.Include(project => project.Members)
                                            .FirstOrDefaultAsync(project=> project.Id.ToString() == projectId);

            try
            {
                foreach (var member in project?.Members)
                {
                    if (await _roleService.IsUserInRoleAsync(member, Roles.ProjectManager.ToString()))
                    {
                        await RemoveUserFromProjectAsync(member.Id.ToString(), projectId);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task RemoveUserFromProjectAsync(string userId, string projectId)
        {
            try
            {
                BTUser user = await _context.Users.FirstOrDefaultAsync(user => user.Id.ToString() == userId);
                Project project = await _context.Projects.FirstOrDefaultAsync(project => project.Id.ToString() == projectId);
                
                try
                {
                    if (await IsUserOnProjectAsync((string)user.Id, projectId))
                    {
                        project.Members.Remove(user);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"**** ERROR **** - Problem removing user from project. --> {ex.Message}");
            }
        }

        public async Task RemoveUsersFromProjectByRoleAsync(string role, string projectId)
        {
            try
            {
                List<BTUser> members = await GetProjectMembersByRoleAsync(role, projectId);
                Project project = await _context.Projects.FirstOrDefaultAsync(project => project.Id.ToString() == projectId);

                foreach(var member in members)
                {
                    try
                    {
                        project.Members.Remove(member);
                        await _context.SaveChangesAsync();
                    }
                    catch(Exception)
                    {
                        throw;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"*** ERROR *** - Problem removing users from project --> {ex.Message}");
                throw;
            }
        }

        public async Task UpdateProjectAsync(Project project)
        {
            _context.Update(project);
            await _context.SaveChangesAsync();
        }
    }
}
