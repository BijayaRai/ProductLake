using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductLake.DataManagerService;
using ProductLake.Models;

namespace ProductLake.Controllers
{
    [ApiController]
    public class ProductController : Controller
    {
        private IDataStoreServiceFactory dataStoreServiceFactory;

        public ProductController(IDataStoreServiceFactory factory)
        {
            dataStoreServiceFactory = factory;
        }

        [Authorize]
        [HttpGet("api/[controller]")]
        public IEnumerable<Product> GetProducts() 
        {
            IProductDataStore productDataStore = this.dataStoreServiceFactory.GetDataStore("inMemory");
            return productDataStore.RetrieveProductList();
        }


        [HttpGet("api/[controller]/{colour}")]
        [Authorize]
        public IEnumerable<Product> GetProductsByColour(string colour)
        {
            IProductDataStore productDataStore = this.dataStoreServiceFactory.GetDataStore("inMemory");
            return productDataStore.RetrieveFilteredProductList(colour);
        }
    }
}
