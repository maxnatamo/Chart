using Chart.Models.AST;

namespace Chart.Core.Parsers
{
    public partial class SchemaWriter
    {
        /// <summary>
        /// Descend into a GraphOperationDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphOperationDefinition definition)
        {
            switch(definition.OperationKind)
            {
                case GraphOperationKind.Query:
                    this.Write("query ");
                    break;

                case GraphOperationKind.Mutation:
                    this.Write("mutation ");
                    break;

                case GraphOperationKind.Subscription:
                    this.Write("subscription ");
                    break;
            }

            if(definition.Name != null)
            {
                this.Write($"{definition.Name.ToString()} ");
            }

            if(definition.Variables != null)
            {
                this.Visit(definition.Variables);
            }

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            this.Visit(definition.Selections);
        }
    }
}