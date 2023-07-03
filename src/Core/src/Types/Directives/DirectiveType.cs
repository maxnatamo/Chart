namespace Chart.Core
{
    public class DirectiveType
    {
        public virtual void Configure(DirectiveTypeDescriptor descriptor)
        { }
    }

    public class DirectiveType<TObject> : DirectiveType where TObject : class
    {
        public virtual void Configure(DirectiveTypeDescriptor<TObject> descriptor)
        { }
    }
}