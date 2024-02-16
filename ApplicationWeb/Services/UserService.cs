using ApplicationWeb.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ApplicationWeb.Security
{
    public class UserService : IUserService
    {
        List<User> users = new List<User>()
        {
            new User(){ Email = "test@mail.com", Password = "123456" },
            new User(){ Email = "test2@mail.com", Password = "123456" },
        };

        public bool IsUser(String email, string password) 
        {
            return users.Where(d=>d.Email == email && d.Password == password ).Count() > 0;
        }

    }
}
