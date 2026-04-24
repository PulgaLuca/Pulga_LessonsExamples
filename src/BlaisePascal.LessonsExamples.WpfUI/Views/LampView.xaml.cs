using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Commands;
using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Commands.AddLamp;
using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Dto;
using BlaisePascal.LessonsExamples.Application.Devices.Lightning.Lamps.Queries.GetAll;
using MediatR;
using System.Windows;
using System.Windows.Controls;

namespace BlaisePascal.LessonsExamples.WpfUI.Views
{
    public partial class LampView : UserControl
    {
        // Install NuGet packages:
        // - MediatR
        // - DependencyInjection

        // TODO: "_repo" will be replaced by a service locator or DI container in a real application due to
        // violation of the Dependency Inversion Principle, this means... refactoring... again yes
        private readonly IMediator _mediator;

        public LampView(IMediator mediator)
        {
            InitializeComponent();
            _mediator = mediator;
            Refresh();
        }

        private LampDto SelectedLamp => LampList.SelectedItem as LampDto;

        private async void Refresh()
        {
            var result = await _mediator.Send(new GetAllLampsQuery());

            if (result.IsFailure)
            {
                MessageBox.Show("Error loading lamps");
                return;
            }

            LampList.ItemsSource = result.Value;
        }

        private async void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = NewLampNameTextBox.Text;

                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Insert Lamp name");
                    return;
                }

                var command = new AddLampCommand(name, "");

                var result = await _mediator.Send(command);

                if (result.IsFailure)
                {
                    MessageBox.Show(result.Error.Code, "Error adding lamp");
                    return;
                }

                Refresh();

                NewLampNameTextBox.Text = "";
                NewLampIntensityTextBox.Text = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //private void Remove_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (SelectedLamp == null)
        //            return;

        //        new RemoveLampCommand(_repo).Execute(SelectedLamp.Id);
        //        Refresh();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

        //private void On_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (SelectedLamp == null)
        //            return;

        //        new SwitchOnLampCommand(_repo).Execute(SelectedLamp.Id);
        //        Refresh();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error on Switching On Lamp", MessageBoxButton.OK, MessageBoxImage.Warning);
        //    }
        //}

        //private void Off_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (SelectedLamp == null)
        //            return;

        //        new SwitchOffLampCommand(_repo).Execute(SelectedLamp.Id);
        //        Refresh();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

        //private void Intensity_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (SelectedLamp == null)
        //            return;

        //        if (!int.TryParse(IntensityTextBox.Text, out int intensity) || intensity < 0 || intensity > 100)
        //        {
        //            MessageBox.Show("Intensity must be between 0 and 100");
        //            return;
        //        }

        //        new ChangeIntensityCommand(_repo).Execute(SelectedLamp.Id, intensity);

        //        Refresh();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}
    }
}