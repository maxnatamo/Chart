using Chart.Core;

using Microsoft.Extensions.DependencyInjection;

namespace Chart.Examples.Services
{
    public class Query
    {
        public string GetUsername() => "maxnatamo";
    }

    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Service container
            RequestServiceBuilder service = new ServiceCollection()
                .AddChart();

            // Execute request
            ExecutionResult result =
                await Schema
                    .Create(service, c => c.AddType<Query>())
                    .MakeExecutable()
                    .ExecuteAsync("{ username }");

            // Prints 'maxnatamo'
            Console.WriteLine("Username: {0}", result.Data["username"]);
        }
    }
}