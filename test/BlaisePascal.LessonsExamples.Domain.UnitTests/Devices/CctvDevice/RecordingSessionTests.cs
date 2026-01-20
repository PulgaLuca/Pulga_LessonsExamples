using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.Errors;
using BlaisePascal.LessonsExamples.Domain.Devices.CctvDevice.VO;

namespace BlaisePascal.LessonsExamples.Domain.UnitTests.Devices.CctvDevice
{
    public class RecordingSessionTests
    {
        [Fact]
        public void Start_CreatesActiveSession()
        {
            var session = RecordingSession.Start();

            Assert.True(session.IsActive);
            Assert.NotEqual(default, session.StartedAtUtc);
            Assert.Null(session.StoppedAtUtc);
        }

        [Fact]
        public void Stop_StopsActiveSession()
        {
            var session = RecordingSession.Start();

            session.Stop();

            Assert.False(session.IsActive);
            Assert.NotNull(session.StoppedAtUtc);
        }

        [Fact]
        public void Stop_WhenAlreadyStopped_Throws()
        {
            var session = RecordingSession.Start();
            session.Stop();

            Assert.Throws<DomainException>(() => session.Stop());
        }
    }
}
