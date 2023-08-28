using Chart.Core.Validation;

namespace Chart.Core.Tests.Validation.Rules.FragmentsMustBeUsedRuleTests
{
    public class ValidationTests : DocumentValidationTestBase
    {
        public ValidationTests()
            : base(builder => builder.AddValidationVisitor<FragmentsMustBeUsedRule>())
        { }

        [Fact]
        public async Task Enter_RaisesNoErrors_GivenReferencedFragment()
        {
            DocumentSourceString source = new()
            {
                Query = @"
                    fragment queryFragment on Query {
                        name
                    }

                    query {
                        ...queryFragment
                    }"
            };

            await this.ExpectNoErrorsAsync(source);
        }

        [Fact]
        public async Task Enter_RaisesError_GivenUnreferencedFragment()
        {
            DocumentSourceString source = new()
            {
                Query = @"
                    fragment nameFragment on Dog {
                        name
                    }

                    { dog { name } }"
            };

            await this.ExpectErrorsAsync(source,
                error => error.Code.Should().Be(ErrorCodes.FragmentNotReferenced));
        }
    }
}