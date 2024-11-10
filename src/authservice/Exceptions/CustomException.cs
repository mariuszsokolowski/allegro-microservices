namespace authservice.Exceptions
{
    internal abstract class CustomException : Exception
    {
        protected CustomException(string message) : base(message)
        {
        }
    }
}
