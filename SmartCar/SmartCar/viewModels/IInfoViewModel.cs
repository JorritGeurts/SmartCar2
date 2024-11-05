using SmartCar.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SmartCar.ViewModels
{
    public interface IInfoViewModel
    {
        ObservableCollection<SmarterCar> Cars { get; }

        ICommand UpdateCarCommand { get; set; }
        void LoadCars();
        SmarterCar SelectedCar { get; set; }


    }
}
