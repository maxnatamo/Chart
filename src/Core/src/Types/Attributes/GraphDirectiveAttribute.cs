using System.Collections.ObjectModel;

using Chart.Language.SyntaxTree;

using static System.AttributeTargets;

namespace Chart.Core
{
    [AttributeUsage(Class | Property | Field | Struct | Method | Parameter | AttributeTargets.Enum)]
    public class GraphDirectiveAttribute : Attribute
    {
        public readonly GraphName Name;

        public readonly IReadOnlyDictionary<string, object?>? Arguments = null;

        public GraphDirectiveAttribute(string name)
            => this.Name = new GraphName(name);

        public GraphDirectiveAttribute(string name, params object?[] arguments)
            : this(name)
        {
            if(arguments.Length % 2 != 0)
            {
                throw new ArgumentException($"Directive attribute '{this.Name}' does not have an even number of parameters.");
            }

            Dictionary<string, object?> _arguments = new();

            for(int i = 0; i < arguments.Length; i += 2)
            {
                object? key = arguments[i];

                if(key is not string _key)
                {
                    throw new ArgumentException($"Directive attribute '{this.Name}' argument name is not a string. Found '{key?.GetType().Name ?? "null"}'");
                }

                _arguments.Add(_key, arguments[i + 1]);
            }

            this.Arguments = new ReadOnlyDictionary<string, object?>(_arguments);
        }
    }
}