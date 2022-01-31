using System.Collections.Generic;
using System.Threading.Tasks;
using UNIbugger.Models;

namespace UNIbugger.Services.Interfaces
{
    public interface IBTCompanyInfoService
    {
        public Task<Company> GetCompanyInfoByIdAsync(string? companyId);

        public Task<List<BTUser>> GetAllMembersAsync(string? companyId);
        
        public Task<List<Project>> GetAllProjectsAsync(string? companyId);

        public Task<List<Ticket>> GetAllTicketsAsync(string? companyId);

    }
}
