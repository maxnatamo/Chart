namespace Chart.Core
{
    public class ArgumentMismatchException : GraphException
    {
        public ArgumentMismatchException(string message) =>
            this.Error = new ErrorBuilder()
                .SetCode(ErrorCodes.InvalidArgumentValue)
                .SetMessage(message)
                .Build();
    }
}