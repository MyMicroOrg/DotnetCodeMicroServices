using PlateformService.Dtos;

namespace PlateformService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewPlateform(PlateformPublishDto plateformPublishDto);
    }
}