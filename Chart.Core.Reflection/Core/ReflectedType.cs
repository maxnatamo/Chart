using System.Dynamic;

namespace Chart.Core.Reflection
{
    public class ReflectedType
    {
        private dynamic Data { get; set; }

        public ReflectedType()
        {
            this.Data = new ExpandoObject();
        }

        public bool ContainsKey(string name)
            => this.GetDictionary().ContainsKey(name);

        public object? GetValue(string name)
            => this.GetDictionary()[name];

        public void SetValue(string name, object? value)
            => this.GetDictionary()[name] = value;

        public void Remove(string name)
            => this.GetDictionary().Remove(name);

        public object? this[string key]
        {
            get => this.GetValue(key);
            set => this.SetValue(key, value);
        }

        public IDictionary<string, object?> GetDictionary()
            => this.Data;
    }
}