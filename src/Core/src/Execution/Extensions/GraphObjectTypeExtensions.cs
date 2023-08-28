using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public static partial class GraphObjectTypeExtensions
    {
        public static GraphNamedType? GetFieldType(this GraphObjectType type, string fieldName)
        {
            if(type.Fields is null)
            {
                return null;
            }

            GraphType? fieldType = type.Fields.Fields.FirstOrDefault(v => v.Name == fieldName)?.Type;
            if(fieldType is null)
            {
                return null;
            }

            return fieldType.GetInnerType();
        }

        public static GraphNamedType? GetFieldType(this GraphInterfaceDefinition type, string fieldName)
        {
            if(type.Fields is null)
            {
                return null;
            }

            GraphType? fieldType = type.Fields.Fields.FirstOrDefault(v => v.Name == fieldName)?.Type;
            if(fieldType is null)
            {
                return null;
            }

            return fieldType.GetInnerType();
        }

        public static GraphNamedType? GetFieldType(this GraphUnionDefinition type, string fieldName)
        {
            if(type.Members is null)
            {
                return null;
            }

            GraphName? fieldTypeName = type.Members.Members.FirstOrDefault(v => v.Value == fieldName);
            if(fieldTypeName is null)
            {
                foreach(GraphName n in type.Members.Members)
                {
                }
                return null;
            }

            return new GraphNamedType(fieldTypeName);
        }

        public static bool IsDerivedFrom(this GraphDefinition type, GraphDefinition from) =>
            (type, from) switch
            {
                (GraphInterfaceDefinition _type, GraphInterfaceDefinition _from)
                    => _type.Interface?.Implements.Any(v => v.Name == _from.Name) ?? false,

                (GraphInterfaceDefinition _type, GraphObjectType _from)
                    => _from.Interface?.Implements.Any(v => v.Name == _type.Name) ?? false,

                (GraphObjectType _type, GraphUnionDefinition _from)
                    => _from.Members?.Members.Any(v => v.Value == _type.Name) ?? false,

                (GraphObjectType _type, GraphObjectType _from)
                    => _from.Name == _type.Name,

                _ => false
            };
    }
}