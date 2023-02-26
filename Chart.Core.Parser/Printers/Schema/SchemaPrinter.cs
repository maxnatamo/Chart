namespace Chart.Core.Parser
{
    /// <summary>
    /// Utility class for printing/outputing the structure of a GraphDocument-object
    /// into a GraphQL schema, which can be used as an actual schema.
    /// </summary>
    public partial class SchemaPrinter
    {
        /// <summary>
        /// The context use for doing the actual printing.
        /// </summary>
        private readonly PrinterContext context;

        /// <summary>
        /// The zero-indexed depth of the context.
        /// </summary>
        private int Depth { get; set; } = 0;

        /// <summary>
        /// Read in a parsed object and visit all nodes in the tree.
        /// </summary>
        public SchemaPrinter()
        {
            this.context = new PrinterContext();
        }

        /// <summary>
        /// Return the structure of the GraphDocument-object as a string.
        /// </summary>
        /// <remarks>
        /// Descriptions are not included, as they are usually very long and
        /// will clutter the entire console.
        /// </remarks>
        public override string ToString()
        {
            return this.context.ToString();
        }

        /// <summary>
        /// Descend into the parent GraphDocument-object.
        /// </summary>
        /// <param name="document">The document to descend into.</param>
        public SchemaPrinter Visit(GraphDocument document)
        {
            foreach(var definition in document.Definitions)
            {
                if(definition.Description != null)
                {
                    this.Visit(definition.Description);
                }

                switch(definition.DefinitionKind)
                {
                    case GraphDefinitionKind.Operation:
                        this.Visit((GraphOperationDefinition) definition);
                        break;

                    case GraphDefinitionKind.Type:
                        this.Visit((GraphTypeDefinition) definition);
                        break;

                    case GraphDefinitionKind.Input:
                        this.Visit((GraphInputDefinition) definition);
                        break;

                    case GraphDefinitionKind.Enum:
                        this.Visit((GraphEnumDefinition) definition);
                        break;

                    case GraphDefinitionKind.Union:
                        this.Visit((GraphUnionDefinition) definition);
                        break;

                    case GraphDefinitionKind.Fragment:
                        this.Visit((GraphFragmentDefinition) definition);
                        break;

                    case GraphDefinitionKind.Directive:
                        this.Visit((GraphDirectiveDefinition) definition);
                        break;

                    case GraphDefinitionKind.Schema:
                        this.Visit((GraphSchemaDefinition) definition);
                        break;

                    case GraphDefinitionKind.Extension:
                        this.Visit((GraphExtensionDefinition) definition);
                        break;

                    case GraphDefinitionKind.Interface:
                        this.Visit((GraphInterfaceDefinition) definition);
                        break;

                    default:
                        throw new NotImplementedException(definition.DefinitionKind.ToString());
                }

                this.context.WriteLine("");
            }

            return this;
        }
    }
}