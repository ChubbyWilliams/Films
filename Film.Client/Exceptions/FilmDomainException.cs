using System;

namespace Film.Client.Exceptions
{
    public class FilmDomainException : Exception
    {
        internal FilmDomainException()
        {
        }

        internal FilmDomainException(string message) : base(message)
        {
        }

        internal FilmDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
