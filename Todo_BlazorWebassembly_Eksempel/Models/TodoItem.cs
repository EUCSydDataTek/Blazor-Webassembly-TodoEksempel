using System.ComponentModel.DataAnnotations;

namespace Todo_BlazorWebassembly_Eksempel.Models
{
    public class TodoItem
    {

        public int Id { get; set; }

        [Required]
        [StringLength(12,ErrorMessage = "Max length for the title is 12 carecters")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(500, ErrorMessage = "Max length for the description is 500 carecters")]
        public string Description { get; set; } = string.Empty;

        public bool IsCompleted = false;

        public DateTime TimeCreated { get; set; }

    }
}
