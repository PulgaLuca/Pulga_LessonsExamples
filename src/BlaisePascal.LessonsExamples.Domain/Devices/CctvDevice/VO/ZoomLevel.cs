using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.Errors;

namespace BlaisePascal.LessonsExamples.Domain.Devices.CctvDevice.VO
{
    public sealed record ZoomLevel
    {
        public int Value { get; }

        public ZoomLevel(int value, int min, int max)
        {
            if (value < min || value > max)
                throw new DomainException("Invalid zoom level.");

            Value = value;
        }
    }
}
