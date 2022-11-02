namespace Todo_BlazorWebassembly_Eksempel.Models
{
    public class TodoItem
    {

        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime TimeCreated { get; set; }

    }
}
