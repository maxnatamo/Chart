using Chart.Language.SyntaxTree;

namespace Chart.Language
{
    public partial class SchemaWriter
    {
        /// <summary>
        /// Descend into a GraphOperationDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public SchemaWriter Visit(GraphOperationDefinition definition)
        {
            switch(definition.OperationKind)
            {
                case GraphOperationKind.Query:
                    this.Write(Keywords.Query);
                    this.WriteSpace();
                    break;

                case GraphOperationKind.Mutation:
                    this.Write(Keywords.Mutation);
                    this.WriteSpace();
                    break;

                case GraphOperationKind.Subscription:
                    this.Write(Keywords.Subscription);
                    this.WriteSpace();
                    break;
            }

            this.Write(definition.Name.ToString());
            this.WriteSpace();

            if(definition.Variables != null)
            {
                this.Visit(definition.Variables);
            }

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            this.Visit(definition.Selections);

            return this;
        }
    }
}