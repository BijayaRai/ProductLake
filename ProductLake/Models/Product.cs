namespace ProductLake.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Colour { get; set; } = "";

        public Product(string name)
        { 
            this.Name = name;
        }
    }
}
