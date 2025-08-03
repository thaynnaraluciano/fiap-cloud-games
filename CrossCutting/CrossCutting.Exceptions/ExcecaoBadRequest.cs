namespace CrossCutting.Exceptions
{
    public class ExcecaoBadRequest : Exception
    {
        public int StatusCode { get; } = 400;

        public ExcecaoBadRequest(string message)
            : base(message)
        {
        }

        public ExcecaoBadRequest(string message, int statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
