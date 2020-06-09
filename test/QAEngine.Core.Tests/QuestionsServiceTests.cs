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
            var service = QuestionsServiceBuilder.Configure(builder =>
            {
                builder.MockedQuestionsRepository.Setup(r => r.GetByIdAsync(3)).ReturnsAsync((Data.Question)null);
            }).Build();

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

            var service = QuestionsServiceBuilder.Configure(builder =>
            {
                builder.MockedQuestionsRepository.Setup(r => r.GetByIdAsync(3)).ReturnsAsync(returnedData);
            }).Build();

            var result = await service.GetByIdAsync(3);

            result.Should().BeEquivalentTo(expectedModel);
        }

        private class QuestionsServiceBuilder
        {
            private QuestionsServiceBuilder()
            {
                this.MockedQuestionsRepository = new Mock<IQuestionsRepository>();
            }
            
            public Mock<IQuestionsRepository> MockedQuestionsRepository { get; }

            public static QuestionsServiceBuilder Configure(Action<QuestionsServiceBuilder> options)
            {
                var builder = new QuestionsServiceBuilder();
                
                options(builder);

                return builder;
            }

            public QuestionsService Build()
            {
                return new QuestionsService(this.MockedQuestionsRepository.Object);
            }

            public QuestionsServiceBuilder ConfigureRepository<TResult>(
                Expression<Func<IQuestionsRepository, Task<TResult>>> expression,
                TResult returnedValue)
            {
                this.MockedQuestionsRepository.Setup(expression).ReturnsAsync(returnedValue);

                return this;
            }
        }
    }
}
