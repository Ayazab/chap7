using Chapter7.Database;
using CommunityToolkit.Maui.Alerts;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Chapter7.ViewModel.Execise5
{
    public class AddTaskViewModel : INotifyPropertyChanged
    {
        private TaskDatabase _taskDatabase;
        private string _taskName;
        private string _description;
        private DateTime _completionDate;
        private TimeSpan _startTime;
        private TimeSpan _endTime;
        public string TaskName
        {
            get => _taskName;
            set
            {
                _taskName = value;
                OnPropertyChanged();
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }
        public DateTime CompletionDate
        {
            get => _completionDate;
            set
            {
                _completionDate = value;
                OnPropertyChanged();
            }
        }
        public TimeSpan StartTime
        {
            get => _startTime;
            set
            {
                _startTime = value;
                OnPropertyChanged();
            }
        }
        public TimeSpan EndTime
        {
            get => _endTime;
            set
            {
                _endTime = value;
                OnPropertyChanged();
            }
        }
        public ICommand AddTaskCommand { get; private set; }
        public event EventHandler<bool> AddEvent;

        public AddTaskViewModel()
        {
            _taskDatabase = new TaskDatabase();
            _taskDatabase.CreateDatabase();
            _ = _taskDatabase.CreateTaskTableAsync();
            _ = _taskDatabase.GetListAsync();
            AddTaskCommand = new Command(Validation);
        }

        public void Validation()
        {
            if (string.IsNullOrWhiteSpace(TaskName))
            {
                Toast.Make("Please enter task name", CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
            }
            else if (string.IsNullOrWhiteSpace(Description))
            {
                Toast.Make("Please Enter Description", CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
            }
            else
            {
                _ = AddDetailsAsync();
            }
        }

        
        public async Task AddDetailsAsync()
        {
            _taskDatabase.TaskName = TaskName;
            _taskDatabase.Description = Description;
            _taskDatabase.CompletionDate = CompletionDate;
            _taskDatabase.StartTime = StartTime;
            _taskDatabase.EndTime = EndTime;
            var result = await _taskDatabase.InsertTaskAsync();
            AddEvent?.Invoke(this, result);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
