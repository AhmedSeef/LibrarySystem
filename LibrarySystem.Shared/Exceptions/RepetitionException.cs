namespace LibrarySystem.Shared.Exceptions
{
    public class RepetitionException : Exception
    {
        public RepetitionException() { }

        public RepetitionException(string message) : base(message) { }

        public RepetitionException(string message, Exception innerException) : base(message, innerException) { }
    }
}
