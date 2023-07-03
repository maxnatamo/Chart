using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public interface IObjectFieldDescriptor
    {
        /// <summary>
        /// Set the schema type of the field.
        /// </summary>
        /// <returns>The current descriptor, to allow for method chaining.</returns>
        /// <exception cref="Exception">Thrown if the type is not assignable from <see cref="IGraphValue" />.</exception>
        IObjectFieldDescriptor Type(Type fieldType);

        /// <inheritdoc cref="IObjectFieldDescriptor.Type(Type)" />
        /// <typeparam name="T">The new schema type for the field.</typeparam>
        IObjectFieldDescriptor Type<T>() where T : IGraphValue;

        /// <summary>
        /// Set the name of the field in the schema.
        /// </summary>
        /// <param name="name">The new name for the field.</param>
        /// <returns>The current descriptor, to allow for method chaining.</returns>
        IObjectFieldDescriptor Name(string name);

        /// <summary>
        /// Set an optional description of the field in the schema.
        /// Can be set to <c>null</c> to remove the description.
        /// </summary>
        /// <param name="description">The new description for the field.</param>
        /// <returns>The current descriptor, to allow for method chaining.</returns>
        IObjectFieldDescriptor Description(string? description);

        /// <summary>
        /// Set an explicit resolver onto the field.
        /// </summary>
        /// <param name="resolverDelegate">Delegate for selecting the field value.</param>
        /// <returns>The current descriptor, to allow for method chaining.</returns>
        IObjectFieldDescriptor Resolve(FieldResolverDelegate resolverDelegate);
    }

    public class ObjectFieldDescriptor : IObjectFieldDescriptor
    {
        /// <summary>
        /// Common context for descriptors.
        /// </summary>
        private readonly DescriptorContext _descriptorContext;

        /// <summary>
        /// The name of the field in the schema.
        /// </summary>
        protected internal string fieldName { get; set; } = string.Empty;

        /// <summary>
        /// An optional description of the field in the schema.
        /// </summary>
        protected internal string? description { get; set; } = null;

        /// <summary>
        /// The type of the field in the schema.
        /// </summary>
        protected internal Type? schemaType { get; set; } = null;

        /// <summary>
        /// Delegate for resolving the value of the field.
        /// </summary>
        protected internal FieldResolverDelegate? resolver { get; set; } = null;

        internal ObjectFieldDescriptor(DescriptorContext descriptorContext)
            => this._descriptorContext = descriptorContext;

        public IObjectFieldDescriptor Type(Type type)
        {
            if(!type.IsAssignableTo(typeof(IGraphValue)))
            {
                throw new Exception($"Type is not assignable to IGraphValue: {type.FullName}");
            }

            this.schemaType = type;
            return this;
        }

        public IObjectFieldDescriptor Type<T>() where T : IGraphValue
            => this.Type(typeof(T));

        public IObjectFieldDescriptor Name(string name)
        {
            this.fieldName = name;
            return this;
        }

        public IObjectFieldDescriptor Description(string? description)
        {
            this.description = description;
            return this;
        }

        public IObjectFieldDescriptor Resolve(FieldResolverDelegate resolver)
        {
            this.resolver = resolver;
            return this;
        }
    }
}