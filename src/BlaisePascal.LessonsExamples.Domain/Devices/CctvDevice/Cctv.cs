using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions;
using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.Errors;
using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.VO;
using BlaisePascal.LessonsExamples.Domain.Devices.CctvDevice.VO;

namespace BlaisePascal.LessonsExamples.Domain.Devices.CctvDevice
{
    public sealed class CCTV : AbstractDevice, ICCTV
    {
        private ZoomLevel _zoom;
        private CameraOrientation _orientation;

        private RecordingSession? _recordingSession;

        public bool IsRecording => _recordingSession is not null;

        public ZoomLevel Zoom => _zoom;
        public CameraOrientation Orientation => _orientation;

        public CCTV(DeviceName name, DeviceImage? image = null) : base(name, image)
        {
            _zoom = new ZoomLevel(1, 0, 100);
            _orientation = new CameraOrientation(0, 0);
        }

        public void StartRecording()
        {
            EnsureDeviceIsOn();

            if (IsRecording)
                throw new DomainException("CCTV is already recording.");

            _recordingSession = RecordingSession.Start();
            Touch();
        }

        public void StopRecording()
        {
            if (!IsRecording)
                throw new DomainException("CCTV is not recording.");

            _recordingSession?.Stop();
            _recordingSession = null;
            Touch();
        }

        public void ZoomTo(ZoomLevel level)
        {
            EnsureDeviceIsOn();
            _zoom = level;
            Touch();
        }

        public void MoveTo(CameraOrientation orientation)
        {
            EnsureDeviceIsOn();
            _orientation = orientation;
            Touch();
        }

        public override void SwitchOff()
        {
            if (IsRecording)
                throw new DomainException("Cannot switch off while recording.");

            base.SwitchOff();
        }
    }
}
