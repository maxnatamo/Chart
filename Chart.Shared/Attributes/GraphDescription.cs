namespace Chart.Shared.Attributes
{
    [System.AttributeUsage(
        AttributeTargets.Property |
        AttributeTargets.Field |
        AttributeTargets.Class |
        AttributeTargets.Struct |
        AttributeTargets.Method |
        AttributeTargets.Parameter
    )]
    public class GraphDescriptionAttribute : Attribute
    {
        public string Description { get; }

        public GraphDescriptionAttribute(string description)
        {
            this.Description = description;
        }
    }
}