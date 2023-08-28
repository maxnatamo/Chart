using System.Text.Json.Serialization;

using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    /// <summary>
    /// Error structure for the response.
    /// </summary>
    /// <see href="https://spec.graphql.org/October2021/#sec-Errors.Error-result-format" />
    public class Error
    {
        /// <summary>
        /// The message of the error.
        /// </summary>
        public string Message { get; protected internal set; } = string.Empty;

        /// <summary>
        /// The location(s) of the error message.
        /// </summary>
        public List<GraphLocation> Locations { get; protected internal set; } = new List<GraphLocation>();

        /// <summary>
        /// Path to the path in the response, which caused the error.
        /// </summary>
        public List<object> Path { get; protected internal set; } = new List<object>();

        /// <summary>
        /// An optional error code for the error.
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public string? Code
        {
            get => (string?) this.Extensions.GetValueOrDefault("code");
            protected internal set => this.Extensions.Set("code", value);
        }

        /// <summary>
        /// If validation-error, the reference to the corresponding specification section.
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public string? Reference
        {
            get => (string?) this.Extensions.GetValueOrDefault("reference");
            protected internal set => this.Extensions.Set("reference", value);
        }

        /// <inheritdoc cref="Error.Exception" />
        private Exception? _exception { get; set; } = null;

        /// <summary>
        /// An optional exception associated with the error.
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public Exception? Exception
        {
            get => this._exception;
            protected internal set
            {
                if(value is null)
                {
                    this.Extensions.Remove("message");
                    this.Extensions.Remove("stackTrace");
                }
                else
                {
                    this.Extensions.Set("message", value?.Message);
                    this.Extensions.Set("stackTrace", value?.ToString());
                }

                this._exception = value;
            }
        }

        /// <summary>
        /// Optional extensions of the error message.
        /// </summary>
        private Dictionary<string, object?>? _extensions { get; set; } = null;

        /// <inheritdoc cref="Error._extensions" />
        public Dictionary<string, object?> Extensions
        {
            get => this._extensions ??= new Dictionary<string, object?>();
            protected internal set => this._extensions = value;
        }

        public Error WithLocation(GraphLocation location)
        {
            this.Locations.Add(location);
            return this;
        }

        public Error WithPath(IEnumerable<object> path)
        {
            this.Path = path.Reverse().ToList();
            return this;
        }
    }
}