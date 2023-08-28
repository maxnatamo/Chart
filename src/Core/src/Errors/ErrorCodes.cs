namespace Chart.Core
{
    public static class ErrorCodes
    {
        public readonly static string ArgumentNotFound = "ARGUMENT_NOT_FOUND";
        public readonly static string LeafSelectionNotAllowed = "LEAF_SELECTION_NOT_ALLOWED";
        public readonly static string InvalidDirectiveName = "INVALID_DIRECTIVE_NAME";
        public readonly static string InvalidArgumentName = "INVALID_ARGUMENT_NAME";
        public readonly static string InvalidArgumentValue = "INVALID_ARGUMENT_VALUE";
        public readonly static string UnmatchedArgument = "UNMATCHED_ARGUMENT";
        public readonly static string AmbiguousDefinition = "AMBIGUOUS_DEFINITION";
        public readonly static string DefinitionNotFound = "DEFINITION_NOT_DEFINED";
        public readonly static string DirectiveNotFound = "DIRECTIVE_NOT_DEFINED";
        public readonly static string AmbiguousDirective = "AMBIGUOUS_DIRECTIVE_DEFINITION";
        public readonly static string DirectiveNotRepeatable = "DIRECTIVE_NOT_REPEATABLE";
        public readonly static string DirectiveLocationNotValid = "INVALID_DIRECTIVE_LOCATION";
        public readonly static string OperationNotFound = "OPERATION_NOT_DEFINED";
        public readonly static string MultipleOperations = "MULTIPLE_OPERATIONS_FOUND";
        public readonly static string SubscriptionRootFieldMissing = "SUBSCRIPTION_ROOT_FIELD_MISSING";
        public readonly static string InvalidAnonymousOperation = "INVALID_ANONYMOUS_OPERATION";
        public readonly static string InvalidFragmentSpread = "INVALID_FRAGMENT_SPREAD";
        public readonly static string FragmentSpreadFormsCycle = "RECURSIVE_FRAGMENT_DETECTED";
        public readonly static string FragmentSpreadNotComposite = "FRAGMENT_SPREAD_NOT_COMPOSITE";
        public readonly static string FragmentNotReferenced = "FRAGMENT_NOT_REFERENCED";
        public readonly static string IntrospectionNotValid = "INTROSPECTION_NOT_VALID";
        public readonly static string InvalidName = "INVALID_NAME";
        public readonly static string FieldNotFound = "FIELD_NOT_FOUND";
        public readonly static string NullabilityViolation = "NULLABILITY_VIOLATION";
        public readonly static string ValueNotCoercible = "VALUE_NOT_COERCIBLE";
        public readonly static string ResolverNotDefined = "NO_RESOLVER_DEFINED";
    }
}