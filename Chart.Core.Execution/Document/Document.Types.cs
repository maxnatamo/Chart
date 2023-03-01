using Chart.Core.Parsers;
using Chart.Models.AST;

namespace Chart.Core.Execution
{
    public partial class Document
    {
        public Document RegisterType<T>()
        {
            GraphObjectType obj = this.ModelParser.ConvertType<T>();

            this.GraphDocument.Definitions.Add(obj);

            return this;
        }
    }
}