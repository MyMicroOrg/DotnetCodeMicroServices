using PlaterformService.Dtos;

namespace PlateformService.SyncDataService.Http{

    public interface ICommandDataClient
    {
        Task SendPlateformToCommand(PlateformReadDto plateform);
    }
}