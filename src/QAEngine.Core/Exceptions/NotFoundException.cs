using QAEngine.Core.Errors;

namespace QAEngine.Core.Exceptions
{
    public class NotFoundException : DomainException
    {
        public NotFoundException(string message)
            : this(message, ErrorCodes.Generic.NotFound)
        {
        }

        public NotFoundException(string message, string errorCode)
            : base(message, errorCode)
        {
        }

        public override int StatusCode => 404;
    }
}
