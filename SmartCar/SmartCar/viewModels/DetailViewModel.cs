using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SmartCar.Messages;
using SmartCar.Models;
using SmartCar.Services;
using System.Windows.Input;

namespace SmartCar.ViewModels
{
    public class DetailViewModel : ObservableRecipient, IDetailViewModel, IRecipient<CarSelectedMessages>
    {
        public void Receive(CarSelectedMessages message)
        {
            Car = message.Value;
            SaveText = "Update";
        }

        private SmarterCarDTO car = new SmarterCarDTO();
        public SmarterCarDTO Car
        {
            get => car;
            set
            {
                SetProperty(ref car, value);
            }
        }


        private string saveText = string.Empty;
        public string SaveText
        {
            get => saveText;
            set
            {
                SetProperty(ref saveText, value);
                OnPropertyChanged(nameof(CanDelete));
            }
        }

        public bool CanDelete
        {
            get => SaveText == "Update";
        }

        public ICommand SaveCommand {  get; set; }
        public ICommand DeleteCommand {  get; set; }
        public ICommand CancelCommand { get; set; }
        private INavigationService _navigationService;

        public DetailViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Messenger.Register<DetailViewModel, CarSelectedMessages>(this,(r,m)=> r.Receive(m));
            BindCommands();
        }

        private void BindCommands() 
        { 
            SaveCommand = new AsyncRelayCommand(SaveAndGoBack);
            DeleteCommand = new AsyncRelayCommand(DeleteAndGoBack);
            CancelCommand = new AsyncRelayCommand(GoBack);
        }

        private async Task SaveAndGoBack()
        {
            await SmartCarService.UpdateCarAsync(Car);
            WeakReferenceMessenger.Default.Send(new RefreshCarMessage());
            await _navigationService.NavigateBackAsync();
        }

        private async Task DeleteAndGoBack()
        {
            await SmartCarService.DeleteCarAsync(Car.Id);
            WeakReferenceMessenger.Default.Send(new RefreshCarMessage());
            await _navigationService.NavigateBackAsync();
        }

        private async Task GoBack()
        {
            await _navigationService.NavigateBackAsync();
        }
    }
}
