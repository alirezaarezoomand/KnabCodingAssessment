namespace Knab.CodingAssessment.Seedwork.Domain
{
    public class DomainException : Exception
    {
        public DomainException(string message, object? info = null) : base(message)
        {
            Info = info;
        }

        public DomainException(string message, Exception innerException, object? info = null) : base(message, innerException)
        {
            Info = info;
        }

        public object? Info { get; protected set; }
    }
}