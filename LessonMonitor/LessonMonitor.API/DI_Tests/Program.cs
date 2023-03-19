using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http.Headers;
using System.Threading;
using System.Web.Mvc;

namespace DI_Tests
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var collection = new ServiceCollection();
            collection.AddTransient<UserService>();
            collection.AddTransient<GithubClient>();


            var provider = collection.BuildServiceProvider();

            using (var scope = provider.CreateScope())
            {
                var service = scope.ServiceProvider.GetService<UserService>();
                var controller = new UsersContoller(service);
                controller.Print();
            }

            using (var scope = provider.CreateScope())
            {
                var service = scope.ServiceProvider.GetService<UserService>();
                var controller = new UsersContoller(service);
                controller.Print();
            }


        }

        private static void Multiplerequest()
        {
            //  Request - scope

            {
                var client = new GithubClient();
                var service = new UserService(client);
                var controller = new UsersContoller(service);

                controller.Print();

            }

            {
                var client = new GithubClient();
                var service = new UserService(client);
                var controller = new UsersContoller(service);

                controller.Print();
            }
        }
        //  SingleTon 
        private static void SingleRequest()
        {
            var client = new GithubClient();
            var service = new UserService(client);
            var controller = new UsersContoller(service);

            controller.Print();
            controller.Print();
            controller.Print();
        }
    }


    public class UsersContoller
    {
        private readonly UserService _userService;

        private readonly Guid _guid;
        public UsersContoller(UserService userService)
        {
            _guid= Guid.NewGuid();
            _userService = userService;
           
        }

        public void Print()
        {
            Console.WriteLine(nameof(UsersContoller) + " - " + _guid);
            _userService.Print();
        }
            
    }

    public class UserService
    {
        private readonly GithubClient _githubClient;

        private readonly Guid _guid;
        public UserService(GithubClient githubClient)
        {
            _guid = Guid.NewGuid();
            _githubClient = githubClient;
        }
        public void Print()
        {
            Console.WriteLine(nameof(UserService) + " - " + _guid);
            _githubClient.Print(); 
        }
            
    }

    public class GithubClient
    {
        private Guid _guid;
        public GithubClient()
        {
            _guid = Guid.NewGuid(); 
        }
        public void Print() =>
            Console.WriteLine(nameof(GithubClient) + " - " + _guid);
    } 
}
