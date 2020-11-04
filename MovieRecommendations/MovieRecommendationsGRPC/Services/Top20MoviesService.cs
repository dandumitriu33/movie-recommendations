using Grpc.Core;
using Microsoft.Extensions.Logging;
using MoviesDataAccessLibrary.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoviesDataAccessLibrary.Repositories;

namespace MovieRecommendationsGRPC.Services
{
    public class Top20MoviesService : Top20Movies.Top20MoviesBase
    {
        private readonly ILogger<Top20MoviesService> _logger;
        private readonly IRepository _repository;

        public Top20MoviesService(ILogger<Top20MoviesService> logger,
                                  IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public override Task<Top20MoviesGRPCModel> GetTop20Movies(Top20MoviesGRPCLookupModel request, ServerCallContext context)
        {
            Top20MoviesGRPCModel output = new Top20MoviesGRPCModel();

            IEnumerable<Movie> top20MoviesFromDb = _repository.GetAllMoviesTop20();
            output.Json = JsonSerializer.Serialize(top20MoviesFromDb);

            return Task.FromResult(output);
        }
    }
}
