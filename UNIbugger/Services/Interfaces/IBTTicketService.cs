using System.Collections.Generic;
using System.Threading.Tasks;
using UNIbugger.Models;

namespace UNIbugger.Services.Interfaces
{
    public interface IBTTicketService
    {
        // CRUD Methods
        public Task AddNewTicketAsync(Ticket ticket);
        
        public Task UpdateTicketAsync(Ticket ticket);

        public Task<Ticket> GetTicketByIdAsync(string id);

        public Task ArchiveTicketAsync(Ticket ticket);


        public Task AssignTicketAsync(string ticketId, string userId);

        public Task<List<Ticket>> GetArchivedTicketsAsync(string companyId);

        public Task<List<Ticket>> GetAllTicketsByCompanyAsync(string companyId);

        public Task<List<Ticket>> GetAllTicketsByPriorityAsync(string companyId, string priorityName);

        public Task<List<Ticket>> GetAllTicketsByStatusAsync(string copmanyId, string statusName);

        public Task<List<Ticket>> GetAllTicketsByTypeAsync(string companyId, string typeName);

        public Task<BTUser> GetTicketDeveloperAsync(string ticketId);

        public Task<List<Ticket>> GetTicketsByRoleAsync(string role, string userId, string companyId);

        public Task<List<Ticket>> GetTicketsByUserIdAsync(string userId, string companyId);

        public Task<List<Ticket>> GetProjectTicketsByRoleAsync(string role, string userId, string projectId,string companyId);

        public Task<List<Ticket>> GetProjectTicketsByStatusAsync(string statusName, string companyId, string projectId);

        public Task<List<Ticket>> GetProjectTicketsByPriorityAsync(string priorityName, string companyId, string projectId);

        public Task<List<Ticket>> GetProjectTicketsByTypeAsync(string typeName, string companyId, string projectId);


        public Task<string?> LookupTicketPriorityIdAsync(string priorityName);

        public Task<string?> LookupTicketStatusIdAsync(string statusName);

        public Task<string?> LookupTicketTypeIdAsync(string typeName);
    }
}
