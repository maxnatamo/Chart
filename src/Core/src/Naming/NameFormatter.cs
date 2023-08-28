using System.Globalization;

using static Chart.Core.NameFormattingBehaviour;

namespace Chart.Core
{
    public interface INameFormatter
    {
        /// <summary>
        /// Get the options for the formatter.
        /// </summary>
        NameFormattingBehaviour GetOptions();

        /// <summary>
        /// Set the options for the formatter.
        /// </summary>
        /// <param name="options">The new options for the formatter.</param>
        void SetOptions(NameFormattingBehaviour options);

        /// <summary>
        /// Format a type name into a GraphQL name.
        /// </summary>
        string FormatTypeName(string name);

        /// <summary>
        /// Format an enum type name into a GraphQL name.
        /// </summary>
        string FormatEnumName(string name);

        /// <summary>
        /// Format a type's name into a GraphQL name.
        /// </summary>
        string GetTypeName(Type type);

        /// <summary>
        /// Get the formatted GraphQL name of a generic type.
        /// </summary>
        /// <param name="genericType">The generic type to get the name from.</param>
        string GetGenericsName(Type genericType);
    }

    public class NameFormatter : INameFormatter
    {
        private NameFormattingBehaviour _behaviour;

        public NameFormatter()
            => this._behaviour = PascalCase | RemoveAsyncPostfix | RemoveGetPrefix;

        public NameFormattingBehaviour GetOptions()
            => this._behaviour;

        public void SetOptions(NameFormattingBehaviour options)
            => this._behaviour = options;

        public virtual string FormatTypeName(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                return string.Empty;
            }

            foreach(NameFormattingBehaviour flag in Enum.GetValues<NameFormattingBehaviour>())
            {
                if(!this._behaviour.HasFlag(flag))
                {
                    continue;
                }

                name = flag switch
                {
                    PascalCase => this.HandlePascalCase(name),
                    CamelCase => this.HandleCamelCase(name),
                    RemoveAsyncPostfix => this.HandleAsyncPostfix(name),
                    RemoveGetPrefix => this.HandleGetPrefix(name),

                    _ => throw new InvalidDataException($"Unhandled NameFormattingBehaviour-flag: {flag}")
                };
            }

            return name;
        }

        public virtual string FormatEnumName(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                return string.Empty;
            }

            string _name = string.Empty;

            foreach(char c in name.ToCharArray())
            {
                _name += char.IsUpper(c)
                    ? "_" + c
                    : c;
            }

            return _name.Trim('_').ToUpper();
        }

        public virtual string GetTypeName(Type type)
            => this.FormatTypeName(type.Name);

        public virtual string GetGenericsName(Type genericType)
        {
            if(!genericType.IsGenericType)
            {
                return this.HandleCamelCase(genericType.Name);
            }

            string[] typeArgumentNames = genericType
                .GetGenericArguments()
                .Select(this.GetGenericsName)
                .ToArray();

            string genericTypeName = genericType.GetGenericTypeDefinition().Name;
            genericTypeName = genericTypeName[..genericTypeName.IndexOf('`')];

            string typeArgumentNameExtension = string.Join("And", typeArgumentNames);

            return $"{genericTypeName}Of{typeArgumentNameExtension}";
        }

        /// <summary>
        /// Format a name into pascal-case.
        /// </summary>
        /// <remarks>
        /// If the length of the string is 1 or less, the method does nothing.
        /// </remarks>
        protected virtual string HandlePascalCase(string name)
        {
            if(name.Length <= 1)
            {
                return name;
            }

            return char.ToLowerInvariant(name[0]) + name[1..];
        }

        /// <summary>
        /// Format a name into camel-case.
        /// </summary>
        /// <remarks>
        /// If the length of the string is 1 or less, the method does nothing.
        /// </remarks>
        protected virtual string HandleCamelCase(string name)
        {
            if(name.Length <= 1)
            {
                return name;
            }

            return char.ToUpperInvariant(name[0]) + name[1..];
        }

        /// <summary>
        /// Remove the 'Async'-postfix from names.
        /// </summary>
        protected virtual string HandleAsyncPostfix(string name)
        {
            if(name.Length <= 5 || !name.EndsWith("Async", ignoreCase: true, culture: CultureInfo.InvariantCulture))
            {
                return name;
            }

            return name[..^5];
        }

        /// <summary>
        /// Remove the 'Get'-prefix from names.
        /// </summary>
        protected virtual string HandleGetPrefix(string name)
        {
            if(name.Length <= 3 || !name.StartsWith("Get", ignoreCase: true, culture: CultureInfo.InvariantCulture))
            {
                return name;
            }

            return name[3..];
        }
    }
}