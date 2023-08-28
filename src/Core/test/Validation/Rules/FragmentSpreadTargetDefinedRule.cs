using Chart.Core.Validation;

namespace Chart.Core.Tests.Validation.Rules.FragmentSpreadTargetDefinedRuleTests
{
    public class ValidationTests : DocumentValidationTestBase
    {
        public ValidationTests()
            : base(builder => builder.AddValidationVisitor<FragmentSpreadTargetDefinedRule>())
        { }

        [Fact]
        public async Task Enter_RaisesNoErrors_GivenDefinedFragment()
        {
            DocumentSourceString source = new()
            {
                Query = @"
                    fragment wizardFragment on Human {
                        hasFireBolt
                    }

                    query {
                        population {
                        ...wizardFragment
                        }
                    }"
            };

            await this.ExpectNoErrorsAsync(source);
        }

        [Fact]
        public async Task Enter_RaisesError_GivenUndefinedFragment()
        {
            DocumentSourceString source = new()
            {
                Query = @"
                    query {
                        population {
                        ...wizardFragment
                        }
                    }"
            };

            await this.ExpectErrorsAsync(source, error => error.Code.Should().Be(ErrorCodes.DefinitionNotFound));
        }
    }
}