using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Commands;
using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Dto;
using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Queries;
using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.Repositories;

public class LampController
{
    private readonly ILampRepository _repository;

    public LampController(ILampRepository repository)
    {
        _repository = repository;
    }

    // Mostra lampade
    public void ShowLamps()
    {
        var lamps = new GetAllLampsQuery(_repository).Execute();

        Console.WriteLine("LAMPS:");
        Console.WriteLine("-------------------------------------");

        if (lamps.Count == 0)
        {
            Console.WriteLine("No lamps available");
            return;
        }

        for (int i = 0; i < lamps.Count; i++)
        {
            var l = lamps[i];
            Console.WriteLine($"{i + 1}. {l.Name}\n{l}");
        }
    }

    // Add Lamp
    public void AddLamp()
    {
        Console.Write("Lamp name: ");
        string name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Invalid name");
            return;
        }

        new AddLampCommand(_repository).Execute(name: name, imageUrl: "");
        Console.WriteLine("Lamp added!");
    }

    // Remove Lamp
    public void RemoveLamp()
    {
        var lamp = SelectLamp();
        if (lamp == null) return;

        new RemoveLampCommand(_repository).Execute(lamp.Id);
        Console.WriteLine("Lamp removed!");
    }

    // Switch ON Lamp
    public void SwitchOn()
    {
        try
        {
            var lamp = SelectLamp();
            if (lamp == null) return;

            new SwitchOnLampCommand(_repository).Execute(lamp.Id);
            Console.WriteLine("Lamp switched ON");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR: {ex.Message}");
        }
    }


    // Switch OFF Lamp
    public void SwitchOff()
    {
        try
        {
            var lamp = SelectLamp();
            if (lamp == null) 
                return;

            new SwitchOffLampCommand(_repository).Execute(lamp.Id);
            Console.WriteLine("Lamp switched OFF");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR: {ex.Message}");
        }
    }

    // Change Intensity
    public void ChangeIntensity()
    {
        var lamp = SelectLamp();
        if (lamp == null) return;

        Console.Write("New intensity (0-100): ");
        int intensity;

        if (!int.TryParse(Console.ReadLine(), out intensity))
        {
            Console.WriteLine("Invalid value");
            return;
        }

        try
        {
            new ChangeIntensityCommand(_repository)
                .Execute(lamp.Id, intensity);

            Console.WriteLine("Intensity updated");
        }
        catch (InvalidOperationException ex)
        {
            // Errore di dominio (lamp spenta)
            Console.WriteLine($"ERROR: {ex.Message}");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("ERROR: Intensity must be greater than 0");
        }
    }


    // Select Lamp
    private LampDto SelectLamp()
    {
        var lamps = new GetAllLampsQuery(_repository).Execute();

        if (lamps.Count == 0)
        {
            Console.WriteLine("No lamps available");
            return null;
        }

        Console.Write("Lamp number: ");
        int index;

        if (!int.TryParse(Console.ReadLine(), out index))
        {
            Console.WriteLine("Invalid number");
            return null;
        }

        if (index < 1 || index > lamps.Count)
        {
            Console.WriteLine("Lamp not found");
            return null;
        }

        return lamps[index - 1];
    }
}
