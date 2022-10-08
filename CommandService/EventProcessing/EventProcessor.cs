using System.Text.Json;
using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CommandService.EventProcessor
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }
        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);
            switch (eventType)
            {
                case EventType.PlateformPublished:
                    addPlateform(message);
                    break;
                default:
                    break;
            }
        }

        private void addPlateform(string pateformPublishedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ICommandRepo>();

                var plateformPublishedDto = JsonSerializer.Deserialize<PlateformPublishedDto>(pateformPublishedMessage);

                try
                {
                    var plat = _mapper.Map<Platform>(plateformPublishedDto);
                    if (!repo.ExternalPatformExists(plat.ExternalId))
                    {
                        repo.CreatePlatform(plat);
                        repo.SaveChanges();
                        Console.WriteLine($"-----> Patform Added");
                    }
                    else
                    {
                        Console.WriteLine($"Plateform already exists");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Could not add Platform to DB: {ex.Message}");
                }
            }
        }

        private EventType DetermineEvent(string notificationMessage)
        {
            System.Console.WriteLine("---> Determining Event");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

            switch (eventType.Event)
            {
                case "Plateform_Published":
                    System.Console.WriteLine("---> Platform published Event Detected");
                    return EventType.PlateformPublished;
                default:
                    System.Console.WriteLine("---> Could not determinde the event type");
                    return EventType.Undetermined;
            }
        }
    }

    enum EventType
    {
        PlateformPublished,
        Undetermined
    }
}