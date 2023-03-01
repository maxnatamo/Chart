using Chart.Models.AST;

namespace Chart.Core.Parsers
{
    public partial class SchemaWriter
    {
        /// <summary>
        /// Descend into a GraphExtensionDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphExtensionDefinition definition)
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
                    throw new NotImplementedException(definition.ExtensionKind.ToString());
            }
        }

        /// <summary>
        /// Descend into a GraphScalarExtension-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphScalarExtension definition)
        {
            this.WriteLine($"extend scalar ");

            if(definition.Name != null)
            {
                this.Write(definition.Name.ToString());
            }

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }
        }

        /// <summary>
        /// Descend into a GraphObjectExtension-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphObjectExtension definition)
        {
            this.WriteLine($"extend type ");

            if(definition.Name != null)
            {
                this.Write(definition.Name.ToString());
            }

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
        }

        /// <summary>
        /// Descend into a GraphInterfaceExtension-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphInterfaceExtension definition)
        {
            this.WriteLine($"extend interface ");

            if(definition.Name != null)
            {
                this.Write(definition.Name.ToString());
            }

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
        }

        /// <summary>
        /// Descend into a GraphUnionExtension-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphUnionExtension definition)
        {
            this.WriteLine($"extend union ");

            if(definition.Name != null)
            {
                this.Write(definition.Name.ToString());
            }

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

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
            this.WriteLine($"extend enum ");

            if(definition.Name != null)
            {
                this.Write(definition.Name.ToString());
            }

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

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
            this.WriteLine($"extend input ");

            if(definition.Name != null)
            {
                this.Write(definition.Name.ToString());
            }

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            if(definition.Arguments != null)
            {
                this.Visit(definition.Arguments);
            }
        }
    }
}