using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.LessonsExamples.Domain.UnitTests
{
    using BlaisePascal.LessonsExamples.Domain;
    using global::BlaisePascal.LessonsExamples.Domain.BlaisePascal.LessonsExamples.Domain;
    using System;
    using Xunit;

    namespace BlaisePascal.LessonsExamples.Domain.UnitTests
    {
        public class HeroTests
        {
            // Constructor
            [Fact]
            public void HeroConstructor_WhenCalledWithoutParameters_ShouldSetDefaultValues()
            {
                // Arrange & Act
                Hero hero = new Hero();

                // Assert
                Assert.Equal("Unnamed Hero", hero.GetName());
                Assert.Equal("Adventurer", hero.GetClassType());
                Assert.Equal(1, hero.GetLevel());
                Assert.Equal(100, hero.GetHealth());
                Assert.Equal(0, hero.GetExperience());
                Assert.True(hero.GetIsAlive());
            }

            [Fact]
            public void HeroConstructor_WhenValidParametersProvided_ShouldAssignValuesCorrectly()
            {
                // Arrange
                string name = "Arthas";
                string classType = "Paladin";

                // Act
                Hero hero = new Hero(name, classType);

                // Assert
                Assert.Equal(name, hero.GetName());
                Assert.Equal(classType, hero.GetClassType());
                Assert.Equal(1, hero.GetLevel());
                Assert.Equal(100, hero.GetHealth());
                Assert.Equal(0, hero.GetExperience());
                Assert.True(hero.GetIsAlive());
            }

            [Fact]
            public void HeroConstructor_WhenNameIsNull_ShouldThrowArgumentException()
            {
                // Arrange
                string invalidName = null;
                string classType = "Warrior";

                // Act & Assert
                Assert.Throws<ArgumentException>(() => new Hero(invalidName, classType));
            }

            [Fact]
            public void HeroConstructor_WhenClassTypeIsNull_ShouldThrowArgumentException()
            {
                // Arrange
                string name = "Arthas";
                string invalidClass = null;

                // Act & Assert
                Assert.Throws<ArgumentException>(() => new Hero(name, invalidClass));
            }

            // Getter and Setter
            [Fact]
            public void HeroHeroSetName_WhenValid_ShouldChangeName()
            {
                // Arrange
                Hero hero = new Hero();
                string newName = "Lancelot";

                // Act
                hero.SetName(newName);

                // Assert
                Assert.Equal(newName, hero.GetName());
            }

            [Fact]
            public void HeroSetName_WhenInvalid_ShouldThrowArgumentException()
            {
                // Arrange
                Hero hero = new Hero();
                string invalidName = " ";

                // Act & Assert
                Assert.Throws<ArgumentException>(() => hero.SetName(invalidName));
            }

            [Fact]
            public void HeroSetClassType_WhenValid_ShouldChangeClassType()
            {
                // Arrange
                Hero hero = new Hero();
                string newClass = "Mage";

                // Act
                hero.SetClassType(newClass);

                // Assert
                Assert.Equal(newClass, hero.GetClassType());
            }

            [Fact]
            public void HeroSetClassType_WhenInvalid_ShouldThrowArgumentException()
            {
                // Arrange
                Hero hero = new Hero();
                string invalidClass = "";

                // Act & Assert
                Assert.Throws<ArgumentException>(() => hero.SetClassType(invalidClass));
            }

            [Fact]
            public void HeroSetHealth_WhenValid_ShouldUpdateHealth()
            {
                // Arrange
                Hero hero = new Hero();

                // Act
                hero.SetHealth(80);

                // Assert
                Assert.Equal(80, hero.GetHealth());
            }

            [Fact]
            public void HeroSetHealth_WhenNegative_ShouldSetHealthToZeroAndKillHero()
            {
                // Arrange
                Hero hero = new Hero();

                // Act
                hero.SetHealth(-10);

                // Assert
                Assert.Equal(0, hero.GetHealth());
                Assert.False(hero.GetIsAlive());
            }

            [Fact]
            public void HeroSetHealth_WhenAboveMax_ShouldClampToMax()
            {
                // Arrange
                Hero hero = new Hero();

                // Act
                hero.SetHealth(500);

                // Assert
                Assert.Equal(100, hero.GetHealth());
            }

            // TakeDamage

            [Fact]
            public void HeroTakeDamage_WhenValidDamage_ShouldReduceHealth()
            {
                // Arrange
                Hero hero = new Hero();
                int damage = 30;

                // Act
                hero.TakeDamage(damage);

                // Assert
                Assert.Equal(70, hero.GetHealth());
            }

            [Fact]
            public void HeroTakeDamage_WhenDamageIsNegative_ShouldThrowArgumentException()
            {
                // Arrange
                Hero hero = new Hero();
                int damage = -10;

                // Act & Assert
                Assert.Throws<ArgumentException>(() => hero.TakeDamage(damage));
            }

            [Fact]
            public void HeroTakeDamage_WhenDamageExceedsHealth_ShouldKillHero()
            {
                // Arrange
                Hero hero = new Hero();
                int damage = 200;

                // Act
                hero.TakeDamage(damage);

                // Assert
                Assert.Equal(0, hero.GetHealth());
                Assert.False(hero.GetIsAlive());
            }

            // Heal

            [Fact]
            public void HeroHeal_WhenValid_ShouldIncreaseHealth()
            {
                // Arrange
                Hero hero = new Hero();
                hero.SetHealth(60);
                int healAmount = 30;

                // Act
                hero.Heal(healAmount);

                // Assert
                Assert.Equal(90, hero.GetHealth());
            }

            [Fact]
            public void HeroHeal_WhenAmountNegative_ShouldThrowArgumentException()
            {
                // Arrange
                Hero hero = new Hero();
                int healAmount = -10;

                // Act & Assert
                Assert.Throws<ArgumentException>(() => hero.Heal(healAmount));
            }

            [Fact]
            public void HeroHeal_WhenExceedsMax_ShouldSetToMaxHealth()
            {
                // Arrange
                Hero hero = new Hero();
                hero.SetHealth(95);
                int healAmount = 20;

                // Act
                hero.Heal(healAmount);

                // Assert
                Assert.Equal(100, hero.GetHealth());
            }

            [Fact]
            public void HeroHeal_WhenHeroIsDead_ShouldThrowInvalidOperationException()
            {
                // Arrange
                Hero hero = new Hero();
                hero.Die();

                // Act & Assert
                Assert.Throws<InvalidOperationException>(() => hero.Heal(10));
            }

            // Experience and Level

            [Fact]
            public void HeroGainExperience_WhenValidPoints_ShouldIncreaseExperience()
            {
                // Arrange
                Hero hero = new Hero();
                int points = 50;

                // Act
                hero.GainExperience(points);

                // Assert
                Assert.Equal(50, hero.GetExperience());
                Assert.Equal(1, hero.GetLevel());
            }

            [Fact]
            public void HeroGainExperience_WhenEnoughToLevelUp_ShouldIncreaseLevel()
            {
                // Arrange
                Hero hero = new Hero();
                int points = 120;

                // Act
                hero.GainExperience(points);

                // Assert
                Assert.Equal(2, hero.GetLevel());
                Assert.Equal(20, hero.GetExperience());
                Assert.Equal(100, hero.GetHealth()); // should be full health after level up
            }

            [Fact]
            public void HeroGainExperience_WhenNegativePoints_ShouldThrowArgumentException()
            {
                // Arrange
                Hero hero = new Hero();

                // Act & Assert
                Assert.Throws<ArgumentException>(() => hero.GainExperience(-10));
            }

            [Fact]
            public void HeroLevelUp_ShouldIncreaseLevelAndRestoreHealth()
            {
                // Arrange
                Hero hero = new Hero();
                hero.SetHealth(40);

                // Act
                hero.LevelUp();

                // Assert
                Assert.Equal(2, hero.GetLevel());
                Assert.Equal(100, hero.GetHealth());
            }

            // Death and Revival

            [Fact]
            public void HeroDie_ShouldSetHealthToZeroAndSetIsAliveFalse()
            {
                // Arrange
                Hero hero = new Hero();

                // Act
                hero.Die();

                // Assert
                Assert.Equal(0, hero.GetHealth());
                Assert.False(hero.GetIsAlive());
            }

            [Fact]
            public void HeroRevive_WhenHeroIsDead_ShouldRestoreHalfHealth()
            {
                // Arrange
                Hero hero = new Hero();
                hero.Die();

                // Act
                hero.Revive();

                // Assert
                Assert.True(hero.GetIsAlive());
                Assert.Equal(50, hero.GetHealth());
            }

            [Fact]
            public void HeroRevive_WhenHeroIsAlive_ShouldThrowInvalidOperationException()
            {
                // Arrange
                Hero hero = new Hero();

                // Act & Assert
                Assert.Throws<InvalidOperationException>(() => hero.Revive());
            }
        }
    }
}
