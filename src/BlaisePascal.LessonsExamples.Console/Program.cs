using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.Repositories;
using BlaisePascal.LessonsExamples.Infrastructure.Repositories.Devices.Lightning.Lamps;

class Program
{
    static void Main()
    {
        ILampRepository repository = new XmlLampRepository();
        LampController controller = new LampController(repository);

        bool exit = false;

        while (!exit)
        {
            Console.Clear();
            controller.ShowLamps();
            ShowMenu();

            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    controller.AddLamp();
                    break;

                case "2":
                    controller.RemoveLamp();
                    break;

                case "3":
                    controller.SwitchOn();
                    break;

                case "4":
                    controller.SwitchOff();
                    break;

                case "5":
                    controller.ChangeIntensity();
                    break;

                case "0":
                    exit = true;
                    continue;

                default:
                    Console.WriteLine("Invalid option");
                    break;
            }

            Pause();
        }
    }

    static void ShowMenu()
    {
        Console.WriteLine();
        Console.WriteLine("1 - Add lamp");
        Console.WriteLine("2 - Remove lamp");
        Console.WriteLine("3 - Switch ON");
        Console.WriteLine("4 - Switch OFF");
        Console.WriteLine("5 - Change intensity");
        Console.WriteLine("0 - Exit");
        Console.WriteLine();
    }

    static void Pause()
    {
        Console.WriteLine();
        Console.WriteLine("Press ENTER to continue...");
        Console.ReadLine();
    }
}