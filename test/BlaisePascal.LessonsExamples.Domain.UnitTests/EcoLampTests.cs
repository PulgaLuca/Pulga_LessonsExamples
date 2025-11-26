using BlaisePascal.LessonsExamples.Domain.LuminousDevices;
using BlaisePascal.LessonsExamples.Domain.Shared;
using System;
using Xunit;

namespace BlaisePascal.LessonsExamples.Domain.UnitTests
{
    public class EcoLampTests
    {
        // --- SWITCH ON ---
        [Fact]
        public void SwitchOn_ShouldSetStatusOn_AndDefaultEcoIntensity()
        {
            var lamp = new EcoLamp("Eco Lamp");

            lamp.SwitchOn();

            Assert.Equal(DeviceStatus.On, lamp.Status);
            Assert.Equal(lamp.GetEcoDefaultIntensity(), lamp.Intensity);
        }

        [Fact]
        public void SwitchOn_WhenAlreadyOn_ShouldThrow()
        {
            var lamp = new EcoLamp("Eco Lamp");
            lamp.SwitchOn();

            Assert.Throws<InvalidOperationException>(() => lamp.SwitchOn());
        }

        // --- SWITCH OFF ---
        [Fact]
        public void SwitchOff_ShouldSetStatusOff_AndMinIntensity()
        {
            var lamp = new EcoLamp("Eco Lamp");
            lamp.SwitchOn();

            lamp.SwitchOff();

            Assert.Equal(DeviceStatus.Off, lamp.Status);
            Assert.Equal(lamp.GetEcoMinIntensity(), lamp.Intensity);
        }

        [Fact]
        public void SwitchOff_WhenAlreadyOff_ShouldThrow()
        {
            var lamp = new EcoLamp("Eco Lamp");

            Assert.Throws<InvalidOperationException>(() => lamp.SwitchOff());
        }

        // --- SET INTENSITY ---
        [Fact]
        public void SetIntensity_ShouldUpdateIntensity_WhenValid()
        {
            var lamp = new EcoLamp("Eco Lamp");
            lamp.SwitchOn();

            lamp.SetIntensity(50);

            Assert.Equal(50, lamp.Intensity);
        }

        [Fact]
        public void SetIntensity_BelowEcoMinimum_ShouldThrow()
        {
            var lamp = new EcoLamp("Eco Lamp");
            lamp.SwitchOn();

            Assert.Throws<ArgumentOutOfRangeException>(() => lamp.SetIntensity(-5));
        }

        [Fact]
        public void SetIntensity_AboveEcoMaximum_ShouldThrow()
        {
            var lamp = new EcoLamp("Eco Lamp");
            lamp.SwitchOn();

            Assert.Throws<ArgumentOutOfRangeException>(() => lamp.SetIntensity(80));
        }

        [Fact]
        public void SetIntensity_WhenOff_ShouldThrow()
        {
            var lamp = new EcoLamp("Eco Lamp");

            Assert.Throws<InvalidOperationException>(() => lamp.SetIntensity(40));
        }

        // --- DIMMER ---
        [Fact]
        public void Dimmer_ShouldDecreaseIntensity()
        {
            var lamp = new EcoLamp("Eco Lamp");
            lamp.SwitchOn();
            lamp.SetIntensity(60);

            lamp.Dimmer(20);

            Assert.Equal(40, lamp.Intensity);
        }

        [Fact]
        public void Dimmer_WhenOff_ShouldThrow()
        {
            var lamp = new EcoLamp("Eco Lamp");

            Assert.Throws<InvalidOperationException>(() => lamp.Dimmer(-1));
        }

        [Fact]
        public void Dimmer_CannotDecreaseBelowZero_ShouldThrow()
        {
            var lamp = new EcoLamp("Eco Lamp");
            lamp.SwitchOn();
            lamp.SetIntensity(0);

            Assert.Throws<InvalidOperationException>(() => lamp.Dimmer(10));
        }

        // --- BRIGHTEN ---
        [Fact]
        public void Brighten_ShouldIncreaseIntensity()
        {
            var lamp = new EcoLamp("Eco Lamp");
            lamp.SwitchOn();
            lamp.SetIntensity(40);

            lamp.Brighten(20);

            Assert.Equal(60, lamp.Intensity);
        }

        [Fact]
        public void Brighten_WhenOff_ShouldThrow()
        {
            var lamp = new EcoLamp("Eco Lamp");

            Assert.Throws<InvalidOperationException>(() => lamp.Brighten(-1));
        }

        [Fact]
        public void Brighten_CannotIncreaseBeyondMax_ShouldThrow()
        {
            var lamp = new EcoLamp("Eco Lamp");
            lamp.SwitchOn();
            lamp.SetIntensity(70);

            Assert.Throws<InvalidOperationException>(() => lamp.Brighten(10));
        }

        // --- TOGGLE ---
        [Fact]
        public void Toggle_FromOff_ShouldTurnOn()
        {
            var lamp = new EcoLamp("Eco Lamp");

            lamp.Toggle();

            Assert.Equal(DeviceStatus.On, lamp.Status);
            Assert.Equal(lamp.GetEcoDefaultIntensity(), lamp.Intensity);
        }

        [Fact]
        public void Toggle_FromOn_ShouldTurnOff()
        {
            var lamp = new EcoLamp("Eco Lamp");
            lamp.SwitchOn();

            lamp.Toggle();

            Assert.Equal(DeviceStatus.Off, lamp.Status);
            Assert.Equal(lamp.GetEcoMinIntensity(), lamp.Intensity);
        }

        // --- AUTO OFF ---
        [Fact]
        public void CheckAutoOff_ShouldTurnOff_WhenTimeHasElapsed()
        {
            var lamp = new EcoLamp("Eco Lamp");
            lamp.SwitchOn();

            // Simula il passare del tempo
            typeof(EcoLamp)
                .GetField("AutoOffAtUtc", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(lamp, DateTime.UtcNow.AddMinutes(-1));

            lamp.CheckAutoOff();

            Assert.Equal(DeviceStatus.Off, lamp.Status);
            Assert.Equal(lamp.GetEcoMinIntensity(), lamp.Intensity);
        }

        [Fact]
        public void CheckAutoOff_ShouldNotTurnOff_WhenTimeNotElapsed()
        {
            var lamp = new EcoLamp("Eco Lamp");
            lamp.SwitchOn();

            lamp.CheckAutoOff();

            Assert.Equal(DeviceStatus.On, lamp.Status);
            Assert.Equal(lamp.GetEcoDefaultIntensity(), lamp.Intensity);
        }
    }
}
