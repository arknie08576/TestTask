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

public static class AuthenticationHelper
{
    public static string? loggedUser;
    private static OfficeContex context;

    public static void LoadDbContext(OfficeContex dbcontext)
    {
        context=dbcontext;

    }
        
        /// <summary>
    //)
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <param name="fullName"></param>
    /// <param name="subdivision"></param>
    /// <param name="position"></param>
    /// <param name="status"></param>
    /// <param name="peoplePartner"></param>
    /// <param name="out_of_OfficeBalance"></param>
    /// <param name="photo"></param>
    /// <exception cref="InvalidOperationException"></exception>

    public static void RegisterUser(string username,string password,string fullName, Subdivision subdivision, Position position,EmployeeStatus status, int? peoplePartner, int out_of_OfficeBalance, byte[] photo)
    {
     
            if (context.Employes.Any(u => u.Username == username))
            {
                throw new InvalidOperationException("Username already exists.");
            }

            var salt = GenerateSalt();
            var hash = HashPassword(password, salt);

            var user = new Employee { Username = username, PasswordHash = hash, Salt = salt, FullName=fullName, Subdivision=subdivision, Position=position, Status=status, PeoplePartner=peoplePartner, Out_of_OfficeBalance = out_of_OfficeBalance, Photo=photo};
            context.Employes.Add(user);
            context.SaveChanges();
        
    }

    public static bool AuthenticateUser(string username, string password)
    {
       
            var user = context.Employes.SingleOrDefault(u => u.Username == username);
            if (user == null) return false;

            var hash = HashPassword(password, user.Salt);
            loggedUser=user.Username;
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
}