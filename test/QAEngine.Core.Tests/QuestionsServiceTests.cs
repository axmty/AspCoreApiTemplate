using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using QAEngine.Core.Errors;
using QAEngine.Core.Exceptions;
using QAEngine.Core.Repositories;
using QAEngine.Core.Services;
using QAEngine.Tests.Utils;
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
                builder.QuestionsRepository.Setup(r => r.GetByIdAsync(3)).ReturnsAsync((Data.Question)null);
            }).Build();

            await service.Invoking(s => s.GetByIdAsync(3)).Should().ThrowDomainExceptionAsync<NotFoundException>(
                "Question [3] does not exist.",
                ErrorCodes.Generic.NotFound);
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
                builder.QuestionsRepository.Setup(r => r.GetByIdAsync(3)).ReturnsAsync(returnedData);
            }).Build();

            var result = await service.GetByIdAsync(3);

            result.Should().BeEquivalentTo(expectedModel);
        }

        private class QuestionsServiceBuilder
        {
            private QuestionsServiceBuilder()
            {
                this.QuestionsRepository = new Mock<IQuestionsRepository>();
            }
            
            public Mock<IQuestionsRepository> QuestionsRepository { get; }

            public static QuestionsServiceBuilder Configure(Action<QuestionsServiceBuilder> options)
            {
                var builder = new QuestionsServiceBuilder();
                
                options(builder);

                return builder;
            }

            public QuestionsService Build()
            {
                return new QuestionsService(this.QuestionsRepository.Object);
            }
        }
    }
}
