namespace Chart.Core
{
    public class ResolverNotDefinedException : GraphException
    {
        public ResolverNotDefinedException(string objectName, string fieldName) =>
            this.Error = new ErrorBuilder()
                .SetCode(ErrorCodes.ResolverNotDefined)
                .SetMessage(
                    $"Field '{fieldName}' on type '{objectName}' doesn't have any resolver. " +
                    "To explicitly set a resolver, use the .Resolve() in the Configure method on the type.")
                .Build();

        public ResolverNotDefinedException(string fieldName) =>
            this.Error = new ErrorBuilder()
                .SetCode(ErrorCodes.ResolverNotDefined)
                .SetMessage(
                    $"Field '{fieldName}' doesn't have any resolver. " +
                    "To explicitly set a resolver, use the .Resolve() in the Configure method on the type.")
                .Build();
    }
}