using LessonMonitor.BusinessLogic;
using LessonMonitor.Core.Repositories;
using LessonMonitor.Core.Services;
using LessonMonitor.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace LessonMonitor.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeworksController : ControllerBase
    {
        private readonly IHomeworksService _homeworkService;

        public HomeworksController(IHomeworksService homeworkService)
        {
            _homeworkService = homeworkService;
        }
    }


    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
       

        public UsersController()
        {
            IUsersRepository usersRepository = new UsersRepository();

            IUsersService _usersService = new UsersService(usersRepository, null) ;
           
        }

        [HttpGet]
        public User[] Get(string userName) 
        {
     
            var user = _usersService.Get();

            var rusult = new User ();


            return new[] { rusult };




            //var random = new Random();
            //var users = new List<User>();

            //for (int i = 0; i < 10; i++)
            //{
            //    var user = new User();

            //    user.Name = userName + i;
            //    user.Age = random.Next(20, 51);

            //    users.Add(user);
            //}

            //return users.ToArray();
            //
        }

        [HttpPost]
        
        public User Create(User newUser)
        {
             var user = new Core.User
            {
                Age = newUser.Age,
                Name = newUser.Name,
            };

            _usersService.Create(user);

           
            return  newUser ;
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
