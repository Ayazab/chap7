using Chapter7.ViewModel.Execise5;
using CommunityToolkit.Maui.Alerts;

namespace Chapter7.View.Exercise5;

public partial class UpdateTaskPage : ContentPage
{
	private EditTaskViewModel _viewModel;
	public UpdateTaskPage(int id)
	{
		InitializeComponent();
		_viewModel = (EditTaskViewModel)BindingContext;
		_viewModel.Id = id;
		_ = _viewModel.DisplayDetails();
		_viewModel.UpdateEvent += UpdateEvent;
	}

    private async void UpdateEvent(object sender, bool e)
    {
        if (e)
        {
            await Toast.Make("Task Updated Successfully", CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
            await Navigation.PushAsync(new TaskPage());
        }
        else
        {
            await Toast.Make("Something went wrong", CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
        }
    }
}