namespace InventoryManagementSystem.DTOs.InventoryTransaction
{
    public class TransferStockDTO
    {
        public int ProductId { get; set; }
        public int SourceWarehouseId { get; set; }
        public int DestinationWarehouseId { get; set; }
        public int Quantity { get; set; }
    }
}
