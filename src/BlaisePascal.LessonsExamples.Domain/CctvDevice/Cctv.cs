using BlaisePascal.LessonsExamples.Domain.CctvDevice;
using BlaisePascal.LessonsExamples.Domain.Devices;
using System;

namespace BlaisePascal.LessonsExamples.Domain.CCTV
{
    public class CCTV : AbstractDevice
    {
        // Constants for zoom range, tilt, and pan
        private const int DefaultMinZoom = 1;
        private const int DefaultMaxZoom = 10;
        private const int DefaultInitialZoom = 1;
        // private const int DefaultFieldOfView = 100;

        private const int MinTilt = -90;
        private const int MaxTilt = 90;
        private const int FullCircle = 360;

        // Zoom between MinZoom and MaxZoom
        public int Zoom { get; private set; }
        public int MinZoom { get; }
        public int MaxZoom { get; }

        // Orientation
        public int Pan { get; private set; }   // 0°–360°
        public int Tilt { get; private set; }  // -90° – +90°
        public MovementStatus MovementStatus { get; private set; }

        public RecordingStatus RecordingStatus { get; private set; }

        // public int FieldOfView { get; } // FOV in degrees (probably not used but that's ok)

        public CCTV(
            string name,
            int minZoom = DefaultMinZoom,
            int maxZoom = DefaultMaxZoom,
            int initialZoom = DefaultInitialZoom,
            string? imageUrl = null)
            : base(name, imageUrl)
        {
            if (minZoom < DefaultMinZoom || maxZoom <= minZoom)
                throw new ArgumentException("Zoom ranges are invalid.");

            MinZoom = minZoom;
            MaxZoom = maxZoom;
            Zoom = Math.Clamp(initialZoom, MinZoom, MaxZoom);

            // FieldOfView = fieldOfView;

            Pan = 0;
            Tilt = 0;
            MovementStatus = MovementStatus.Idle;

            RecordingStatus = RecordingStatus.NotRecording;
        }

        public void StartRecording()
        {
            if (Status == DeviceStatus.Off)
                throw new InvalidOperationException("Cannot start recording when CCTV is off.");

            if (RecordingStatus == RecordingStatus.Recording)
                throw new InvalidOperationException($"{Name} is already recording.");

            RecordingStatus = RecordingStatus.Recording;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void StopRecording()
        {
            if (RecordingStatus == RecordingStatus.NotRecording)
                throw new InvalidOperationException($"{Name} is not recording.");

            RecordingStatus = RecordingStatus.NotRecording;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void SetZoom(int zoom)
        {
            if (Status == DeviceStatus.Off)
                throw new InvalidOperationException("Cannot adjust zoom when CCTV is off.");

            Zoom = Math.Clamp(zoom, MinZoom, MaxZoom);
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void Move(int panDelta, int tiltDelta)
        {
            if (Status == DeviceStatus.Off)
                throw new InvalidOperationException("Cannot move the camera when off.");

            MovementStatus = MovementStatus.Moving;

            // Adjust pan, ensuring it wraps around 360 degrees
            Pan = (Pan + panDelta) % FullCircle;
            if (Pan < 0)
                Pan += FullCircle;

            // Clamp tilt to within the valid range
            Tilt = Math.Clamp(Tilt + tiltDelta, MinTilt, MaxTilt);

            MovementStatus = MovementStatus.Idle;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public override void SwitchOff()
        {
            if (RecordingStatus == RecordingStatus.Recording)
                throw new InvalidOperationException("Cannot turn off while recording.");

            // Must implement some shutdown procedure and save recording

            base.SwitchOff();
            LastModifiedAtUtc = DateTime.UtcNow;
        }
    }
}
