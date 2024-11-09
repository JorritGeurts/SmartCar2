using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SmartCar.Messages;
using SmartCar.Models;
using SmartCar.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace SmartCar.ViewModels
{
    public class InfoViewModel : ObservableRecipient, IInfoViewModel, IRecipient<RefreshCarMessage>
    {
        //private readonly IStorageService _storageService;
        public void Receive(RefreshCarMessage message)
        {
            LoadCars();
        }

        //public ObservableCollection<SmarterCar> Cars { get; } = new ObservableCollection<SmarterCar>();
        private ObservableCollection<SmarterCar> cars;
        public ObservableCollection<SmarterCar> Cars
        {
            get => cars;
            set => SetProperty(ref cars, value);
        }
        private SmarterCar selectedCar;
        public SmarterCar SelectedCar
        {
            get => selectedCar;
            set => SetProperty(ref selectedCar, value);
        }
        public ICommand UpdateCarCommand { get; set; }
        public INavigationService _navigationService;
        public InfoViewModel(IStorageService storageService, INavigationService navigationService)
        {
            //_storageService = storageService;
            _navigationService = navigationService;
            
            Messenger.Register<InfoViewModel, RefreshCarMessage>(this, (r,m)=> r.Receive(m));

            LoadCars();
            BindCommands();
        }

        private async void LoadCars()
        {
            //var cars = await _storageService.GetAllCarsAsync();
            //foreach (var car in cars)
            //{
            //    Cars.Add(car);
            //    Console.WriteLine($"Car loaded: {car.Name}"); // Debug output
            //}

            //Console.WriteLine($"Total cars loaded: {Cars.Count}"); // Total count debug output
            Cars = new ObservableCollection<SmarterCar>(await SmartCarService.GetCarsAsync());

            foreach (SmarterCar car in Cars)
            {
                await LoadDamagesAndSeverities(car);
            }
        }

        private async Task LoadDamagesAndSeverities(SmarterCar car)
        {
            var carSeverities = await APIService<CarSeverityDTO>.GetCarSeveritiesByCarIdAsync(car.Id);

            if (carSeverities != null)
            {
                car.CarSeverities = new ObservableCollection<CarSeverityDTO>(carSeverities);

                foreach (var carSeverity in carSeverities)
                {
                    // Fetch related Damage and Severity data if necessary (example)
                    carSeverity.DamageTypes = await APIService<DamageTypes>.GetAsync($"Damages/{carSeverity.DamageId}");
                    carSeverity.Severities = await APIService<Severities>.GetAsync($"Severity/{carSeverity.SeverityId}");
                }

            }
        }

        private void BindCommands()
        {
            UpdateCarCommand = new RelayCommand(GoToDetailsUpdate);
        }

        private async void GoToDetailsUpdate()
        {
            await _navigationService.NavigateToDetailsPageAsync();
            var dto = SmartCarService.MapToDto(selectedCar);
            WeakReferenceMessenger.Default.Send(new CarSelectedMessages(dto));
        }
    }
}
