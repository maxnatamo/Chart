using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public static class DirectiveLocations
    {
        public static readonly List<string> ExecutableDirectiveLocations = new()
        {
            "QUERY",
            "MUTATION",
            "SUBSCRIPTION",
            "FIELD",
            "FRAGMENT_DEFINITION",
            "FRAGMENT_SPREAD",
            "INLINE_FRAGMENT",
            "VARIABLE_DEFINITION",
        };

        public static readonly List<string> TypeSystemDirectiveLocations = new()
        {
            "SCHEMA",
            "SCALAR",
            "OBJECT",
            "FIELD_DEFINITION",
            "ARGUMENT_DEFINITION",
            "INTERFACE",
            "UNION",
            "ENUM",
            "ENUM_VALUE",
            "INPUT_OBJECT",
            "INPUT_FIELD_DEFINITION",
        };

        public static bool IsLocationValid(string location)
            => ExecutableDirectiveLocations.Contains(location) || TypeSystemDirectiveLocations.Contains(location);

        public static GraphDirectiveLocation[] SplitLocation(GraphDirectiveLocation location)
        {
            List<GraphDirectiveLocation> locations = new();
            foreach(GraphDirectiveLocation locationFlag in Enum.GetValues<GraphDirectiveLocation>())
            {
                if(location.HasFlag(locationFlag))
                {
                    locations.Add(locationFlag);
                }
            }

            return locations.ToArray();
        }

        public static GraphDirectiveLocation GetLocation<TNode>(TNode node) =>
            node switch
            {
                // Executable
                GraphQueryOperation => GraphDirectiveLocation.QUERY,
                GraphMutationOperation => GraphDirectiveLocation.MUTATION,
                GraphSubscriptionOperation => GraphDirectiveLocation.SUBSCRIPTION,
                GraphFieldSelection => GraphDirectiveLocation.FIELD,
                GraphFragmentDefinition => GraphDirectiveLocation.FRAGMENT_DEFINITION,
                GraphFragmentSelection => GraphDirectiveLocation.FRAGMENT_SPREAD,
                GraphInlineFragmentSelection => GraphDirectiveLocation.INLINE_FRAGMENT,
                GraphVariable => GraphDirectiveLocation.VARIABLE_DEFINITION,

                // Non-executable
                GraphSchemaDefinition => GraphDirectiveLocation.SCHEMA,
                GraphSchemaExtension => GraphDirectiveLocation.SCHEMA,
                GraphScalarType => GraphDirectiveLocation.SCALAR,
                GraphScalarExtension => GraphDirectiveLocation.SCALAR,
                GraphObjectType => GraphDirectiveLocation.OBJECT,
                GraphObjectExtension => GraphDirectiveLocation.OBJECT,
                GraphField => GraphDirectiveLocation.FIELD_DEFINITION,
                GraphArgumentDefinition => GraphDirectiveLocation.ARGUMENT_DEFINITION,
                GraphInterfaceDefinition => GraphDirectiveLocation.INTERFACE,
                GraphInterfaceExtension => GraphDirectiveLocation.INTERFACE,
                GraphUnionDefinition => GraphDirectiveLocation.UNION,
                GraphUnionExtension => GraphDirectiveLocation.UNION,
                GraphEnumDefinition => GraphDirectiveLocation.ENUM,
                GraphEnumExtension => GraphDirectiveLocation.ENUM,
                GraphEnumDefinitionValue => GraphDirectiveLocation.ENUM_VALUE,
                GraphInputDefinition => GraphDirectiveLocation.INPUT_OBJECT,
                GraphInputExtension => GraphDirectiveLocation.INPUT_OBJECT,
                GraphInputFieldsDefinition => GraphDirectiveLocation.INPUT_FIELD_DEFINITION,

                _ => throw new NotSupportedException()
            };
    }
}