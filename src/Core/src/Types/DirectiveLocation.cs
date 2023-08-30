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

        public static GraphDirectiveLocationFlags[] SplitLocation(GraphDirectiveLocationFlags location)
        {
            List<GraphDirectiveLocationFlags> locations = new();
            foreach(GraphDirectiveLocationFlags locationFlag in Enum.GetValues<GraphDirectiveLocationFlags>())
            {
                if(location.HasFlag(locationFlag))
                {
                    locations.Add(locationFlag);
                }
            }

            return locations.ToArray();
        }

        public static GraphDirectiveLocationFlags GetLocation<TNode>(TNode node) =>
            node switch
            {
                // Executable
                GraphQueryOperation => GraphDirectiveLocationFlags.QUERY,
                GraphMutationOperation => GraphDirectiveLocationFlags.MUTATION,
                GraphSubscriptionOperation => GraphDirectiveLocationFlags.SUBSCRIPTION,
                GraphFieldSelection => GraphDirectiveLocationFlags.FIELD,
                GraphFragmentDefinition => GraphDirectiveLocationFlags.FRAGMENT_DEFINITION,
                GraphFragmentSelection => GraphDirectiveLocationFlags.FRAGMENT_SPREAD,
                GraphInlineFragmentSelection => GraphDirectiveLocationFlags.INLINE_FRAGMENT,
                GraphVariable => GraphDirectiveLocationFlags.VARIABLE_DEFINITION,

                // Non-executable
                GraphSchemaDefinition => GraphDirectiveLocationFlags.SCHEMA,
                GraphSchemaExtension => GraphDirectiveLocationFlags.SCHEMA,
                GraphScalarType => GraphDirectiveLocationFlags.SCALAR,
                GraphScalarExtension => GraphDirectiveLocationFlags.SCALAR,
                GraphObjectType => GraphDirectiveLocationFlags.OBJECT,
                GraphObjectExtension => GraphDirectiveLocationFlags.OBJECT,
                GraphField => GraphDirectiveLocationFlags.FIELD_DEFINITION,
                GraphArgumentDefinition => GraphDirectiveLocationFlags.ARGUMENT_DEFINITION,
                GraphInterfaceDefinition => GraphDirectiveLocationFlags.INTERFACE,
                GraphInterfaceExtension => GraphDirectiveLocationFlags.INTERFACE,
                GraphUnionDefinition => GraphDirectiveLocationFlags.UNION,
                GraphUnionExtension => GraphDirectiveLocationFlags.UNION,
                GraphEnumDefinition => GraphDirectiveLocationFlags.ENUM,
                GraphEnumExtension => GraphDirectiveLocationFlags.ENUM,
                GraphEnumDefinitionValue => GraphDirectiveLocationFlags.ENUM_VALUE,
                GraphInputDefinition => GraphDirectiveLocationFlags.INPUT_OBJECT,
                GraphInputExtension => GraphDirectiveLocationFlags.INPUT_OBJECT,
                GraphInputFieldsDefinition => GraphDirectiveLocationFlags.INPUT_FIELD_DEFINITION,

                _ => throw new NotSupportedException()
            };
    }
}