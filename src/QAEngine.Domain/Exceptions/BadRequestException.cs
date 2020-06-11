using QAEngine.Domain.Errors;

namespace QAEngine.Domain.Exceptions
{
    public class BadRequestException : DomainException
    {
        public BadRequestException(string message)
            : this(message, ErrorCodes.Generic.BadRequest)
        {
        }

        public BadRequestException(string message, string errorCode)
            : base(message, errorCode)
        {
        }

        public override int StatusCode => 400;
    }
}
