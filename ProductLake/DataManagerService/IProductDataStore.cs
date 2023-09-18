using ProductLake.Models;

namespace ProductLake.DataManagerService
{
    public interface IProductDataStore
    {
        IEnumerable<Product> RetrieveProductList();

        IEnumerable<Product> RetrieveFilteredProductList(string colour);
    }

    public class InMemoryProductDataStore : IProductDataStore
    {
        private readonly List<Product> products = new List<Product>()
            {
                new Product("Phone"),
                new Product("Laptop"),
            };
        public IEnumerable<Product> RetrieveProductList() => this.products;

        public IEnumerable<Product> RetrieveFilteredProductList(string colour)
        {
            return this.products.Where(item => item.Colour == colour);
        }
    }
    //  public class MagicDataStore : IProductDataStore {.............. }
    //  public class SqlDataStore : IProductDataStore {.............. }
    //  public class NoSqlDataStore : IProductDataStore {.............. }
}
