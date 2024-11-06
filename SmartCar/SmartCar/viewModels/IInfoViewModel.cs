using SmartCar.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SmartCar.ViewModels
{
    public interface IInfoViewModel
    {
        ObservableCollection<SmarterCar> Cars { get; }

        ICommand UpdateCarCommand { get; set; }
        SmarterCar SelectedCar { get; set; }


    }
}
