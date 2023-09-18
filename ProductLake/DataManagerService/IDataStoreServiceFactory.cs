namespace ProductLake.DataManagerService
{
    public interface IDataStoreServiceFactory 
    { 
        IProductDataStore GetDataStore(string dataStoreType);
    }

    public class DataStoreServiceFactory : IDataStoreServiceFactory
    {
        public IProductDataStore GetDataStore(string dataStoreType)
        {
            if (dataStoreType == "inMemory")
                return new InMemoryProductDataStore();

            return null;
        }
    }
}
