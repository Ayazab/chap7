using Chapter7.Model.Exercise1;
using Chapter7.Table;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter7.Database
{
    public class TaskDatabase
    {
        private SQLiteAsyncConnection _connection;
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public DateTime CompletionDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public List<TaskTable> TaskDetails { get; set; }
        public void CreateDatabase()
        {
            var databaseName = "Exercise5";
            var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var databasePath = Path.Combine(folderPath, databaseName);
            _connection = new SQLiteAsyncConnection(databasePath, true);
        }

        //create table
        public async Task CreateTaskTableAsync()
        {
            try
            {
                await _connection.CreateTableAsync<TaskTable>();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }

        //fetch
        public async Task<List<TaskTable>> GetListAsync()
        {
            try
            {
                var list = await _connection.Table<TaskTable>().ToListAsync();
                TaskDetails = list;
                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        //insert
        public async Task<bool> InsertTaskAsync()
        {
            var taskTable = new TaskTable()
            {
                TaskName = TaskName,
                Description = Description,
                CompletionDate = CompletionDate,
                StartTime = StartTime,
                EndTime = EndTime
            };
            try
            {
                var insertedRecord = await _connection.InsertAsync(taskTable);
                var result = insertedRecord > 0;
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        //update
        public async Task<bool> UpdateTaskAsync()
        {
            var taskTable = new TaskTable()
            {
                Id = Id,
                TaskName = TaskName,
                Description = Description,
                CompletionDate = CompletionDate,
                StartTime = StartTime,
                EndTime = EndTime
            };
            try
            {
                var updatedRecord = await _connection.UpdateAsync(taskTable);
                var result = updatedRecord > 0;
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        //delete
        public async Task<bool> DeleteTaskAsync()
        {
            var taskTable = new TaskTable()
            {
                Id = Id
            };
            try
            {
                var deletedRecord = await _connection.DeleteAsync(taskTable);
                var result = deletedRecord > 0;
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public async Task<Results> GetTaskInfo()
        {
            try
            {
                var list = await _connection.Table<TaskTable>().ToListAsync();

                var taskRecords = list.Where(x => x.Id == Id).FirstOrDefault();
                TaskName = taskRecords.TaskName;
                Description = taskRecords.Description;
                CompletionDate = taskRecords.CompletionDate;
                StartTime = taskRecords.StartTime;
                EndTime = taskRecords.EndTime;
                if (taskRecords == null)
                {
                    return new Results()
                    {
                        IsSuccess = false
                    };
                }
                else
                {
                    return new Results()
                    {
                        IsSuccess = true,
                        Id = Id
                    };
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}