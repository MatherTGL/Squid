namespace GameAssets.Core.Data
{
    public static class DataController
    {
        public static IModel dataModel { get; private set; } = new DataModel();
    }
}
