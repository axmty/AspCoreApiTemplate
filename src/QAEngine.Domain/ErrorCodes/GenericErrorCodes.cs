namespace QAEngine.Domain.Errors
{
    public static partial class ErrorCodes
    {
        public static class Generic
        {
            public const string BadRequest = "generic_bad_request";
            public const string NotFound = "generic_not_found";
            public const string InternalServerError = "generic_internal_server_error";
        }
    }
}
