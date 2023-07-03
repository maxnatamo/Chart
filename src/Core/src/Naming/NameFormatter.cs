using System.Globalization;

namespace Chart.Core
{
    public interface INameFormatter
    {
        /// <summary>
        /// Set the options for the formatter.
        /// </summary>
        /// <param name="options">The new options for the formatter.</param>
        public void SetOptions(NameFormattingBehaviour options);

        /// <summary>
        /// Format a GraphQL name (field, type, etc.) using the set options.
        /// </summary>
        /// <param name="name">Name to format.</param>
        /// <returns>The formatted name.</returns>
        public string FormatName(string name);
    }

    public class NameFormatter : INameFormatter
    {
        private NameFormattingBehaviour _behaviour;

        public NameFormatter()
        {
            this._behaviour = NameFormattingBehaviour.PascalCase |
                              NameFormattingBehaviour.RemoveAsyncPostfix |
                              NameFormattingBehaviour.RemoveGetPrefix;
        }

        public void SetOptions(NameFormattingBehaviour options)
            => this._behaviour = options;

        public string FormatName(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                return string.Empty;
            }

            if(this._behaviour.HasFlag(NameFormattingBehaviour.RemoveAsyncPostfix))
            {
                name = this.RemoveAsyncPostfix(name);
            }

            if(this._behaviour.HasFlag(NameFormattingBehaviour.RemoveGetPrefix))
            {
                name = this.RemoveGetPrefix(name);
            }

            if(this._behaviour.HasFlag(NameFormattingBehaviour.PascalCase))
            {
                name = this.FormatPascalCase(name);
            }

            return name;
        }

        /// <summary>
        /// Format a name into pascal-case.
        /// </summary>
        /// <remarks>
        /// If the length of the string is 1 or less, the method does nothing.
        /// </remarks>
        protected string FormatPascalCase(string name)
        {
            if(name.Length <= 1)
            {
                return name;
            }

            return Char.ToLowerInvariant(name[0]) + name.Substring(1);
        }

        /// <summary>
        /// Remove the 'Async'-postfix from names.
        /// </summary>
        protected string RemoveAsyncPostfix(string name)
        {
            if(name.Length <= 5 || !name.EndsWith("Async", ignoreCase: true, culture: CultureInfo.InvariantCulture))
            {
                return name;
            }

            return name.Substring(0, name.Length - 5);
        }

        /// <summary>
        /// Remove the 'Get'-prefix from names.
        /// </summary>
        protected string RemoveGetPrefix(string name)
        {
            if(name.Length <= 3 || !name.StartsWith("Get", ignoreCase: true, culture: CultureInfo.InvariantCulture))
            {
                return name;
            }

            return name.Substring(3);
        }
    }
}