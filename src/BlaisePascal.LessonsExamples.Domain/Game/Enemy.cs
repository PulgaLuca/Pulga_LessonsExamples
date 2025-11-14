using System;

namespace BlaisePascal.LessonsExamples.Domain
{
    /// <summary>
    /// Represents an enemy character.
    /// </summary>
    public class Enemy
    {
        // Properties
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Health { get; private set; }
        public bool IsAlive => Health > 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Enemy"/> class.
        /// </summary>
        /// <param name="name">The name of the enemy.</param>
        /// <param name="health">The starting health of the enemy (default: 100).</param>
        /// <param name="description">An optional description of the enemy.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="name"/> is null or whitespace.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="health"/> is out of range.</exception>
        public Enemy(string name, int health = CharachterValidator.MaxHealth, string description = "")
        {
            Name = CharachterValidator.ValidateName(name);
            Health = CharachterValidator.ValidateHealth(health);
            Description = description ?? string.Empty;
        }

        /// <summary>
        /// Applies damage to the enemy, reducing its health.
        /// </summary>
        /// <param name="damage">The amount of damage to apply.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="damage"/> is negative.</exception>
        public void TakeDamage(int damage)
        {
            if (damage < 0)
                throw new ArgumentException("Damage cannot be negative.", nameof(damage));

            Health = Math.Max(Health - damage, CharachterValidator.MinHealth);
        }

        /// <summary>
        /// Heals the enemy, increasing its health up to the maximum limit.
        /// </summary>
        /// <param name="amount">The amount of health to restore.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="amount"/> is negative.</exception>
        public void Heal(int amount)
        {
            if (amount < 0)
                throw new ArgumentException("Heal amount cannot be negative.", nameof(amount));

            Health = Math.Min(Health + amount, CharachterValidator.MaxHealth);
        }

        /// <summary>
        /// Changes the enemy's name to a new valid name.
        /// </summary>
        /// <param name="newName">The new name for the enemy.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="newName"/> is null or whitespace.</exception>
        public void Rename(string newName)
        {
            Name = CharachterValidator.ValidateName(newName);
        }
    }
}
