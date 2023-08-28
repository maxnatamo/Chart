using Chart.Core.Validation;

namespace Chart.Core.Tests.Validation.Rules.FragmentSpreadTypeExistenceRuleTests
{
    public class ValidationTests : DocumentValidationTestBase
    {
        public ValidationTests()
            : base(builder => builder.AddValidationVisitor<FragmentSpreadTypeExistenceRule>())
        { }

        [Fact]
        public async Task Enter_RaisesNoErrors_GivenFragmentWithExistingType()
        {
            DocumentSourceString source = new()
            {
                Query = @"
                    fragment nameFragment on Dog {
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
        public async Task Enter_RaisesNoErrors_GivenInlineFragmentWithExistingTypeOnCondition()
        {
            DocumentSourceString source = new()
            {
                Query = @"
                    fragment inlineFragment on Dog {
                        ... on Dog {
                            name
                        }
                    }",

                Schema = @"
                    type Dog {
                        name: String!
                    }"
            };

            await this.ExpectNoErrorsAsync(source);
        }

        [Fact]
        public async Task Enter_RaisesNoErrors_GivenInlineFragmentWithExistingType()
        {
            DocumentSourceString source = new()
            {
                Query = @"
                    fragment inlineFragment on Dog {
                        ... @include(if: true) {
                            name
                        }
                    }",

                Schema = @"
                    type Dog {
                        name: String!
                    }"
            };

            await this.ExpectNoErrorsAsync(source);
        }

        [Fact]
        public async Task Enter_RaisesError_GivenFragmentWithNonExistingType()
        {
            DocumentSourceString source = new()
            {
                Query = @"
                    fragment notOnExistingType on NotInSchema {
                        name
                    }"
            };

            await this.ExpectErrorsAsync(source, error => error.Code.Should().Be(ErrorCodes.InvalidFragmentSpread));
        }

        [Fact]
        public async Task Enter_RaisesError_GivenInlineFragmentWithNonExistingType()
        {
            DocumentSourceString source = new()
            {
                Query = @"
                    fragment inlineNotExistingType on Dog {
                        ... on NotInSchema {
                            name
                        }
                    }",

                Schema = @"
                    type Dog {
                        name: String!
                    }"
            };

            await this.ExpectErrorsAsync(source, error => error.Code.Should().Be(ErrorCodes.InvalidFragmentSpread));
        }
    }
}