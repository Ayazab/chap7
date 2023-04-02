using Chapter7.Database;
using Chapter7.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Chapter7.ViewModel.Execise5
{
    public class EditTaskViewModel : INotifyPropertyChanged
    {
        private TaskDatabase _taskDatabase;
        private string _taskName;
        private string _description;
        private DateTime _completionDate;
        private TimeSpan _startTime;
        private TimeSpan _endTime;
        public int Id { get; set; }
        public List<TaskTable> TaskDetails { get; set; }
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
        public ICommand UpdateCommand { get; private set; }
        public event EventHandler<bool> UpdateEvent;

        public EditTaskViewModel()
        {
            _taskDatabase = new TaskDatabase();
            _taskDatabase.CreateDatabase();
            _ = _taskDatabase.CreateTaskTableAsync();
            UpdateCommand = new Command(() => { _ = EditTaskDetails(); });
        }

        public async Task DisplayDetails()
        {
            _taskDatabase.Id = Id;
            await _taskDatabase.GetTaskInfo();
            TaskName = _taskDatabase.TaskName;
            Description = _taskDatabase.Description;
            CompletionDate = _taskDatabase.CompletionDate;
            StartTime = _taskDatabase.StartTime;
            EndTime = _taskDatabase.EndTime;
        }
        public async Task EditTaskDetails()
        {
            _taskDatabase.TaskName = TaskName;
            _taskDatabase.Description = Description;
            _taskDatabase.CompletionDate = CompletionDate;
            _taskDatabase.StartTime = StartTime;
            _taskDatabase.EndTime = EndTime;
            var result = await _taskDatabase.UpdateTaskAsync();
            UpdateEvent?.Invoke(this, result);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyChanged = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyChanged));
        }
    }
}

