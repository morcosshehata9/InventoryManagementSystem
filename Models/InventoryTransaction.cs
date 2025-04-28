using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryManagementSystem.Enums;

namespace InventoryManagementSystem.Models
{
    public class InventoryTransaction: BaseModel
    {
        public TransactionType TransactionType { get; set; }
        public int Quantity { get; set; }
        public DateTime TransactionDate { get; set; }
        public string PerformedBy { get; set; }
        [ForeignKey("Product")]
        [Required]
        public int ProductId { get; set; }

        public Product Product { get; set; }

    }
}
