using SmartCar.ViewModels;

namespace SmartCar.Views;

public partial class InfoPage : ContentPage
{
    private readonly IInfoViewModel _viewModel;
    public InfoPage(IInfoViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadCars();  // Call your function every time the page appears
    }
}
