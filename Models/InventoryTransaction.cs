using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryManagementSystem.Enums;

namespace InventoryManagementSystem.Models
{
    public class InventoryTransaction
    {
        public int Id { get; set; }
        public TransactionType TransactionType { get; set; }
        public int Quantity { get; set; }
        public DateTime TransactionDate { get; set; }
        public string PerformedBy { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("Product")]
        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [ForeignKey("SourceWarehouse")]
        public int SourceWarehouseId { get; set; }
        [ForeignKey("DestinationWarehouse")]
        public int DestinationWarehouseId { get; set; }
        public Warehouse SourceWarehouse { get; set; }
        public Warehouse DestinationWarehouse { get; set; }



    }
}
