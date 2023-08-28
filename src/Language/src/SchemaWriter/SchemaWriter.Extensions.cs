using Chart.Language.SyntaxTree;

namespace Chart.Language
{
    public partial class SchemaWriter
    {
        /// <summary>
        /// Descend into a GraphExtensionDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected SchemaWriter Visit(GraphExtensionDefinition definition)
        {
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
                    throw new NotSupportedException(definition.ExtensionKind.ToString());
            }

            return this;
        }

        /// <summary>
        /// Descend into a GraphScalarExtension-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected SchemaWriter Visit(GraphScalarExtension definition)
        {
            this.Write(Keywords.Extend);
            this.Write(Keywords.Scalar);
            this.WriteLine();

            this.Write(definition.Name.ToString());

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            return this;
        }

        /// <summary>
        /// Descend into a GraphObjectExtension-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected SchemaWriter Visit(GraphObjectExtension definition)
        {
            this.Write(Keywords.Extend);
            this.Write(Keywords.Type);
            this.WriteLine();

            this.Write(definition.Name.ToString());

            if(definition.Interface != null)
            {
                this.Visit(definition.Interface);
            }

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            if(definition.Fields != null)
            {
                this.Visit(definition.Fields);
            }

            return this;
        }

        /// <summary>
        /// Descend into a GraphInterfaceExtension-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected SchemaWriter Visit(GraphInterfaceExtension definition)
        {
            this.Write(Keywords.Extend);
            this.Write(Keywords.Interface);
            this.WriteLine();

            this.Write(definition.Name.ToString());

            if(definition.Interface != null)
            {
                this.Visit(definition.Interface);
            }

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
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
        protected SchemaWriter Visit(GraphUnionExtension definition)
        {
            this.Write(Keywords.Extend);
            this.Write(Keywords.Union);
            this.WriteLine();

            this.Write(definition.Name.ToString());

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

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
        protected SchemaWriter Visit(GraphEnumExtension definition)
        {
            this.Write(Keywords.Extend);
            this.Write(Keywords.Enum);
            this.WriteLine();

            this.Write(definition.Name.ToString());

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

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
        protected SchemaWriter Visit(GraphInputExtension definition)
        {
            this.Write(Keywords.Extend);
            this.Write(Keywords.Input);
            this.WriteLine();

            this.Write(definition.Name.ToString());

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            if(definition.Arguments != null)
            {
                this.Visit(definition.Arguments);
            }

            return this;
        }
    }
}