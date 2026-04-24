using BlaisePascal.LessonsExamples.SharedKernel;

namespace BlaisePascal.SmartHouse.Domain.Devices.LuminousDevices.Errors
{
    public static class LampErrors
    {
        public static Error AlreadyOn =
            new("Lamp.AlreadyOn", "Lamp is already on", ErrorType.Conflict);

        public static Error AlreadyOff =
            new("Lamp.AlreadyOff", "Lamp is already off", ErrorType.Conflict);

        public static Error LampIsOff =
            new("Lamp.IsOff", "Lamp must be on", ErrorType.Failure);

        public static Error BrightnessOutOfRange(int min, int max) =>
            new("Lamp.Brightness.OutOfRange",
                $"Brightness must be between {min} and {max}", ErrorType.Failure);
    }
}
