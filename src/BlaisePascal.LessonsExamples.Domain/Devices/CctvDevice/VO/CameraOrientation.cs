using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.Errors;

namespace BlaisePascal.LessonsExamples.Domain.Devices.CctvDevice.VO
{
    public sealed record CameraOrientation
    {
        public int Pan { get; }
        public int Tilt { get; }

        public CameraOrientation(int pan, int tilt)
        {
            Pan = ((pan % 360) + 360) % 360;

            if (tilt < -90 || tilt > 90)
                throw new DomainException("Tilt must be between -90 and 90.");

            Tilt = tilt;
        }
    }
}
