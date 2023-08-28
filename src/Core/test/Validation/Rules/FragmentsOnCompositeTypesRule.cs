using Chart.Core.Validation;

namespace Chart.Core.Tests.Validation.Rules.FragmentsOnCompositeTypesRuleTests
{
    public class ValidationTests : DocumentValidationTestBase
    {
        public ValidationTests()
            : base(builder => builder.AddValidationVisitor<FragmentsOnCompositeTypesRule>())
        { }

        [Fact]
        public async Task Enter_RaisesNoErrors_GivenFragmentOnUnionType()
        {
            DocumentSourceString source = new()
            {
                Query = @"
                    fragment fragOnUnion on CatOrDog {
                        ... on Dog {
                            name
                        }
                    }",

                Schema = @"
                    type Cat {
                        name: String!
                    }

                    type Dog {
                        name: String!
                    }

                    union CatOrDog = Cat | Dog"
            };

            await this.ExpectNoErrorsAsync(source);
        }

        [Fact]
        public async Task Enter_RaisesNoErrors_GivenFragmentOnInterfaceType()
        {
            DocumentSourceString source = new()
            {
                Query = @"
                    fragment fragOnInterface on Pet {
                        name
                    }",

                Schema = @"
                    interface Pet {
                        name: String!
                    }

                    type Cat implements Pet {
                        name: String!
                    }

                    type Dog implements Pet {
                        name: String!
                    }"
            };

            await this.ExpectNoErrorsAsync(source);
        }

        [Fact]
        public async Task Enter_RaisesNoErrors_GivenFragmentOnObjectType()
        {
            DocumentSourceString source = new()
            {
                Query = @"
                    fragment fragOnObject on Dog {
                        name
                    }",

                Schema = @"
                    type Dog {
                        name: String!
                    }"
            };

            await this.ExpectNoErrorsAsync(source);
        }

        [Fact]
        public async Task Enter_RaisesError_GivenFragmentOnScalarType()
        {
            DocumentSourceString source = new()
            {
                Query = @"
                    fragment fragOnScalar on Int {
                        value
                    }"
            };

            await this.ExpectErrorsAsync(source,
                error => error.Code.Should().Be(ErrorCodes.FragmentSpreadNotComposite));
        }

        [Fact]
        public async Task Enter_RaisesError_GivenInlineFragmentOnScalarType()
        {
            DocumentSourceString source = new()
            {
                Query = @"
                    fragment inlineFragOnScalar on Dog {
                        ... on Boolean {
                            somethingElse
                        }
                    }"
            };

            await this.ExpectErrorsAsync(source,
                error => error.Code.Should().Be(ErrorCodes.FragmentSpreadNotComposite));
        }
    }
}