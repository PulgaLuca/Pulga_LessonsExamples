using BlaisePascal.LessonsExamples.Domain.LuminousDevices;
using System;
using System.Collections.Generic;
using Xunit;

namespace BlaisePascal.LessonsExamples.Domain.UnitTests
{
    public class LampsRowTests
    {
        private Lamp _lamp1;
        private EcoLamp _lamp2;

        public LampsRowTests()
        {
            _lamp1 = new Lamp("Lamp1");
            _lamp2 = new EcoLamp("Lamp2");
        }

        // --- COSTRUTTORI ---
        [Fact]
        public void Constructor_ShouldInitializeWithMultipleLamps()
        {
            // Arrange
            var lamps = new List<AbstractLamp> { _lamp1, _lamp2 };

            // Act
            var row = new LampsRow("Row1", lamps);

            // Assert
            Assert.Equal("Row1", row.Name);
            Assert.Contains(_lamp1, row.Lamps);
            Assert.Contains(_lamp2, row.Lamps);
        }


        [Fact]
        public void Constructor_SingleLamp_ShouldInitializeCorrectly()
        {
            var row = new LampsRow("Row2", _lamp1);

            Assert.Single(row.Lamps);
            Assert.Contains(_lamp1, row.Lamps);
        }

        [Fact]
        public void Constructor_NullName_ShouldThrow()
        {
            Assert.Throws<ArgumentException>(() => new LampsRow(null!, _lamp1));
        }

        [Fact]
        public void Constructor_EmptyLampCollection_ShouldThrow()
        {
            Assert.Throws<ArgumentException>(() => new LampsRow("Row", new List<AbstractLamp>()));
        }

        // --- SWITCH ON ---
        [Fact]
        public void SwitchOn_ShouldTurnOnAllLamps()
        {
            var row = new LampsRow("Row", new[] { _lamp1, _lamp2 });

            row.SwitchOn();

            Assert.Equal(DeviceStatus.On, _lamp1.Status);
            Assert.Equal(DeviceStatus.On, _lamp2.Status);
        }

        [Fact]
        public void SwitchOn_SingleLamp_ShouldTurnOnOnlySelected()
        {
            var row = new LampsRow("Row", new[] { _lamp1, _lamp2 });

            row.SwitchOn(2);

            Assert.Equal(DeviceStatus.Off, _lamp1.Status);
            Assert.Equal(DeviceStatus.On, _lamp2.Status);
        }

        [Fact]
        public void SwitchOn_InvalidIndex_ShouldThrow()
        {
            var row = new LampsRow("Row", new[] { _lamp1 });

            Assert.Throws<ArgumentOutOfRangeException>(() => row.SwitchOn(0));
        }

        // --- SWITCH OFF ---
        [Fact]
        public void SwitchOff_ShouldTurnOffAllLamps()
        {
            var row = new LampsRow("Row", new[] { _lamp1, _lamp2 });
            row.SwitchOn();

            row.SwitchOff();

            Assert.Equal(DeviceStatus.Off, _lamp1.Status);
            Assert.Equal(DeviceStatus.Off, _lamp2.Status);
        }

        [Fact]
        public void SwitchOff_SingleLamp_ShouldTurnOffOnlySelected()
        {
            var row = new LampsRow("Row", new[] { _lamp1, _lamp2 });
            row.SwitchOn();

            row.SwitchOff(1);

            Assert.Equal(DeviceStatus.Off, _lamp1.Status);
            Assert.Equal(DeviceStatus.On, _lamp2.Status);
        }

        [Fact]
        public void SwitchOff_InvalidIndex_ShouldThrow()
        {
            var row = new LampsRow("Row", new[] { _lamp1 });

            Assert.Throws<ArgumentOutOfRangeException>(() => row.SwitchOff(2));
        }

        // --- SET INTENSITY ---
        [Fact]
        public void SetIntensityAll_ShouldSetAllLamps()
        {
            var row = new LampsRow("Row", new[] { _lamp1, _lamp2 });
            row.SwitchOn();

            row.SetIntensityAll(50);

            Assert.Equal(50, _lamp1.Intensity);
            Assert.Equal(50, _lamp2.Intensity);
        }

