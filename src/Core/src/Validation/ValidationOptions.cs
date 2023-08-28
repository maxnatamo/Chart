using Chart.Core.Execution;

namespace Chart.Core
{
    public class ValidationOptions
    {
        /// <summary>
        /// Enable to skip validation of schema and ignore unbound types. Mostly used for testing.
        /// </summary>
        public bool SkipSchemaValidation = false;

        /// <summary>
        /// Enable to add all unbound types from the schema builder to the schema. Mostly used for testing.
        /// </summary>
        public bool AddUnboundTypes = false;

        /// <summary>
        /// Which execution strategy should be used when validation a request.
        /// </summary>
        public ExecutionStrategy ValidationStrategy = ExecutionStrategy.Parallel;
    }
}