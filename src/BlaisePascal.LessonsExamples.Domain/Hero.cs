using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.LessonsExamples.Domain
{
    using System;

    namespace BlaisePascal.LessonsExamples.Domain
    {
        /// <summary>
        /// Represents a hero character.
        /// </summary>
        public class Hero
        {
            // Attributes
            private string _name;
            private string _classType;
            private int _level;
            private int _health;
            private int _experience;
            private bool _isAlive;

            // Constants
            private const int MaxHealth = 100;
            private const int ExperienceToLevelUp = 100;

            // Constructors

            /// <summary>
            /// Initializes a new instance of the <see cref="Hero"/> class.
            /// </summary>
            public Hero()
            {
                _name = "Unnamed Hero";
                _classType = "Adventurer";
                _level = 1;
                _health = MaxHealth;
                _experience = 0;
                _isAlive = true;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="Hero"/> class with name and class type.
            /// </summary>
            /// <param name="name">The hero's name.</param>
            /// <param name="classType">The hero's class type (e.g., Warrior, Mage).</param>
            public Hero(string name, string classType)
            {
                SetName(name);
                SetClassType(classType);
                _level = 1;
                _health = MaxHealth;
                _experience = 0;
                _isAlive = true;
            }

            // Getter Methods

            public string GetName()
            {
                return _name;
            }

            
            public string GetClassType()
            {
                return _classType;
            }

            public int GetLevel()
            {
                return _level;
            }

            public int GetHealth()
            {
                return _health;
            }

            public int GetExperience()
            {
                return _experience;
            }

            public bool GetIsAlive()
            {
                return _isAlive;
            }

            // Setter Methods

            /// <summary>
            /// Sets the hero's name.
            /// </summary>
            /// <param name="name">The new name to assign.</param>
            public void SetName(string name)
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new ArgumentException("Hero name cannot be null or empty.", nameof(name));
                }

                _name = name.Trim();
            }

            /// <summary>
            /// Sets the hero's class type.
            /// </summary>
            /// <param name="classType">The new class type.</param>
            public void SetClassType(string classType)
            {
                if (string.IsNullOrWhiteSpace(classType))
                {
                    throw new ArgumentException("Class type cannot be null or empty.", nameof(classType));
                }

                _classType = classType.Trim();
            }

            /// <summary>
            /// Sets the hero's health to a new value.
            /// </summary>
            /// <param name="health">The new health value.</param>
            public void SetHealth(int health)
            {
                if (health < 0)
                {
                    _health = 0;
                    _isAlive = false;
                    return;
                }

                if (health > MaxHealth)
                {
                    _health = MaxHealth;
                }
                else
                {
                    _health = health;
                }

                _isAlive = _health > 0;
            }

            // Additional Methods

            /// <summary>
            /// Makes the hero take damage and reduces health.
            /// </summary>
            /// <param name="damage">The damage amount.</param>
            public void TakeDamage(int damage)
            {
                if (damage < 0)
                {
                    throw new ArgumentException("Damage cannot be negative.", nameof(damage));
                }

                SetHealth(_health - damage);
            }

            /// <summary>
            /// Heals the hero by a certain amount, up to the maximum health limit.
            /// </summary>
            /// <param name="amount">The healing amount.</param>
            public void Heal(int amount)
            {
                if (!_isAlive)
                {
                    throw new InvalidOperationException("Cannot heal a dead hero.");
                }

                if (amount < 0)
                {
                    throw new ArgumentException("Heal amount cannot be negative.", nameof(amount));
                }

                SetHealth(_health + amount);
            }

            /// <summary>
            /// Adds experience points to the hero. Levels up automatically if enough experience is gained.
            /// </summary>
            /// <param name="points">The number of experience points to add.</param>
            public void GainExperience(int points)
            {
                if (points < 0)
                {
                    throw new ArgumentException("Experience points cannot be negative.", nameof(points));
                }

                _experience += points;

                while (_experience >= ExperienceToLevelUp)
                {
                    _experience -= ExperienceToLevelUp;
                    LevelUp();
                }
            }

            /// <summary>
            /// Increases the hero's level by one and restores full health.
            /// </summary>
            public void LevelUp()
            {
                _level += 1;
                _health = MaxHealth;
            }

            /// <summary>
            /// Kills the hero immediately.
            /// </summary>
            public void Die()
            {
                _health = 0;
                _isAlive = false;
            }

            /// <summary>
            /// Revives the hero with half health.
            /// </summary>
            public void Revive()
            {
                if (_isAlive)
                {
                    throw new InvalidOperationException("Hero is already alive.");
                }

                _health = MaxHealth / 2;
                _isAlive = true;
            }
        }
    }

}
