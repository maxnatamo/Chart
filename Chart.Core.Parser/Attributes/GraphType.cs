namespace Chart.Core.Parser
{
    [System.AttributeUsage(
        System.AttributeTargets.Property |
        System.AttributeTargets.Field |
        System.AttributeTargets.Parameter
    )]
    public class GraphTypeAttribute : Attribute
    {
        public Type Type { get; }

        public GraphTypeAttribute(Type type)
        {  
            this.Type = type;
        }
    }
}