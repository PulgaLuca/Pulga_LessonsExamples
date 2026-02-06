using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Commands;
using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Dto;
using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Queries;
using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.Repositories;
using BlaisePascal.LessonsExamples.Infrastructure.Repositories.InMemory.Devices.Lightning;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BlaisePascal.LessonsExamples.WpfUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ILampRepository _repo;

        public MainWindow()
        {
            InitializeComponent();
            _repo = new InMemoryLampRepository();
            Refresh();
        }

        private LampDto SelectedLamp => LampList.SelectedItem as LampDto;

        private void Refresh()
        {
            LampList.ItemsSource = new GetAllLampsQuery(_repo).Execute();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string name = NewLampNameTextBox.Text;

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Insert a lamp name");
                return;
            }

            int intensity = 0;
            int.TryParse(NewLampIntensityTextBox.Text, out intensity);

            var addCommand = new AddLampCommand(_repo);
            addCommand.Execute(name, "");

            // opzionale: imposta intensità iniziale
            var lamps = _repo.GetAll();
            var newLamp = lamps.Last();

            if (intensity > 0)
            {
                newLamp.SwitchOn();
                newLamp.ChangeBrightnessTo(intensity);
            }

            Refresh();

            // pulizia campi
            NewLampNameTextBox.Text = "";
            NewLampIntensityTextBox.Text = "0";
        }


        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedLamp == null) return;
            new RemoveLampCommand(_repo).Execute(SelectedLamp.Id);
            Refresh();
        }

        private void On_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedLamp == null) return;
            new SwitchOnLampCommand(_repo).Execute(SelectedLamp.Id);
            Refresh();
        }

        private void Off_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedLamp == null) return;
            new SwitchOffLampCommand(_repo).Execute(SelectedLamp.Id);
            Refresh();
        }

        private void Intensity_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedLamp == null)
                return;

            int intensity;

            if (!int.TryParse(IntensityTextBox.Text, out intensity) && (intensity < 0 || intensity > 100))
            {
                MessageBox.Show("Intensity must be between 0 and 100");
                return;
            }

            new ChangeIntensityCommand(_repo).Execute(SelectedLamp.Id, intensity);

            Refresh();
        }
    }
}