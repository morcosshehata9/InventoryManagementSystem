using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.DTOs.Product
{
    public class AddProductDTO
    {
        [Required]
        public string Name { get; set; }
        [MaxLength(255)]
        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }
        [Required]
        public int LowStockThreshold { get; set; }

        public int CategoryId { get; set; }
    }
}
