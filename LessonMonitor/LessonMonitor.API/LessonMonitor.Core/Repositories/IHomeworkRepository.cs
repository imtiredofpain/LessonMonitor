namespace LessonMonitor.Core
{
    public interface IHomeworkRepository
    {
        void Add(Homework homework);
        Homework Get();
        void Update(Homework homework);
        void Delete(int homeworkId);

    }
}