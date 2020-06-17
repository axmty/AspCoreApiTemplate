using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using QAEngine.Domain.Persistence;
using QAEngine.Domain.Errors;
using QAEngine.Domain.Exceptions;
using QAEngine.Domain.Resources;
using QAEngine.Domain.Services;
using QAEngine.Tests.Core;
using Xunit;

namespace QAEngine.Domain.Tests
{
    public class QuestionsServiceTests
    {
        [Fact]
        [Trait(Constants.TraitCategory, nameof(QuestionsService.ListAsync))]
        public async Task ListAsync_WhenRepositoryMethodGetAsync_ReturnsData_ReturnsExpectedCollection()
        {
            var builder = QuestionsServiceBuilder.Configure(builder =>
            {
                var returnedData = new Question[]
                {
                    new Question
                    {
                        Id = 3,
                        Content = "content",
                        CreateDate = DateTimeOffset.Parse("2019-01-01"),
                        IsClosed = true
                    },
                    new Question
                    {
                        Id = 5,
                        Content = "other content",
                        CreateDate = DateTimeOffset.Parse("2020-01-01"),
                        IsClosed = false
                    }
                };

                builder.QuestionsRepository.Setup(r => r.ListAsync()).ReturnsAsync(returnedData);
            });

            var expected = new QuestionResponse[]
{
                new QuestionResponse
                {
                    Id = 3,
                    Content = "content",
                    CreateDate = DateTimeOffset.Parse("2019-01-01"),
                    IsClosed = true
                },
                new QuestionResponse
                {
                    Id = 5,
                    Content = "other content",
                    CreateDate = DateTimeOffset.Parse("2020-01-01"),
                    IsClosed = false
                }
            };

            var result = await builder.Build().ListAsync();

            result.Should().BeEquivalentTo(expected);
            builder.QuestionsRepository.Verify(r => r.ListAsync(), Times.Once);
        }

        [Fact]
        [Trait(Constants.TraitCategory, nameof(QuestionsService.GetByIdAsync))]
        public async Task GetByIdAsync_WhenRepositoryMethodGetByIdAsync_ReturnsNull_ThrowsNotFoundException()
        {
            var builder = QuestionsServiceBuilder.Configure(builder =>
            {
                builder.QuestionsRepository.Setup(r => r.GetByIdAsync(3)).ReturnsAsync((Question)null);
            });

            await builder.Build().Invoking(s => s.GetByIdAsync(3)).Should().ThrowDomainExceptionAsync<NotFoundException>(
                "Question [3] does not exist.",
                ErrorCodes.Generic.NotFound);
            builder.QuestionsRepository.Verify(r => r.GetByIdAsync(3), Times.Once);
        }

        [Fact]
        [Trait(Constants.TraitCategory, nameof(QuestionsService.GetByIdAsync))]
        public async Task GetByIdAsync_WhenRepositoryMethodGetByIdAsync_ReturnsData_ReturnsExpected()
        {
            var builder = QuestionsServiceBuilder.Configure(builder =>
            {
                var returnedData = new Question
                {
                    Id = 3,
                    Content = "content",
                    CreateDate = DateTimeOffset.Parse("2020-01-01"),
                    IsClosed = true
                };

                builder.QuestionsRepository.Setup(r => r.GetByIdAsync(3)).ReturnsAsync(returnedData);
            });

            var expected = new QuestionResponse
            {
                Id = 3,
                Content = "content",
                CreateDate = DateTimeOffset.Parse("2020-01-01"),
                IsClosed = true
            };

            var result = await builder.Build().GetByIdAsync(3);

            result.Should().BeEquivalentTo(expected);
            builder.QuestionsRepository.Verify(r => r.GetByIdAsync(3), Times.Once);
        }

        [Fact]
        [Trait(Constants.TraitCategory, nameof(QuestionsService.CreateAsync))]
        public async Task CreateAsync_WhenRepositoryMethodCreateAsync_ReturnsData_ReturnsExpected()
        {
            var builder = QuestionsServiceBuilder.Configure(builder =>
            {
                builder.QuestionsRepository.Setup(r => r.CreateAsync(It.IsAny<QuestionCreate>())).ReturnsAsync(3);
            });

            var result = await builder.Build().CreateAsync(new QuestionCreateRequest());

            result.Should().Be(3);
            builder.QuestionsRepository.Verify(r => r.CreateAsync(It.IsAny<QuestionCreate>()), Times.Once);
        }

        private class QuestionsServiceBuilder : SubjectBuilder<QuestionsService, QuestionsServiceBuilder>
        {
            public Mock<IQuestionsRepository> QuestionsRepository { get; private set; }

            public override QuestionsService Build()
            {
                return new QuestionsService(this.UseConfiguredMapper(), this.QuestionsRepository.Object);
            }

            protected override void Init()
            {
                this.QuestionsRepository = new Mock<IQuestionsRepository>();
            }
        }
    }
}
