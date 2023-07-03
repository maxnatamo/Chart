using System.Linq.Expressions;

namespace Chart.Core
{
    public interface IObjectTypeDescriptor<TObject> : IObjectTypeDescriptor where TObject : class
    {
        /// <inheritdoc cref="IObjectTypeDescriptor.UseBindingBehavour(ObjectBindingBehaviour)" />
        new ObjectTypeDescriptor<TObject> UseBindingBehavour(ObjectBindingBehaviour behaviour);

        /// <inheritdoc cref="IObjectTypeDescriptor.UseImplicitBinding" />
        new ObjectTypeDescriptor<TObject> UseImplicitBinding();

        /// <inheritdoc cref="IObjectTypeDescriptor.UseExplicitBinding" />
        new ObjectTypeDescriptor<TObject> UseExplicitBinding();

        /// <inheritdoc cref="IObjectTypeDescriptor.Name(string)" />
        new ObjectTypeDescriptor<TObject> Name(string name);

        /// <inheritdoc cref="IObjectTypeDescriptor.Field(string)" />
        new IObjectFieldDescriptor Field(string name);

        /// <inheritdoc cref="IObjectTypeDescriptor.Field{TObject, TRuntime}(Expression{Func{TObject, TRuntime}})" />
        IObjectFieldDescriptor Field<TRuntime>(Expression<Func<TObject, TRuntime>> fieldSelector);

        /// <inheritdoc cref="IObjectTypeDescriptor.Directive(Type)" />
        new ObjectTypeDescriptor<TObject> Directive(Type directiveType);

        /// <inheritdoc cref="IObjectTypeDescriptor.Directive{TDirective}()" />
        new ObjectTypeDescriptor<TObject> Directive<TDirective>() where TDirective : DirectiveType;
    }

    public class ObjectTypeDescriptor<TObject> : ObjectTypeDescriptor  where TObject : class
    {
        private readonly IObjectFieldDescriptorFactory _fieldDescriptorFactory;
        private readonly INameFormatter _nameFormatter;

        protected internal ObjectTypeDescriptor(
            IObjectFieldDescriptorFactory fieldDescriptorFactory,
            INameFormatter nameFormatter)
            : base(fieldDescriptorFactory, nameFormatter)
        {
            this._fieldDescriptorFactory = fieldDescriptorFactory;
            this._nameFormatter = nameFormatter;

            this.objectName = this._nameFormatter.FormatName(nameof(TObject));
        }

        public new ObjectTypeDescriptor<TObject> UseBindingBehavour(ObjectBindingBehaviour behaviour)
        {
            base.UseBindingBehavour(behaviour);
            return this;
        }

        public new ObjectTypeDescriptor<TObject> UseImplicitBinding()
            => this.UseBindingBehavour(ObjectBindingBehaviour.Implicit);

        public new ObjectTypeDescriptor<TObject> UseExplicitBinding()
            => this.UseBindingBehavour(ObjectBindingBehaviour.Explicit);

        public new ObjectTypeDescriptor<TObject> Name(string name)
        {
            base.Name(name);
            return this;
        }

        public new IObjectFieldDescriptor Field(string name)
            => base.Field(name);

        public IObjectFieldDescriptor Field<TRuntime>(Expression<Func<TObject, TRuntime>> fieldSelector)
            => base.Field(fieldSelector);

        public new ObjectTypeDescriptor<TObject> Directive(Type directiveType)
        {
            if(directiveType is null)
            {
                throw new ArgumentNullException(nameof(directiveType));
            }

            if(!directiveType.IsAssignableTo(typeof(DirectiveType)))
            {
                throw new ArgumentException($"Directive type is not derived from {nameof(DirectiveType)}.");
            }

            return this;
        }

        public new ObjectTypeDescriptor<TObject> Directive<TDirective>() where TDirective : DirectiveType
        {
            return this;
        }
    }
}