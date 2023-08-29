using System.Reflection;

using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public static partial class DefaultErrors
    {
        public static Error ArgumentsMissing(GraphDefinition definition) =>
            new ErrorBuilder()
                .SetCode(ErrorCodes.ArgumentNotFound)
                .SetMessage($"Arguments on '{definition.Name}' don't match their declaration.")
                .SetReference("sec-Argument-Names.Formal-Specification")
                .AddLocation(definition.Location)
                .Build();

        public static Error ArgumentNotFound(string argumentName, MethodInfo method) =>
            new ErrorBuilder()
                .SetCode(ErrorCodes.ArgumentNotFound)
                .SetMessage($"Argument '{argumentName}' could not be found on {method.Name}.")
                .SetReference("sec-Argument-Names.Formal-Specification")
                .SetExtension("method", method.Name)
                .SetExtension("object", method.ReflectedType ?? method.DeclaringType)
                .SetExtension("argument", argumentName)
                .Build();

        public static Error ArgumentNotFound(GraphArgument argument, GraphDefinition parent) =>
            new ErrorBuilder()
                .SetCode(ErrorCodes.ArgumentNotFound)
                .SetMessage($"Argument '{argument.Name.Value}' could not be found on {parent.Name}.")
                .SetReference("sec-Argument-Names.Formal-Specification")
                .SetExtension("definition", parent.Name)
                .SetExtension("argumentName", argument.Name.Value)
                .AddLocation(argument.Location)
                .Build();

        public static Error InvalidArgumentValue(
            string methodName,
            string argumentName,
            Type received,
            Type expected) =>
            new ErrorBuilder()
                .SetCode(ErrorCodes.InvalidArgumentValue)
                .SetMessage($"Argument '{argumentName}' value '{received}' cannot be converted to type {expected}")
                .SetReference("sec-Required-Arguments")
                .SetExtension("definition", methodName)
                .SetExtension("argumentName", argumentName)
                .SetExtension("received", received)
                .SetExtension("expected", expected)
                .Build();

        public static Error InvalidArgumentValue(
            GraphArgument argument,
            GraphArgumentDefinition definition,
            GraphDefinition parent) =>
            new ErrorBuilder()
                .SetCode(ErrorCodes.InvalidArgumentValue)
                .SetMessage($"Argument '{argument.Name}' value '{argument.Value}' cannot be converted to type {definition.Type}")
                .SetReference("sec-Required-Arguments")
                .SetExtension("definition", parent.Name)
                .SetExtension("argumentName", argument.Name)
                .SetExtension("argumentKind", argument.Value.ValueKind)
                .SetExtension("expectedType", definition.Type)
                .AddLocation(argument.Location)
                .Build();

        public static Error DirectiveNotFound(GraphDirective directive) =>
            new ErrorBuilder()
                .SetCode(ErrorCodes.DirectiveNotFound)
                .SetMessage($"Directive '{directive.Name}' was not found in the document.")
                .SetReference("sec-Directives-Are-Defined")
                .SetExtension("name", directive.Name)
                .AddLocation(directive.Location)
                .Build();

        public static Error AmbiguousDirective(GraphDirective directive) =>
            new ErrorBuilder()
                .SetCode(ErrorCodes.AmbiguousDirective)
                .SetMessage($"Ambiguous directive '{directive.Name}' was found in the document.")
                .SetReference("sec-Directives-Are-Defined")
                .SetExtension("name", directive.Name)
                .AddLocation(directive.Location)
                .Build();

        public static Error DirectiveNonUnique(GraphDirective directive) =>
            new ErrorBuilder()
                .SetCode(ErrorCodes.DirectiveNotRepeatable)
                .SetMessage($"Directive '{directive.Name}' cannot repeat.")
                .SetReference("sec-Directives-Are-Unique-Per-Location")
                .SetExtension("name", directive.Name)
                .AddLocation(directive.Location)
                .Build();

        public static Error DirectiveInvalidLocation(
            GraphDirective directive,
            GraphDirectiveDefinition directiveDefinition,
            GraphDirectiveLocationFlags location) =>
            new ErrorBuilder()
                .SetCode(ErrorCodes.DirectiveLocationNotValid)
                .SetMessage($"Directive '{directive.Name}' was not used in correct location.")
                .SetReference("sec-Directives-Are-In-Valid-Locations")
                .SetExtension("name", directive.Name)
                .SetExtension("expected", DirectiveLocations
                    .SplitLocation(directiveDefinition.Locations.Locations)
                    .Select(v => v.ToString()))
                .SetExtension("found", location.ToString())
                .AddLocation(directive.Location)
                .Build();

        public static Error AmbiguousFragment<T>(T fragment, IEnumerable<GraphLocation?> locations)
            where T : IHasName, IGraphNode =>
            new ErrorBuilder()
                .SetCode(ErrorCodes.AmbiguousDefinition)
                .SetMessage($"Fragment definition '{fragment.Name}' is ambiguous.")
                .SetReference("sec-Fragment-Name-Uniqueness")
                .SetExtension("name", fragment.Name)
                .SetExtension("ambiguity", locations)
                .AddLocation(fragment.Location)
                .Build();

        public static Error FragmentNotFound<T>(T fragment)
            where T : IHasName, IGraphNode =>
            new ErrorBuilder()
                .SetCode(ErrorCodes.DefinitionNotFound)
                .SetMessage($"Fragment definition '{fragment.Name}' was not found in the document.")
                .SetReference("sec-Fragment-Name-Uniqueness")
                .SetExtension("name", fragment.Name)
                .AddLocation(fragment.Location)
                .Build();

        public static Error FragmentTypeNotFound(GraphFragmentDefinition fragment) =>
            new ErrorBuilder()
                .SetCode(ErrorCodes.InvalidFragmentSpread)
                .SetMessage($"Fragment spread type '{fragment.Type.Name}' was not found in the document.")
                .SetReference("sec-Fragment-Spread-Type-Existence")
                .SetExtension("name", fragment.Name)
                .SetExtension("type", fragment.Type.Name)
                .AddLocation(fragment.Location)
                .Build();

        public static Error FragmentTypeNotFound(GraphInlineFragmentSelection fragment) =>
            new ErrorBuilder()
                .SetCode(ErrorCodes.InvalidFragmentSpread)
                .SetMessage($"Fragment spread type '{fragment.TypeCondition?.Name}' was not found in the document.")
                .SetReference("sec-Fragment-Spread-Type-Existence")
                .SetExtension("type", fragment.TypeCondition?.Name)
                .AddLocation(fragment.Location)
                .Build();

        public static Error FragmentTypeNotComposite(GraphFragmentDefinition fragment) =>
            new ErrorBuilder()
                .SetCode(ErrorCodes.FragmentSpreadNotComposite)
                .SetMessage($"Fragment spread type '{fragment.Type.Name}' must be a composite type (union, interface or object).")
                .SetReference("sec-Fragments-On-Composite-Types")
                .SetExtension("name", fragment.Name)
                .SetExtension("type", fragment.Type.Name)
                .AddLocation(fragment.Location)
                .Build();

        public static Error FragmentTypeNotComposite(GraphInlineFragmentSelection fragment) =>
            new ErrorBuilder()
                .SetCode(ErrorCodes.FragmentSpreadNotComposite)
                .SetMessage($"Fragment spread type '{fragment.TypeCondition?.Name}' must be a composite type (union, interface or object).")
                .SetReference("sec-Fragments-On-Composite-Types")
                .SetExtension("type", fragment.TypeCondition?.ToString())
                .AddLocation(fragment.Location)
                .Build();

        public static Error FragmentNotReferenced(GraphFragmentDefinition fragment) =>
            new ErrorBuilder()
                .SetCode(ErrorCodes.FragmentNotReferenced)
                .SetMessage($"Fragment definition '{fragment.Name}' was never referenced in the query.")
                .SetReference("sec-Fragment-Name-Uniqueness")
                .SetExtension("name", fragment.Name)
                .AddLocation(fragment.Location)
                .Build();

        public static Error FragmentSpreadsFormCycle(GraphFragmentDefinition fragment, Stack<object> path) =>
            new ErrorBuilder()
                .SetCode(ErrorCodes.FragmentSpreadFormsCycle)
                .SetMessage($"Fragment spread '{fragment.Name}' formed a recursive cycle.")
                .SetReference("sec-Fragment-spreads-must-not-form-cycles")
                .SetExtension("name", fragment.Name)
                .SetExtension("type", fragment.Type.Name)
                .AddLocation(fragment.Location)
                .SetPath(path.ToList())
                .Build();

        public static Error SubscriptionContainsIntrospectionField(GraphSubscriptionOperation operation) =>
            new ErrorBuilder()
                .SetCode(ErrorCodes.IntrospectionNotValid)
                .SetMessage($"Subscriptions cannot contain introspection fields.")
                .SetReference("sec-Single-root-field")
                .SetExtension("operation", operation.Name)
                .AddLocation(operation.Location)
                .Build();

        public static Error NameNotValid(GraphName name) =>
            new ErrorBuilder()
                .SetCode(ErrorCodes.InvalidName)
                .SetMessage($"Name '{name.Value}' is not a valid introspection name.")
                .SetReference("sec-Introspection.Reserved-Names")
                .SetExtension("name", name)
                .AddLocation(name.Location)
                .Build();

        public static Error InvalidAnonymousOperation(GraphOperationDefinition anonoymousOperation) =>
            new ErrorBuilder()
                .SetCode(ErrorCodes.InvalidAnonymousOperation)
                .SetMessage($"Anonymous operations cannot co-exist with other operations.")
                .SetReference("sec-Lone-Anonymous-Operation")
                .AddLocation(anonoymousOperation.Location)
                .Build();

        public static Error OperationNotFound(GraphOperationDefinition operation) =>
            new ErrorBuilder()
                .SetCode(ErrorCodes.OperationNotFound)
                .SetMessage($"Operation '{operation.Name}' was not found in the document.")
                .SetReference("sec-Operation-Name-Uniqueness")
                .SetExtension("operation", operation.Name)
                .SetExtension("kind", operation.OperationKind.ToString())
                .AddLocation(operation.Location)
                .Build();

        public static Error OperationAlreadyExists(GraphOperationDefinition operation) =>
            new ErrorBuilder()
                .SetCode(ErrorCodes.MultipleOperations)
                .SetMessage($"Operation '{operation.Name}' already exists in the document.")
                .SetReference("sec-Operation-Name-Uniqueness")
                .SetExtension("operation", operation.Name.ToString())
                .SetExtension("kind", operation.OperationKind.ToString())
                .AddLocation(operation.Location)
                .Build();

        public static Error SubscriptionRootFieldMissing(GraphSubscriptionOperation operation) =>
            new ErrorBuilder()
                .SetCode(ErrorCodes.SubscriptionRootFieldMissing)
                .SetMessage($"Subscription '{operation.Name}' must have exactly one root field")
                .SetReference("sec-Single-root-field")
                .SetExtension("operation", operation.Name?.ToString() ?? "(anonymous)")
                .AddLocation(operation.Location)
                .Build();

        public static Error TypeNotFound(string typeName) =>
            new ErrorBuilder()
                .SetCode(ErrorCodes.DefinitionNotFound)
                .SetMessage($"Type '{typeName}' was not found in the document.")
                .SetExtension("type", typeName)
                .Build();
    }
}