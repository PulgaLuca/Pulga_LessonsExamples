using System;
using Xunit;

namespace BlaisePascal.LessonsExamples.Domain.UnitTests
{
    public class LampTests
    {
        // --- SWITCH ON ---
        [Fact]
        public void SwitchOn_ShouldSetStatusOn_AndDefaultIntensity()
        {
            var lamp = new Lamp("Lamp");

            lamp.SwitchOn();

            Assert.Equal(DeviceStatus.On, lamp.Status);
            Assert.Equal(lamp.DefaultIntensity, lamp.Intensity);
        }

        [Fact]
        public void SwitchOn_WhenAlreadyOn_ShouldThrow()
        {
            var lamp = new Lamp("Lamp");
            
            lamp.SwitchOn();

            Assert.Throws<InvalidOperationException>(() => lamp.SwitchOn());
        }

        // --- SWITCH OFF ---
        [Fact]
        public void SwitchOff_ShouldSetStatusOff_AndMinIntensity()
        {
            var lamp = new Lamp("Lamp");
            lamp.SwitchOn();

            lamp.SwitchOff();

            Assert.Equal(DeviceStatus.Off, lamp.Status);
            Assert.Equal(lamp.DefaultIntensity, lamp.Intensity);
        }

        [Fact]
        public void SwitchOff_WhenAlreadyOff_ShouldThrow()
        {
            var lamp = new Lamp("Lamp");
            Assert.Throws<InvalidOperationException>(() => lamp.SwitchOff());
        }

        // --- SET INTENSITY ---
        [Fact]
        public void SetIntensity_ShouldUpdateIntensity_WhenValid()
        {
            var lamp = new Lamp("Lamp");
            lamp.SwitchOn();

            lamp.SetIntensity(80);

            Assert.Equal(80, lamp.Intensity);
        }

        [Fact]
        public void SetIntensity_BelowMinimum_ShouldThrow()
        {
            // Arrange
            var lamp = new Lamp("Lamp");
            
            // Act
            lamp.SwitchOn();

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => lamp.SetIntensity(-10));
        }

        [Fact]
        public void SetIntensity_AboveMaximum_ShouldThrow()
        {
            // Arrange
            var lamp = new Lamp("Lamp");
            
            // Act
            lamp.SwitchOn();

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => lamp.SetIntensity(101));
        }

        [Fact]
        public void SetIntensity_WhenOff_ShouldThrow()
        {
            var lamp = new Lamp("Lamp");
            Assert.Throws<InvalidOperationException>(() => lamp.SetIntensity(30));
        }

        // --- DIMMER ---
        [Fact]
        public void Dimmer_ShouldDecreaseIntensity()
        {
            var lamp = new Lamp("Lamp");
            lamp.SwitchOn();
            lamp.SetIntensity(80);

            lamp.Dimmer(20);

            Assert.Equal(60, lamp.Intensity);
        }

        [Fact]
        public void Dimmer_WhenOff_ShouldThrow()
        {
            var lamp = new Lamp("Lamp");
            Assert.Throws<InvalidOperationException>(() => lamp.Dimmer(-1));
        }

        [Fact]
        public void Dimmer_CannotDecreaseBelowZero_ShouldThrow()
        {
            var lamp = new Lamp("Lamp");
            lamp.SwitchOn();
            lamp.SetIntensity(0);

            Assert.Throws<InvalidOperationException>(() => lamp.Dimmer(10));
        }

        // --- BRIGHTEN ---
        [Fact]
        public void Brighten_ShouldIncreaseIntensity()
        {
            var lamp = new Lamp("Lamp");
            lamp.SwitchOn();
            lamp.SetIntensity(50);

            lamp.Brighten(20);

            Assert.Equal(70, lamp.Intensity);
        }

        [Fact]
        public void Brighten_WhenOff_ShouldThrow()
        {
            var lamp = new Lamp("Lamp");
            Assert.Throws<InvalidOperationException>(() => lamp.Brighten(-1));
        }

        [Fact]
        public void Brighten_CannotIncreaseBeyondMax_ShouldThrow()
        {
            var lamp = new Lamp("Lamp");
            lamp.SwitchOn();
            lamp.SetIntensity(100);

            Assert.Throws<InvalidOperationException>(() => lamp.Brighten(10));
        }

        // --- TOGGLE ---
        //[Fact]
        //public void Toggle_FromOff_ShouldTurnOn()
        //{
        //    var lamp = new Lamp("Lamp");

        //    lamp.Toggle();

        //    Assert.Equal(DeviceStatus.On, lamp.Status);
        //    Assert.Equal(lamp.DefaultIntensity, lamp.Intensity);
        //}

        //[Fact]
        //public void Toggle_FromOn_ShouldTurnOff()
        //{
        //    var lamp = new Lamp("Lamp");
        //    lamp.SwitchOn();

        //    lamp.Toggle();

        //    Assert.Equal(DeviceStatus.Off, lamp.Status);
        //    Assert.Equal(0, lamp.Intensity);
        //}
    }
}
