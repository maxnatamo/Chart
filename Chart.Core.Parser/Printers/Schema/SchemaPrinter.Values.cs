namespace Chart.Core.Parser
{
    public partial class SchemaPrinter
    {
        /// <summary>
        /// Descend into a GraphValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphValue definition)
        {
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
        }

        /// <summary>
        /// Descend into a GraphValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphVariableValue definition)
        {
            this.context.Write($"${definition.Variable.ToString()}");
        }

        /// <summary>
        /// Descend into a GraphIntValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphIntValue definition)
        {
            this.context.Write($"{definition.Value.ToString()}");
        }

        /// <summary>
        /// Descend into a GraphFloatValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphFloatValue definition)
        {
            this.context.Write($"{definition.Value.ToString()}");
        }

        /// <summary>
        /// Descend into a GraphStringValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphStringValue definition)
        {
            this.context.Write($"\"{definition.Value.ToString()}\"");
        }

        /// <summary>
        /// Descend into a GraphBooleanValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphBooleanValue definition)
        {
            this.context.Write($"{definition.Value.ToString().ToLower()}");
        }

        /// <summary>
        /// Descend into a GraphNullValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphNullValue definition)
        {
            this.context.Write($"null");
        }

        /// <summary>
        /// Descend into a GraphEnumValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphEnumValue definition)
        {
            this.context.Write($"{definition.Value.ToString()}");
        }

        /// <summary>
        /// Descend into a GraphEnumValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphListValue definition)
        {
            this.context.Write("[");

            for(int i = 0; i < definition.Values.Count; i++)
            {
                this.Visit(definition.Values[i]);

                if(i < definition.Values.Count - 1)
                {
                    this.context.Write(", ");
                }
            }

            this.context.Write("]");
        }

        /// <summary>
        /// Descend into a GraphObjectValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphObjectValue definition)
        {
            this.context.Write("{");
    
            foreach(var field in definition.Fields)
            {

            }

            this.context.Write("}");
        }
    }
}