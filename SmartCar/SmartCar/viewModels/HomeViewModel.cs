using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SafariSnap.Services;
using SmartCar.Messages;
using SmartCar.Models;
using SmartCar.Services;
using SmartCar.ViewModels;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;

namespace SmartCar.ViewModels
{
    public class HomeViewModel : ObservableObject, IHomeViewModel,IRecipient<CarSelectedMessages>
    {
        public void Receive(CarSelectedMessages message)
        {
            var dto = SmartCarService.MapToDto(classifiedCar);
            dto = message.Value;
        }
        private bool isRunning = false;

        public bool IsRunning
        {
            get => isRunning;
            set => SetProperty(ref isRunning, value);
        }

        private bool isCarClassified = false;
        public bool IsCarClassified
        {
            get => isCarClassified;
            set
            {
                SetProperty(ref isCarClassified, value);
            }
        }

        private ObservableCollection<ImageSource> photos = new ObservableCollection<ImageSource>();
        public ObservableCollection<ImageSource> Photos
        {
            get => photos;
            set
            {
                SetProperty(ref photos, value);
                OnPropertyChanged(nameof(HasPhotos));
                OnPropertyChanged(nameof(CanPickOrTakePhoto));
            }
        }

        private ObservableCollection<DamageEntry> _damageEntries = new ObservableCollection<DamageEntry>
        {
            new DamageEntry()
        };
        public ObservableCollection<DamageEntry> DamageEntries
        {
            get => _damageEntries;
            set => SetProperty(ref _damageEntries, value);
        }

        public bool HasPhotos => Photos?.Count >= 1;
        public bool CanPickOrTakePhoto => !HasPhotos;

        private SmarterCar classifiedCar;
        public SmarterCar ClassifiedCar
        {
            get => classifiedCar;
            set => SetProperty(ref classifiedCar, value);
        }

        public ObservableCollection<DamageTypes> DamageTypes { get; set; } = new ObservableCollection<DamageTypes>();

        private DamageTypes selectedDamageType;
        public DamageTypes SelectedDamageType
        {
            get => selectedDamageType;
            set
            {
                SetProperty(ref selectedDamageType, value);
            }
        }

        public ObservableCollection<Severities> Severities { get; set; } = new ObservableCollection<Severities>();

        private Severities selectedSeverity;
        public Severities SelectedSeverity
        {
            get => selectedSeverity;
            set
            {
                SetProperty(ref selectedSeverity, value);
            }
        }


        public ICommand PickPhotoCommand { get; set; }
        public ICommand TakePhotoCommand { get; set; }
        public ICommand AddPhotoCommand { get; set; }
        public ICommand RemovePhotoCommand { get; set; }
        public ICommand ShowAddPhotoMenuCommand { get; set; }
        public ICommand SaveAllInfoCommand { get; set; }
        public ICommand AddDamageEntryCommand { get; }

        private IStorageService _storageService;
        private INavigationService _navigationService;

        public HomeViewModel(IStorageService storageService, INavigationService navigationService)
        {
            BindCommands();
            LoadDamageTypes();
            LoadSeverityTyeps();
            _storageService = storageService;
            _navigationService = navigationService;
            AddDamageEntryCommand = new RelayCommand(AddDamageEntry);
        }

        private void BindCommands()
        {
            PickPhotoCommand = new AsyncRelayCommand(PickAndClassifyPhoto);
            TakePhotoCommand = new AsyncRelayCommand(TakeAndClassifyPhoto);
            ShowAddPhotoMenuCommand = new RelayCommand(ShowAddPhotoMenu);
            AddPhotoCommand = new AsyncRelayCommand(AddPhotoCommandExecute);
            RemovePhotoCommand = new RelayCommand<ImageSource>(RemovePhotoCommandExecute);
            SaveAllInfoCommand = new AsyncRelayCommand(SaveAllInfoAndNavigate);
        }

