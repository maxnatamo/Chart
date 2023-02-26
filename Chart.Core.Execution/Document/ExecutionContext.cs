namespace Chart.Core.Parser.Execution
{
    public class ExecutionContext
    {
        public string Query { get; set; } = default!;

        public object Data { get; set; } = default!;
    }
}