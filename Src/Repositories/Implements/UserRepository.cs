using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using project_dotnet7_api.Src.Data;
using project_dotnet7_api.Src.Models;
using project_dotnet7_api.Src.Repositories.Interfaces;

namespace project_dotnet7_api.Src.Repositories.Implements
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsers(){
            var users = await _context.Users.Include(u => u.Role)
                                            .Include(u => u.Gender)
                                            .ToListAsync();

            return users;
        }

        public async Task<IEnumerable<User>> SearchUsers(string query)
        {
            var users = await _context.Users.Where(u => u.Id.ToString().Contains(query)
                                            || u.Rut.Contains(query) 
                                            || u.Name.Contains(query)
                                            || u.Birthday.ToString().Contains(query)
                                            || u.Email.Contains(query)
                                            || u.IsActive.ToString().Contains(query)
                                            || u.Gender.Type.Contains(query))
                                            .Include(u => u.Role)
                                            .Include(u => u.Gender)
                                            .ToListAsync();

            return users;
        }

        public async Task<User?> GetUserById(int id){
            var user = await _context.Users.Where(u => u.Id == id)
                                            .Include(u => u.Role)
                                            .Include(u => u.Gender)
                                            .FirstOrDefaultAsync();
            return user;
        }

        public async Task<User?> GetUserByEmail(string email){
            var user = await _context.Users.Where(u => u.Email == email)
                                            .Include(u => u.Role)
                                            .Include(u => u.Gender)
                                            .FirstOrDefaultAsync();
            return user;
        }

        public async Task<bool> VerifyRut(string rut){
            var user = await _context.Users.Where(u => u.Rut == rut).FirstOrDefaultAsync();
            if(user == null){
                return false;
            }
            return true;
        }

        public async Task<bool> VerifyEmail(string email){
            var user = await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
            if(user == null){
                return false;
            }
            return true;
        }

        public async Task<bool> VerifyUser(int id){
            var user = await _context.Users.FindAsync(id);
            if(user == null){
                return false;
            }
            return true;
        }

        public async Task<bool> AddUser(User user){
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditUser(int id, EditUserInfoDto user){
            var existingUser = await _context.Users.FindAsync(id);
            if(existingUser == null){
                return false;
            }

            existingUser.Name = user.Name ?? existingUser.Name;
            existingUser.Birthday = user.Birthday ?? existingUser.Birthday;
            existingUser.GenderId = user.GenderId ?? existingUser.GenderId;


            _context.Entry(existingUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ChangeUserState(int id, bool newUserState)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if(existingUser == null){
                return false;
            }

            existingUser.IsActive = newUserState;

            _context.Entry(existingUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ChangePassword(int id, string newPassword)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if(existingUser == null){
                return false;
            }

            existingUser.Password = newPassword;

            _context.Entry(existingUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }        
    }
}