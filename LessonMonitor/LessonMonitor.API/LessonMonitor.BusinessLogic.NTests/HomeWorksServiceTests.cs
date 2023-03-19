using LessonMonitor.Core.Exceptions;
using LessonMonitor.Core;
using NUnit.Framework;
using System;
using AutoFixture;
using FluentAssertions;
using Moq;

namespace LessonMonitor.BusinessLogic.NTests
{
    
    public class HomeWorksServiceTests
    {
        private Mock<IHomeworkRepository> _homeworkRepositoryMock;
        private HomeWorksService _service;
        public HomeWorksServiceTests()
        {

        }

        [SetUp]
        public void SetUp()
        {
            _homeworkRepositoryMock = new Mock<IHomeworkRepository>();
            _service = new HomeWorksService(_homeworkRepositoryMock.Object);
        }


        // unit testing name patterns
        // MethodName_Condition_Result
        [Test]
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
        [Test]
        public void Create_HomeworkIsNull_ShouldThrowArgumentNullException()
        {

            //arrange - подготавливаем данные

            Homework homework = null;

            //act - запускаем тестируемый метод

            bool result = false;
            var exception = Assert.Throws<ArgumentNullException>(() => result = _service.Create(homework));

            //assert - проверяем / вилидируем результаты теста
            exception.Should().NotBeNull()
                .And
                .Match<ArgumentNullException>(x => x.ParamName == "homework");

            result.Should().BeFalse();
            _homeworkRepositoryMock.Verify(x => x.Add(homework), Times.Never);

        }

        [Test]
        public void Create_HomeworksIsInvalid_ShouldThrowBusinessException()
        {

            //arrange - подготавливаем данные

            var homework = new Homework();

            //act - запускаем тестируемый метод

            bool result = false;
            var exception = Assert.Throws<BusinessException>(() => result = _service.Create(homework));

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

        [Test]
        public void Delete_ShouldDeleteHomework()
        {

            //arrange - подготавливаем данные
            var fixture = new Fixture();
            var homeworkId = fixture.Create<int>();

            //act - запускаем тестируемый метод
            var result = _service.Delete(homeworkId);

            //assert - проверяем / вилидируем результаты теста
            result.Should().BeTrue();
            _homeworkRepositoryMock.Verify(x => x.Delete(homeworkId), Times.Once);
        }


    }
}