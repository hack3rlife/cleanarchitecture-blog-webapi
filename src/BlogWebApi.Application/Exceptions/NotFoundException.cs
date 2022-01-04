using System;

namespace BlogWebApi.Application.Exceptions
{
    public class NotFoundException: ApplicationException
    {
        public NotFoundException(string name, object key)
            : base($"The {name}: {key} does not exist.")
        {
        }
    }
}
