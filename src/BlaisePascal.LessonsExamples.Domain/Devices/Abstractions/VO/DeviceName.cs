using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.Errors;

namespace BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.VO
{
    public sealed record DeviceName
    {
        public string Value { get; }

        public DeviceName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Device name cannot be empty.");

            Value = value;
        }

        public override string ToString() => Value;
    }
}
