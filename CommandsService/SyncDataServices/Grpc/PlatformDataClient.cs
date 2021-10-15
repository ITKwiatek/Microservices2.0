using System;
using System.Collections.Generic;
using AutoMapper;
using CommandsService.Models;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using PlatformService;

namespace CommandsService.SyncDataServices.Grpc
{
    public class PlatformDataClient : IPlatformDataClient
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public PlatformDataClient(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }
        public IEnumerable<Platform> ReturnAllPlatforms()
        {
            System.Console.WriteLine($"--> Calling gRPC Service {_configuration["GrpcPlatform"]}");
            var channel = GrpcChannel.ForAddress(_configuration["GrpcPlatform"]);
            var client = new GrpcPlatform.GrpcPlatformClient(channel);
            var request = new GetAllRequests();

            System.Console.WriteLine($"Got request: {request}");

            try
            {
                var reply = client.GetAllPlatforms(request);
                System.Console.WriteLine($"--> Platforms from PlatformService: {reply}");
                return _mapper.Map<IEnumerable<Platform>>(reply.Platform);
            }
            catch(Exception ex)
            {
                System.Console.WriteLine($"--> Could not call GRPC Server {ex.Message}");
                return null;
            }
        }
    }
}