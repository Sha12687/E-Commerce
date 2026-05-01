using E_Commerce.Data2.Models;
using E_Commerce.Models;
using ECommerce.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Service
{
    public interface IAdminService 
    {
        Task<IEnumerable<AdminProfileVM>> GetAllAdminProfiles();
        Task CreateAdminProfile(AdminProfileVM adminProfile);

        void UpdateAdminProfile(AdminProfileVM adminProfile);

        Task<AdminProfileVM> GetAdminProfileById(string id);

        Task DeleteAdminProfile(string id);

    }
}
