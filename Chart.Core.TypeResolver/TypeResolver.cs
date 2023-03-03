using System.Collections;
using System.Reflection;

using Chart.Models.AST;
using Chart.Shared.Exceptions;
using Chart.Shared.Extensions;

namespace Chart.Core.TypeResolver
{
    public partial class Resolver
    {
        private readonly ObjectType ObjectParser;

        private readonly Dictionary<Type, GraphBaseType> DefinedTypes;

        public Resolver()
        {
            this.ObjectParser = new ObjectType();

            this.DefinedTypes = new Dictionary<Type, GraphBaseType>
            {
                { typeof(Boolean), new BooleanScalarType() },
                { typeof(Single), new FloatScalarType() },
                { typeof(Double), new FloatScalarType() },
                { typeof(Decimal), new FloatScalarType() },
                { typeof(SByte), new IntScalarType() },
                { typeof(Int16), new IntScalarType() },
                { typeof(Int32), new IntScalarType() },
                { typeof(Int64), new IntScalarType() },
                { typeof(Int128), new IntScalarType() },
                { typeof(Byte), new IntScalarType()},
                { typeof(UInt16), new IntScalarType() },
                { typeof(UInt32), new IntScalarType() },
                { typeof(UInt64), new IntScalarType() },
                { typeof(UInt128), new IntScalarType() },
                { typeof(String), new StringScalarType() },
            };
        }

        /// <summary>
        /// Register a type for the resolver.
        /// </summary>
        /// <typeparam name="T">The type to register.</typeparam>
        public void RegisterType<T>()
            => this.RegisterType(typeof(T));

        /// <summary>
        /// Register a type for the resolver.
        /// </summary>
        /// <param name="type">The type to register.</param>
        public void RegisterType(Type type)
        {
            // Transform Enumerables to their underlying type
            if(type.IsEnumerable() && type.GetGenericArguments().Length > 0)
            {
                type = type.GetGenericArguments().First();
            }

            // Ignore if the type already.
            if(this.DefinedTypes.ContainsKey(type))
            {
                return;
            }

            this.RegisterObjectType(type);
        }

        /// <summary>
        /// Register an object type for the resolver and recurse through the object.
        /// </summary>
        /// <param name="type">The object type to parse and register.</param>
        private void RegisterObjectType(Type type)
        {
            this.DefinedTypes.Add(type, this.ObjectParser);
    
            // Properties
            var propertyInfos = type.GetProperties();
            for(int i = 0; i < propertyInfos.Count(); i++)
            {
                this.ParseObjectProperty(propertyInfos[i]);
            }

            // Fields
            var fieldInfos = type.GetFields();
            for(int i = 0; i < fieldInfos.Count(); i++)
            {
                this.ParseObjectField(fieldInfos[i]);
            }

            // Methods
            var methodInfos = type.GetMethods(
                BindingFlags.DeclaredOnly |
                BindingFlags.Instance |
                BindingFlags.Public
            );
    
            for(int i = 0; i < methodInfos.Count(); i++)
            {
                if(methodInfos[i].IsSpecialName)
                {
                    continue;
                }

                this.ParseObjectMethod(methodInfos[i]);
            }
        }

        /// <summary>
        /// Resolve a Type into a GraphType-object.
        /// </summary>
        /// <param name="type">The type to resolve a GraphType.</param>
        /// <returns>The parsed GraphType-object.</returns>
        /// <exception cref="InvalidTypeException">Thrown if a type is not registered or is invalid.</exception>
        public GraphType ResolveType(Type type)
        {
            // Handle list types.
            if(type.IsEnumerable() && type.GetGenericArguments().Length > 0)
            {
                var underlyingType = type.GetGenericArguments().First();
                return new GraphListType(this.ResolveType(underlyingType));
            }

            if(this.DefinedTypes.ContainsKey(type))
            {
                return new GraphNamedType(type.Name);
            }

            throw new InvalidTypeException(type.Name);
        }

        /// <summary>
        /// Resolve an object into a GraphValue-object.
        /// </summary>
        /// <param name="obj">The object to resolve a GraphValue.</param>
        /// <returns>The parsed GraphValue-object.</returns>
        /// <exception cref="InvalidTypeException">Thrown if a type is not registered or is invalid.</exception>
        public GraphValue ResolveValue(object? obj)
        {
            if(obj == null)
            {
                return new GraphNullValue();
            }

            Type type = obj.GetType();

            // Handle list types.
            if(type.IsEnumerable() && type.GetGenericArguments().Length > 0)
            {
                IEnumerable list = (IEnumerable) obj;
                GraphListValue listValue = new GraphListValue();

                foreach(var elem in list)
                {
                    var value = this.ResolveValue(elem);
                    listValue.Values.Add(value);
                }

                return listValue;
            }

            foreach(var def in this.DefinedTypes)
            {
                if(def.Key != type)
                {
                    continue;
                }

                return def.Value.ParseLiteral(obj);
            }

            throw new InvalidTypeException(type.Name);
        }
    }
}