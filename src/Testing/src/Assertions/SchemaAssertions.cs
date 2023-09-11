using Chart.Core;

using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace Chart.Testing.Assertions
{
    public class SchemaAssertions : ReferenceTypeAssertions<Schema, SchemaAssertions>
    {
        protected override string Identifier => "schema";

        public SchemaAssertions(Schema instance)
            : base(instance)
        { }

        public AndConstraint<SchemaAssertions> ContainType(
            string typeName, string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(!string.IsNullOrEmpty(typeName))
                .FailWith("You can't assert if a type exists if you don't pass a proper name")
                .Then
                .Given(() => this.Subject.GetDefinitions())
                .ForCondition(definitions => definitions.Any(definition => definition.Name.Equals(typeName)))
                .FailWith("Expected {context:schema} to contain {0}{reason}, but it wasn't found within the schema.",
                    _ => typeName);

            return new AndConstraint<SchemaAssertions>(this);
        }

        public AndConstraint<SchemaAssertions> ContainType(
            Type type, string because = "", params object[] becauseArgs)
            => this.ContainType(type.Name, because, becauseArgs);

        public AndConstraint<SchemaAssertions> ContainType<T>(
            string because = "", params object[] becauseArgs)
            => this.ContainType(typeof(T).Name, because, becauseArgs);

        public AndConstraint<SchemaAssertions> NotContainType(
            string typeName, string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(!string.IsNullOrEmpty(typeName))
                .FailWith("You can't assert if a type exists if you don't pass a proper name")
                .Then
                .Given(() => this.Subject.GetDefinitions())
                .ForCondition(definitions => !definitions.Any(definition => definition.Name.Equals(typeName)))
                .FailWith("Expected {context:schema} to not contain {0}{reason}, but it was found within the schema.",
                    _ => typeName);

            return new AndConstraint<SchemaAssertions>(this);
        }

        public AndConstraint<SchemaAssertions> NotContainType(
            Type type, string because = "", params object[] becauseArgs)
            => this.NotContainType(type.Name, because, becauseArgs);

        public AndConstraint<SchemaAssertions> NotContainType<T>(
            string because = "", params object[] becauseArgs)
            => this.NotContainType(typeof(T).Name, because, becauseArgs);
    }
}