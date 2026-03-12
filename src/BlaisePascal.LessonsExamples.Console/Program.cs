using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Commands;
using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Queries;
using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.Repositories;
using BlaisePascal.LessonsExamples.Infrastructure.Repositories.Devices.Lightning.Lamps.InMemory;

class Program
{
    static void Main()
    {
        // quale formato di persistenza sto utilizzando
        ILampRepository repository = new InMemoryLampRepository();

        bool exit = false;

        // Finchè l'utente non decide di uscire, mostro la lista delle lampade e il menu
        while (!exit)
        {
            Console.Clear();
            var lamps = new GetAllLampsQuery(repository).Execute();

            Console.WriteLine("LAMPS:");
            Console.WriteLine("-------------------------------------");

            if (lamps.Count == 0)
            {
                Console.WriteLine("No lamps available");
                return;
            }

            // Mostro la lista delle lampade con il loro stato
            for (int i = 0; i < lamps.Count; i++)
            {
                var l = lamps[i];
                Console.WriteLine($"{i + 1}. {l.Name}\n{l}");
            }

            // Mostro il menu di opzioni per le lampade
            ShowMenu();

            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Lamp name: ");
                    string name = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(name))
                    {
                        Console.WriteLine("Invalid name");
                        return;
                    }

                    new AddLampCommand(repository).Execute(name, "");
                    Console.WriteLine("Lamp added!");
                    break;

                case "2":

                    break;

                case "3":

                    break;

                case "4":

                    break;

                case "5":

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

    public static void ShowMenu()
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
