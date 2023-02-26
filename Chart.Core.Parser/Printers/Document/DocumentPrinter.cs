namespace Chart.Core.Parser
{
    /// <summary>
    /// Utility class for printing/outputing the structure of a GraphDocument-object
    /// into a hierarchical string, showing all components. 
    /// </summary>
    /// <remarks>
    /// Descriptions are not included, as they are usually very long and
    /// will clutter the entire console.
    /// </remarks>
    public partial class DocumentPrinter
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
        public DocumentPrinter()
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
        public void Visit(GraphDocument document)
        {
            this.context.WriteLine(document.ToString());

            this.context.Descend();
            document.Definitions.ForEach(this.Visit);
            this.context.Ascend();
        }

        /// <summary>
        /// Descend into a GraphDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphDefinition definition)
        {
            this.context.WriteLine(definition.ToString());

            this.context.Descend();

            if(definition.Name != null)
            {
                this.context.WriteLine($"[GraphName] {definition.Name.ToString()}");
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

                case GraphDefinitionKind.Extension:
                    this.Visit((GraphExtensionDefinition) definition);
                    break;

                case GraphDefinitionKind.Schema:
                    this.Visit((GraphSchemaDefinition) definition);
                    break;

                case GraphDefinitionKind.Interface:
                    this.Visit((GraphInterfaceDefinition) definition);
                    break;

                default:
                    throw new NotImplementedException(definition.DefinitionKind.ToString());
            }

            this.context.Ascend();
        }
    }
}