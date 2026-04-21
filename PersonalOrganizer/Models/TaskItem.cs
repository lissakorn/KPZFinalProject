using System;
using System.ComponentModel.DataAnnotations;

namespace PersonalOrganizer.Models
{
    public enum TaskPriority { Low, Medium, High }
    public enum TaskStatus { Todo, InProgress, Completed }

    public class TaskItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва обов'язкова")]
        [StringLength(100)]
        public string Title { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Вкажіть дату завершення")]
        public DateTime DueDate { get; set; }

        public TaskPriority Priority { get; set; }
        public TaskStatus Status { get; set; }

        public string TimeColorClass
        {
            get
            {
                if (Status == TaskStatus.Completed) return "text-success";
                var timeLeft = DueDate - DateTime.Now;
                if (timeLeft.TotalDays < 0) return "text-danger fw-bold";
                if (timeLeft.TotalDays <= 1) return "text-warning fw-bold";
                return "text-body";
            }
        }
    }
}