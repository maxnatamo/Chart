using System.Diagnostics.CodeAnalysis;

namespace Chart.Core
{
    public interface IOptional
    {
        /// <summary>
        /// The underlying value.
        /// </summary>
        object? Value { get; }

        /// <summary>
        /// Whether the instance has a value associated.
        /// </summary>
        [MemberNotNullWhen(true, nameof(Value))]
        bool HasValue { get; }

        /// <summary>
        /// Whether the instance has an empty value.
        /// </summary>
        [MemberNotNullWhen(false, nameof(Value))]
        bool IsEmpty { get; }
    }

    public readonly struct Optional<T> : IOptional, IEquatable<Optional<T>>
    {
        /// <inheritdoc cref="IOptional.Value" /> 
        public T? Value { get; }

        /// <inheritdoc /> 
        readonly object? IOptional.Value => this.Value;

        /// <inheritdoc /> 
        [MemberNotNullWhen(true, nameof(Value))]
        public bool HasValue { get; }

        /// <inheritdoc /> 
        [MemberNotNullWhen(false, nameof(Value))]
        public bool IsEmpty => !this.HasValue;

        /// <summary>
        /// Initializes a new instance of <see cref="Optional{T}"/> with the given value.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public Optional(T? value, bool? hasValue = null)
        {
            this.Value = value;
            this.HasValue = hasValue ?? this.Value is null;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Optional{T}"/> with an empty value.
        /// </summary>
        /// <param name="defaultValue">The default value to set as the value.</param>
        public static Optional<T> Empty(T? defaultValue = default)
            => new(defaultValue, false);

        /// <inheritdoc />
        public override string ToString()
            => this.HasValue ? this.Value?.ToString() ?? "null" : "(invalid)";

        /// <inheritdoc />
        public bool Equals(Optional<T> other)
        {
            if(!this.HasValue && !other.HasValue)
            {
                return true;
            }

            if(this.HasValue != other.HasValue)
            {
                return false;
            }

            return Equals(this.Value, other.Value);
        }

        /// <inheritdoc />
        public override bool Equals(object? other)
        {
            if(other is null)
            {
                return this.IsEmpty;
            }

            return other is Optional<T> n && this.Equals(n);
        }

        /// <inheritdoc />
        public override int GetHashCode()
            => this.HasValue ? this.Value.GetHashCode() : 0;

        public static bool operator ==(Optional<T> left, Optional<T> right)
            => left.Equals(right);

        public static bool operator !=(Optional<T> left, Optional<T> right)
            => !left.Equals(right);

        public static implicit operator Optional<T>(T value)
            => new(value);

        public static implicit operator T?(Optional<T> optional)
            => optional.Value;
    }
}