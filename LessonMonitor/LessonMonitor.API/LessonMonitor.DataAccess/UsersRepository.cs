using System;
using LessonMonitor.Core;
using LessonMonitor.Core.Repositories;

namespace LessonMonitor.DataAccess
{
    public class UsersRepository : IUsersRepository
    {
        public UsersRepository()
        {

        }

        public object[] Get()
        {
            throw new NotImplementedException();
        }

        public void Create(object user)
        {

        }

        User[] IUsersRepository.Get()
        {
            throw new NotImplementedException();
        }
    }
}
