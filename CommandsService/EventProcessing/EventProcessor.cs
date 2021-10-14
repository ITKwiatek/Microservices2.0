using System;
using System.Text.Json;
using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CommandsService.EventProcessing
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

            switch(eventType)
            {
                case EventType.PlatformPublished:
                    //Todo
                    break;
                default:
                    break;
            }
        }

        private EventType DetermineEvent(string nofiticationMessage)
        {
            System.Console.WriteLine("--> Determining Event");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(nofiticationMessage);

            switch(eventType.Event)
            {
                case "Platform_Published":
                    System.Console.WriteLine("--> Platform_Published event detected");
                    return EventType.PlatformPublished;
                default:
                    System.Console.WriteLine("--> Could not determine event type");
                    return EventType.Undetermined;
            }
        }

        private void AddPlatform(string platformPublishedMethod)
        {
            using(var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ICommandRepo>();

                var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishedDto>(platformPublishedMethod);

                try
                {
                    var plat = _mapper.Map<Platform>(platformPublishedDto);
                    if(!repo.ExternalPlatformExistst(plat.ExternalId))
                    {
                        repo.CreatePlatform(plat);
                        repo.SaveChanges();
                    }
                    else
                    {
                        System.Console.WriteLine("--> Platform already exists...");
                    }
                }
                catch(Exception e)
                {
                    System.Console.WriteLine($"--> Could not add Platform do DB {e.Message}");
                }
            }
        }
    }

    enum EventType
    {
        PlatformPublished,
        Undetermined
    }
}