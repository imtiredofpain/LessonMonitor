namespace LessonMonitor.Core.Repositories
{
    public interface IUsersRepository
    {
        void Create(object user);
        User[] Get();
    }
}