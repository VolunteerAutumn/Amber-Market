using System.ComponentModel.DataAnnotations;

namespace Market.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string Condition { get; set; } = string.Empty;

        public string Rarity { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ImagePath { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}