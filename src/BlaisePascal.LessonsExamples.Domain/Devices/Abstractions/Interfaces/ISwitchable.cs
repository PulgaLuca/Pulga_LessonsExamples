using BlaisePascal.LessonsExamples.SharedKernel;

namespace BlaisePascal.LessonsExamples.Domain.Devices.Abstractions.Interfaces
{
    public interface ISwitchable
    {
        Result SwitchOn();
        Result SwitchOff();
    }
}
