namespace BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.Errors
{
    public sealed class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }
    }
}
