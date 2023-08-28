namespace Chart.Core
{
    public class FieldNotFoundException : GraphException
    {
        public FieldNotFoundException(string objectName, string fieldName) =>
            this.Error = new ErrorBuilder()
                .SetCode(ErrorCodes.FieldNotFound)
                .SetMessage($"Field '{fieldName}' was not found on type '{objectName}' ")
                .Build();
    }
}