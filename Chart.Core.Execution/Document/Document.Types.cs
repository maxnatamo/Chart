namespace Chart.Core.Parser.Execution
{
    public partial class Document
    {
        public Document RegisterType<T>()
        {
            GraphObjectType obj = this.Serializer.ConvertObjectType<T>();

            this.GraphDocument.Definitions.Add(obj);

            return this;
        }
    }
}