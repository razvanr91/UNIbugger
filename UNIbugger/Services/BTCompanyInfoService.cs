using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNIbugger.Data;
using UNIbugger.Models;
using UNIbugger.Services.Interfaces;

namespace UNIbugger.Services
{
    public class BTCompanyInfoService : IBTCompanyInfoService
    {
        private readonly ApplicationDbContext _context;

        public BTCompanyInfoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BTUser>> GetAllMembersAsync(string companyId)
        {
            List<BTUser> result = new();
            result = await _context.Users.Where(user => user.CompanyId == companyId).ToListAsync();

            return result;
        }

        public async Task<List<Project>> GetAllProjectsAsync(string companyId)
        {
            List<Project> result = new();
            result = await _context.Projects.Where(project => project.CompanyId.ToString() == companyId)
                                            .Include(project => project.Members)
                                            .Include(project => project.Tickets)
                                                .ThenInclude(ticket => ticket.TicketStatus)
                                            .Include(project => project.Tickets)
                                                .ThenInclude(ticket => ticket.Comments)
                                            .Include(project => project.Tickets)
                                                .ThenInclude(ticket => ticket.TicketPriority)
                                            .Include(project => project.Tickets)
                                                .ThenInclude(ticket => ticket.TicketType)
                                            .Include(project => project.Tickets)
                                                .ThenInclude(ticket => ticket.Attachments)
                                            .Include(project => project.Tickets)
                                                .ThenInclude(ticket => ticket.History)
                                            .Include(project => project.Tickets)
                                                .ThenInclude(ticket => ticket.DeveloperUser)
                                            .Include(project => project.Tickets)
                                                .ThenInclude(ticket => ticket.OwnerUser)
                                            .Include(project => project.Tickets)
                                                .ThenInclude(ticket => ticket.Notifications)
                                            .Include(project => project.ProjectPriority)
                                            .ToListAsync();

            return result;
        }

        public async Task<List<Ticket>> GetAllTicketsAsync(string companyId)
        {
            List<Ticket> result = new();

            /* my method
            result = await _context.tickets.where(ticket => ticket.project.companyid.tostring() == companyid).tolistasync();
            */

            /* Coder Foundry Method */

            List<Project> projects = new();
            projects = await (GetAllProjectsAsync(companyId));
            result = projects.SelectMany(project => project.Tickets).ToList();

            return result;
        }

        public async Task<Company> GetCompanyInfoByIdAsync(string? companyId)
        {
            Company result = new();

            if(companyId != null)
            {
                result = await _context.Companies
                                       .Include(company => company.Members)
                                       .Include(company => company.Projects)
                                       .Include(company => company.Invites)
                                       .FirstOrDefaultAsync(company => company.Id.ToString() == companyId);
            }

            return result;
        }
    }
}