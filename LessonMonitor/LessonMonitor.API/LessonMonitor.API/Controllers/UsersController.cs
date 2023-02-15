using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace LessonMonitor.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {

        [HttpGet]
        public User[] Get(string userName) 
        {
            var random = new Random();
            var users = new List<User>();

            for (int i = 0; i < 10; i++)
            {
                var user = new User();

                user.Name = userName + i;
                user.Age = random.Next(20, 51);

                users.Add(user);
            }

            return users.ToArray();        
        }

        [HttpGet("model")]
        public void GetModel([FromQuery]User user)
        {
            var model = user.GetType();

            foreach (var property in model.GetProperties())
            {
                var attributes = property.CustomAttributes;
            }
               
        }
    
    }
}
