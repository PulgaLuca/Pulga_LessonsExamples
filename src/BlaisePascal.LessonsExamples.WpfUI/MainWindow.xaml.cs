using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace BlaisePascal.LessonsExamples.WpfUI
{
    public partial class MainWindow : Window
    {
        // IServiceProvider è il contenitore dei servizi (Dependency Injection Container).
        // Serve per recuperare automaticamente oggetti già registrati
        // come ??
        //
        // Dunque, aizichè creare manualmente gli oggetti con "new <costruttore",
        // lasciamo che il container li costruisca per noi.
        private readonly IServiceProvider _services;

        public MainWindow(IServiceProvider services)
        {
            InitializeComponent();
            _services = services;
            LoadDefaultView();
        }

        private void LoadDefaultView()
        {
            // GetRequiredService<T>()
            // chiede al container DI di creare e/o recuperare un'istanza di LampView.
            //
            // Vantaggio principale è la gestione automatica delle dipendenze, oltre alla pulizia del codice e migliore testabilità
            MainContentArea.Content = _services.GetRequiredService<Views.LampView>();
        }

        private void NavLamps_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = _services.GetRequiredService<Views.LampView>();
        }

        private void NavCctv_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("CCTV View non ancora implementata.", "Info");
        }

        private void NavDoors_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Doors View non ancora implementata.", "Info");
        }

        private void NavAirCon_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Air Conditioner View non ancora implementata.", "Info");
        }
    }
}