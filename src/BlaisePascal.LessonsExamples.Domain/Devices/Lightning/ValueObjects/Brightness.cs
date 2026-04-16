using BlaisePascal.LessonsExamples.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.LessonsExamples.Domain.Devices.Lightning.ValueObjects
{
    public sealed class Brightness : ValueObject
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

        public Brightness Increase(int amount) => From(Value + amount);
        public Brightness Decrease(int amount) => From(Value - amount);


        // Operators overriding
        // in questo caso è più utile int per le operazioni in AbstractLamp
        // public static Brightness operator +(Brightness b, int amount) => new(b.Value + amount);
        public static int operator +(Brightness b, int amount) => b.Value + amount;
        public static int operator -(Brightness b, int amount) => b.Value - amount;

        protected override IEnumerable<object> GetAtomicValues()
        {
            // iteratore lazy, più efficiente (non alloca subito una lista),
            // permette di restituire i valori uno alla volta, utile se ci sono più proprietà
            yield return Value;
        }

        /*
         * public class Money : ValueObject
            {
                public decimal Amount { get; }
                public string Currency { get; }

                protected override IEnumerable<object> GetAtomicValues()
                {
                    yield return Amount;
                    yield return Currency;
                }
            }
         */

        public override string ToString() => $"{Value}%";
    }
}
