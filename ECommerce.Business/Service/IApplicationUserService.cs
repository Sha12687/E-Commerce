using ECommerce.Business.Models;
using ECommerce.Data2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Service
{
    public interface IApplicationUserService
    {
        Task<StaffVM?> GetByIdAsync(string id);
        Task<StaffVM?> GetByIdAsyncView(string id);
        Task UpdateProfileAsync(StaffVM user, string Id);
        Task<List<StaffVM>> GetAllStaffAsync();
        Task CreateStaffAsync(StaffVM user);
        Task DeleteStaffAsync(string id);
    }
}
