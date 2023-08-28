using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public static partial class GraphObjectTypeExtensions
    {
        public static GraphNamedType GetInnerType(this GraphType type) =>
            type switch
            {
                GraphListType _type => _type.UnderlyingType.GetInnerType(),
                GraphNamedType _type => _type,

                _ => throw new NotSupportedException()
            };

        /// <summary>
        /// Get the type definition from the object field.
        /// </summary>
        /// <param name="type">The object type to retrieve the field from.</param>
        /// <param name="fieldName">The name of the field.</param>
        /// <param name="context">The current execution context.</param>
        public static GraphDefinition? GetObjectFieldType(
            this GraphDefinition type,
            string fieldName,
            QueryExecutionContext context)
        {
            if(type is GraphUnionDefinition unionType)
            {
                return unionType.GetObjectFieldType(fieldName, context);
            }

            GraphNamedType? fieldType = type switch
            {
                GraphInterfaceDefinition _type => _type.GetFieldType(fieldName),
                GraphObjectType _type => _type.GetFieldType(fieldName),

                _ => null
            };

            if(fieldType is null)
            {
                context.RaiseRequestError(DefaultErrors.FieldNotFound(type.Name, fieldName));
                return null;
            }

            if(ScalarTypes.IsDefaultScalar(fieldType.Name))
            {
                return new GraphScalarType()
                {
                    Name = new GraphName(fieldType.Name)
                };
            }

            if(!context.Schema.TryGetDefinition(fieldType.Name, out GraphDefinition? selectionTypeDefinition))
            {
                context.RaiseRequestError(DefaultErrors.TypeNotFound(fieldType.Name));
                return null;
            }

            return selectionTypeDefinition;
        }

        /// <summary>
        /// Get the type definition from the object field.
        /// </summary>
        /// <param name="type">The object type to retrieve the field from.</param>
        /// <param name="fieldName">The name of the field.</param>
        /// <param name="context">The current execution context.</param>
        public static GraphDefinition? GetObjectFieldType(
            this GraphUnionDefinition type,
            string fieldName,
            QueryExecutionContext context)
        {
            if(type.Members is null)
            {
                return null;
            }

            foreach(GraphName member in type.Members.Members)
            {
                if(!context.Schema.TryGetDefinition<GraphDefinition>(member, out GraphDefinition? memberType))
                {
                    context.RaiseRequestError(DefaultErrors.TypeNotFound(member));
                    return null;
                }

                GraphDefinition? field = memberType.GetObjectFieldType(fieldName, context);
                if(field is not null)
                {
                    return field;
                }
            }

            return null;
        }
    }
}