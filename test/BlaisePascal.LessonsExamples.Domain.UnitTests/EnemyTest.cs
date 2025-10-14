using System;
using Xunit;
using BlaisePascal.LessonsExamples.Domain;

namespace BlaisePascal.LessonsExamples.Domain.UnitTests
{
    public class EnemyTests
    {
        // Constructor Tests
        [Fact]
        public void EnemyConstructor_WhenValidValuesProvided_ShouldCreateEnemyCorrectly()
        {
            // Arrange
            string name = "Goblin";
            int health = 80;
            string description = "A small green creature";

            // Act
            Enemy enemy = new Enemy(name, health, description);

            // Assert
            Assert.Equal(name, enemy.Name);
            Assert.Equal(health, enemy.Health);
            Assert.Equal(description, enemy.Description);
            Assert.True(enemy.IsAlive);
        }

        [Fact]
        public void EnemyConstructor_WhenHealthNotProvided_ShouldSetHealthToMax()
        {
            // Arrange
            string name = "Orc";

            // Act
            Enemy enemy = new Enemy(name);

            // Assert
            Assert.Equal(100, enemy.Health);
        }

        [Fact]
        public void EnemyConstructor_WhenNameIsNull_ShouldThrowArgumentException()
        {
            // Arrange
            string invalidName = null;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Enemy(invalidName));
        }

        [Fact]
        public void EnemyConstructor_WhenHealthBelowMinimum_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            string name = "Troll";
            int invalidHealth = -10;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Enemy(name, invalidHealth));
        }

        [Fact]
        public void EnemyConstructor_WhenHealthAboveMaximum_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            string name = "Dragon";
            int invalidHealth = 200;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Enemy(name, invalidHealth));
        }


        // Parameterized Rename Tests
        [Theory]
        [InlineData("Goblin", "Orc")]
        [InlineData("Dragon", "Wyvern")]
        public void EnemyRename_ValidName_ShouldUpdateName(string original, string newName)
        {
            Enemy enemy = new Enemy(original);

            enemy.Rename(newName);

            Assert.Equal(newName, enemy.Name);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void EnemyRename_InvalidName_ShouldThrowArgumentException(string invalidName)
        {
            Enemy enemy = new Enemy("Goblin");

            Assert.Throws<ArgumentException>(() => enemy.Rename(invalidName));
        }



        // Rename Tests
        [Fact]
        public void EnemyRename_WhenValidName_ShouldUpdateName()
        {
            // Arrange
            Enemy enemy = new Enemy("Goblin");
            string newName = "Orc";

            // Act
            enemy.Rename(newName);

            // Assert
            Assert.Equal(newName, enemy.Name);
        }

        [Fact]
        public void EnemyRename_WhenNameIsEmpty_ShouldThrowArgumentException()
        {
            // Arrange
            Enemy enemy = new Enemy("Goblin");
            string invalidName = "";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => enemy.Rename(invalidName));
        }

        [Fact]
        public void EnemyRename_WhenNameIsWhitespace_ShouldThrowArgumentException()
        {
            // Arrange
            Enemy enemy = new Enemy("Goblin");
            string invalidName = "   ";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => enemy.Rename(invalidName));
        }

        [Fact]
        public void EnemyRename_WhenNameIsNull_ShouldThrowArgumentException()
        {
            // Arrange
            Enemy enemy = new Enemy("Goblin");
            string invalidName = null;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => enemy.Rename(invalidName));
        }

        // TakeDamage Tests
        [Fact]
        public void EnemyTakeDamage_WhenValidDamage_ShouldReduceHealth()
        {
            // Arrange
            Enemy enemy = new Enemy("Goblin", 80);
            int damage = 30;

            // Act
            enemy.TakeDamage(damage);

            // Assert
            Assert.Equal(50, enemy.Health);
        }

        [Fact]
        public void EnemyTakeDamage_WhenDamageIsNegative_ShouldThrowArgumentException()
        {
            // Arrange
            Enemy enemy = new Enemy("Goblin");
            int damage = -5;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => enemy.TakeDamage(damage));
        }

        [Fact]
        public void EnemyTakeDamage_WhenDamageExceedsHealth_ShouldSetHealthToZero()
        {
            // Arrange
            Enemy enemy = new Enemy("Goblin", 50);
            int damage = 100;

            // Act
            enemy.TakeDamage(damage);

            // Assert
            Assert.Equal(0, enemy.Health);
            Assert.False(enemy.IsAlive);
        }

        // Heal Tests

        [Fact]
        public void EnemyHeal_WhenValidAmount_ShouldIncreaseHealth()
        {
            // Arrange
            Enemy enemy = new Enemy("Goblin", 50);
            int amount = 30;

            // Act
            enemy.Heal(amount);

            // Assert
            Assert.Equal(80, enemy.Health);
        }

        [Fact]
        public void EnemyHeal_WhenNegativeAmount_ShouldThrowArgumentException()
        {
            // Arrange
            Enemy enemy = new Enemy("Goblin", 50);
            int invalidAmount = -10;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => enemy.Heal(invalidAmount));
        }

        [Fact]
        public void EnemyHeal_WhenResultExceedsMaxHealth_ShouldSetHealthToMax()
        {
            // Arrange
            Enemy enemy = new Enemy("Goblin", 95);
            int amount = 20;

            // Act
            enemy.Heal(amount);

            // Assert
            Assert.Equal(100, enemy.Health);
        }

        // IsAlive Tests

        [Fact]
        public void EnemyIsAlive_WhenHealthIsZero_ShouldReturnFalse()
        {
            // Arrange
            Enemy enemy = new Enemy("Zombie", 0);

            // Act
            bool result = enemy.IsAlive;

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void EnemyIsAlive_WhenHealthIsPositive_ShouldReturnTrue()
        {
            // Arrange
            Enemy enemy = new Enemy("Skeleton", 10);

            // Act
            bool result = enemy.IsAlive;

            // Assert
            Assert.True(result);
        }
    }
}
