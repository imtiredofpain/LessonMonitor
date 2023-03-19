using AutoFixture;
using FluentAssertions;
using LessonMonitor.Core;
using LessonMonitor.Core.Exceptions;
using LessonMonitor.Core.Repositories;
using LessonMonitor.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics;

namespace LessonMonitor.BusinessLogic.MsTests
{
    [TestClass]
    public class HomeWorksServiceTests
    {
        private readonly Mock<IHomeworkRepository> _homeworkRepositoryMock;
        private readonly HomeWorksService _service;

        public HomeWorksServiceTests()
        {
            _homeworkRepositoryMock = new Mock<IHomeworkRepository>();
            _service = new HomeWorksService(_homeworkRepositoryMock.Object);
        }
        

       
        // unit testing name patterns
        // MethodName_Condition_Result
        [TestMethod]
        public void Create_HomeworkIsValid_ShouldCreateNewHomework()
        {

            //arrange - подготавливаем данные

            var fixture = new Fixture();

            var homework = fixture.Build<Homework>()
                //.With(x => x.MemberId, 525)
                .Without(x => x.MentorID)
                .Create();


            //act - запускаем тестируемый метод

            var result = _service.Create(homework);


            //assert - проверяем / вилидируем результаты теста
            result.Should().BeTrue();   
            //Assert.IsTrue(result);
            _homeworkRepositoryMock.Verify(x => x.Add(homework), Times.Once);



        }
        [TestMethod]
        public void Create_HomeworkIsNull_ShouldThrowArgumentNullException()
        {
           
            //arrange - подготавливаем данные

            Homework homework = null;

            //act - запускаем тестируемый метод

            bool result = false;
            var exception = Assert.ThrowsException<ArgumentNullException>(() => result = _service.Create(homework));

            //assert - проверяем / вилидируем результаты теста
            exception.Should().NotBeNull()
                .And
                .Match<ArgumentNullException>(x => x.ParamName == "homework");

           result.Should().BeFalse();
           _homeworkRepositoryMock.Verify(x => x.Add(homework), Times.Never);

        }

        [TestMethod]
        public void Create_HomeworksIsInvalid_ShouldThrowBusinessException()
        {

            //arrange - подготавливаем данные

            var homework = new Homework();

            //act - запускаем тестируемый метод

            bool result = false;
            var exception = Assert.ThrowsException<BusinessException>(() => result = _service.Create(homework));

            //assert - проверяем / вилидируем результаты теста

            exception.Should().NotBeNull()
                .And
                .Match<BusinessException>(x => x.Message == HomeWorksService.HOMEWORK_IS_INVALID);
            result.Should().BeFalse();    

            //Assert.IsNotNull(exception);
            //Assert.AreEqual(HomeWorksService.HOMEWORK_IS_INVALID, exception.Message);
            //Assert.IsFalse(result);
            _homeworkRepositoryMock.Verify(x => x.Add(homework), Times.Never);
        }

        [TestMethod]
        public void Delete_ShouldDeleteHomework()
        {
            
            //arrange - подготавливаем данные
            var fixture = new Fixture();
            var homeworkId = fixture.Create<int>();
            
            //act - запускаем тестируемый метод
            var result = _service.Delete(homeworkId);

            //assert - проверяем / вилидируем результаты теста
            result.Should().BeTrue();  
            _homeworkRepositoryMock.Verify(x=> x.Delete(homeworkId), Times.Once);
        }


    }
}
