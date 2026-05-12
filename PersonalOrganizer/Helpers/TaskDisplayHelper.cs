using PersonalOrganizer.Models;

namespace PersonalOrganizer.Helpers
{
    public static class TaskDisplayHelper
    {
        public static string GetTimeColorClass(TaskItem task)
        {
            if (task.Status == Models.TaskStatus.Completed) return "text-success";

            var timeLeft = task.DueDate - DateTime.Now;

            if (timeLeft.TotalDays < 0) return "text-danger fw-bold";
            if (timeLeft.TotalDays <= 1) return "text-warning fw-bold";

            return "text-body";
        }
    }
}
