using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.Errors;
using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.VO;
using BlaisePascal.LessonsExamples.Domain.Devices.CctvDevice;
using BlaisePascal.LessonsExamples.Domain.Devices.CctvDevice.VO;

namespace BlaisePascal.LessonsExamples.Domain.UnitTests.Devices.CctvDevice
{
    public class CCTVTests
    {
        private static CCTV CreateAndSwitchOn()
        {
            var device = new CCTV(new DeviceName("CCTV-1"));
            device.SwitchOn();
            return device;
        }

        [Fact]
        public void Constructor_Initializes_Default_State()
        {
            var cctv = new CCTV(new DeviceName("CCTV"));

            Assert.False(cctv.IsRecording);
            Assert.Equal(1, cctv.Zoom.Value);
            Assert.Equal(0, cctv.Orientation.Pan);
            Assert.Equal(0, cctv.Orientation.Tilt);
        }

        [Fact]
        public void StartRecording_WhenDeviceIsOff_Throws()
        {
            var cctv = new CCTV(new DeviceName("CCTV"));

            Assert.Throws<DomainException>(() => cctv.StartRecording());
        }

        [Fact]
        public void StartRecording_WhenNotRecording_StartsRecording()
        {
            var cctv = CreateAndSwitchOn();

            cctv.StartRecording();

            Assert.True(cctv.IsRecording);
        }

        [Fact]
        public void StartRecording_WhenAlreadyRecording_Throws()
        {
            var cctv = CreateAndSwitchOn();
            cctv.StartRecording();

            Assert.Throws<DomainException>(() => cctv.StartRecording());
        }

        [Fact]
        public void StopRecording_WhenRecording_StopsRecording()
        {
            var cctv = CreateAndSwitchOn();
            cctv.StartRecording();

            cctv.StopRecording();

            Assert.False(cctv.IsRecording);
        }

        [Fact]
        public void StopRecording_WhenNotRecording_Throws()
        {
            var cctv = CreateAndSwitchOn();

            Assert.Throws<DomainException>(() => cctv.StopRecording());
        }

        [Fact]
        public void ZoomTo_WhenDeviceIsOn_ChangesZoom()
        {
            var cctv = CreateAndSwitchOn();
            var zoom = new ZoomLevel(10, 0, 100);

            cctv.ZoomTo(zoom);

            Assert.Equal(10, cctv.Zoom.Value);
        }

        [Fact]
        public void ZoomTo_WhenDeviceIsOff_Throws()
        {
            var cctv = new CCTV(new DeviceName("CCTV"));
            var zoom = new ZoomLevel(10, 0, 100);

            Assert.Throws<DomainException>(() => cctv.ZoomTo(zoom));
        }

        [Fact]
        public void MoveTo_WhenDeviceIsOn_ChangesOrientation()
        {
            var cctv = CreateAndSwitchOn();
            var orientation = new CameraOrientation(89, 10);

            cctv.MoveTo(orientation);

            Assert.Equal(89, cctv.Orientation.Pan);
            Assert.Equal(10, cctv.Orientation.Tilt);
        }

        [Fact]
        public void MoveTo_WhenDeviceIsOff_Throws()
        {
            var cctv = new CCTV(new DeviceName("CCTV"));

            Assert.Throws<DomainException>(() =>
                cctv.MoveTo(new CameraOrientation(0, 0)));
        }

        [Fact]
        public void SwitchOff_WhileRecording_Throws()
        {
            var cctv = CreateAndSwitchOn();
            cctv.StartRecording();

            Assert.Throws<DomainException>(() => cctv.SwitchOff());
        }
    }
}
