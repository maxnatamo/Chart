namespace Chart.Core
{
    public class Operations
    {
        public static string Query => nameof(Query);
        public static string Mutation => nameof(Mutation);
        public static string Subscription => nameof(Subscription);

        private static readonly List<string> _defaultOperations = new()
        {
            Query,
            Mutation,
            Subscription
        };

        public static bool IsDefaultOperation(string name, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            foreach(string operation in Operations._defaultOperations)
            {
                if(operation.Equals(name, comparison))
                {
                    return true;
                }
            }

            return false;
        }
    }
}