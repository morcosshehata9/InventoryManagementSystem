using InventoryManagementSystem.Enums;

namespace InventoryManagementSystem.DTOs.Reports
{
    public class TransactionHistoryDTO
    {
        public int TransactionId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public string CategoryName { get; set; }
        public string TransactionType { get; set; }
        public int Quantity { get; set; }
        public string PerformedBy { get; set; }
        public DateTime TransactionDate { get; set; }
        public string SourceWarehouse { get; set; }
        public string DestinationWarehouse { get; set; }
    }
}
