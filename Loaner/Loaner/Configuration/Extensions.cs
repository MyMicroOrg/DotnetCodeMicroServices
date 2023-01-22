namespace Loaner.Configuration
{
    public static class Extensions
    {
        public static TModel GetOptions<TModel>(this IConfiguration configuration, string sectionName) where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(sectionName).Bind(model);

            return model;
        }

        public static string DatabaseConnectionString(this IConfiguration configuration)
        {
            var dbOptions = configuration.GetOptions<DatabaseSettingOptions>(DatabaseSettingOptions.SectionName);
            return dbOptions.ConnectionString;
        }
    }
}
