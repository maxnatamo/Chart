using Chart.Models.AST;

namespace Chart.Core.Parsers
{
    /// <summary>
    /// Utility class for printing/outputing the structure of a GraphDocument-object
    /// into a GraphQL schema, which can be used as an actual schema.
    /// </summary>
    public partial class SchemaWriter
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
        public SchemaWriter()
        {
            this.Lines = new List<string>{ "" };
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
        public SchemaWriter Visit(GraphDocument document)
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

                this.WriteLine("");
            }

            return this;
        }

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
            => string.Join("", Enumerable.Repeat("   ", Depth));
    }
}