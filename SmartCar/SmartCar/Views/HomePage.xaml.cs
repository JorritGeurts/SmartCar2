using SmartCar.ViewModels;

namespace SmartCar.Views;

public partial class HomePage : ContentPage
{
	public HomePage(IHomeViewModel viewModel)
	{
        InitializeComponent();

		BindingContext = viewModel;
	}

    private void KmAmountEntry_Focused(object sender, FocusEventArgs e)
    {

    }
}