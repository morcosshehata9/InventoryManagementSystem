using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [MaxLength(255)]
        public string? Description { get; set; }
        [NotMapped]
        public int TotalQuantity => ProductWarehouses?.Sum(pw => pw.Quantity) ?? 0; 
        [Required]
        [Precision(18, 2)]
        public decimal Price { get; set; }
        [Required]
        public int LowStockThreshold { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public bool IsDeleted { get; set; }

        public Category Category { get; set; }
        public ICollection<InventoryTransaction> InventoryTransactions { get; set; } = new List<InventoryTransaction>();
        public ICollection<ProductWarehouse> ProductWarehouses { get; set; } = new List<ProductWarehouse>();


    }
}
