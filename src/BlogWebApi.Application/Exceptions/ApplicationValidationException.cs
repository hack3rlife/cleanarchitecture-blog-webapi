using System;
using System.Collections.Generic;

namespace BlogWebApi.Application.Exceptions
{
    public class ApplicationValidationException : ApplicationException
    {
        public IDictionary<string, string[]> Errors { get; }

        public ApplicationValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }
    }
}