        //Load all Damages for dropdown
        private async void LoadDamageTypes()
        {
            try
            {
                var damageTypes = await SmartCarService.GetAllDamageTypes(); // API call to fetch data
                DamageTypes.Clear();

                foreach (var type in damageTypes)
                {
                    DamageTypes.Add(type);  // Add the fetched data to the ObservableCollection
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading damage types: {ex.Message}");
            }
        }

        //Load all severities for dropdown
        private async void LoadSeverityTyeps()
        {
            try
            {
                var damageTypes = await SmartCarService.GetAllSeverities(); // API call to fetch data
                Severities.Clear();

                foreach (var type in damageTypes)
                {
                    Severities.Add(type);  // Add the fetched data to the ObservableCollection
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading damage types: {ex.Message}");
            }
        }


        private void AddDamageEntry()
        {

            DamageEntries.Add(new DamageEntry());
        }


        private async Task PickPhoto()
        {
            var photo = await MediaPicker.Default.PickPhotoAsync();
            await AddPhoto(photo);
        }

        private async Task TakePhoto()
        {
            var photo = await MediaPicker.Default.CapturePhotoAsync();
            await AddPhoto(photo);
        }

        private void ShowAddPhotoMenu()
        {
            Application.Current.MainPage.DisplayActionSheet("Add Photo", "Cancel", null, "Pick Photo", "Take Photo")
                .ContinueWith(async (task) =>
                {
                    var action = await task;
                    if (action == "Pick Photo")
                    {
                        await PickPhoto();
                    }
                    else if (action == "Take Photo")
                    {
                        await TakePhoto();
                    }
                });
        }

        private async Task AddPhoto(FileResult photo)
        {
            if (photo != null)
            {
                var stream = await photo.OpenReadAsync();
                Photos.Add(ImageSource.FromStream(() => stream));
                OnPropertyChanged(nameof(HasPhotos));
            }
        }

        private async Task PickAndClassifyPhoto()
        {
            var photo = await MediaPicker.Default.PickPhotoAsync();
            await ClassifyPhotoAsync(photo);
        }

        private async Task TakeAndClassifyPhoto()
        {
            var photo = await MediaPicker.Default.CapturePhotoAsync();
            await ClassifyPhotoAsync(photo);
        }

        private async Task AddPhotoCommandExecute()
        {
            var photo = await MediaPicker.Default.PickPhotoAsync();
            if (photo != null)
            {
                var stream = await photo.OpenReadAsync();
                Photos.Add(ImageSource.FromStream(() => stream));
                OnPropertyChanged(nameof(HasPhotos));
            }
        }

        private void RemovePhotoCommandExecute(ImageSource photo)
        {
            if (photo != null && Photos.Contains(photo) && !IsFirstPhoto(photo))
            {
                Photos.Remove(photo);
                OnPropertyChanged(nameof(HasPhotos));
                OnPropertyChanged(nameof(CanPickOrTakePhoto));
            }
        }

        public bool IsFirstPhoto(ImageSource photo)
        {
            return Photos.IndexOf(photo) == 0;
        }

        private async Task SaveAllInfoAndNavigate()
        {

            var dto = SmartCarService.MapToDto(classifiedCar);

            var carId = await APIService<SmarterCarDTO>.PostAsyncAndReturnId("Car/", dto);


            foreach (var damageEntry in DamageEntries)
            {

                if (damageEntry.SelectedDamageType != null && damageEntry.SelectedSeverity != null)
                {
                    // Create CarSeverityDTO using the CarId, SeverityId, and DamageId from the entry
                    var carSeverityDto = new CarSeverityDTO
                    {
                        CarId = carId,
                        SeverityId = damageEntry.SelectedSeverity.Id,
                        DamageId = damageEntry.SelectedDamageType.Id,
                    };

                    // Call the API to save this severity
                    await SmartCarService.AddCarSeverity(carSeverityDto);
                }
                else
                {
                    Console.WriteLine("Skipping incomplete entry: both DamageType and Severity are required.");
                }

            }

            try
            {
                await _navigationService.NavigateToInfoPageAsync(ClassifiedCar);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fout bij opslaan en navigeren: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", $"Failed to save data: {ex.Message}", "OK");
            }
        }

        private void RecalculatePrice()
        {
            double basePrice = ClassifiedCar.Price;
            double newPrice = basePrice;
            if (ClassifiedCar.IsDamaged)
            {
                newPrice *= 0.8;
            }
            
            ClassifiedCar.OldPrice = basePrice;
            ClassifiedCar.NewPrice = newPrice;
            OnPropertyChanged(nameof(ClassifiedCar));
        }


        private async Task ClassifyPhotoAsync(FileResult photo)
        {
            if (photo is { })
            {
                IsRunning = true;
                ClassifiedCar = new SmarterCar();
                var resizedPhoto = await PhotoImageService.ResizePhotoStreamAsync(photo);
                Photos.Add(ImageSource.FromStream(() => new MemoryStream(resizedPhoto)));
                OnPropertyChanged(nameof(HasPhotos));

                var result = await CustomVisionService.ClassifyImageAsync(new MemoryStream(resizedPhoto));
                if (result.TagName.Equals("Negative"))
                {
                    ClassifiedCar.Name = "Dit is geen Audi.";
                }
                else
                {
                    ClassifiedCar = SmartCarService.GetSmartCarByTag(result.TagName)!;
                    ClassifiedCar.Name += " ";
                }
                IsCarClassified = true;
                OnPropertyChanged(nameof(CanPickOrTakePhoto));
                IsRunning = false;
                RecalculatePrice();
            }
        }
    }
}
