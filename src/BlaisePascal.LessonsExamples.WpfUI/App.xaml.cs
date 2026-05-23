using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Commands.AddLamp;
using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.Repositories;
using BlaisePascal.LessonsExamples.Infrastructure.Repositories.Devices.Lightning.Lamps.Json.Devices.Lightning;
using BlaisePascal.LessonsExamples.WpfUI.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Windows;

namespace BlaisePascal.LessonsExamples.WpfUI
{
    // Applicazione principale WPF
    // System.Windows.Application è la base/bootstrap di tutte le app desktop WPF
    public partial class App : System.Windows.Application
    {
        // contenitore dei servizi, serve per creare e collegare automaticamente le classi tra loro
        public static IServiceProvider Services { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // CREAZIONE DEL CONTENITORE DI DIPENDENZE (Dependency Injection)
            // Queste sono tutte le parti del sistema e come si collegano
            var services = new ServiceCollection();

            // LOGGING necessario per debugging e per capire cosa succede mentre l'app è in esecuzione
            // serve per stampare errori o informazioni a runtime (ovvero mentre il programma gira)
            services.AddLogging(cfg =>
            {
                cfg.AddDebug(); // manda i log alla finestra "Output" di Visual Studio
            });

            // INFRASTRUTTURA, per la parte di persistenza
            // Quando qualcuno chiede ILampRepository, inietta JsonLampRepository
            services.AddSingleton<ILampRepository, JsonLampRepository>();

            // APPLICATION LAYER
            // MediatR prende richieste (Command/Query) e le dispatcha al giusto handler
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(AddLampCommand).Assembly);
            });

            // PRESENTATION LAYER
            // Registriamo le finestre e le viste WPF
            services.AddSingleton<LampView>();
            services.AddSingleton<MainWindow>();

            // assembliamo tutto il sistema
            Services = services.BuildServiceProvider();

            // AVVIO DELL'APPLICAZIONE
            // Impostare la finestra principale di avvio
            var mainWindow = Services.GetRequiredService<MainWindow>();

            // Mostriamo la finestra a schermo
            mainWindow.Show();
        }
    }
}