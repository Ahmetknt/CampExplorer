namespace CatalogAPI.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string EquipmentCollectionName { get; set; }
        public string CategoryCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
