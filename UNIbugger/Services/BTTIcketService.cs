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
    public class BTTIcketService : IBTTicketService
    {
        private readonly ApplicationDbContext _context;
        private readonly IBTRolesService _rolesService;
        private readonly IBTProjectService _projectService;

        public BTTIcketService(ApplicationDbContext context, IBTRolesService rolesService, IBTProjectService projectService)
        {
            _context = context;
            _rolesService = rolesService;
            _projectService = projectService;
        }

        public async Task AddNewTicketAsync(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task ArchiveTicketAsync(Ticket ticket)
        {
            ticket.Archived = true;
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
        }

        public Task AssignTicketAsync(string ticketId, string userId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Ticket>> GetAllTicketsByCompanyAsync(string companyId)
        {
            try
            {
                List<Ticket> companyTickets = await _context.Projects.Where(project => project.CompanyId.ToString() == companyId)
                                                            .SelectMany(project => project.Tickets)
                                                                .Include(ticket => ticket.Attachments)
                                                                .Include(ticket => ticket.Comments)
                                                                .Include(ticket => ticket.History)
                                                                .Include(ticket => ticket.DeveloperUser)
                                                                .Include(ticket => ticket.OwnerUser)
                                                                .Include(ticket => ticket.TicketPriority)
                                                                .Include(ticket => ticket.TicketStatus)
                                                                .Include(ticket => ticket.Project)
                                                            .ToListAsync();
                return companyTickets;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Ticket>> GetAllTicketsByPriorityAsync(string companyId, string priorityName)
        {
            string ticketPriority = await LookupTicketPriorityIdAsync(priorityName);
            try
            {
                List<Ticket> tickets = await _context.Projects.Where(project => project.CompanyId.ToString() == companyId)
                                                     .SelectMany(project => project.Tickets)
                                                         .Include(ticket => ticket.Attachments)
                                                         .Include(ticket => ticket.Comments)
                                                         .Include(ticket => ticket.History)
                                                         .Include(ticket => ticket.DeveloperUser)
                                                         .Include(ticket => ticket.OwnerUser)
                                                         .Include(ticket => ticket.TicketPriority)
                                                         .Include(ticket => ticket.TicketStatus)
                                                         .Include(ticket => ticket.Project)
                                                     .Where(ticket => ticket.TicketPriorityId == ticketPriority).ToListAsync();
                return tickets;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Ticket>> GetAllTicketsByStatusAsync(string companyId, string statusName)
        {
            string ticketStatus = await LookupTicketStatusIdAsync(statusName);
            
            try
            {
                List<Ticket> tickets = await _context.Projects.Where(project => project.CompanyId.ToString() == companyId)
                                                     .SelectMany(project => project.Tickets)
                                                        .Include(ticket => ticket.Attachments)
                                                        .Include(ticket => ticket.Comments)
                                                        .Include(ticket => ticket.History)
                                                        .Include(ticket => ticket.DeveloperUser)
                                                        .Include(ticket => ticket.OwnerUser)
                                                        .Include(ticket => ticket.TicketPriority)
                                                        .Include(ticket => ticket.TicketStatus)
                                                        .Include(ticket => ticket.Project)
                                                    .Where(ticket => ticket.TicketStatusId == ticketStatus).ToListAsync();
                return tickets;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Ticket>> GetAllTicketsByTypeAsync(string companyId, string typeName)
        {
            string ticketType = await LookupTicketTypeIdAsync(typeName);

            try
            {
                List<Ticket> tickets = await _context.Projects.Where(project => project.CompanyId.ToString() == companyId)
                                                     .SelectMany(project => project.Tickets)
                                                        .Include(ticket => ticket.Attachments)
                                                        .Include(ticket => ticket.Comments)
                                                        .Include(ticket => ticket.History)
                                                        .Include(ticket => ticket.DeveloperUser)
                                                        .Include(ticket => ticket.OwnerUser)
                                                        .Include(ticket => ticket.TicketPriority)
                                                        .Include(ticket => ticket.TicketStatus)
                                                        .Include(ticket => ticket.Project)
                                                    .Where(ticket => ticket.TicketTypeId == ticketType).ToListAsync();
                return tickets;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<List<Ticket>> GetArchivedTicketsAsync(string companyId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Ticket>> GetProjectTicketsByPriorityAsync(string priorityName, string companyId, string projectId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Ticket>> GetProjectTicketsByRoleAsync(string role, string userId, string projectId, string companyId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Ticket>> GetProjectTicketsByStatusAsync(string statusName, string companyId, string projectId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Ticket>> GetProjectTicketsByTypeAsync(string typeName, string companyId, string projectId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Ticket> GetTicketByIdAsync(string ticketId)
        {
            Ticket ticket = await _context.Tickets.FirstOrDefaultAsync(ticket => ticket.Id.ToString() == ticketId);
            return ticket;
        }

        public Task<BTUser> GetTicketDeveloperAsync(string ticketId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Ticket>> GetTicketsByRoleAsync(string role, string userId, string companyId)
        {
            List<Ticket> tickets = new();

            try
            {
                if(role == Roles.Admin.ToString())
                {
                    tickets = await GetAllTicketsByCompanyAsync(companyId);
                } 
                else if(role == Roles.Developer.ToString())
                {
                    tickets = (await GetAllTicketsByCompanyAsync(companyId)).Where(ticket => ticket.DeveloperUserId == userId).ToList();
                } 
                else if(role == Roles.Submitter.ToString())
                {
                    tickets = (await GetAllTicketsByCompanyAsync(companyId)).Where(ticket => ticket.OwnerUserId == userId).ToList();
                } 
                else if(role == Roles.ProjectManager.ToString())
                {
                    tickets = await GetTicketsByUserIdAsync(userId, companyId);
                }

                return tickets;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Ticket>> GetTicketsByUserIdAsync(string userId, string companyId)
        {
            BTUser user = await _context.Users.FirstOrDefaultAsync(user => user.Id == userId);
            List<Ticket> tickets = new();
            try
            {
                if(await _rolesService.IsUserInRoleAsync(user, Roles.Admin.ToString()))
                {
                    tickets = (await _projectService.GetAllProjectsByCompany(companyId)).SelectMany(project => project.Tickets).ToList();
                }
                else if(await _rolesService.IsUserInRoleAsync(user, Roles.Developer.ToString()))
                {
                    tickets = (await _projectService.GetAllProjectsByCompany(companyId)).SelectMany(project => project.Tickets)
                                                    .Where(ticket => ticket.DeveloperUserId == userId).ToList();
                }
                else if(await _rolesService.IsUserInRoleAsync(user, Roles.Submitter.ToString()))
                {
                    tickets = (await _projectService.GetAllProjectsByCompany(companyId)).SelectMany(project => project.Tickets)
                                                    .Where(ticket => ticket.OwnerUserId == userId).ToList();
                }
                else if(await _rolesService.IsUserInRoleAsync(user, Roles.ProjectManager.ToString()))
                {
                    tickets = (await _projectService.GetUserProjectsAsync(userId)).SelectMany(project => project.Tickets).ToList();
                }

                return tickets;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<string?> LookupTicketPriorityIdAsync(string priorityName)
        {
            try
            {
                TicketPriority ticketPriority = await _context.TicketPriorities.FirstOrDefaultAsync(priority => priority.Name == priorityName);
                return ticketPriority?.Id.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string?> LookupTicketStatusIdAsync(string statusName)
        {
            try
            {
                TicketStatus ticketStatus = await _context.TicketStatuses.FirstAsync(status => status.Name == statusName);
                return ticketStatus?.Id.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string?> LookupTicketTypeIdAsync(string typeName)
        {
            try
            {
                TicketType ticketType = await _context.TicketTypes.FirstOrDefaultAsync(type => type.Name == typeName);
                return ticketType?.Id.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateTicketAsync(Ticket ticket)
        {
            _context.Update(ticket);
            await _context.SaveChangesAsync();
        }
    }
}
