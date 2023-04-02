using Chapter7.ViewModel.Execise5;
using CommunityToolkit.Maui.Alerts;

namespace Chapter7.View.Exercise5;

public partial class NewTaskPage : ContentPage
{
	private AddTaskViewModel _viewModel;
	public NewTaskPage()
	{
		InitializeComponent();
		_viewModel = (AddTaskViewModel)BindingContext;
		_viewModel.AddEvent += AddEvent;
	}

    private async void AddEvent(object sender, bool e)
    {
        if (e)
        {
            await Toast.Make("Task Created SuccessFully", CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
            await Navigation.PushAsync(new TaskPage());
        }
        else
        {
            await Toast.Make("Something went wrong", CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
        }
    }
}