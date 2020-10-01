using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRecommendationsGRPC.Services
{
    public class Top20MoviesService : Top20Movies.Top20MoviesBase
    {
        private readonly ILogger<Top20MoviesService> _logger;

        public Top20MoviesService(ILogger<Top20MoviesService> logger)
        {
            _logger = logger;
        }

        public override Task<Top20MoviesGRPCModel> GetTop20Movies(Top20MoviesGRPCLookupModel request, ServerCallContext context)
        {
            Top20MoviesGRPCModel output = new Top20MoviesGRPCModel();

            if (request.UserId == 1)
            {
                output.FirstName = "Jamie";
                output.LastName = "Smith";
            }
            else if (request.UserId == 2)
            {
                output.FirstName = "Jane";
                output.LastName = "Doe";
            }
            else
            {
                output.FirstName = "Bilbo";
                output.LastName = "Baggins";
            }

            return Task.FromResult(output);
        }
    }
}
