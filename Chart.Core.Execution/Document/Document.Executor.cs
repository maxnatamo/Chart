using System.Dynamic;

namespace Chart.Core.Parser.Execution
{
    public partial class Document
    {
        public string Execute(Action<ExecutionContext> action)
        {
            ExecutionContext ctx = new ExecutionContext();
            
            // Context is passed via reference
            action(ctx);

            return this.Execute(ctx);
        }

        public string Execute(ExecutionContext ctx)
        {
            GraphDocument doc = this.Parser.Parse(ctx.Query);

            foreach(var ops in doc.Definitions)
            {
                if(ops is not GraphOperationDefinition)
                {
                    continue;
                }

                GraphOperationDefinition op = (GraphOperationDefinition) ops;

                this.Query(op.Selections, ctx.Data);
            }

            return ctx.Query;
        }

        private object Query(GraphSelectionSet selectionSet, object data)
        {
            Type dataType = data.GetType();
            dynamic result = new ExpandoObject();

            foreach(var selection in selectionSet.Selections)
            {
                GraphFieldSelection field = (GraphFieldSelection) selection;

                var valueProp = dataType.GetProperty(field.Name.Value);
                if(valueProp == null)
                {
                    throw new Exception();
                }

                ((IDictionary<string, object?>) result)[field.Name.Value] = valueProp.GetValue(data);
            }

            return result;
        }
    }
}