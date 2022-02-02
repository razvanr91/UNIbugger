﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UNIbugger.Data;
using UNIbugger.Models;
using UNIbugger.Services.Interfaces;

namespace UNIbugger.Services
{
    public class BTTIcketService : IBTTicketService
    {
        private readonly ApplicationDbContext _context;

        public BTTIcketService(ApplicationDbContext context)
        {
            _context = context;
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

        public async Task<Ticket> GetTicketByIdAsync(string ticketId)
        {
            Ticket ticket = await _context.Tickets.FirstOrDefaultAsync(ticket => ticket.Id.ToString() == ticketId);
            return ticket;
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

        public async Task<string?> LookupTicketPriorityIdAsync(string priorityName)
        {
            try
            {
                TicketPriority ticketPriority = await _context.TicketPriorities.FirstOrDefaultAsync(priority => priority.Name == priorityName);
                return ticketPriority?.Id.ToString();
            }
            catch
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
            catch
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
            catch
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
