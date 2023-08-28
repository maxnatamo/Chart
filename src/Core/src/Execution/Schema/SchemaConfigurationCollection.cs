using System.Collections.ObjectModel;

namespace Chart.Core
{
    /// <summary>
    /// Collection of <see cref="ISchemaConfiguration" />-instances.
    /// </summary>
    public class SchemaConfigurationCollection : List<ISchemaConfiguration>
    {
        /// <inheritdoc cref="List{T}.Add(T)" />
        public void Add(SchemaConfigurationDelegate configure)
            => this.Add(new SchemaConfiguration(configure));

        /// <inheritdoc cref="List{T}.AddRange(IEnumerable{T})" />
        public void AddRange(IEnumerable<SchemaConfigurationDelegate> configures)
            => this.AddRange(configures.Select(v => new SchemaConfiguration(v)));

        /// <summary>
        /// Execute all configurations in the collection, using the given <see cref="SchemaBuilder" /> and <see cref="IServiceProvider" />.
        /// </summary>
        public void Execute(SchemaBuilder builder, IServiceProvider provider)
        {
            foreach(ISchemaConfiguration configuration in this)
            {
                configuration.Configure(builder, provider);
            }
        }
    }
}