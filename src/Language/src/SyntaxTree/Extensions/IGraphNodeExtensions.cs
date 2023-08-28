namespace Chart.Language.SyntaxTree
{
    public static class IGraphNodeExtensions
    {
        public static bool IsCompositeType(this IGraphNode node) =>
            node switch
            {
                GraphObjectType => true,
                GraphInterfaceDefinition => true,
                GraphUnionDefinition => true,

                _ => false
            };
    }
}