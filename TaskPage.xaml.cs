using Chapter7.Model.Exercise1;
using Chapter7.ViewModel.Execise5;
using CommunityToolkit.Maui.Alerts;

namespace Chapter7.View.Exercise5;

public partial class TaskPage : ContentPage
{
	public TaskViewModel _viewModel;
	public TaskPage()
	{
		InitializeComponent();
		_viewModel = (TaskViewModel)BindingContext;
		_viewModel.EditEvent += EditEvent;
		_viewModel.DeleteEvent += DeleteEvent;
	}

    private void DeleteEvent(object sender, bool e)
    {
        if (e)
        {
            Toast.Make("Task Deleted SuccessFully", CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
        }
        else
        {
            Toast.Make("Something went wrong", CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
        }
    }

    private async void EditEvent(object sender, Results e)
    {
        if (e.IsSuccess)
        {
            await Navigation.PushAsync(new UpdateTaskPage(e.Id));
        }
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NewTaskPage());
    }
}