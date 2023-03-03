using System.Collections;
using System.Reflection;

using Chart.Models.AST;
using Chart.Shared.Exceptions;
using Chart.Shared.Extensions;

namespace Chart.Core.TypeResolver
{
    public partial class Resolver
    {
        /// <summary>
        /// Map of all GraphQL scalar types and their respective parsers.
        /// </summary>
        private readonly Dictionary<Type, GraphBaseType> ScalarTypes;

        /// <summary>
        /// List of all visited types. This is to avoid re-checking types.
        /// </summary>
        private readonly List<Type> VisitedTypes;

        public Resolver()
        {
            this.ScalarTypes = new Dictionary<Type, GraphBaseType>
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

            // Initialize with all scalar types
            this.VisitedTypes = this.ScalarTypes.Select(v => v.Key).ToList();
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
        /// <typeparam name="T">The type to register.</typeparam>
        public void RegisterType<T>(T obj)
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
            if(this.VisitedTypes.Contains(type))
            {
                return;
            }

            this.VisitedTypes.Add(type);
            this.RegisterObjectType(type);
        }

        /// <summary>
        /// Register an object type for the resolver and recurse through the object.
        /// </summary>
        /// <param name="type">The object type to parse and register.</param>
        private void RegisterObjectType(Type type)
        {
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
        /// Whether the type has been visited.
        /// </summary>
        /// <typeparam name="T">The type to check for.</typeparam>
        /// <returns>True, if the type has been visited. Otherwise, false.</returns>
        public bool Visited<T>()
            => this.Visited(typeof(T));

        /// <summary>
        /// Whether the type has been visited.
        /// </summary>
        /// <param name="type">The type to check for.</param>
        /// <returns>True, if the type has been visited. Otherwise, false.</returns>
        public bool Visited(Type type)
            => this.VisitedTypes.Contains(type);

            /// <summary>
        /// Resolve a Type into a GraphType-object.
        /// </summary>
        /// <typeparam name="T">The type to resolve a GraphType.</typeparam>
        /// <returns>The parsed GraphType-object.</returns>
        /// <exception cref="InvalidTypeException">Thrown if a type is not registered or is invalid.</exception>
        public GraphType ResolveType<T>()
            => this.ResolveType(typeof(T));

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

            // Resolve scalar types.
            if(this.ScalarTypes.ContainsKey(type))
            {
                return new GraphNamedType(this.ScalarTypes[type].Name);
            }

            // Resolve object types.
            if(this.VisitedTypes.Contains(type))
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

            // Resolve scalar types.
            if(this.ScalarTypes.ContainsKey(type))
            {
                return this.ScalarTypes[type].ParseLiteral(obj);
            }

            // Resolve non-scalar, object types.
            if(this.VisitedTypes.Contains(type))
            {
                return this.ResolveObjectValue(obj);
            }

            throw new InvalidTypeException(type.Name);
        }

        /// <summary>
        /// Resolve an object into a GraphObjectValue-object.
        /// </summary>
        /// <param name="obj">The object to resolve a GraphObjectValue.</param>
        /// <returns>The parsed GraphObjectValue-object.</returns>
        /// <exception cref="InvalidTypeException">Thrown if a type is not registered or is invalid.</exception>
        protected GraphObjectValue ResolveObjectValue(object obj)
        {
            Type type = obj.GetType();
            GraphObjectValue def = new GraphObjectValue();

            // Properties
            var propertyInfos = type.GetProperties();
            for(int i = 0; i < propertyInfos.Count(); i++)
            {
                var name = new GraphName(propertyInfos[i].Name);
                var value = this.ResolveValue(propertyInfos[i].GetValue(obj));

                def.Fields.Add(name, value);
            }

            // Fields
            var fieldInfos = type.GetFields();
            for(int i = 0; i < fieldInfos.Count(); i++)
            {
                var name = new GraphName(fieldInfos[i].Name);
                var value = this.ResolveValue(fieldInfos[i].GetValue(obj));

                def.Fields.Add(name, value);
            }

            // Methods
            var methodInfos = type.GetMethods();
            for(int i = 0; i < methodInfos.Count(); i++)
            {
                var name = new GraphName(methodInfos[i].Name);
            }

            return def;
        }
    }
}