using LessonMonitor.Core;
using LessonMonitor.Core.Repositories;
using LessonMonitor.Core.Services;
using Microsoft.VisualBasic;

namespace LessonMonitor.BusinessLogic
{
    public class UsersService : IUsersService
    {
        private IUsersRepository _usersRepository;

        private GithubClient _githubClient;
        private IUsersRepository usersRepository;

        public UsersService(IUsersRepository usersRepository, 
            GithubClient githubClient)
        {
            _usersRepository = usersRepository;

            _githubClient = githubClient;   
        }

 

        public User[] Get()
        {
            var users = _usersRepository.Get();

            _githubClient.get("");
       
            return users;
        }

        public void Create(User user)
        {

        }

        
    }

    public class GithubClient
    {
        public GithubUser get(string nickname)
        {
            return new GithubUser();
        }

    }

    public class GithubUser
    { 

    }



}