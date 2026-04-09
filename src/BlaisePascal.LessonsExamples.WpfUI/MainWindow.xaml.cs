using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Commands;
using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Dto;
using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Queries;
using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.Repositories;
using BlaisePascal.LessonsExamples.Infrastructure.Repositories.Devices.Lightning.Lamps.InMemory;
using BlaisePascal.LessonsExamples.Infrastructure.Repositories.Devices.Lightning.Lamps.Json.Devices.Lightning;
using System.Windows;

using System.Windows;

namespace BlaisePascal.LessonsExamples.WpfUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // necessario impostare la vista di default all'avvio
            MainContentArea.Content = new Views.LampView();
        }

        private void NavLamps_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new Views.LampView();
        }

        private void NavCctv_Click(object sender, RoutedEventArgs e)
        {
            // MainContentArea.Content = new Views.CctvView();
            MessageBox.Show("CCTV View non ancora implementata.", "Info");
        }

        private void NavDoors_Click(object sender, RoutedEventArgs e)
        {
            // MainContentArea.Content = new Views.DoorView();
            MessageBox.Show("Doors View non ancora implementata.", "Info");
        }

        private void NavAirCon_Click(object sender, RoutedEventArgs e)
        {
            // MainContentArea.Content = new Views.AirConditionerView();
            MessageBox.Show("Air Conditioner View non ancora implementata.", "Info");
        }
    }
}