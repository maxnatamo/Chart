using System.Linq.Expressions;
using System.Reflection;

namespace Chart.Core
{
    public interface IObjectFieldDescriptorFactory
    {
        /// <summary>
        /// Create a new <see cref="ObjectFieldDescriptor" />-instance, using the field name on the object-type.
        /// </summary>
        /// <param name="fieldName">The name of the field, in the schema.</param>
        /// <returns>The created <see cref="ObjectFieldDescriptor" />-instance.</returns>
        ObjectFieldDescriptor CreateDescriptor(string fieldName);

        /// <summary>
        /// Create a new <see cref="ObjectFieldDescriptor" />-instance, using the field name on the object-type.
        /// </summary>
        /// <remarks>
        /// This method will attempt to infer the type and resolver of the field, if found on the object-type.
        /// </remarks>
        /// <param name="fieldName">The name of the field, in the schema.</param>
        /// <typeparam name="TObject">The runtime type of object to resolve from.</typeparam>
        /// <returns>The created <see cref="ObjectFieldDescriptor" />-instance.</returns>
        ObjectFieldDescriptor CreateDescriptor<TObject>(string fieldName);

        /// <summary>
        /// Create a new <see cref="ObjectFieldDescriptor" />-instance, using the field selector on the object-type.
        /// </summary>
        /// <remarks>
        /// This method will attempt to infer the type and name of the field.
        /// </remarks>
        /// <param name="resolver">Expression for selecting a member on the type.</param>
        /// <typeparam name="TObject">The runtime type of object to resolve from.</typeparam>
        /// <typeparam name="TRuntime">The type of the selected member.</typeparam>
        /// <returns>The created <see cref="ObjectFieldDescriptor" />-instance.</returns>
        ObjectFieldDescriptor CreateDescriptor<TObject, TRuntime>(Expression<Func<TObject, TRuntime>> resolver);
    }

    public class ObjectFieldDescriptorFactory : IObjectFieldDescriptorFactory
    {
        private readonly DescriptorContext _descriptorContext;
        private readonly INameFormatter _nameFormatter;

        public ObjectFieldDescriptorFactory(DescriptorContext descriptorContext, INameFormatter nameFormatter)
        {
            this._descriptorContext = descriptorContext;
            this._nameFormatter = nameFormatter;
        }

        public ObjectFieldDescriptor CreateDescriptor(string fieldName)
        {
            ObjectFieldDescriptor descriptor = new ObjectFieldDescriptor(this._descriptorContext);
            descriptor.Name(fieldName);

            return descriptor;
        }

        public ObjectFieldDescriptor CreateDescriptor<TObject>(string fieldName)
        {
            ObjectFieldDescriptor descriptor = this.CreateDescriptor(fieldName);

            MemberInfo[] members = typeof(TObject).GetMember(fieldName);
            if(members.Length > 0)
            {
                descriptor.FillWith(this._descriptorContext, members[0]);
            }

            return descriptor;
        }

        public ObjectFieldDescriptor CreateDescriptor<TObject, TRuntime>(Expression<Func<TObject, TRuntime>> resolver)
        {
            ObjectFieldDescriptor descriptor = new ObjectFieldDescriptor(this._descriptorContext);

            switch(resolver.Body)
            {
                case MemberExpression memberExpression:
                    descriptor.FillWith(this._descriptorContext, this._nameFormatter, memberExpression);
                    break;

                case MethodCallExpression methodCallExpression:
                    descriptor.FillWith(this._descriptorContext, this._nameFormatter, methodCallExpression);
                    break;
            }

            return descriptor;
        }
    }
}