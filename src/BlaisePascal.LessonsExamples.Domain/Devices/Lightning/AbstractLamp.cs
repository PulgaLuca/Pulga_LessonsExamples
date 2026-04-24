using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions;
using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.Events;
using BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.VO;
using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.Events;
using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.ValueObjects;
using BlaisePascal.LessonsExamples.SharedKernel;
using BlaisePascal.SmartHouse.Domain.Devices.LuminousDevices.Errors;
using System;

namespace BlaisePascal.LessonsExamples.Domain.Devices.Lightning
{
    public abstract class AbstractLamp : AbstractDevice, ILamp
    {
        public Brightness Brightness { get; protected set; }

        public abstract Brightness DefaultBrightness { get; }

        protected AbstractLamp(DeviceName name, DeviceImage imageUrl) : base(name, imageUrl)
        {
            Brightness = Brightness.From(Brightness.Min);
        }

        public override Result SwitchOn()
        {
            var result = base.SwitchOn();
            if (result.IsFailure)
                return result;

            var brightnessResult = Result.Success(DefaultBrightness);

            if (brightnessResult.IsFailure)
                return brightnessResult;

            Brightness = DefaultBrightness;

            Raise(new DeviceSwitchedOnEvent(Id));
            Touch();

            return Result.Success();
        }

        public override Result SwitchOff()
        {
            var result = base.SwitchOff();
            if (result.IsFailure)
                return result;

            var brightnessResult = Brightness.From(Brightness.Min);

            Raise(new DeviceSwitchedOffEvent(Id));
            Touch();

            return Result.Success();
        }

        public virtual Result ChangeBrightnessTo(int newIntensity)
        {
            if (Status == DeviceStatus.Off)
                return Result.Failure(LampErrors.LampIsOff);

            var brightnessResult = Brightness.From(newIntensity);
            
            var oldBrightness = Brightness;
            Brightness = brightnessResult;

            if (oldBrightness != Brightness)
                Raise(new BrightnessChangedEvent(Id, oldBrightness, Brightness));

            Touch();

            return Result.Success();
        }

        public Result Dimmer() => Dimmer(Brightness.DefaultStepAmount);

        public virtual Result Dimmer(int amount)
        {
            if (Status == DeviceStatus.Off)
                return Result.Failure(LampErrors.LampIsOff);

            if (amount < 1)
                return Result.Failure(LampErrors.BrightnessOutOfRange(1, Brightness.Max));

            var brightnessResult = Brightness.Decrease(amount);
            
            var oldBrightness = Brightness;
            Brightness = brightnessResult;

            Raise(new BrightnessChangedEvent(Id, oldBrightness, Brightness));
            Touch();

            return Result.Success();
        }

        public Result Brighten() => Brighten(Brightness.DefaultStepAmount);

        public virtual Result Brighten(int amount)
        {
            if (Status == DeviceStatus.Off)
                return Result.Failure(LampErrors.LampIsOff);

            if (amount < 1)
                return Result.Failure(LampErrors.BrightnessOutOfRange(1, Brightness.Max));

            var brightnessResult = Brightness.Increase(amount);

            var oldBrightness = Brightness;
            Brightness = brightnessResult;

            Raise(new BrightnessChangedEvent(Id, oldBrightness, Brightness));
            Touch();

            return Result.Success();
        }
    }
}