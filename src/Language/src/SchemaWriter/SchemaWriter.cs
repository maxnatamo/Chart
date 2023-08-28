using Chart.Language.SyntaxTree;

namespace Chart.Language
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
        private readonly List<string> _lines;

        /// <summary>
        /// The zero-indexed depth of the context.
        /// </summary>
        private int depth { get; set; } = 0;

        /// <summary>
        /// Read in a parsed object and visit all nodes in the tree.
        /// </summary>
        public SchemaWriter()
        {
            this._lines = new List<string> { "" };
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
            return string.Join("\n", this._lines.Select(v => v.ToString())).Trim();
        }

        /// <summary>
        /// Descend into the parent GraphDocument-object.
        /// </summary>
        /// <param name="document">The document to descend into.</param>
        public SchemaWriter Visit(GraphDocument document)
        {
            foreach(GraphDefinition definition in document.Definitions)
            {
                this.Visit(definition);
            }

            return this;
        }

        /// <summary>
        /// Descend into the parent GraphDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public SchemaWriter Visit(GraphDefinition definition)
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
                    throw new NotSupportedException(definition.DefinitionKind.ToString());
            }

            this.WriteLine();

            return this;
        }

        /// <summary>
        /// Increase the indentation of the schema writer.
        /// </summary>
        private void IncreaseDepth()
            => this.depth++;

        /// <summary>
        /// Decrease the indentation of the schema writer.
        /// </summary>
        private void DecreaseDepth()
            => this.depth--;

        /// <summary>
        /// Write the given content string to the current line.
        /// </summary>
        private void Write(string content)
        {
            if(this._lines[^1].Length == 0)
            {
                this._lines[^1] += this.GetIndentation();
            }
            this._lines[^1] += content;
        }

        /// <summary>
        /// Write the a single space to the current line.
        /// </summary>
        private void WriteSpace()
            => this.Write(" ");

        /// <summary>
        /// Write the given content string to the current line and start a new line.
        /// </summary>
        private void WriteLine(string content)
        {
            this.Write(content);
            this.WriteLine();
        }

        /// <summary>
        /// Start a new line.
        /// </summary>
        private void WriteLine()
            => this._lines.Add("");

        /// <summary>
        /// Iterate through the list and execute the <paramref name="write" />-action, with an optional joiner between entries.
        /// </summary>
        private void WriteMany<T>(List<T> list, Action<T> write, string? joiner = null)
        {
            for(int i = 0; i < list.Count; i++)
            {
                write(list[i]);

                if(i < list.Count - 1 && joiner is not null)
                {
                    this.Write(joiner);
                }
            }
        }

        /// <summary>
        /// Iterate through the dictionary and execute the <paramref name="write" />-action, with an optional joiner between entries.
        /// </summary>
        private void WriteMany<TKey, TValue>(Dictionary<TKey, TValue> dict, Action<KeyValuePair<TKey, TValue>> write, string? joiner = null)
            where TKey : notnull
        {
            int i = 0;
            foreach(KeyValuePair<TKey, TValue> pair in dict)
            {
                write(pair);

                if(i < dict.Count - 1 && joiner is not null)
                {
                    this.Write(joiner);
                }

                i++;
            }
        }

        private string GetIndentation()
            => string.Join("", Enumerable.Repeat("  ", this.depth));
    }
}