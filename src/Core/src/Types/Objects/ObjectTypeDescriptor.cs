using System.Linq.Expressions;

namespace Chart.Core
{
    public interface IObjectTypeDescriptor
    {
        /// <summary>
        /// Set the behaviour to follow when binding fields onto objects, inside of the current descriptor.
        /// </summary>
        /// <param name="behaviour">The new binding behaviour.</param>
        /// <returns>The current descriptor, to allow for method chaining.</returns>
        ObjectTypeDescriptor UseBindingBehavour(ObjectBindingBehaviour behaviour);

        /// <summary>
        /// Use implicit binding behaviour, inside of the current descriptor.
        /// </summary>
        /// <inheritdoc cref="ObjectTypeDescriptor.UseBindingBehavour(ObjectBindingBehaviour)" />
        ObjectTypeDescriptor UseImplicitBinding();

        /// <summary>
        /// Use explicit binding behaviour, inside of the current descriptor.
        /// </summary>
        /// <inheritdoc cref="ObjectTypeDescriptor.UseBindingBehavour(ObjectBindingBehaviour)" />
        ObjectTypeDescriptor UseExplicitBinding();

        /// <summary>
        /// Explicitly set the name of the object in the schema.
        /// </summary>
        /// <param name="name">The new name for the object.</param>
        /// <returns>The current descriptor, to allow for method chaining.</returns>
        ObjectTypeDescriptor Name(string name);

        /// <summary>
        /// Append a new field to the type, with the specified name.
        /// </summary>
        /// <remarks>
        /// If the name exists on the object type, the type and resolver will be inferred.
        /// </remarks>
        /// <param name="name">The name of the field in the schema.</param>
        /// <returns>The new field descriptor, to allow for method chaining.</returns>
        IObjectFieldDescriptor Field(string name);

        /// <summary>
        /// Append a new field to the type, with the specified selector.
        /// </summary>
        /// <remarks>
        /// The type and resolver will be inferred.
        /// </remarks>
        /// <param name="fieldSelector">Selector for selecting the runtime object field.</param>
        /// <returns>The new field descriptor, to allow for method chaining.</returns>
        IObjectFieldDescriptor Field<TObject, TRuntime>(Expression<Func<TObject, TRuntime>> fieldSelector);

        /// <summary>
        /// Append a new directive onto the object type.
        /// </summary>
        /// <param name="directiveType">The directive type to append.</param>
        /// <returns>The current descriptor, to allow for method chaining.</returns>
        ObjectTypeDescriptor Directive(Type directiveType);

        /// <summary>
        /// Append a new directive onto the object type.
        /// </summary>
        /// <typeparam name="TDirective">The directive type to append.</typeparam>
        /// <returns>The current descriptor, to allow for method chaining.</returns>
        ObjectTypeDescriptor Directive<TDirective>() where TDirective : DirectiveType;
    }

    public class ObjectTypeDescriptor : IObjectTypeDescriptor
    {
        private readonly IObjectFieldDescriptorFactory _fieldDescriptorFactory;
        private readonly INameFormatter _nameFormatter;

        /// <summary>
        /// Which behaviour to follow when bindings fields onto objects.
        /// </summary>
        protected ObjectBindingBehaviour bindingBehaviour { get; set; }

        /// <summary>
        /// The name of the object in the schema.
        /// </summary>
        internal string objectName { get; set; } = string.Empty;

        /// <summary>
        /// List of fields on the object type.
        /// </summary>
        internal readonly List<ObjectFieldDescriptor> _fields = new();

        protected internal ObjectTypeDescriptor(
            IObjectFieldDescriptorFactory fieldDescriptorFactory,
            INameFormatter nameFormatter)
        {
            this._fieldDescriptorFactory = fieldDescriptorFactory;
            this._nameFormatter = nameFormatter;
        }

        public ObjectTypeDescriptor UseBindingBehavour(ObjectBindingBehaviour behaviour)
        {
            this.bindingBehaviour = behaviour;
            return this;
        }

        public ObjectTypeDescriptor UseImplicitBinding()
            => this.UseBindingBehavour(ObjectBindingBehaviour.Implicit);

        public ObjectTypeDescriptor UseExplicitBinding()
            => this.UseBindingBehavour(ObjectBindingBehaviour.Explicit);

        public ObjectTypeDescriptor Name(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            this.objectName = name;
            return this;
        }

        public IObjectFieldDescriptor Field(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            IObjectFieldDescriptor? _fieldDescriptor = this._fields.Find(f => f.fieldName.Equals(name, StringComparison.Ordinal));
            if(_fieldDescriptor is not null)
            {
                return _fieldDescriptor;
            }

            ObjectFieldDescriptor fieldDescriptor = this._fieldDescriptorFactory.CreateDescriptor(name);
            this._fields.Add(fieldDescriptor);

            return fieldDescriptor;
        }

        public IObjectFieldDescriptor Field<TObject, TRuntime>(Expression<Func<TObject, TRuntime>> fieldSelector)
        {
            ObjectFieldDescriptor fieldDescriptor = this._fieldDescriptorFactory.CreateDescriptor<TObject, TRuntime>(fieldSelector);

            IObjectFieldDescriptor? _fieldDescriptor = this._fields.Find(f => f.fieldName.Equals(fieldDescriptor.fieldName, StringComparison.Ordinal));
            if(_fieldDescriptor is not null)
            {
                return _fieldDescriptor;
            }

            this._fields.Add(fieldDescriptor);

            return fieldDescriptor;
        }

        public ObjectTypeDescriptor Directive(Type directiveType)
        {
            return this;
        }

        public ObjectTypeDescriptor Directive<TDirective>() where TDirective : DirectiveType
        {
            return this;
        }
    }
}