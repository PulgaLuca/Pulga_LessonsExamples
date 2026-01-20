using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.Errors;
using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.VO;
using BlaisePascal.LessonsExamples.Domain.Devices.DoorDevice;

namespace BlaisePascal.LessonsExamples.Domain.UnitTests.Devices.DoorDevice
{
    public class DoorTests
    {
        private static Door CreateDoor() => new Door(new DeviceName("FrontDoor"));

        [Fact]
        public void Constructor_InitializesDoorAsClosedAndLocked()
        {
            // Arrange
            var door = CreateDoor();

            // Act
            var isOpen = door.IsOpen;
            var isLocked = door.IsLocked;

            // Assert
            Assert.False(isOpen);
            Assert.True(isLocked);
        }

        [Fact]
        public void Open_WhenDoorIsUnlockedAndClosed_OpensDoor()
        {
            // Arrange
            var door = CreateDoor();
            door.Unlock();

            // Act
            door.Open();

            // Assert
            Assert.True(door.IsOpen);
        }

        [Fact]
        public void Open_WhenDoorIsLocked_ThrowsException()
        {
            // Arrange
            var door = CreateDoor();

            // Act & Assert
            Assert.Throws<DomainException>(() => door.Open());
        }

        [Fact]
        public void Open_WhenDoorIsAlreadyOpen_ThrowsException()
        {
            // Arrange
            var door = CreateDoor();
            door.Unlock();
            door.Open();

            // Act & Assert
            Assert.Throws<DomainException>(() => door.Open());
        }
        
        [Fact]
        public void Close_WhenDoorIsOpen_ClosesDoor()
        {
            // Arrange
            var door = CreateDoor();
            door.Unlock();
            door.Open();

            // Act
            door.Close();

            // Assert
            Assert.False(door.IsOpen);
        }

        [Fact]
        public void Close_WhenDoorIsAlreadyClosed_ThrowsException()
        {
            // Arrange
            var door = CreateDoor();

            // Act & Assert
            Assert.Throws<DomainException>(() => door.Close());
        }

        [Fact]
        public void Unlock_WhenDoorIsLocked_UnlocksDoor()
        {
            // Arrange
            var door = CreateDoor();

            // Act
            door.Unlock();

            // Assert
            Assert.False(door.IsLocked);
        }

        [Fact]
        public void Unlock_WhenDoorIsAlreadyUnlocked_ThrowsException()
        {
            // Arrange
            var door = CreateDoor();
            door.Unlock();

            // Act & Assert
            Assert.Throws<DomainException>(() => door.Unlock());
        }

        [Fact]
        public void Lock_WhenDoorIsClosedAndUnlocked_LocksDoor()
        {
            // Arrange
            var door = CreateDoor();
            door.Unlock();

            // Act
            door.Lock();

            // Assert
            Assert.True(door.IsLocked);
        }

        [Fact]
        public void Lock_WhenDoorIsOpen_ThrowsException()
        {
            // Arrange
            var door = CreateDoor();
            door.Unlock();
            door.Open();

            // Act & Assert
            Assert.Throws<DomainException>(() => door.Lock());
        }

        [Fact]
        public void Lock_WhenDoorIsAlreadyLocked_ThrowsException()
        {
            // Arrange
            var door = CreateDoor();

            // Act & Assert
            Assert.Throws<DomainException>(() => door.Lock());
        }
    }
}
