using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public abstract class ScalarType<TRuntime, TSchemaType> : TypeDefinition
        where TSchemaType : IGraphValue
    {
        /// <summary>
        /// Whether a given runtime value is an instance of this scalar.
        /// </summary>
        public virtual bool IsValid(TRuntime value)
            => true;

        /// <summary>
        /// Whether a given schema value is an instance of this scalar.
        /// </summary>
        public virtual bool IsValid(TSchemaType value)
            => true;

        /// <summary>
        /// Convert an instance of <typeparamref name="TSchemaType" /> to an instance of <typeparamref name="TRuntime" />.
        /// </summary>
        public abstract TRuntime ToLiteral(TSchemaType value);

        /// <summary>
        /// Convert an instance of <typeparamref name="TRuntime" /> to an instance of <typeparamref name="TSchemaType" />.
        /// </summary>
        public abstract TSchemaType ToValue(TRuntime value);
    }
}