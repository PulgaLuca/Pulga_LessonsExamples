using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.Errors;

namespace BlaisePascal.LessonsExamples.Domain.Devices.CctvDevice.VO
{
    public sealed class RecordingSession
    {
        public DateTime StartedAtUtc { get; }
        public DateTime? StoppedAtUtc { get; private set; }

        public bool IsActive => StoppedAtUtc is null;

        private RecordingSession(DateTime startedAtUtc)
        {
            StartedAtUtc = startedAtUtc;
        }

        public static RecordingSession Start()
        {
            return new RecordingSession(DateTime.UtcNow);
        }

        public void Stop()
        {
            if (!IsActive)
                throw new DomainException("Recording session is already stopped.");

            StoppedAtUtc = DateTime.UtcNow;
        }
    }

}
