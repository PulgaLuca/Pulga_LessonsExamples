using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.LessonsExamples.Domain
{
    public static class CharachterValidator
    {
        // Constants
        public const int MaxHealth = 100;
        public const int MinHealth = 0;

        /// <summary>
        /// Validates that the name is not null, empty, or whitespace.
        /// </summary>
        /// <param name="name">The name to validate.</param>
        /// <returns>The validated and trimmed name.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="name"/> is invalid.</exception>
        public static string ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name.Trim()))
                throw new ArgumentException("Enemy name cannot be null or empty.", nameof(name));

            return name;
        }

        /// <summary>
        /// Validates that the health value is within the valid range.
        /// </summary>
        /// <param name="health">The health value to validate.</param>
        /// <returns>The validated health value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="health"/> is out of range.</exception>
        public static int ValidateHealth(int health)
        {
            if (health < MinHealth || health > MaxHealth)
                throw new ArgumentOutOfRangeException(nameof(health), $"Health must be between {MinHealth} and {MaxHealth}.");

            return health;
        }
    }
}
