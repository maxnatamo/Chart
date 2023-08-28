using Chart.Language.SyntaxTree;

namespace Chart.Utilities
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
        /// The list of lines in the writer.
        /// </summary>
        private readonly List<string> Lines;

        /// <summary>
        /// The zero-indexed depth of the context.
        /// </summary>
        private int Depth { get; set; } = 0;

        /// <summary>
        /// Read in a parsed object and visit all nodes in the tree.
        /// </summary>
        public DocumentPrinter()
        {
            this.Lines = new List<string> { "" };
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
            return string.Join("\n", this.Lines.Select(v => v.ToString()));
        }

        /// <summary>
        /// Descend into the parent GraphDocument-object.
        /// </summary>
        /// <param name="document">The document to descend into.</param>
        public DocumentPrinter Visit(GraphDocument document)
        {
            this.WriteLine(document.ToString());

            this.IncreaseDepth();
            document.Definitions.ForEach(v => this.Visit(v));
            this.DecreaseDepth();

            return this;
        }

        /// <summary>
        /// Descend into a GraphDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphDefinition definition)
        {
            this.WriteLine(definition.ToString());

            this.IncreaseDepth();

            if(definition.Name is not null)
            {
                this.WriteLine($"[GraphName] {definition.Name.ToString()}");
            }

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
                    throw new NotSupportedException(definition.DefinitionKind.ToString());
            }

            this.DecreaseDepth();

            return this;
        }

        /// <summary>
        /// Descend into a GraphDefinition-object.
        /// </summary>
        /// <param name="node">The node to descend into.</param>
        public DocumentPrinter Visit(IGraphNode node) =>
            node switch
            {
                GraphInputFieldsDefinition _node => this.Visit(_node),
                GraphArgument _node => this.Visit(_node),
                GraphArgumentDefinition _node => this.Visit(_node),
                GraphArguments _node => this.Visit(_node),
                GraphArgumentsDefinition _node => this.Visit(_node),
                GraphDefinition _node => this.Visit(_node),
                GraphDescription _node => this.Visit(_node),
                GraphDirective _node => this.Visit(_node),
                GraphDirectives _node => this.Visit(_node),
                GraphDocument _node => this.Visit(_node),
                GraphField _node => this.Visit(_node),
                GraphFields _node => this.Visit(_node),
                GraphInterfaces _node => this.Visit(_node),
                GraphSelection _node => this.Visit(_node),
                GraphSelectionSet _node => this.Visit(_node),
                IGraphValue _node => this.Visit(_node),
                GraphVariable _node => this.Visit(_node),
                GraphVariables _node => this.Visit(_node),

                _ => throw new NotSupportedException()
            };

        private void IncreaseDepth()
            => this.Depth++;

        private void DecreaseDepth()
            => this.Depth--;

        private void Write(string content)
        {
            if(this.Lines[this.Lines.Count - 1].Length == 0)
            {
                this.Lines[this.Lines.Count - 1] += this.GetIndentation();
            }
            this.Lines[this.Lines.Count - 1] += content;
        }

        private void WriteLine(string content)
        {
            this.Write(content);
            this.Lines.Add("");
        }

        private string GetIndentation()
            => string.Join("", Enumerable.Repeat("   ", this.Depth));
    }
}