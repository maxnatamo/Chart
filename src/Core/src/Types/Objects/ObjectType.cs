namespace Chart.Core
{
    public abstract class ObjectType<TObject> : TypeDefinition
        where TObject : class
    {
        public virtual void Configure(IObjectTypeDescriptor<TObject> descriptor)
        { }
    }
}