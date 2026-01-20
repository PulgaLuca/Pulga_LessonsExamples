namespace BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.Interfaces
{
    public interface IAuditable
    {
        DateTime CreatedAtUtc { get; }
        DateTime LastModifiedAtUtc { get; }
    }
}
