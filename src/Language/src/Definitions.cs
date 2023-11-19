using System.Collections.ObjectModel;

using Chart.Language.SyntaxTree;

namespace Chart.Language
{
    internal static class Definitions
    {
        public static readonly ReadOnlyCollection<Type> ExecutableTypes = new(new List<Type>()
        {
            typeof(GraphQueryOperation),
            typeof(GraphMutationOperation),
            typeof(GraphSubscriptionOperation),
            typeof(GraphFragmentDefinition),
        });

        public static bool IsDefinitionExecutable(Type definitionType)
            => Definitions.ExecutableTypes.Contains(definitionType);

        public static bool IsDefinitionExecutable<TDefinition>()
            where TDefinition : GraphDefinition
            => Definitions.IsDefinitionExecutable(typeof(TDefinition));
    }
}