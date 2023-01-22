namespace Loaner.Configuration
{
    public class DatabaseSettingOptions
    {
        public const string SectionName = "ConnectionStrings";
        public string ConnectionString { get; set; } = "";
    }
}
