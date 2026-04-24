using System.ComponentModel.DataAnnotations;

namespace PersonalOrganizer.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва категорії обов'язкова")]
        [StringLength(50, ErrorMessage = "Назва не може бути довшою за 50 символів")]
        [Display(Name = "Назва категорії")]
        public string Name { get; set; } = string.Empty;

      
        public virtual ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}