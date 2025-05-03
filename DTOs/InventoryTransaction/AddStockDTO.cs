namespace InventoryManagementSystem.DTOs.InventoryTransaction
{
    public class AddStockDTO
    {
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
        public int Quantity { get; set; }
    }
}
