using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Loaner.Configuration
{
    public static class OptionSettingsExtention
    {
        public static void AddOptionSettings(this IServiceCollection services, IConfiguration _config) {

            services.Configure<DatabaseSettingOptions>(_config.GetSection(DatabaseSettingOptions.SectionName));
        }  
    }
}
