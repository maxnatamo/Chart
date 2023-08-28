using Chart.Language.SyntaxTree;

namespace Chart.Language
{
    public partial class SchemaWriter
    {
        /// <summary>
        /// Descend into a GraphValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public SchemaWriter Visit(IGraphValue definition)
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
                    throw new NotSupportedException(definition.ValueKind.ToString());
            }

            return this;
        }

        /// <summary>
        /// Descend into a GraphValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public SchemaWriter Visit(GraphVariableValue definition)
        {
            this.Write("$");
            this.Write(definition.Value.ToString());

            return this;
        }

        /// <summary>
        /// Descend into a GraphIntValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public SchemaWriter Visit(GraphIntValue definition)
        {
            this.Write(definition.Value.ToString());

            return this;
        }

        /// <summary>
        /// Descend into a GraphFloatValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public SchemaWriter Visit(GraphFloatValue definition)
        {
            this.Write(definition.Value.ToString());

            return this;
        }

        /// <summary>
        /// Descend into a GraphStringValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public SchemaWriter Visit(GraphStringValue definition)
        {
            this.Write($"\"{definition.Value.ToString()}\"");

            return this;
        }

        /// <summary>
        /// Descend into a GraphBooleanValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public SchemaWriter Visit(GraphBooleanValue definition)
        {
            this.Write(definition.Value.ToString().ToLower());

            return this;
        }

        /// <summary>
        /// Descend into a GraphNullValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public SchemaWriter Visit(GraphNullValue definition)
        {
            this.Write("null");

            return this;
        }

        /// <summary>
        /// Descend into a GraphEnumValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public SchemaWriter Visit(GraphEnumValue definition)
        {
            this.Write(definition.Value.ToString());

            return this;
        }

        /// <summary>
        /// Descend into a GraphEnumValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public SchemaWriter Visit(GraphListValue definition)
        {
            this.Write("[");
            this.WriteMany(definition.Value, v => this.Visit(v), ", ");
            this.Write("]");

            return this;
        }

        /// <summary>
        /// Descend into a GraphObjectValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public SchemaWriter Visit(GraphObjectValue definition)
        {
            this.Write("{");

            this.WriteMany(definition.Value, v =>
            {
                this.Write(v.Key.ToString());
                this.Write(": ");
                this.Visit(v.Value);
            }, ", ");

            this.Write("}");

            return this;
        }
    }
}