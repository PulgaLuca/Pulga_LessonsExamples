using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.LessonsExamples.Domain.Devices.Lightning.ValueObjects
{
    public sealed class Brightness
    {
        public int Value { get; }

        public const int Min = 0;
        public const int Max = 100;
        public const int DefaultStepAmount = 10;

        private Brightness(int value)
        {
            Value = Math.Clamp(value, Min, Max);
        }

        public static Brightness From(int value) => new(value);

        // Factory Pattern: methods using private constructor with predefined values
        public static Brightness Low() => new(25);
        public static Brightness Medium() => new(50);
        public static Brightness High() => new(75);

        // Operators overriding
        // in questo caso è più utile int per le operazioni in AbstractLamp
        // public static Brightness operator +(Brightness b, int amount) => new(b.Value + amount);
        public static int operator +(Brightness b, int amount) => b.Value + amount;
        public static int operator -(Brightness b, int amount) => b.Value - amount;

        public override string ToString() => $"{Value}%";

    }
}
