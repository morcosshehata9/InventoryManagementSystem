using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class Product : BaseModel
    {
        [Required]
        public string Name { get; set; }
        [MaxLength(255)]
        public string? Description { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int LowStockThreshold { get; set; }

        public ICollection<InventoryTransaction> InventoryTransactions { get; set; }


    }
}
