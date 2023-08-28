namespace Chart.Core
{
    public interface ITypeConverter
    {
        /// <summary>
        /// Attempt to convert the given input, <paramref name="input" /> from type <paramref name="from" /> to type <paramref name="to" />.
        /// </summary>
        /// <param name="from">The type to convert from.</param>
        /// <param name="to">The type to convert to.</param>
        /// <param name="input">The value to convert.</param>
        /// <param name="output">If the method returns <see langword="true" />, contains the converted value. Otherwise, <see langword="null" />.</param>
        /// <returns><see langword="true" />, if the converstion was successful. Otherwise, <see langword="false" />.</returns>
        bool TryConvertValue(Type from, Type to, object? input, out object? output);

        /// <summary>
        /// Convert the given input, <paramref name="input" /> from type <paramref name="from" /> to type <paramref name="to" />.
        /// </summary>
        /// <param name="from">The type to convert from.</param>
        /// <param name="to">The type to convert to.</param>
        /// <param name="input">The value to convert.</param>
        /// <returns>The converted value.</returns>
        object? ConvertValue(Type from, Type to, object? input);
    }

    public partial class TypeConverter : ITypeConverter
    {
        /// <summary>
        /// Delegate for change the type of a value.
        /// </summary>
        /// <param name="value">The value to change.</param>
        /// <returns>The new value, with a different type.</returns>
        private delegate object ChangeTypeDelegate(object value);

        /// <inheritdoc cref="ChangeTypeDelegate" />
        private delegate TTo ChangeTypeDelegate<TFrom, TTo>(TFrom value);

        private readonly Dictionary<(Type, Type), ChangeTypeDelegate> _conversionMap;

        public TypeConverter()
        {
            this._conversionMap = new Dictionary<(Type, Type), ChangeTypeDelegate>();

            this.RegisterDefaultMappers();
        }

        /// <inheritdoc />
        public bool TryConvertValue(Type from, Type to, object? input, out object? output)
        {
            ArgumentNullException.ThrowIfNull(from, nameof(from));
            ArgumentNullException.ThrowIfNull(to, nameof(to));

            if(this._conversionMap.TryGetValue((from, to), out ChangeTypeDelegate? mapper))
            {
                if(input is null)
                {
                    output = to.IsValueType ? default : null;
                    return true;
                }

                if(from == to)
                {
                    output = input;
                    return true;
                }

                if(to.IsAssignableTo(from))
                {
                    output = input;
                    return true;
                }

                try
                {
                    output = mapper(input);
                    return true;
                }
                catch(Exception)
                {
                    output = null;
                    return false;
                }
            }

            output = null;
            return false;
        }

        /// <inheritdoc />
        public object? ConvertValue(Type from, Type to, object? input)
        {
            ArgumentNullException.ThrowIfNull(from, nameof(from));
            ArgumentNullException.ThrowIfNull(to, nameof(to));

            if(!this.TryConvertValue(from, to, input, out object? output))
            {
                throw new NotSupportedException($"Value '{input?.GetType().Name ?? "null"}' could not be converted from '{from}' to '{to}.'");
            }

            return output;
        }

        private void RegisterMapper<TFrom, TTo>(ChangeTypeDelegate<TFrom, TTo> mapper)
        {
            ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));

            Type from = typeof(TFrom);
            Type to = typeof(TTo);

            if(this._conversionMap.ContainsKey((from, to)))
            {
                throw new ArgumentException($"A mapper with the same type signature is already registered ({from.Name} -> {to.Name})");
            }

            this._conversionMap.Add((from, to), value => (TTo) mapper((TFrom) value)!);
        }
    }
}