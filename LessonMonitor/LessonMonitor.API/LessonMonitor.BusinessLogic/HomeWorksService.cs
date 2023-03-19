using LessonMonitor.Core;
using LessonMonitor.Core.Exceptions;
using LessonMonitor.Core.Repositories;
using LessonMonitor.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonMonitor.BusinessLogic
{
    public class HomeWorksService : IHomeworksService
    {
        public const string HOMEWORK_IS_INVALID = "Homework is invalid!";

        private readonly IHomeworkRepository _homeworkRepository;

        public HomeWorksService(IHomeworkRepository homeworkRepository)
        {
            _homeworkRepository = homeworkRepository;
          
        }
        public bool Create(Homework homework)
        {
            // валидация 
            if (homework is null)
            {
                throw new ArgumentNullException(nameof(homework));
            }


            var isInvalid = string.IsNullOrWhiteSpace(homework.Link)
                || string.IsNullOrWhiteSpace(homework.Title)
                || homework.MemberId <= 0;

            if (isInvalid)
            {
                throw new BusinessException(HOMEWORK_IS_INVALID); 
            }

            // сохранение в базе 
            _homeworkRepository.Add(homework);


            return true;
        }

        public bool Delete(int homeworkId)
        {
            _homeworkRepository.Delete(homeworkId);
            return true;
        }
    }

}
