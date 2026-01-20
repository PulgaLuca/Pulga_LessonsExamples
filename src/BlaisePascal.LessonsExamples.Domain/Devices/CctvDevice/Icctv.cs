using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.Interfaces;
using BlaisePascal.LessonsExamples.Domain.Devices.CctvDevice.VO;

namespace BlaisePascal.LessonsExamples.Domain.Devices.CctvDevice
{
    public interface ICCTV : IDevice
    {
        bool IsRecording { get; }

        void StartRecording();
        void StopRecording();

        void ZoomTo(ZoomLevel level);
        void MoveTo(CameraOrientation orientation);
    }
}
