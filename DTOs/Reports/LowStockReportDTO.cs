namespace InventoryManagementSystem.DTOs.Reports
{
    public class LowStockReportDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public int Quantity { get; set; }
        public int LowStockThreshold { get; set; }
    }
}
