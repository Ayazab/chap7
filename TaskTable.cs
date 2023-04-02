using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter7.Table
{
    [Table("Tasks")]
    public class TaskTable
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }

        [Unique, Column("TaskName")]
        public string TaskName { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("CompletionDate")]
        public DateTime CompletionDate { get; set; }

        [Column("StartTime")]
        public TimeSpan StartTime { get; set; }

        [Column("EndTime")]
        public TimeSpan EndTime { get; set; }
    }
}
