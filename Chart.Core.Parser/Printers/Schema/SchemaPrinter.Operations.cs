namespace Chart.Core.Parser
{
    public partial class SchemaPrinter
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
                    this.context.WriteLine("query ");
                    break;

                case GraphOperationKind.Mutation:
                    this.context.WriteLine("mutation ");
                    break;

                case GraphOperationKind.Subscription:
                    this.context.WriteLine("subscription ");
                    break;
            }

            if(definition.Name != null)
            {
                this.context.Write($"{definition.Name.ToString()} ");
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