namespace Film.Client.Exceptions
{
    public sealed class FilmDomainRoleException : FilmDomainException
    {
        private new const string Message = "Current user cann't edit this film";

        internal FilmDomainRoleException() : base(Message)
        {
        }

    }
}
