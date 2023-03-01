using Newtonsoft.Json;

using Chart.Core.Parsers;
using Chart.Models.AST;

namespace Chart.Core.Execution
{
    using OperationResult = Dictionary<string, object?>;

    public partial class Document
    {
        /// <summary>
        /// Execute an operation with a supplied data-object.
        /// </summary>
        /// <param name="action">Action for the execution context.</param>
        /// <returns>The resulting object, serialized in JSON.</returns>
        public string Execute(Action<ExecutionContext> action)
        {
            ExecutionContext ctx = new ExecutionContext();
            
            // Context is passed via reference
            action(ctx);

            return this.Execute(ctx);
        }

        /// <summary>
        /// Execute an operation with a supplied data-object.
        /// </summary>
        /// <param name="ctx">The execution context.</param>
        /// <returns>The resulting object, serialized in JSON.</returns>
        public string Execute(ExecutionContext ctx)
        {
            List<OperationResult?> operationResults = new List<OperationResult?>();

            GraphDocument doc = this.Parser.Parse(ctx.Query);
            GraphObjectType obj = this.ModelParser.ConvertAnonymousType(ctx.Data.GetType());

            foreach(var ops in doc.Definitions)
            {
                if(ops is not GraphOperationDefinition)
                {
                    continue;
                }

                GraphOperationDefinition op = (GraphOperationDefinition) ops;

                // operationResults.Add(this.Query(op.Selections, obj));
            }

            return JsonConvert.SerializeObject(operationResults, Formatting.Indented);
        }

        /// <summary>
        /// Perform an operation with the specified selection-set on the object-type.
        /// </summary>
        /// <param name="selectionSet">The fields to selection on the object.</param>
        /// <param name="obj">The object to perform the operation on.</param>
        /// <returns>The resulting object dictionary.</returns>
        private OperationResult? Query(GraphSelectionSet selectionSet, GraphFields obj)
        {
            OperationResult result = new OperationResult();

            foreach(var selection in selectionSet.Selections)
            {
                GraphFieldSelection field = (GraphFieldSelection) selection;

                
            }

            return result;
        }

#if false
        private OperationResult? Query(GraphSelectionSet selectionSet, object? data)
        {
            if(data == null)
            {
                return null;
            }

            Type dataType = data.GetType();
            OperationResult result = new OperationResult();

            foreach(var selection in selectionSet.Selections)
            {
                GraphFieldSelection field = (GraphFieldSelection) selection;

                var valueProp = dataType.GetProperty(field.Name.Value);
                if(valueProp == null)
                {
                    throw new Exception(field.Name.Value);
                }

                if(field.SelectionSet != null)
                {
                    Console.WriteLine("Non-empty");
                    result[field.Name.Value] = this.Query(field.SelectionSet, valueProp.GetValue(data));
                }
                else
                {
                    Console.WriteLine("Empty");
                    result[field.Name.Value] = valueProp.GetValue(data);
                }
            }

            return result;
        }
#endif
    }
}