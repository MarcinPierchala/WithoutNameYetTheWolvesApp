using System.ComponentModel.DataAnnotations;

namespace WithoutNameYetTheWolvesApp.Models
{
    public class SearchModel
    {
        [Required]
        public string Prompt { get; set; }
        [Required]
        public string Response { get; set; }
    }
}
