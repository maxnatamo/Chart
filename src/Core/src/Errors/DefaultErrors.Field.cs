using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public static partial class DefaultErrors
    {
        public static Error FieldNotFound(string objectName, string fieldName) =>
            new ErrorBuilder()
                .SetCode(ErrorCodes.FieldNotFound)
                .SetMessage($"Field '{fieldName}' does not exist on the type '{objectName}'")
                .SetReference("sec-Field-Selections.Formal-Specification")
                .SetExtension("field", fieldName)
                .SetExtension("type", objectName)
                .Build();

        public static Error ArgumentNullabilityViolation(GraphArgumentDefinition definition) =>
            new ErrorBuilder()
                .SetCode(ErrorCodes.NullabilityViolation)
                .SetMessage($"Argument '{definition.Name}' recieved a null value, whilst expecting a non-null value.")
                .SetExtension("argument", definition.Name)
                .Build();

        public static Error ValueNotCoercible(IGraphValue value, GraphType type) =>
            new ErrorBuilder()
                .SetCode(ErrorCodes.ValueNotCoercible)
                .SetMessage($"Value '{value.ValueKind}' could not be coerced into '{type.ToString()}'")
                .SetExtension("value", value.ToString())
                .SetExtension("type", type.ToString())
                .Build();

        public static Error ValueNotCoercible(object? value, GraphType type) =>
            new ErrorBuilder()
                .SetCode(ErrorCodes.ValueNotCoercible)
                .SetMessage($"Value '{value?.ToString() ?? "null"}' could not be coerced into '{type.ToString()}'")
                .SetExtension("value", value?.ToString() ?? "null")
                .SetExtension("type", type.ToString())
                .Build();

        public static Error ValueNullabilityViolation(GraphLocation location) =>
            new ErrorBuilder()
                .SetCode(ErrorCodes.NullabilityViolation)
                .SetMessage($"Expected non-null value, got null value.")
                .AddLocation(location)
                .Build();
    }
}