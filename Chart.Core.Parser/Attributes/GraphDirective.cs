namespace Chart.Core.Parser
{
    [System.AttributeUsage(
        AttributeTargets.Property |
        AttributeTargets.Field |
        AttributeTargets.Class |
        AttributeTargets.Struct |
        AttributeTargets.Method |
        AttributeTargets.Parameter
    , AllowMultiple = true)]
    public class GraphDirectiveAttribute : Attribute
    {
        public string Name { get; }

        public object? Arguments { get; }

        public GraphDirectiveAttribute(string name, object? arguments = null)
        {  
            this.Name = name;
            this.Arguments = arguments;
        }
    }
}