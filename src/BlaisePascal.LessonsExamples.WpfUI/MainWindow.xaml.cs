using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace BlaisePascal.LessonsExamples.WpfUI
{
    public partial class MainWindow : Window
    {
        private readonly IServiceProvider _services;

        public MainWindow(IServiceProvider services)
        {
            InitializeComponent();
            _services = services;
            LoadDefaultView();
        }

        private void LoadDefaultView()
        {
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