using Chart.Core;

namespace Chart.Examples.QueryExecutor
{
    public class Program
    {
        private class Query
        {
            /* NOTE: Gets renamed to 'username' */
            public string GetUsername() => "maxnatamo";
        }

        public static async Task Main(string[] args)
        {
            // Create schema
            Schema schema = Schema
                .Create(c => c.AddType<Query>());

            // Create executor
            IQueryExecutor executor = schema.MakeExecutable();

            // Create request
            QueryRequest request = new QueryRequestBuilder()
                .SetQuery("{ username }")
                .Create();

            // Execute request
            ExecutionResult result = await executor.ExecuteAsync(request);

            // Prints 'maxnatamo'
            Console.WriteLine("Username: {0}", result.Data["username"]);
        }
    }
}