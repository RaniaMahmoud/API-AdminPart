using E_commerceAPI.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace E_commerceAPI.Repository.AdminRepo
{
    public class AdminRepository : IAdminRepository
    {
        ContextDB contextDB;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        //public PasswordHasher passwordHasher = new PasswordHasher<AppUser>();
        public AdminRepository(ContextDB _contextDB, UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
            contextDB = _contextDB;
        }
        public int Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<Admin> GetAll()
        {
            var list = new List<Admin>();
            var Roles = contextDB.Roles.ToList();
            var UserRole = contextDB.UserRoles.Where(r => r.RoleId == Roles[0].Id).Select(u => u.UserId);
            
            foreach (var user in contextDB.Users.ToList())
            {
                
                if (UserRole.Contains(user.Id))
                {
                    list.Add(new Admin() { Name = user.UserName, Password = user.PasswordHash });
                }
            }
            return list;
        }
        public static string base64Decode(string password) //Decode    
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }
        public Admin GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public int Insert(Admin entity)
        {
            throw new System.NotImplementedException();
        }

        public int Update(int id, Admin entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Admin>> GetAllAdmin()
        {
            throw new System.NotImplementedException();
        }

        //List<Admin> IRepository<Admin>.GetAll()
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}
