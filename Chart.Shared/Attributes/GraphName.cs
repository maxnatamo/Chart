using Chart.Models.AST;

namespace Chart.Shared.Attributes
{
    [System.AttributeUsage(
        System.AttributeTargets.Class |
        System.AttributeTargets.Struct |
        System.AttributeTargets.Property |
        System.AttributeTargets.Field |
        System.AttributeTargets.Method |
        System.AttributeTargets.Parameter
    )]
    public class GraphNameAttribute : Attribute
    {
        public GraphName Name { get; protected set; }

        public GraphNameAttribute(string name)  
        {  
            this.Name = new GraphName(name);
        }
    }
}