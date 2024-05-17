using System;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;


namespace PasswordHasherExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var passwordHasher = new PasswordHasher<IdentityUser>();
            var user = new IdentityUser();
            string password = "1";
            string hashedPassword = passwordHasher.HashPassword(user, password);
            Console.WriteLine("Hashed password: " + hashedPassword);
        }
    }
}
