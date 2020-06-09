using System;
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
            var repository = new Mock<IQuestionsRepository>();
            repository.Setup(r => r.GetByIdAsync(3)).ReturnsAsync((Data.Question)null);
            var service = new QuestionsService(repository.Object);

            var invoke = service.Invoking(s => s.GetByIdAsync(3));

            await invoke.Should().ThrowAsync<NotFoundException>();
            repository.Verify(r => r.GetByIdAsync(3), Times.Once);
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
            var repository = new Mock<IQuestionsRepository>();
            repository.Setup(r => r.GetByIdAsync(3)).ReturnsAsync(returnedData);
            var service = new QuestionsService(repository.Object);

            var result = await service.GetByIdAsync(3);

            repository.Verify(r => r.GetByIdAsync(3), Times.Once);
            result.Should().BeEquivalentTo(expectedModel);
        }
    }
}