        [Fact]
        public void SetIntensity_SingleLamp_ShouldAffectOnlyOne()
        {
            var row = new LampsRow("Row", new[] { _lamp1, _lamp2 });
            row.SwitchOn();

            row.SetIntensity(1, 70);

            Assert.Equal(70, _lamp1.Intensity);
            Assert.NotEqual(70, _lamp2.Intensity);
        }

        [Fact]
        public void SetIntensity_InvalidIndex_ShouldThrow()
        {
            var row = new LampsRow("Row", new[] { _lamp1 });

            Assert.Throws<ArgumentOutOfRangeException>(() => row.SetIntensity(3, 50));
        }

        // --- ADD / REMOVE LAMP ---
        [Fact]
        public void AddLamp_ShouldAddLamp()
        {
            var row = new LampsRow("Row", _lamp1);
            row.AddLamp(_lamp2);

            Assert.Equal(2, row.Lamps.Count);
            Assert.Contains(_lamp2, row.Lamps);
        }

        [Fact]
        public void AddLamp_ExistingLamp_ShouldThrow()
        {
            var row = new LampsRow("Row", _lamp1);

            Assert.Throws<InvalidOperationException>(() => row.AddLamp(_lamp1));
        }

        [Fact]
        public void RemoveLamp_ShouldRemoveLamp()
        {
            var row = new LampsRow("Row", new[] { _lamp1, _lamp2 });
            row.RemoveLamp(_lamp2);

            Assert.Single(row.Lamps);
            Assert.DoesNotContain(_lamp2, row.Lamps);
        }

        [Fact]
        public void RemoveLamp_NotPresent_ShouldThrow()
        {
            var row = new LampsRow("Row", _lamp1);

            Assert.Throws<InvalidOperationException>(() => row.RemoveLamp(_lamp2));
        }

        // --- UTILITY ---
        [Fact]
        public void FindLampWithMaxIntensity_ShouldReturnCorrectLamp()
        {
            var row = new LampsRow("Row", new[] { _lamp1, _lamp2 });
            row.SwitchOn();
            _lamp1.SetIntensity(40);
            _lamp2.SetIntensity(70);

            var maxLamp = row.FindLampWithMaxIntensity();

            Assert.Equal(_lamp2, maxLamp);
        }

        [Fact]
        public void FindLampByIntensity_ShouldReturnCorrectLamp()
        {
            var row = new LampsRow("Row", new[] { _lamp1, _lamp2 });
            row.SwitchOn();
            _lamp1.SetIntensity(30);

            var found = row.FindLampByIntensity(30);

            Assert.Equal(_lamp1, found);
        }

        [Fact]
        public void FindLampByIntensity_NotFound_ShouldReturnNull()
        {
            var row = new LampsRow("Row", new[] { _lamp1, _lamp2 });
            row.SwitchOn();

            var found = row.FindLampByIntensity(999);

            Assert.Null(found);
        }

        [Fact]
        public void SortByIntensity_ShouldReturnCorrectOrder()
        {
            var row = new LampsRow("Row", new[] { _lamp1, _lamp2 });
            row.SwitchOn();
            _lamp1.SetIntensity(50);
            _lamp2.SetIntensity(30);

            var ascending = row.SortByIntensity();
            var descending = row.SortByIntensity(true);

            Assert.Equal(_lamp2, ascending[0]);
            Assert.Equal(_lamp1, ascending[1]);
            Assert.Equal(_lamp1, descending[0]);
            Assert.Equal(_lamp2, descending[1]);
        }

        // --- STATUS AND AVERAGE INTENSITY ---
        [Fact]
        public void Status_ShouldReflectLampsState()
        {
            var row = new LampsRow("Row", new[] { _lamp1, _lamp2 });
            Assert.Equal(DeviceStatus.Off, row.Status);

            _lamp1.SwitchOn();
            Assert.Equal(DeviceStatus.On, row.Status);

            _lamp1.SwitchOff();
            _lamp2.SwitchOn();
            Assert.Equal(DeviceStatus.On, row.Status);

            _lamp2.SwitchOff();
            Assert.Equal(DeviceStatus.Off, row.Status);
        }

        [Fact]
        public void AverageIntensity_ShouldComputeCorrectly()
        {
            var row = new LampsRow("Row", new[] { _lamp1, _lamp2 });
            row.SwitchOn();
            _lamp1.SetIntensity(40);
            _lamp2.SetIntensity(60);

            Assert.Equal(50, row.AverageIntensity);
        }
    }
}
