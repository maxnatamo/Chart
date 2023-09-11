using Chart.Language;
using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public static class TypeReference
    {
        private static readonly SchemaParser _schemaParser = new();

        public static GraphType Parse(string type)
            => TypeReference._schemaParser.ParseString(type, parser => parser.ParseType());

        /// <summary>
        /// Translate the given type into an instance of <see cref="GraphType" />.
        /// </summary>
        public static GraphType Parse(Type type)
        {
            bool isNonNullType = type.IsAssignableTo(typeof(NonNullType));
            bool isListType = type.IsAssignableTo(typeof(ListType));

            if(isNonNullType || isListType)
            {
                Type innerType = type.GetGenericArguments().First();
                GraphType innerGraphType = TypeReference.Parse(innerType);

                if(isNonNullType)
                {
                    innerGraphType.NonNullable = true;
                    return innerGraphType;
                }

                if(isListType)
                {
                    return new GraphListType(innerGraphType);
                }
            }

            return new GraphNamedType(type.Name)
            {
                NonNullable = isNonNullType
            };
        }
    }
}