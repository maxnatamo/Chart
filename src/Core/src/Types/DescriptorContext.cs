using Microsoft.Extensions.Options;

namespace Chart.Core
{
    public class DescriptorContext
    {
        public TypeRegistry TypeRegistry { get; internal set; }

        public DescriptorContext(IOptionsMonitor<TypeRegistry> typeRegistryMonitor)
        {
            this.TypeRegistry = typeRegistryMonitor.CurrentValue;
            typeRegistryMonitor.OnChange(reg => this.TypeRegistry = reg);
        }
    }
}