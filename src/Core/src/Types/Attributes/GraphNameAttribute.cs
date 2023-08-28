using Chart.Language.SyntaxTree;

using static System.AttributeTargets;

namespace Chart.Core
{
    [AttributeUsage(Class | Property | Field | Struct | Method | Parameter | AttributeTargets.Enum)]
    public class GraphNameAttribute : Attribute
    {
        public readonly GraphName Name;

        public GraphNameAttribute(string name)
        {
            if(!GraphName.Verify(name))
            {
                throw new ArgumentException($"Name '{name}' is not a valid GraphQL name.");
            }

            this.Name = new GraphName(name);
        }
    }
}