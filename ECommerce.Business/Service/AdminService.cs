using E_Commerce.Data2.Models;
using E_Commerce.Models;
using ECommerce.Business.Models;
using ECommerce.Business.Repository;
using ECommerce.Data2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Service
{
    public class AdminService : IAdminService
    {
        private readonly MyContext _context;


        public AdminService(MyContext context)
        {
            _context = context;
        }
        public  Task CreateAdminProfile(AdminProfileVM adminProfile)
        {

            return null;
        }

        Task IAdminService.DeleteAdminProfile(string id)
        {
            throw new NotImplementedException();
        }

        async Task<AdminProfileVM> IAdminService.GetAdminProfileById(string id)
        {

            var resut = await (from u in _context.Users
                               join address in _context.Addresses 
                               on u.Id equals address.UserId 
                               
                        select new AdminProfileVM
                               {
                                   FullName = u.FullName,
                                   Email = u.Email,
                                   DOB = u.DOB,
                                   ExistingImage = u.ProfileImage,
                                   Street = address.AddressLine,
                                   City = address.City,
                                   Country = address.Country,
                                   PinCode = address.Pincode,
                                   State = address.state
                               }).FirstOrDefaultAsync();
            return resut;

        }

        Task<IEnumerable<AdminProfileVM>> IAdminService.GetAllAdminProfiles()
        {
           return null;
        }

        void IAdminService.UpdateAdminProfile(AdminProfileVM admin)
        {
            throw new NotImplementedException();
        }
    }
}
