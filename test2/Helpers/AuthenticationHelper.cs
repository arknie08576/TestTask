using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using test2;
using test2.Models;
using test2.Data;
using test2.Enums;

namespace test2.Helpers
{
    public static class AuthenticationHelper
    {
        public static string? loggedUser;
        private static OfficeContex context;

        public static void LoadDbContext(OfficeContex dbcontext)
        {
            context = dbcontext;

        }



        public static async Task RegisterUserAsync(string username, string password, string fullName, Subdivision? subdivision, Position? position, EmployeeStatus? status, int? peoplePartner, int out_of_OfficeBalance, byte[] photo)
        {
            var isAny = await context.Employes.AnyAsync(u => u.Username == username);
            if (isAny)
            {
                throw new InvalidOperationException("Username already exists.");
            }

            var salt = GenerateSalt();
            var hash = HashPassword(password, salt);

            var user = new Employee { Username = username, PasswordHash = hash, Salt = salt, FullName = fullName, Subdivision = (Subdivision)subdivision, Position = (Position)position, Status = (EmployeeStatus)status, PeoplePartner = peoplePartner, Out_of_OfficeBalance = out_of_OfficeBalance, Photo = photo };
            await context.Employes.AddAsync(user);
            await context.SaveChangesAsync();

        }

        public static async Task<bool> AuthenticateUserAsync(string username, string password)
        {

            var user = await context.Employes.SingleOrDefaultAsync(u => u.Username == username);
            if (user == null) return false;

            var hash = HashPassword(password, user.Salt);
            loggedUser = user.Username;
            return hash == user.PasswordHash;

        }

        private static string GenerateSalt()
        {
            var randomBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }

        private static string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = password + salt;
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                return Convert.ToBase64String(bytes);
            }
        }
        public static async Task ChangePasswordAsync(string username, string newPassword)
        {
            
            var user = await context.Employes.SingleOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                throw new InvalidOperationException("User does not exist.");
            }


            // Generate new salt and hash the new password
            var newSalt = GenerateSalt();
            var newHash = HashPassword(newPassword, newSalt);

            // Update the user with the new password hash and salt
            user.Salt = newSalt;
            user.PasswordHash = newHash;

            // Save changes to the database
            await context.SaveChangesAsync();
        }
    }

}