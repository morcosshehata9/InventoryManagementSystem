namespace InventoryManagementSystem.Models
{
    public class ProductWarehouse
    {
        public int Quantity { get; set; }
            
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }

    }
}
