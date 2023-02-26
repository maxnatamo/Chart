namespace Chart.Core.Parser
{
    [System.AttributeUsage(
        System.AttributeTargets.Property |
        System.AttributeTargets.Field |
        System.AttributeTargets.Method
    )]
    public class GraphIgnoreAttribute : Attribute
    {
        public GraphIgnoreAttribute()  
        {  
            
        }
    }
}