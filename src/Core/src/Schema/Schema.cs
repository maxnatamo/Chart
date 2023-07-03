namespace Chart.Core
{
    public interface ISchema
    {
        
    }

    public class Schema : ISchema
    {
        private List<DirectiveType> Types { get; } = new List<DirectiveType>();

        private List<DirectiveType> DirectiveTypes { get; } = new List<DirectiveType>();

        public Schema()
        {
            
        }
    }
}