using SmartCar.ViewModels;

namespace SmartCar.Views;

public partial class DetailsPage : ContentPage
{
	public DetailsPage(IDetailViewModel viewModels)
	{
		InitializeComponent();
		BindingContext = viewModels;
	}
}