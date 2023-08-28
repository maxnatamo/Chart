using System.Collections.ObjectModel;

using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public static partial class GraphDirectiveExtensions
    {
        /// <summary>
        /// Whether the given argument has a value equal to the given value.
        /// </summary>
        /// <param name="directive">The directive to perform the comparison on.</param>
        /// <param name="expected">The expected value of the argument.</param>
        /// <param name="argumentName">The argument name to check against.</param>
        /// <param name="variableValues">
        /// Optional variables.
        /// If not defined and if the argument value is a variable, the method returns <see langword="false" />.
        /// </param>
        /// <returns><see langword="true" />, if the argument value is equal to the given value. Otherwise, <see langword="false" />.</returns>
        public static bool IsDirectiveArgumentEqual(
            this GraphDirective directive,
            object? expected,
            string argumentName = "if",
            ReadOnlyDictionary<string, object?>? variableValues = null)
        {
            GraphArgument? directiveArgument = directive.Arguments?.Arguments.FirstOrDefault(v => v.Name == argumentName);
            if(directiveArgument is null)
            {
                return false;
            }

            return directiveArgument.IsArgumentEqual(expected, variableValues);
        }
    }
}