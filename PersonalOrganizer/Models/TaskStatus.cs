using System.ComponentModel.DataAnnotations;

namespace PersonalOrganizer.Models
{
    public enum TaskStatus
    {
        [Display(Name = "До виконання")]
        Todo,

        [Display(Name = "В процесі")]
        InProgress,

        [Display(Name = "Виконано")]
        Completed
    }
}