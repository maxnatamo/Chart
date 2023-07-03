namespace Chart.Core
{
    public interface IDirectiveTypeDescriptor
    {
        /// <summary>
        /// Explicitly set the name of the directive in the schema.
        /// </summary>
        /// <param name="name">The new name for the directive.</param>
        /// <returns>The current descriptor, to allow for method chaining.</returns>
        IDirectiveTypeDescriptor Name(string name);

        /// <summary>
        /// Set the fields which the directive is available on.
        /// </summary>
        /// <param name="locations">The new list of locations.</param>
        /// <returns>The current descriptor, to allow for method chaining.</returns>
        IDirectiveTypeDescriptor Location(DirectoryLocation locations);

        /// <summary>
        /// Allow the directive to be repeatable.
        /// </summary>
        /// <returns>The current descriptor, to allow for method chaining.</returns>
        IDirectiveTypeDescriptor Repeatable(bool repeatable = true);
    }

    public class DirectiveTypeDescriptor : IDirectiveTypeDescriptor
    {
        private string directiveName { get; set; } = string.Empty;
        private DirectoryLocation? locations { get; set; } = null;
        private bool repeatable { get; set; } = false;

        public IDirectiveTypeDescriptor Name(string name)
        {
            this.directiveName = name;
            return this;
        }

        public IDirectiveTypeDescriptor Location(DirectoryLocation locations)
        {
            this.locations = locations;
            return this;
        }

        public IDirectiveTypeDescriptor Repeatable(bool repeatable = true)
        {
            this.repeatable = repeatable;
            return this;
        }
    }
}