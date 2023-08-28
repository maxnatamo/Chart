using System.Collections.ObjectModel;

using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public static partial class GraphArgumentExtensions
    {
        /// <summary>
        /// Whether the given argument has a value equal to the given value.
        /// </summary>
        /// <param name="argument">The argument to perform the comparison on.</param>
        /// <param name="expected">The expected value of the argument.</param>
        /// <param name="variableValues">
        /// Optional variables.
        /// If not defined and if the argument value is a variable, the method returns <see langword="false" />.</param>
        /// <returns><see langword="true" />, if the argument value is equal to the given value. Otherwise, <see langword="false" />.</returns>
        public static bool IsArgumentEqual(
            this GraphArgument argument,
            object? expected,
            ReadOnlyDictionary<string, object?>? variableValues = null)
        {
            IGraphValue argumentValue = argument.Value;
            object? innerValue = argumentValue.Value;

            if(argumentValue is GraphVariableValue variableValue)
            {
                // We can't deduce the value of the variable,
                // but a string comparison might return true.
                if(variableValues is null)
                {
                    return false;
                }

                if(!variableValues.TryGetValue(variableValue.Value, out object? value))
                {
                    return false;
                }

                innerValue = value;
            }

            if(innerValue is null && expected is null)
            {
                return true;
            }

            return innerValue?.Equals(expected) ?? false;
        }
    }
}