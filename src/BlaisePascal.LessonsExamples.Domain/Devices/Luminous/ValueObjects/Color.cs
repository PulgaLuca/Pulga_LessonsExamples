using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.LessonsExamples.Domain.Devices.Luminous.ValueObjects
{
    public sealed class Color
    {
        public byte R { get; }
        public byte G { get; }
        public byte B { get; }

        private Color(byte r, byte g, byte b)
        {
            R = r; G = g; B = b;
        }

        public static Color FromRgb(byte r, byte g, byte b) => new(r, g, b);

        public static Color FromHex(string hex)
        {
            if (string.IsNullOrWhiteSpace(hex))
                throw new ArgumentException("Hex color cannot be empty.");

            hex = hex.TrimStart('#');

            if (hex.Length != 6)
                throw new ArgumentException("Hex code must be 6 characters.");

            return new Color(
                byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber),
                byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber),
                byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber)
            );
        }

        public static Color Named(string name)
        {
            switch (name.ToLowerInvariant())
            {
                case "red":
                    return new Color(255, 0, 0);

                case "green":
                    return new Color(0, 255, 0);

                case "blue":
                    return new Color(0, 0, 255);

                case "white":
                    return new Color(255, 255, 255);

                case "black":
                    return new Color(0, 0, 0);

                case "yellow":
                    return new Color(255, 255, 0);

                default:
                    throw new ArgumentException($"Unknown color name '{name}'.");
            }
        }

        public string ToHex() => $"#{R:X2}{G:X2}{B:X2}";

        public override string ToString() => ToHex();
    }
}
