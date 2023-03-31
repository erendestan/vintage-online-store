namespace Individual_Project___Eren_Destan.Models
{
    public class Product
    {
        protected int id;
        protected string name;
        protected double price;
        protected int stock;

        public Product(int id, string name, double price, int stock)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.stock = stock;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
    }
    
}
