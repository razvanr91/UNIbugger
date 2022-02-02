using System.Collections.Generic;
using System.Threading.Tasks;
using UNIbugger.Models;
using UNIbugger.Services.Interfaces;

namespace UNIbugger.Services
{
    public class BTTIcketService : IBTTicketService
    {
        public Task AddNewTicketAsync(Ticket ticket)
        {
            throw new System.NotImplementedException();
        }

        public Task ArchiveTicketAsync(Ticket ticket)
        {
            throw new System.NotImplementedException();
        }

        public Task AssignTicketAsync(string ticketId, string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Ticket>> GetAllTicketsByCompanyAsync(string companyId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Ticket>> GetAllTicketsByPriorityAsync(string companyId, string priorityName)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Ticket>> GetAllTicketsByStatusAsync(string copmanyId, string statusName)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Ticket>> GetAllTicketsByTypeAsync(string companyId, string typeName)
        {
            throw new System.NotImplementedException();
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

        public Task<Ticket> GetTicketByIdAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<BTUser> GetTicketDeveloperAsync(string ticketId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Ticket>> GetTicketsByRoleAsync(string role, string userId, string companyId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Ticket>> GetTicketsByUserIdAsync(string userId, string companyId)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> LookupTicketPriorityIdAsync(string priorityName)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> LookupTicketStatusIdAsync(string statusName)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> LookupTicketTypeIdAsync(string typeName)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateTicketAsync(Ticket ticket)
        {
            throw new System.NotImplementedException();
        }
    }
}
