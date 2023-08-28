using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text;

namespace Chart.Core
{
    [Serializable]
    public class SchemaException : Exception
    {
        public readonly ReadOnlyCollection<Error> Errors;

        public SchemaException(params Error[] errors)
            : base(message: FormatErrors(errors))
            => this.Errors = new(errors);

        public SchemaException(IEnumerable<Error> errors)
            : this(errors: errors.ToArray())
        { }

        protected SchemaException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
            => this.Errors = new ReadOnlyCollection<Error>(Array.Empty<Error>());

        private static string FormatErrors(Error[] errors)
        {
            if(errors.Length <= 0)
            {
                return "An unexpected schema exception has occured.";
            }

            StringBuilder message = new();
            message.AppendLine("A schema exception has occured. For more details, see below:");
            message.AppendLine();

            for(int i = 0; i < errors.Length; i++)
            {
                message.AppendFormat("\t{0}. {1}", i + 1, errors[i].Message);
                message.AppendLine();
            }

            return message.ToString();
        }
    }
}