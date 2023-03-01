using Chart.Models.AST;

namespace Chart.Utils.Printer
{
    public partial class DocumentPrinter
    {
        /// <summary>
        /// Descend into a GraphValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphValue definition)
        {
            this.WriteLine(definition.ToString());

            this.IncreaseDepth();
    
            switch(definition.ValueKind)
            {
                case GraphValueKind.Variable:
                    this.Visit((GraphVariableValue) definition);
                    break;

                case GraphValueKind.Int:
                    this.Visit((GraphIntValue) definition);
                    break;

                case GraphValueKind.Float:
                    this.Visit((GraphFloatValue) definition);
                    break;

                case GraphValueKind.String:
                    this.Visit((GraphStringValue) definition);
                    break;

                case GraphValueKind.Boolean:
                    this.Visit((GraphBooleanValue) definition);
                    break;

                case GraphValueKind.Null:
                    this.Visit((GraphNullValue) definition);
                    break;

                case GraphValueKind.Enum:
                    this.Visit((GraphEnumValue) definition);
                    break;

                case GraphValueKind.List:
                    this.Visit((GraphListValue) definition);
                    break;

                case GraphValueKind.Object:
                    this.Visit((GraphObjectValue) definition);
                    break;

                default:
                    throw new NotImplementedException(definition.ValueKind.ToString());
            }

            this.DecreaseDepth();
        }

        /// <summary>
        /// Descend into a GraphVariableValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphVariableValue definition)
        {
            this.WriteLine($"[GraphValue] {definition.Variable.ToString()}");
        }

        /// <summary>
        /// Descend into a GraphIntValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphIntValue definition)
        {
            this.WriteLine($"[GraphValue] {definition.Value.ToString()}");
        }

        /// <summary>
        /// Descend into a GraphFloatValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphFloatValue definition)
        {
            this.WriteLine($"[GraphValue] {definition.Value.ToString()}");
        }

        /// <summary>
        /// Descend into a GraphStringValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphStringValue definition)
        {
            this.WriteLine($"[GraphValue] {definition.Value.ToString()}");
        }

        /// <summary>
        /// Descend into a GraphBooleanValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphBooleanValue definition)
        {
            this.WriteLine($"[GraphValue] {definition.Value.ToString()}");
        }

        /// <summary>
        /// Descend into a GraphNullValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphNullValue definition)
        {
            this.WriteLine("[GraphValue] null");
        }

        /// <summary>
        /// Descend into a GraphEnumValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphEnumValue definition)
        {
            this.WriteLine($"[GraphValue] {definition.Value.ToString()}");
        }

        /// <summary>
        /// Descend into a GraphEnumValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphListValue definition)
        {
            definition.Values.ForEach(v =>
            {
                this.Visit(v);
            });
        }

        /// <summary>
        /// Descend into a GraphObjectValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphObjectValue definition)
        {
            definition.Fields.ToList().ForEach(v =>
            {
                this.WriteLine($"[GraphName] {v.Key.Value}");
                this.Visit(v.Value);
            });
        }
    }
}