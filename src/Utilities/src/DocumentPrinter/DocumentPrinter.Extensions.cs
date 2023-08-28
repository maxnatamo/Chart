using Chart.Language.SyntaxTree;

namespace Chart.Utilities
{
    public partial class DocumentPrinter
    {
        /// <summary>
        /// Descend into a GraphExtensionDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphExtensionDefinition definition)
        {
            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            switch(definition.ExtensionKind)
            {
                case GraphExtensionKind.Scalar:
                    this.Visit((GraphScalarExtension) definition);
                    break;

                case GraphExtensionKind.Object:
                    this.Visit((GraphObjectExtension) definition);
                    break;

                case GraphExtensionKind.Interface:
                    this.Visit((GraphInterfaceExtension) definition);
                    break;

                case GraphExtensionKind.Union:
                    this.Visit((GraphUnionExtension) definition);
                    break;

                case GraphExtensionKind.Enum:
                    this.Visit((GraphEnumExtension) definition);
                    break;

                case GraphExtensionKind.Input:
                    this.Visit((GraphInputExtension) definition);
                    break;

                case GraphExtensionKind.Schema:
                    this.Visit((GraphSchemaExtension) definition);
                    break;

                default:
                    throw new NotSupportedException(definition.ExtensionKind.ToString());
            }

            return this;
        }

        /// <summary>
        /// Descend into a GraphScalarExtension-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphScalarExtension definition)
        {
            // Intentionally left empty.

            return this;
        }

        /// <summary>
        /// Descend into a GraphObjectExtension-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphObjectExtension definition)
        {
            if(definition.Interface != null)
            {
                this.Visit(definition.Interface);
            }

            if(definition.Fields != null)
            {
                this.Visit(definition.Fields);
            }

            return this;
        }

        /// <summary>
        /// Descend into a GraphSchemaExtension-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphSchemaExtension definition)
        {
            if(definition.Values != null)
            {
                this.Visit(definition.Values);
            }
        }

        /// <summary>
        /// Descend into a GraphInterfaceExtension-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphInterfaceExtension definition)
        {
            if(definition.Interface != null)
            {
                this.Visit(definition.Interface);
            }

            if(definition.Fields != null)
            {
                this.Visit(definition.Fields);
            }

            return this;
        }

        /// <summary>
        /// Descend into a GraphUnionExtension-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphUnionExtension definition)
        {
            if(definition.Members != null)
            {
                this.Visit(definition.Members);
            }

            return this;
        }

        /// <summary>
        /// Descend into a GraphEnumExtension-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphEnumExtension definition)
        {
            if(definition.Values != null)
            {
                this.Visit(definition.Values);
            }

            return this;
        }

        /// <summary>
        /// Descend into a GraphInputExtension-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphInputExtension definition)
        {
            if(definition.Arguments != null)
            {
                this.Visit(definition.Arguments);
            }

            return this;
        }
    }
}