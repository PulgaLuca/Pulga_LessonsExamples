using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.Repositories;
using BlaisePascal.LessonsExamples.Infrastructure.Repositories.Devices.Lightning.Lamps.InMemory;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // quale formato di persistenza sto utilizzando, attualmente salvo in memoria
        ILampRepository repository = new InMemoryLampRepository();
        LampController lampController = new LampController(repository);
        bool exit = false;

        // Finchè l'utente non decide di uscire, mostro la lista delle lampade e il menu
        while (!exit)
        {
            Console.Clear();
            
            lampController.ShowLamps(); // Mostro le lampade,
            lampController.ShowMenu(); // Mostro il menù per i casi d'uso richiamabili

            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    lampController.AddLamp();
                    break;

                case "2":
                    lampController.RemoveLamp();
                    break;

                case "3":
                    lampController.SwitchOn();
                    break;

                case "4":
                    lampController.SwitchOff();
                    break;

                case "5":
                    lampController.ChangeIntensity();
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

    static void Pause()
    {
        Console.WriteLine();
        Console.WriteLine("Press ENTER to continue...");
        Console.ReadLine();
    }
}
