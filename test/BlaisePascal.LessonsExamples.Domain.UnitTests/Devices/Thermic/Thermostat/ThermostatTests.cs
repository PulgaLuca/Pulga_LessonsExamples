using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.VO;
using BlaisePascal.LessonsExamples.Domain.Devices.Thermic;
using BlaisePascal.LessonsExamples.Domain.Devices.Thermic.ThermostatDevice;
using BlaisePascal.LessonsExamples.Domain.Devices.Thermic.VO;
using System;
using Xunit;

namespace BlaisePascal.LessonsExamples.Domain.Tests.Devices.Thermic
{
    public class ThermostatTests
    {
        private static Thermostat CreateSut(
            double initialTarget = 22,
            double min = 16,
            double max = 28)
        {
            return new Thermostat(
                new DeviceName("Thermostat"),
                Temperature.FromCelsius(initialTarget),
                min,
                max);
        }

        [Fact]
        public void Constructor_ShouldInitializeWithOffModeAndTargetTemperature()
        {
            // Arrange
            var initialTarget = Temperature.FromCelsius(21);

            // Act
            var thermostat = new Thermostat(
                new DeviceName("Thermostat"),
                initialTarget,
                16,
                28);

            // Assert
            Assert.Equal(ThermostatMode.Off, thermostat.Mode);
            Assert.Equal(initialTarget.Value, thermostat.TargetTemperature.Value);
            Assert.Equal(initialTarget.Unit, thermostat.TargetTemperature.Unit);
        }

        [Fact]
        public void ChangeTargetTemperature_WhenDeviceIsOff_ShouldThrow()
        {
            // Arrange
            var thermostat = CreateSut();
            var newTemperature = Temperature.FromCelsius(23);

            // Act
            var act = () => thermostat.ChangeTargetTemperature(newTemperature);

            // Assert
            Assert.Throws<InvalidOperationException>(act);
        }

        [Fact]
        public void ChangeTargetTemperature_WhenOn_ShouldUpdateTargetTemperature()
        {
            // Arrange
            var thermostat = CreateSut();
            thermostat.SwitchOn();
            var newTemperature = Temperature.FromCelsius(25);

            // Act
            thermostat.ChangeTargetTemperature(newTemperature);

            // Assert
            Assert.Equal(25, thermostat.TargetTemperature.Value);
            Assert.Equal(TemperatureUnit.Celsius, thermostat.TargetTemperature.Unit);
        }

        [Fact]
        public void ChangeTargetTemperature_ShouldClampToDeviceMinAndMax()
        {
            // Arrange
            var thermostat = CreateSut(min: 18, max: 26);
            thermostat.SwitchOn();
            var tooHigh = Temperature.FromCelsius(30);

            // Act
            thermostat.ChangeTargetTemperature(tooHigh);

            // Assert
            Assert.Equal(26, thermostat.TargetTemperature.Value);
        }

        [Fact]
        public void ChangeTargetTemperature_ShouldConvertFahrenheitToCelsiusBeforeClamp()
        {
            // Arrange
            var thermostat = CreateSut(min: 18, max: 26);
            thermostat.SwitchOn();
            var fahrenheit = Temperature.FromFahrenheit(86); // 30°C

            // Act
            thermostat.ChangeTargetTemperature(fahrenheit);

            // Assert
            Assert.Equal(26, thermostat.TargetTemperature.Value);
            Assert.Equal(TemperatureUnit.Celsius, thermostat.TargetTemperature.Unit);
        }

        [Fact]
        public void ChangeMode_WhenDeviceIsOff_AndModeIsNotOff_ShouldThrow()
        {
            // Arrange
            var thermostat = CreateSut();

            // Act
            var act = () => thermostat.ChangeMode(ThermostatMode.EcoMode);

            // Assert
            Assert.Throws<InvalidOperationException>(act);
        }

        [Fact]
        public void ChangeMode_WhenOn_ShouldUpdateMode()
        {
            // Arrange
            var thermostat = CreateSut();
            thermostat.SwitchOn();

            // Act
            thermostat.ChangeMode(ThermostatMode.Heating);

            // Assert
            Assert.Equal(ThermostatMode.Heating, thermostat.Mode);
        }

        [Fact]
        public void ChangeMode_ToEcoMode_ShouldApplyEcoTemperatureRules()
        {
            // Arrange
            var thermostat = CreateSut(initialTarget: 30);
            thermostat.SwitchOn();

            // Act
            thermostat.ChangeMode(ThermostatMode.EcoMode);

            // Assert
            Assert.Equal(24, thermostat.TargetTemperature.Value);
        }

        [Fact]
        public void ChangeTargetTemperature_InEcoMode_ShouldApplyEcoClamp()
        {
            // Arrange
            var thermostat = CreateSut();
            thermostat.SwitchOn();
            thermostat.ChangeMode(ThermostatMode.EcoMode);

            var tooLow = Temperature.FromCelsius(16);

            // Act
            thermostat.ChangeTargetTemperature(tooLow);

            // Assert
            Assert.Equal(19, thermostat.TargetTemperature.Value);
        }

        [Fact]
        public void SwitchOff_ShouldSetModeToOff()
        {
            // Arrange
            var thermostat = CreateSut();
            thermostat.SwitchOn();
            thermostat.ChangeMode(ThermostatMode.Heating);

            // Act
            thermostat.SwitchOff();

            // Assert
            Assert.Equal(ThermostatMode.Off, thermostat.Mode);
        }

        [Fact]
        public void ChangeMode_WithInvalidEnumValue_ShouldThrowArgumentException()
        {
            // Arrange
            var thermostat = CreateSut();
            thermostat.SwitchOn();
            var invalidMode = (ThermostatMode)999;

            // Act
            var act = () => thermostat.ChangeMode(invalidMode);

            // Assert
            Assert.Throws<ArgumentException>(act);
        }
    }
}
