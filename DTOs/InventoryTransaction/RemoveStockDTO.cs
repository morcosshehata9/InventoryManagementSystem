namespace InventoryManagementSystem.DTOs.InventoryTransaction
{
    public class RemoveStockDTO
    {
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
        public int Quantity { get; set; }
    }
}
