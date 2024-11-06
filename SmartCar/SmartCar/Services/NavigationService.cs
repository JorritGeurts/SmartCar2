using SmartCar.Models;
using SmartCar.Views;


namespace SmartCar.Services
{
    public class NavigationService : INavigationService
    {
        private INavigation _navigation;
        private IServiceProvider _serviceProvider;
        private IStorageService _storageService;

        public NavigationService(IServiceProvider serviceProvider, IStorageService storageService)
        {
            _serviceProvider = serviceProvider;
            _navigation = Application.Current.MainPage.Navigation;
            _storageService = storageService;
        }

        public async Task NavigateToInfoPageAsync(SmarterCar car)
        {
            try
            {
                // Sla de autogegevens op
                await _storageService.SaveCarAsync(car);

                // Navigeer naar de InfoPage
                await Shell.Current.GoToAsync("//InfoPage");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fout bij opslaan en navigeren: {ex.Message}");
                throw;
            }
        }

        public async Task NavigateBackAsync()
        {
            if(_navigation.NavigationStack.Count > 1)
            {
                await _navigation.PopAsync();
            }
            else
            {
                throw new InvalidOperationException("No pages to navigate back to!");
            }
        }

        public async Task NavigateToDetailsPageAsync()
        {
            await _navigation.PushAsync(_serviceProvider.GetRequiredService<DetailsPage>());
        }

        

        
    }
}
