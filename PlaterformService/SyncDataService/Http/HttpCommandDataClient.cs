using System.Text;
using System.Text.Json;
using PlaterformService.Dtos;

namespace PlateformService.SyncDataService.Http{

   public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task SendPlateformToCommand(PlateformReadDto plateform)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(plateform),
                Encoding.UTF8,
                "application/json"
            );

        
            var response = await _httpClient.PostAsync($"{_configuration["Commandservice"]}",httpContent);
            if(response.IsSuccessStatusCode){
                Console.WriteLine("----> SyncDataService Post to command service was Ok");
            }else{
                Console.WriteLine("----> SyncDataService Post to command service was NOT Ok");
            }
        }
    }
}