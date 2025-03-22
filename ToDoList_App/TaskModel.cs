using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList_App
{
    public class TaskModel
    {
        public string TaskName { get; set; } = string.Empty;  // ✅ Fix: Initialize property
        public string Deadline { get; set; } = "No deadline";
        public bool IsChecked { get; set; }
    }
}
