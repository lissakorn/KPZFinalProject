using System.ComponentModel.DataAnnotations;

namespace PersonalOrganizer.Models
{
    public enum TaskPriority
    {
        [Display(Name = "Низький")]
        Low,

        [Display(Name = "Середній")]
        Medium,

        [Display(Name = "Високий")]
        High
    }
}