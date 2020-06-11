using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Specialized;
using QAEngine.Domain.Exceptions;

namespace QAEngine.Tests.Core
{
    public static class FluentAssertionsExtensions
    {
        public static async Task ThrowDomainExceptionAsync<TDomainException>(
            this AsyncFunctionAssertions assertions,
            string message,
            string errorCode)
            where TDomainException : DomainException
        {
            var exceptionAssertion = await assertions.ThrowExactlyAsync<TDomainException>();

            exceptionAssertion.WithMessage(message).And.ErrorCode.Should().Be(errorCode);
        }
    }
}
