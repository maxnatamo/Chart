using static System.StringComparison;

namespace Chart.Core
{
    /// <summary>
    /// Structure for a registered type.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Some types are registered multiple times with different names. This structure contains
    /// the different names a type has been registered with.
    /// <list type="bullet">
    ///     <listheader>
    ///         <term>Property</term>
    ///         <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///         <term>SchemaName</term>
    ///         <description>The name of the type, which is presented in the published schema.</description>
    ///     </item>
    ///     <item>
    ///         <term>DefinitionName</term>
    ///         <description>The name of the type definition, which describes the type.</description>
    ///     </item>
    ///     <item>
    ///         <term>RuntimeName</term>
    ///         <description>The name of the equivalent runtime type of the type definition, if any.</description>
    ///     </item>
    /// </list>
    /// </para>
    /// <para>
    /// For example, an integer type definintion may be registered as "Int", "IntType" and "Int32", as such:
    /// <list type="table">
    ///     <listheader>
    ///         <term>Property</term>
    ///         <description>Value</description>
    ///     </listheader>
    ///     <item>
    ///         <term>SchemaName</term>
    ///         <description><c>Int</c></description>
    ///     </item>
    ///     <item>
    ///         <term>DefinitionName</term>
    ///         <description><c>IntType</c></description>
    ///     </item>
    ///     <item>
    ///         <term>RuntimeName</term>
    ///         <description><c>Int32</c></description>
    ///     </item>
    /// </list>
    /// </para>
    /// </remarks>
    public readonly struct RegisteredType
    {
        /// <summary>
        /// The actual type definition, which has been registered.
        /// </summary>
        public readonly TypeDefinition TypeDefinition;

        /// <summary>
        /// The name of the schema type name (e.g. 'Int')
        /// </summary>
        public readonly string SchemaName => this.TypeDefinition.Name;

        /// <summary>
        /// The name of the definition type name (e.g. 'IntType')
        /// </summary>
        public readonly string DefinitionName => this.TypeDefinition.GetType().Name;

        /// <summary>
        /// The name of the runtime type name, if any (e.g. 'Int32')
        /// </summary>
        public readonly string? RuntimeName => this.RuntimeType?.Name;

        /// <summary>
        /// The runtime equivalent of the type definition, if any.
        /// </summary>
        public readonly Type? RuntimeType => this.TypeDefinition.RuntimeType;

        /// <summary>
        /// Whether this type has a runtime type registered.
        /// </summary>
        public readonly bool HasRuntimeType
            => this.TypeDefinition is { RuntimeType: not null };

        /// <summary>
        /// Create a new <see cref="RegisteredType" />-instance with the given type definition.
        /// </summary>
        /// <param name="typeDefinition">The type definition which has been registered.</param>
        public RegisteredType(TypeDefinition typeDefinition)
            => this.TypeDefinition = typeDefinition;

        /// <summary>
        /// Whether this type definition has a name which is equal to the given name.
        /// </summary>
        /// <remarks>
        /// Performs a case-sensitive comparison of all names.
        /// </remarks>
        public bool IsOfType(string typeName) =>
            this.IsOfSchemaType(typeName) ||
            this.IsOfDefinitionType(typeName) ||
            this.IsOfRuntimeType(typeName);

        /// <summary>
        /// Whether the definition's schema name is equal to the given name.
        /// </summary>
        /// <inheritdoc cref="IsOfType(string)" />
        public bool IsOfSchemaType(string typeName)
            => this.SchemaName.Equals(typeName, Ordinal);

        /// <summary>
        /// Whether the definition name is equal to the given name.
        /// </summary>
        /// <inheritdoc cref="IsOfType(string)" />
        public bool IsOfDefinitionType(string typeName)
            => this.DefinitionName.Equals(typeName, Ordinal);

        /// <summary>
        /// Whether the runtime type name is equal to the given name, if a runtime type exists.
        /// Otherwise, <see langword="null" /> .
        /// </summary>
        /// <inheritdoc cref="IsOfType(string)" />
        public bool IsOfRuntimeType(string typeName)
            => this.RuntimeName?.Equals(typeName, Ordinal) ?? false;
    }
}