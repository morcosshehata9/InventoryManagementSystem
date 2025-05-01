namespace InventoryManagementSystem.Models
{
    public class Warehouse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location {  get; set; }

        public bool IsDeleted { get; set; }


        public ICollection<ProductWarehouse> ProductWarehouses { get; set; } = new List<ProductWarehouse>();

    }
}
