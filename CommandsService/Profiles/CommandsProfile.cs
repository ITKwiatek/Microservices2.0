using AutoMapper;
using CommandsService.Dtos;
using CommandsService.Models;

namespace CommandsService.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            //Source -> Target

            //Command
            CreateMap<Command, CommandReadDto>();
            CreateMap<CommandCreateDto, Command>();

            //Platform
            CreateMap<Platform, PlatformReadDto>();
        }
    }
}