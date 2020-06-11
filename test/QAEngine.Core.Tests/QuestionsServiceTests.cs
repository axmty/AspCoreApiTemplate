using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using QAEngine.Core.Errors;
using QAEngine.Core.Exceptions;
using QAEngine.Core.Repositories;
using QAEngine.Core.Services;
using QAEngine.Tests.Core;
using Xunit;

namespace QAEngine.Core.Tests
{
    public class QuestionsServiceTests
    {
        [Fact]
        [Trait(nameof(Constants.TraitCategory), nameof(QuestionsService.GetAsync))]
        public async Task GetAsync_WhenRepositoryMethodGetAsync_ReturnsData_ReturnsExpectedCollection()
        {
            var returnedData = new Data.Question[]
            {
                new Data.Question 
                {
                    ID = 3,
                    Content = "content",
                    CreateDate = DateTimeOffset.Parse("2019-01-01")
                },
                new Data.Question
                {
                    ID = 5,
                    Content = "other content",
                    CreateDate = DateTimeOffset.Parse("2020-01-01")
                }
            };
            var expected = new Models.Question[]
            {
                new Models.Question
                {
                    ID = returnedData[0].ID,
                    Content = returnedData[0].Content,
                    CreateDate = returnedData[0].CreateDate
                },
                new Models.Question
                {
                    ID = returnedData[1].ID,
                    Content = returnedData[1].Content,
                    CreateDate = returnedData[1].CreateDate
                }
            };

            var builder = QuestionsServiceBuilder.Configure(builder =>
            {
                builder.QuestionsRepository.Setup(r => r.GetAsync()).ReturnsAsync(returnedData);
            });

            var result = await builder.Build().GetAsync();

            result.Should().BeEquivalentTo(expected);
            builder.QuestionsRepository.Verify(r => r.GetAsync(), Times.Once);
        }

        [Fact]
        [Trait(nameof(Constants.TraitCategory), nameof(QuestionsService.GetByIdAsync))]
        public async Task GetByIdAsync_WhenRepositoryMethodGetByIdAsync_ReturnsNull_ThrowsNotFoundException()
        {
            var builder = QuestionsServiceBuilder.Configure(builder =>
            {
                builder.QuestionsRepository.Setup(r => r.GetByIdAsync(3)).ReturnsAsync((Data.Question)null);
            });

            await builder.Build().Invoking(s => s.GetByIdAsync(3)).Should().ThrowDomainExceptionAsync<NotFoundException>(
                "Question [3] does not exist.",
                ErrorCodes.Generic.NotFound);
            builder.QuestionsRepository.Verify(r => r.GetByIdAsync(3), Times.Once);
        }

        [Fact]
        [Trait(nameof(Constants.TraitCategory), nameof(QuestionsService.GetByIdAsync))]
        public async Task GetByIdAsync_WhenRepositoryMethodGetByIdAsync_ReturnsData_ReturnsExpected()
        {
            var returnedData = new Data.Question
            {
                ID = 3,
                Content = "content",
                CreateDate = DateTimeOffset.Parse("2020-01-01")
            };
            var expected = new Models.Question
            {
                ID = returnedData.ID,
                Content = returnedData.Content,
                CreateDate = returnedData.CreateDate
            };

            var builder = QuestionsServiceBuilder.Configure(builder =>
            {
                builder.QuestionsRepository.Setup(r => r.GetByIdAsync(3)).ReturnsAsync(returnedData);
            });

            var result = await builder.Build().GetByIdAsync(3);

            result.Should().BeEquivalentTo(expected);
            builder.QuestionsRepository.Verify(r => r.GetByIdAsync(3), Times.Once);
        }

        private class QuestionsServiceBuilder : SubjectBuilder<QuestionsService, QuestionsServiceBuilder>
        {
            public Mock<IQuestionsRepository> QuestionsRepository { get; private set; }

            public override QuestionsService Build()
            {
                return new QuestionsService(this.QuestionsRepository.Object);
            }

            protected override void Init()
            {
                this.QuestionsRepository = new Mock<IQuestionsRepository>();
            }
        }
    }
}
