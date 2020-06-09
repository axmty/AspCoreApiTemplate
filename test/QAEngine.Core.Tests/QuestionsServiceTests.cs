using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using QAEngine.Core.Exceptions;
using QAEngine.Core.Repositories;
using QAEngine.Core.Services;
using Xunit;

namespace QAEngine.Core.Tests
{
    public class QuestionsServiceTests
    {
        [Fact]
        public async Task GetByIdAsync_WhenRepositoryMethodGetByIdAsync_ReturnsNull_ThrowsNotFoundException()
        {
            var service = QuestionServiceBuilder
                .Init()
                .ConfigureRepository(r => r.GetByIdAsync(3), null)
                .Build();

            var invoke = service.Invoking(s => s.GetByIdAsync(3));

            await invoke.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task GetByIdAsync_WhenRepositoryMethodGetByIdAsync_ReturnsData_ReturnsExpectedModel()
        {
            var returnedData = new Data.Question
            {
                ID = 3,
                Content = "content",
                CreateDate = DateTimeOffset.Parse("2020-01-01")
            };
            var expectedModel = new Models.QuestionRead
            {
                ID = returnedData.ID,
                Content = returnedData.Content,
                CreateDate = returnedData.CreateDate
            };

            var service = QuestionServiceBuilder
                .Init()
                .ConfigureRepository(r => r.GetByIdAsync(3), returnedData)
                .Build();

            var result = await service.GetByIdAsync(3);

            result.Should().BeEquivalentTo(expectedModel);
        }

        private class QuestionServiceBuilder
        {
            private readonly Mock<IQuestionsRepository> _mockedQuestionsRepository;

            private QuestionServiceBuilder()
            {
                _mockedQuestionsRepository = new Mock<IQuestionsRepository>();
            }

            public static QuestionServiceBuilder Init()
            {
                return new QuestionServiceBuilder();
            }

            public QuestionsService Build()
            {
                return new QuestionsService(_mockedQuestionsRepository.Object);
            }

            public QuestionServiceBuilder ConfigureRepository<TResult>(
                Expression<Func<IQuestionsRepository, Task<TResult>>> expression,
                TResult returnedValue)
            {
                _mockedQuestionsRepository.Setup(expression).ReturnsAsync(returnedValue);

                return this;
            }
        }
    }
}
