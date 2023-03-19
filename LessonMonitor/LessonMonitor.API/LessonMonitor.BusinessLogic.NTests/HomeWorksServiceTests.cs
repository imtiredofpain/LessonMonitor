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

            //arrange - �������������� ������

            var fixture = new Fixture();

            var homework = fixture.Build<Homework>()
                //.With(x => x.MemberId, 525)
                .Without(x => x.MentorID)
                .Create();


            //act - ��������� ����������� �����

            var result = _service.Create(homework);


            //assert - ��������� / ���������� ���������� �����
            result.Should().BeTrue();
            //Assert.IsTrue(result);
            _homeworkRepositoryMock.Verify(x => x.Add(homework), Times.Once);



        }
        [Test]
        public void Create_HomeworkIsNull_ShouldThrowArgumentNullException()
        {

            //arrange - �������������� ������

            Homework homework = null;

            //act - ��������� ����������� �����

            bool result = false;
            var exception = Assert.Throws<ArgumentNullException>(() => result = _service.Create(homework));

            //assert - ��������� / ���������� ���������� �����
            exception.Should().NotBeNull()
                .And
                .Match<ArgumentNullException>(x => x.ParamName == "homework");

            result.Should().BeFalse();
            _homeworkRepositoryMock.Verify(x => x.Add(homework), Times.Never);

        }

        [Test]
        public void Create_HomeworksIsInvalid_ShouldThrowBusinessException()
        {

            //arrange - �������������� ������

            var homework = new Homework();

            //act - ��������� ����������� �����

            bool result = false;
            var exception = Assert.Throws<BusinessException>(() => result = _service.Create(homework));

            //assert - ��������� / ���������� ���������� �����

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

            //arrange - �������������� ������
            var fixture = new Fixture();
            var homeworkId = fixture.Create<int>();

            //act - ��������� ����������� �����
            var result = _service.Delete(homeworkId);

            //assert - ��������� / ���������� ���������� �����
            result.Should().BeTrue();
            _homeworkRepositoryMock.Verify(x => x.Delete(homeworkId), Times.Once);
        }


    }
}