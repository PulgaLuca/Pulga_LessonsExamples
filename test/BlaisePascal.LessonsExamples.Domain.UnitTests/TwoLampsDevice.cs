using BlaisePascal.LessonsExamples.Domain.LuminousDevices;
using BlaisePascal.LessonsExamples.Domain.Shared;
using System;
using Xunit;

namespace BlaisePascal.LessonsExamples.Domain.Tests
{
    public class TwoLampsDeviceTests
    {
        private readonly Lamp _lamp1;
        private readonly EcoLamp _lamp2;
        private readonly TwoLampsDevice _device;

        public TwoLampsDeviceTests()
        {
            _lamp1 = new Lamp("Standard Lamp");
            _lamp2 = new EcoLamp("Eco Lamp");
            _device = new TwoLampsDevice(_lamp1, _lamp2);
        }

        // --- COSTRUTTORE ---
        [Fact]
        public void Constructor_ShouldAssignBothLamps()
        {
            Assert.NotNull(_device.Lamp1);
            Assert.NotNull(_device.Lamp2);
            Assert.Equal("Standard Lamp", _device.Lamp1.Name);
            Assert.Equal("Eco Lamp", _device.Lamp2.Name);
        }

        [Fact]
        public void Constructor_NullLamp1_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new TwoLampsDevice(null!, new Lamp("L2")));
        }

        [Fact]
        public void Constructor_NullLamp2_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new TwoLampsDevice(new Lamp("L1"), null!));
        }

        // --- SWITCH ON ---
        [Fact]
        public void SwitchOn_ShouldTurnOnBothLamps()
        {
            _device.SwitchOn();

            Assert.Equal(DeviceStatus.On, _lamp1.Status);
            Assert.Equal(DeviceStatus.On, _lamp2.Status);
        }

        [Fact]
        public void SwitchOn_OneLamp_ShouldTurnOnSelected()
        {
            _device.SwitchOn(2);

            Assert.Equal(DeviceStatus.Off, _lamp1.Status);
            Assert.Equal(DeviceStatus.On, _lamp2.Status);
        }

        [Fact]
        public void SwitchOn_InvalidLampIndex_ShouldThrow()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _device.SwitchOn(3));
        }

        // --- SWITCH OFF ---
        [Fact]
        public void SwitchOff_ShouldTurnOffBothLamps()
        {
            _device.SwitchOn();
            _device.SwitchOff();

            Assert.Equal(DeviceStatus.Off, _lamp1.Status);
            Assert.Equal(DeviceStatus.Off, _lamp2.Status);
        }

        [Fact]
        public void SwitchOff_OneLamp_ShouldTurnOffSelected()
        {
            _device.SwitchOn();
            _device.SwitchOff(1);

            Assert.Equal(DeviceStatus.Off, _lamp1.Status);
            Assert.Equal(DeviceStatus.On, _lamp2.Status);
        }

        [Fact]
        public void SwitchOff_InvalidLampIndex_ShouldThrow()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _device.SwitchOff(0));
        }

        // --- TOGGLE ---
        [Fact]
        public void Toggle_ShouldToggleBothLamps()
        {
            _device.Toggle();

            Assert.Equal(DeviceStatus.On, _lamp1.Status);
            Assert.Equal(DeviceStatus.On, _lamp2.Status);

            _device.Toggle();

            Assert.Equal(DeviceStatus.Off, _lamp1.Status);
            Assert.Equal(DeviceStatus.Off, _lamp2.Status);
        }

        [Fact]
        public void Toggle_SingleLamp_ShouldToggleOnlyThatLamp()
        {
            _device.Toggle(1);

            Assert.Equal(DeviceStatus.On, _lamp1.Status);
            Assert.Equal(DeviceStatus.Off, _lamp2.Status);
        }

        [Fact]
        public void Toggle_InvalidLampIndex_ShouldThrow()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _device.Toggle(99));
        }

        // --- SET INTENSITY ---
        [Fact]
        public void SetIntensity_ShouldApplyToBothLamps()
        {
            _device.SwitchOn();
            _device.SetIntensity(40);

            Assert.Equal(40, _lamp1.Intensity);
            Assert.Equal(40, _lamp2.Intensity);
        }

        [Fact]
        public void SetIntensity_SingleLamp_ShouldAffectOnlyOne()
        {
            _device.SwitchOn();
            _device.SetIntensity(1, 60);

            Assert.Equal(60, _lamp1.Intensity);
            Assert.NotEqual(60, _lamp2.Intensity);
        }

        [Fact]
        public void SetIntensity_InvalidLampIndex_ShouldThrow()
        {
            _device.SwitchOn();
            Assert.Throws<ArgumentOutOfRangeException>(() => _device.SetIntensity(3, 40));
        }

        // --- DIMMER ---
        [Fact]
        public void Dimmer_ShouldLowerIntensityOfBoth()
        {
            _device.SwitchOn();
            _device.SetIntensity(60);

            _device.Dimmer(20);

            Assert.Equal(40, _lamp1.Intensity);
            Assert.Equal(40, _lamp2.Intensity);
        }

        [Fact]
        public void Dimmer_SingleLamp_ShouldAffectOnlyOne()
        {
            _device.SwitchOn();
            _device.SetIntensity(50);

            _device.Dimmer(2, 30);

            Assert.Equal(50, _lamp1.Intensity);
            Assert.Equal(20, _lamp2.Intensity);
        }

        [Fact]
        public void Dimmer_InvalidLampIndex_ShouldThrow()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _device.Dimmer(0, 10));
        }

        // --- BRIGHTEN ---
        [Fact]
        public void Brighten_ShouldRaiseIntensityOfBoth()
        {
            _device.SwitchOn();
            _device.SetIntensity(40);

            _device.Brighten(20);

            Assert.Equal(60, _lamp1.Intensity);
            Assert.Equal(60, _lamp2.Intensity);
        }

        [Fact]
        public void Brighten_SingleLamp_ShouldAffectOnlyOne()
        {
            _device.SwitchOn();
            _device.SetIntensity(30);

            _device.Brighten(1, 40);

            Assert.Equal(70, _lamp1.Intensity);
            Assert.Equal(30, _lamp2.Intensity);
        }

        [Fact]
        public void Brighten_InvalidLampIndex_ShouldThrow()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _device.Brighten(99, 10));
        }

        // --- CHECK AUTO OFF ---
        [Fact]
        public void CheckAutoOff_ShouldTriggerOnlyEcoLamp()
        {
            _device.SwitchOn();

            typeof(EcoLamp)
                .GetField("AutoOffAtUtc", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(_lamp2, DateTime.UtcNow.AddMinutes(-1));

            _device.CheckAutoOff();

            Assert.Equal(DeviceStatus.On, _lamp1.Status); // normale
            Assert.Equal(DeviceStatus.Off, _lamp2.Status); // eco auto-off
        }
    }
}
