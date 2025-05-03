using InventoryManagementSystem.Enums;

namespace InventoryManagementSystem.DTOs.Reports
{
    public class TransactionHistoryFilterDTO
    {
        public int? ProductId { get; set; }
        public int? CategoryId { get; set; }
        public string? PerformedBy { get; set; }
        public TransactionType? TransactionType { get; set; } 
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
