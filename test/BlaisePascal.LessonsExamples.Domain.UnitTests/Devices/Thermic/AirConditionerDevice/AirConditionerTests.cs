using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.VO;
using BlaisePascal.LessonsExamples.Domain.Devices.Thermic;
using BlaisePascal.LessonsExamples.Domain.Devices.Thermic.AirConditionerDevice;
using BlaisePascal.LessonsExamples.Domain.Devices.Thermic.VO;
using System;
using Xunit;

namespace BlaisePascal.LessonsExamples.Domain.Tests.Devices.Thermic
{
    public class AirConditionerTests
    {
        private static AirConditioner CreateSut(
            TemperatureUnit unit = TemperatureUnit.Celsius,
            double min = 16,
            double max = 30)
        {
            return new AirConditioner(
                new DeviceName("AC"),
                unit,
                min,
                max);
        }

        [Fact]
        public void Constructor_ShouldInitializeWithMinTemperatureAndCorrectUnit()
        {
            // Arrange & Act
            var ac = CreateSut(TemperatureUnit.Celsius);

            // Assert
            Assert.Equal(16, ac.Temperature.Value);
            Assert.Equal(TemperatureUnit.Celsius, ac.Temperature.Unit);
            Assert.Equal(16, ac.MinTemperature.Value);
            Assert.Equal(30, ac.MaxTemperature.Value);
        }

        [Fact]
        public void ChangeTargetTemperature_WhenDeviceIsOff_ShouldThrow()
        {
            // Arrange
            var ac = CreateSut();
            var newTemp = Temperature.FromCelsius(22);

            // Act
            var act = () => ac.ChangeTargetTemperature(newTemp);

            // Assert
            Assert.Throws<InvalidOperationException>(act);
        }

        [Fact]
        public void ChangeTargetTemperature_WhenOn_ShouldUpdateTemperature()
        {
            // Arrange
            var ac = CreateSut();
            ac.SwitchOn();
            var newTemp = Temperature.FromCelsius(24);

            // Act
            ac.ChangeTargetTemperature(newTemp);

            // Assert
            Assert.Equal(24, ac.Temperature.Value);
            Assert.Equal(TemperatureUnit.Celsius, ac.Temperature.Unit);
        }

        [Fact]
        public void ChangeTargetTemperature_WithDifferentUnit_ShouldConvert()
        {
            // Arrange
            var ac = CreateSut(TemperatureUnit.Celsius);
            ac.SwitchOn();
            var fahrenheit = Temperature.FromFahrenheit(77); // 25°C

            // Act
            ac.ChangeTargetTemperature(fahrenheit);

            // Assert
            Assert.Equal(25, ac.Temperature.Value, 1);
            Assert.Equal(TemperatureUnit.Celsius, ac.Temperature.Unit);
        }

        [Fact]
        public void ChangeTargetTemperature_OutOfRange_ShouldThrow()
        {
            // Arrange
            var ac = CreateSut(min: 18, max: 26);
            ac.SwitchOn();
            var tooHigh = Temperature.FromCelsius(30);

            // Act
            var act = () => ac.ChangeTargetTemperature(tooHigh);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(act);
        }

        [Fact]
        public void ChangeTemperatureUnit_ShouldConvertBoundsAndCurrentTemperature()
        {
            // Arrange
            var ac = CreateSut(TemperatureUnit.Celsius);
            ac.SwitchOn();
            ac.ChangeTargetTemperature(Temperature.FromCelsius(20));

            // Act
            ac.ChangeTemperatureUnit(TemperatureUnit.Fahrenheit);

            // Assert
            Assert.Equal(68, ac.Temperature.Value, 1);
            Assert.Equal(TemperatureUnit.Fahrenheit, ac.Temperature.Unit);
            Assert.Equal(TemperatureUnit.Fahrenheit, ac.MinTemperature.Unit);
            Assert.Equal(TemperatureUnit.Fahrenheit, ac.MaxTemperature.Unit);
        }

        [Fact]
        public void ChangeTemperatureUnit_WithSameUnit_ShouldDoNothing()
        {
            // Arrange
            var ac = CreateSut();
            ac.SwitchOn();
            var lastModified = ac.LastModifiedAtUtc;

            // Act
            ac.ChangeTemperatureUnit(TemperatureUnit.Celsius);

            // Assert
            Assert.Equal(lastModified, ac.LastModifiedAtUtc);
        }

        [Fact]
        public void ChangeFanSpeed_WhenOff_ShouldThrow()
        {
            // Arrange
            var ac = CreateSut();

            // Act
            var act = () => ac.ChangeFanSpeed(FanSpeed.High);

            // Assert
            Assert.Throws<InvalidOperationException>(act);
        }

        [Fact]
        public void ChangeFanSpeed_WithInvalidEnum_ShouldThrow()
        {
            // Arrange
            var ac = CreateSut();
            ac.SwitchOn();
            var invalidSpeed = (FanSpeed)999;

            // Act
            var act = () => ac.ChangeFanSpeed(invalidSpeed);

            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void ChangeMode_WhenOff_ShouldThrow()
        {
            // Arrange
            var ac = CreateSut();

            // Act
            var act = () => ac.ChangeMode(AcMode.Cooling);

            // Assert
            Assert.Throws<InvalidOperationException>(act);
        }

        [Fact]
        public void ChangeMode_WithInvalidEnum_ShouldThrow()
        {
            // Arrange
            var ac = CreateSut();
            ac.SwitchOn();
            var invalidMode = (AcMode)999;

            // Act
            var act = () => ac.ChangeMode(invalidMode);

            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void ChangeMode_ToEcoMode_ShouldApplyEcoTemperatureRules()
        {
            // Arrange
            var ac = CreateSut();
            ac.SwitchOn();
            ac.ChangeTargetTemperature(Temperature.FromCelsius(30));

            // Act
            ac.ChangeMode(AcMode.EcoMode);

            // Assert
            Assert.Equal(26, ac.Temperature.Value);
        }

        [Fact]
        public void EcoMode_ShouldLimitFanSpeedToMedium()
        {
            // Arrange
            var ac = CreateSut();
            ac.SwitchOn();
            ac.ChangeFanSpeed(FanSpeed.High);

            // Act
            ac.ChangeMode(AcMode.EcoMode);

            // Assert
            Assert.Equal(FanSpeed.Medium, ac.FanSpeed);
        }

        [Fact]
        public void ChangeTargetTemperature_InEcoMode_ShouldClampEcoRange()
        {
            // Arrange
            var ac = CreateSut();
            ac.SwitchOn();
            ac.ChangeMode(AcMode.EcoMode);

            // Act
            ac.ChangeTargetTemperature(Temperature.FromCelsius(18));

            // Assert
            Assert.Equal(20, ac.Temperature.Value);
        }
    }
}
