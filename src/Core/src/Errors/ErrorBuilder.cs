using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    /// <summary>
    /// Error structure for the response.
    /// </summary>
    /// <see href="https://spec.graphql.org/October2021/#sec-Errors.Error-result-format" />
    public class ErrorBuilder
    {
        private readonly Error _error;

        public ErrorBuilder()
        {
            this._error = new Error();
        }

        /// <summary>
        /// Set the message for the error.
        /// </summary>
        public ErrorBuilder SetMessage(string message)
        {
            this._error.Message = message;
            return this;
        }

        /// <summary>
        /// Set the code for the error.
        /// </summary>
        public ErrorBuilder SetCode(string? code)
        {
            this._error.Code = code;
            return this;
        }

        /// <summary>
        /// Set the reference for the error.
        /// </summary>
        public ErrorBuilder SetReference(string? reference)
        {
            this._error.Reference = reference;
            return this;
        }

        /// <summary>
        /// Add a path to the error.
        /// </summary>
        public ErrorBuilder AddPath(string path)
        {
            this._error.Path.Add(path);
            return this;
        }

        /// <summary>
        /// Add a path to the error.
        /// </summary>
        public ErrorBuilder AddPath(int index)
        {
            this._error.Path.Add(index);
            return this;
        }

        /// <summary>
        /// Set the path of the error.
        /// </summary>
        public ErrorBuilder SetPath(List<object> path)
        {
            this._error.Path = path;
            return this;
        }

        /// <summary>
        /// Add a location to the error.
        /// </summary>
        public ErrorBuilder AddLocation(GraphLocation? location)
        {
            if(location is null)
            {
                return this;
            }

            this._error.Locations.Add(location);
            return this;
        }

        /// <summary>
        /// Set the locations of the error.
        /// </summary>
        public ErrorBuilder SetLocations(List<GraphLocation> location)
        {
            this._error.Locations = location;
            return this;
        }

        /// <summary>
        /// Set an extension of the error.
        /// </summary>
        public ErrorBuilder SetExtension(string key, object? value)
        {
            this._error.Extensions ??= new Dictionary<string, object?>();
            this._error.Extensions[key] = value;
            return this;
        }

        /// <summary>
        /// Set an exception associated with the error.
        /// </summary>
        public ErrorBuilder SetException(Exception? exception)
        {
            this._error.Exception = exception;
            return this;
        }

        /// <summary>
        /// Set an extension of the error.
        /// </summary>
        public Error Build()
            => this._error;
    }
}