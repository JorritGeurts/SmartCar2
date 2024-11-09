using SmartCar.Models;
using SmartCar.ViewModels;

namespace SmartCar.Views;

public partial class HomePage : ContentPage
{
	public HomePage(IHomeViewModel viewModel)
	{
        InitializeComponent();

		BindingContext = viewModel;
	}

    private void OnSeveritySelected(object sender, EventArgs e)
    {
        // Get the Picker that triggered the event
        var picker = sender as Picker;

        // Check if the selected item is a Severity object (from DamageEntry)
        if (picker?.SelectedItem is Severities selectedSeverity)
        {
            // You can now run a function with the selectedSeverity
            HandleSeveritySelection(selectedSeverity);
        }
    }

    // Function to run when a severity is selected
    private void HandleSeveritySelection(Severities selectedSeverity)
    {
        // Example: Print the severity name (you can replace this with any logic you want)
        Console.WriteLine($"Selected Severity: {selectedSeverity.Name}");

        // If you need to perform calculations or update values in your ViewModel
        if (BindingContext is HomeViewModel viewModel)
        {
            // Assuming you have a method like UpdatePriceBasedOnSeverity in your ViewModel
            viewModel.CalculatePriceBasedOnDamage(selectedSeverity);
        }
    }

    private void KmAmountEntry_Focused(object sender, FocusEventArgs e)
    {

    }
}