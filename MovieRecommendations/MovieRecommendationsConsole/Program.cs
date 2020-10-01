using Grpc.Net.Client;
using MovieRecommendationsGRPC;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieRecommendationsConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            #region gRPC
            //var input = new HelloRequest { Name = "John" };

            //var channel = GrpcChannel.ForAddress("https://localhost:5001");
            //// it's acting like I'm instantiating the object but 
            //// we are instantiating the client which talks to the server
            //var client = new Greeter.GreeterClient(channel);

            //var reply = await client.SayHelloAsync(input);

            //Console.WriteLine(reply.Message);

            Console.WriteLine("Press any key to start the test.");
            Console.ReadKey();

            long[] gRPCResults = new long[10];
            for (int i = 0; i < 10; i++)
            {
                var stopwatchGRPC = new System.Diagnostics.Stopwatch();
                stopwatchGRPC.Start();

                var channel = GrpcChannel.ForAddress("https://localhost:5001");
                var top20MoviesClient = new Top20Movies.Top20MoviesClient(channel);

                var top20Variable = new Top20MoviesGRPCLookupModel { };

                var reply = await top20MoviesClient.GetTop20MoviesAsync(top20Variable);

                stopwatchGRPC.Stop();
                gRPCResults[i] = stopwatchGRPC.ElapsedMilliseconds;
                Console.WriteLine($"gRPC fetch time of top 20 movies: {stopwatchGRPC.ElapsedMilliseconds} ms");
            }
            Console.WriteLine($"Average gRPC fetch time: {gRPCResults.Average()}");

            // Uncomment to see the complete string with the top 20 movies
            // Console.WriteLine($"{reply.Json}");
            #endregion

            #region API
            string apiResponse = "";
            long[] apiResults = new long[10];
            for (int i = 0; i < 10; i++)
            {
                var stopwatchAPI = new System.Diagnostics.Stopwatch();
                stopwatchAPI.Start();

                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("https://localhost:44311/api/top20"))
                    {
                        apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }

                stopwatchAPI.Stop();
                apiResults[i] = stopwatchAPI.ElapsedMilliseconds;
                Console.WriteLine($"API fetch time of top 20 movies: {stopwatchAPI.ElapsedMilliseconds} ms");
            }
            Console.WriteLine($"Average API fetch time: {apiResults.Average()}");

            // Uncomment to see the complete string with the top 20 movies
            // Console.WriteLine(apiResponse);
            #endregion


            Console.ReadLine();
        }
    }
}
