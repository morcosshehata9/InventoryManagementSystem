namespace InventoryManagementSystem.DTOs.Product
{
    public class GetProductsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int LowStockThreshold { get; set; }
        public int TotalQuantity { get; set; }
        public string CategoryName { get; set; }
    }
}
