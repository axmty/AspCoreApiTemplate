using System;
using System.Collections.Generic;
using System.Net;
using BookstoreApi.Core.Exceptions;

namespace BookstoreApi.App.Middleware
{
    public static class ExceptionStatusCodeConverter
    {
        private static readonly Dictionary<Type, HttpStatusCode> ExceptionToStatusCodesMapping = new()
        {
            [typeof(ResourceNotFoundException)] = HttpStatusCode.NotFound
        };

        public static HttpStatusCode Convert(Type exceptionType)
        {
            if (!exceptionType.IsAssignableTo(typeof(Exception)))
            {
                throw new ArgumentException("Expected an exception type.", nameof(exceptionType));
            }

            if (exceptionType.IsGenericType)
            {
                exceptionType = exceptionType.GetGenericTypeDefinition();
            }

            var code = HttpStatusCode.InternalServerError;
            if (exceptionType.IsAssignableTo(typeof(DomainException)) && !ExceptionToStatusCodesMapping.TryGetValue(exceptionType, out code))
            {
                throw new InvalidOperationException($"Type {exceptionType} is not mapped to an HTTP status code.");
            }

            return code;
        }
    }
}
