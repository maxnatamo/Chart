using Chart.Models.AST;

namespace Chart.Utils.Printer
{
    public partial class DocumentPrinter
    {
        /// <summary>
        /// Descend into a GraphExtensionDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphExtensionDefinition definition)
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

                default:
                    throw new NotImplementedException(definition.ExtensionKind.ToString());
            }
        }

        /// <summary>
        /// Descend into a GraphScalarExtension-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphScalarExtension definition)
        {
            // Intentionally left empty.
        }

        /// <summary>
        /// Descend into a GraphObjectExtension-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphObjectExtension definition)
        {
            if(definition.Interface != null)
            {
                this.Visit(definition.Interface);
            }

            if(definition.Fields != null)
            {
                this.Visit(definition.Fields);
            }
        }

        /// <summary>
        /// Descend into a GraphSchemaExtension-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphSchemaExtension definition)
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
        protected void Visit(GraphInterfaceExtension definition)
        {
            if(definition.Interface != null)
            {
                this.Visit(definition.Interface);
            }

            if(definition.Fields != null)
            {
                this.Visit(definition.Fields);
            }
        }

        /// <summary>
        /// Descend into a GraphUnionExtension-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphUnionExtension definition)
        {
            if(definition.Members != null)
            {
                this.Visit(definition.Members);
            }
        }

        /// <summary>
        /// Descend into a GraphEnumExtension-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphEnumExtension definition)
        {
            if(definition.Values != null)
            {
                this.Visit(definition.Values);
            }
        }

        /// <summary>
        /// Descend into a GraphInputExtension-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphInputExtension definition)
        {
            if(definition.Arguments != null)
            {
                this.Visit(definition.Arguments);
            }
        }
    }
}