using E_Commerce.Data2.Models;
using ECommerce.Business.Models;
using ECommerce.Data2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Service
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly MyContext _context;
        public ApplicationUserService(UserManager<ApplicationUser> userManager , MyContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task CreateStaffAsync(StaffVM user)
        {
            var applicationUser = new ApplicationUser
            {
                FullName = user.FullName,
                Email = user.Email,
                DOB = user.DOB,
                UserName= user.Email,
                PhoneNumber=user.PhoneNumber,
                ProfileImage = user.ExistingImage,
                Addresses    = new List<Address>()

               {        new Address{
                    AddressLine = user.Street,
                    City = user.City,
                    Country = user.Country,
                    Pincode = user.PinCode,
                    state = user.State
               }
                }

            };
            var result = await _userManager.CreateAsync(applicationUser, user.Password!);


            if (result.Succeeded)
            {
                // ✅ Only then assign role
                await _userManager.AddToRoleAsync(applicationUser, "Staff");
            }
            else
            {
                //  Log actual error
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception(errors);
            }
        }

        public async Task DeleteStaffAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return;

          var address = await _context.Addresses.Where(a => a.UserId == id).ToListAsync();
            if (address != null)
            {
                _context.Addresses.RemoveRange(address);
                await _context.SaveChangesAsync();
            }
            var feedbacks = await _context.Feedbacks.Where(f => f.UserId == id).ToListAsync();  
            foreach ( var feedback in feedbacks)
            {
                _context.Feedbacks.Remove(feedback);
            }
            await _context.SaveChangesAsync(); 
            await _userManager.DeleteAsync(user);
        }

        public async Task<List<StaffVM>> GetAllStaffAsync()
        {
            List<StaffVM> resut = await(from u in _userManager.Users
                              join address in _context.Addresses
                              on u.Id equals address.UserId
                              select new StaffVM
                              {
                                  FullName = u.FullName,
                                  Email = u.Email,
                                  DOB = u.DOB,
                                  ExistingImage = u.ProfileImage,
                                  Street = address.AddressLine,
                                  City = address.City,
                                  Country = address.Country,
                                  PinCode = address.Pincode,
                                  State = address.state,
                                  PhoneNumber = u.PhoneNumber,
                                
                              }).ToListAsync();
            return resut;
        }

        public async Task<StaffVM?> GetByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return null;

            var address = await _context.Addresses.FirstOrDefaultAsync(a => a.UserId == id);
            if (address == null) return null;

            return new StaffVM
            {
                FullName = user.FullName,
                Email = user.Email,
                DOB = user.DOB,
                ExistingImage = user.ProfileImage,
                Street = address.AddressLine,
                City = address.City,
                Country = address.Country,
                PinCode = address.Pincode,
                State = address.state,
             
            };
        }

        public async Task<StaffVM?> GetByIdAsyncView(string id)
        {
            var user1 = (from u in _userManager.Users
                         join address  in _context.Addresses on u.Id equals address.UserId 
                         join feedback in _context.Feedbacks on u.Id equals feedback.UserId into feedbackGroup
                         where u.Id == id
                         select new StaffVM
                         {
                             Id = u.Id,
                             FullName = u.FullName,
                             Email = u.Email,
                             DOB = u.DOB,
                             ExistingImage = u.ProfileImage,
                             Street = address.AddressLine,
                             City = address.City,
                             Country = address.Country,
                             PinCode = address.Pincode,
                             State = address.state,
                             PhoneNumber = u.PhoneNumber,
                             FeedbackVMs = feedbackGroup.Select(f => new FeedbackVM
                             {
                                 Id = f.Id,
                                 UserId = f.UserId,
                                 Question = f.Message,
                                 CreationDate = f.CreatedAt
                             }).ToList()
                         }).FirstOrDefaultAsync();
            return await user1;
        }

        public async Task UpdateProfileAsync(StaffVM user,string Id)
        {
            var existingUser = await _userManager.FindByIdAsync(Id);
            if (existingUser == null) return;

            // ✅ Update user fields
            existingUser.FullName = user.FullName;
            existingUser.Email = user.Email;
            existingUser.UserName = user.Email;
            existingUser.DOB = user.DOB;
            existingUser.PhoneNumber = user.PhoneNumber;
            existingUser.ProfileImage = user.ExistingImage;

            // ✅ Save user
            var result = await _userManager.UpdateAsync(existingUser);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            // ✅ Get existing address
            var address = await _context.Addresses
                .FirstOrDefaultAsync(a => a.UserId == Id);

            if (address != null)
            {
                // Update existing address
                address.AddressLine = user.Street;
                address.City = user.City;
                address.Country = user.Country;
                address.Pincode = user.PinCode;
                address.state = user.State;
            }
            else
            {
                // Create new address
                address = new Address
                {
                    UserId = Id,
                    AddressLine = user.Street,
                    City = user.City,
                    Country = user.Country,
                    Pincode = user.PinCode,
                    state = user.State
                };

                _context.Addresses.Add(address);
            }

            await _context.SaveChangesAsync();
        }


    }
}
