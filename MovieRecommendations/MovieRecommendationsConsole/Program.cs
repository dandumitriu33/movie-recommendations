using Grpc.Net.Client;
using MovieRecommendationsGRPC;
using System;
using System.Threading.Tasks;

namespace MovieRecommendationsConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var input = new HelloRequest { Name = "John" };

            //var channel = GrpcChannel.ForAddress("https://localhost:5001");
            //// it's acting like I'm instantiating the object but 
            //// we are instantiating the client which talks to the server
            //var client = new Greeter.GreeterClient(channel);

            //var reply = await client.SayHelloAsync(input);

            //Console.WriteLine(reply.Message);

            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var top20MoviesClient = new Top20Movies.Top20MoviesClient(channel);

            var top20Variable = new Top20MoviesGRPCLookupModel { };

            var reply = await top20MoviesClient.GetTop20MoviesAsync(top20Variable);

            Console.WriteLine($"{reply.Json}");

            Console.ReadLine();
        }
    }
}
