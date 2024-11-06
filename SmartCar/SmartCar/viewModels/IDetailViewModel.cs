using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SmartCar.Models;


namespace SmartCar.ViewModels
{
    public interface IDetailViewModel
    {
        ICommand SaveCommand { get; set; }
        ICommand CancelCommand { get; set; }
        ICommand DeleteCommand { get; set; }

        string SaveText { get; set; }
        bool CanDelete { get; }

        SmarterCarDTO Car { get; set; }
    }
}
