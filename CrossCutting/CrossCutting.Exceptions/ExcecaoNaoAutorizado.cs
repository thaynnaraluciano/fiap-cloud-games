namespace CrossCutting.Exceptions
{
    public class ExcecaoNaoAutorizado : Exception
    {
        public int StatusCode { get; } = 401;

        public ExcecaoNaoAutorizado(string message)
            : base(message)
        {
        }

        public ExcecaoNaoAutorizado(string message, int statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
