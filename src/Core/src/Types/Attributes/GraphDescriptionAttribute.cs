using static System.AttributeTargets;

namespace Chart.Core
{
    [AttributeUsage(Class | Property | Field | Struct | Method | Parameter | AttributeTargets.Enum)]
    public class GraphDescriptionAttribute : Attribute
    {
        public readonly string Description;

        public GraphDescriptionAttribute(string description)
            => this.Description = description;
    }
}