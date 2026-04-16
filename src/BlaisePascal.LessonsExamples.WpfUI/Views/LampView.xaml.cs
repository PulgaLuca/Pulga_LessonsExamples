using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Commands;
using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Dto;
using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Queries;
using BlaisePascal.LessonsExamples.Domain.Devices.Lightning.Repositories;
using BlaisePascal.LessonsExamples.Infrastructure.Repositories.Devices.Lightning.Lamps.InMemory;
using BlaisePascal.LessonsExamples.Infrastructure.Repositories.Devices.Lightning.Lamps.Json.Devices.Lightning;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BlaisePascal.LessonsExamples.WpfUI.Views
{
    public partial class LampView : UserControl
    {
        private readonly ILampRepository _repo;

        public LampView()
        {
            InitializeComponent();
            _repo = new JsonLampRepository();
            Refresh();
        }

        private LampDto SelectedLamp => LampList.SelectedItem as LampDto;

        private void Refresh()
        {
            LampList.ItemsSource = new GetAllLampsQuery(_repo).Execute();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = NewLampNameTextBox.Text;

                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Insert Lamp name", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int intensity = 0;
                var result = int.TryParse(NewLampIntensityTextBox.Text, out intensity);

                // 
                var addCommand = new AddLampCommand(_repo);
                addCommand.Execute(name, "");

                var lamps = _repo.GetAll();
                var newLamp = lamps.Last();

                if (intensity > 0)
                {
                    newLamp.SwitchOn();
                    newLamp.ChangeBrightnessTo(intensity);
                }

                Refresh();

                NewLampNameTextBox.Text = "";
                NewLampIntensityTextBox.Text = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedLamp == null)
                    return;

                new RemoveLampCommand(_repo).Execute(SelectedLamp.Id);
                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void On_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedLamp == null)
                    return;

                new SwitchOnLampCommand(_repo).Execute(SelectedLamp.Id);
                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on Switching On Lamp", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Off_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedLamp == null)
                    return;

                new SwitchOffLampCommand(_repo).Execute(SelectedLamp.Id);
                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Intensity_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedLamp == null)
                    return;

                if (!int.TryParse(IntensityTextBox.Text, out int intensity) || intensity < 0 || intensity > 100)
                {
                    MessageBox.Show("Intensity must be between 0 and 100");
                    return;
                }

                new ChangeIntensityCommand(_repo).Execute(SelectedLamp.Id, intensity);

                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}