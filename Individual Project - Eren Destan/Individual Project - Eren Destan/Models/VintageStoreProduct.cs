namespace Individual_Project___Eren_Destan.Models
{
    public class VintageStoreProduct : Product
    {
        private ItemType itemType;
        private string description;

        public VintageStoreProduct(int id, string name, ItemType itemType, string description, double price, int stock) : base(id, name, price, stock)
        {
            this.itemType = ItemType;
            this.description = description;
        }
        public ItemType ItemType { get; set; }
        public string Description { get; set; }
    }
}
