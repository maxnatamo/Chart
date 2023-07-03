namespace Chart.Core
{
    public interface IDirectiveTypeDescriptor<TDirective> : IDirectiveTypeDescriptor where TDirective : class
    {
        /// <inheritdoc cref="IDirectiveTypeDescriptor.Name(string)" >
        new IDirectiveTypeDescriptor<TDirective> Name(string name);

        /// <inheritdoc cref="IDirectiveTypeDescriptor.Location(DirectoryLocation)" >
        new IDirectiveTypeDescriptor<TDirective> Location(DirectoryLocation locations);

        /// <inheritdoc cref="IDirectiveTypeDescriptor.Repeatable(bool)" >
        new IDirectiveTypeDescriptor<TDirective> Repeatable(bool repeatable = true);
    }

    public class DirectiveTypeDescriptor<TDirective> : DirectiveTypeDescriptor where TDirective : class
    {
        public new DirectiveTypeDescriptor<TDirective> Name(string name)
        {
            base.Name(name);
            return this;
        }

        public new DirectiveTypeDescriptor<TDirective> Location(DirectoryLocation locations)
        {
            base.Location(locations);
            return this;
        }

        public new DirectiveTypeDescriptor<TDirective> Repeatable(bool repeatable = true)
        {
            base.Repeatable(repeatable);
            return this;
        }
    }
}