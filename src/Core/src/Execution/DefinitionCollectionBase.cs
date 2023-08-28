using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public abstract class DefinitionCollectionBase
    {
        /// <summary>
        /// Collection of all definitions in the query.
        /// </summary>
        protected internal List<GraphDefinition> Definitions { get; }

        public DefinitionCollectionBase()
        {
            this.Definitions = new List<GraphDefinition>();
        }

        /// <summary>
        /// Get all definitions from the collection
        /// </summary>
        public ReadOnlyCollection<GraphDefinition> GetAllDefinitions()
            => this.Definitions.AsReadOnly();

        /// <summary>
        /// Get all definitions with the given name.
        /// </summary>
        /// <param name="name">An optional name filter for the definitions.</param>
        public IEnumerable<GraphDefinition> GetDefinitions(string? name = null)
        {
            foreach(GraphDefinition definition in this.Definitions)
            {
                if(name is not null)
                {
                    if(definition.Name is null || definition.Name.Value != name)
                    {
                        continue;
                    }
                }

                yield return definition;
            }
        }

        /// <summary>
        /// Get all definitions of a specific type from the collection.
        /// </summary>
        /// <param name="name">An optional name filter, for the definitions.</param>
        /// <typeparam name="TDefinition">The type of definitions to resolve.</typeparam>
        public IEnumerable<TDefinition> GetDefinitions<TDefinition>(string? name = null)
            where TDefinition : GraphDefinition =>
            this.GetDefinitions(name)
                .Where(v => v is TDefinition)
                .Cast<TDefinition>();

        /// <summary>
        /// Get a definition of a specific type from the collection.
        /// </summary>
        /// <param name="name">An optional name filter, for the definitions.</param>
        /// <typeparam name="TDefinition">The type of definitions to resolve.</typeparam>
        /// <exception cref="DefinitionNotFoundException">Thrown if no matching definition was found.</exception>
        public TDefinition GetDefinition<TDefinition>(string? name = null)
            where TDefinition : GraphDefinition
        {
            if(!this.TryGetDefinition(name, out TDefinition? graphDefinition))
            {
                throw new DefinitionNotFoundException(typeof(TDefinition).Name);
            }

            return graphDefinition;
        }

        /// <summary>
        /// Try to resolve a definition from the collection.
        /// </summary>
        /// <param name="name">An optional name filter, for the definition.</param>
        /// <param name="graphDefinition">If the method returns <see langword="true" />, contains the found definition. Otherwise, <see langword="null" />.</param>
        /// <typeparam name="TDefinition">The type of definition to resolve.</typeparam>
        public bool TryGetDefinition<TDefinition>(string? name, [NotNullWhen(true)] out TDefinition? graphDefinition)
            where TDefinition : GraphDefinition
        {
            graphDefinition = this.GetDefinitions<TDefinition>(name).FirstOrDefault();
            return graphDefinition is not null;
        }

        /// <summary>
        /// Get all composite type definitions (object, interface, union) from the collection.
        /// </summary>
        /// <param name="name">An optional name filter, for the definitions.</param>
        public IEnumerable<GraphDefinition> GetCompositeTypes(string? name = null) =>
            this.GetDefinitions(name)
                .Where(v => v.IsCompositeType());
    }
}