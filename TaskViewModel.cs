using Chapter7.Database;
using Chapter7.Model.Exercise1;
using Chapter7.Table;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Chapter7.ViewModel.Execise5
{
    public class TaskViewModel : INotifyPropertyChanged
    {
        private TaskDatabase _taskDatabase;
        private List<TaskTable> _taskDetails;
/*        private bool _isLoading;
        private bool _isRefreshing;
        private bool _isVisible;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                _isRefreshing = value;
                OnPropertyChanged();
            }
        }
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                OnPropertyChanged();
            }
        }*/
        public List<TaskTable> TaskDetails
        {
            get => _taskDetails;
            set
            {
                _taskDetails = value;
                OnPropertyChanged();
            }
        }
        public ICommand EditTaskCommand { get; private set; }
        public ICommand DeleteTaskCommand { get; private set; }
        public event EventHandler<Results> EditEvent;
        public event EventHandler<bool> DeleteEvent;

        public TaskViewModel()
        {
            _taskDatabase = new TaskDatabase();
            _taskDatabase.CreateDatabase();
            _ = _taskDatabase.CreateTaskTableAsync();
            _ = TaskListAsync();
            EditTaskCommand = new Command<TaskTable>((TaskTable) => { _ = UpdateTaskDetails(TaskTable); });
            DeleteTaskCommand = new Command<TaskTable>((TaskTable) => { _ = DeleteTaskDetails(TaskTable); });

        }
       
        public async Task TaskListAsync()
        {
            await _taskDatabase.GetListAsync();
            TaskDetails = _taskDatabase.TaskDetails;
            
        }
        public async Task UpdateTaskDetails(TaskTable taskDetails)
        {
            _taskDatabase.Id = taskDetails.Id;
            var result = await _taskDatabase.GetTaskInfo();
            EditEvent?.Invoke(this, result);
        }
        public async Task DeleteTaskDetails(TaskTable taskTable)
        {
            _taskDatabase.Id = taskTable.Id;
            var result = await _taskDatabase.DeleteTaskAsync();
            DeleteEvent?.Invoke(this, result);
            await TaskListAsync();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyChanged = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyChanged));
        }
    }
}
