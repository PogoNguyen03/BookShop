using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShoppingCartMvcUI.Models
{
    [Table("Chapter")]
    public class Chapter
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string? Image { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
