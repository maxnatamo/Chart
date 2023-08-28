using Chart.Core.Validation;
using Chart.Language.SyntaxTree;

namespace Chart.Core.Tests.Validation.Rules.LoneAnonymousOperationRuleTests
{
    public class ValidationTests : DocumentValidationTestBase
    {
        public ValidationTests()
            : base(builder => builder.AddValidationVisitor<LoneAnonymousOperationRule>())
        { }

        [Fact]
        public async Task Enter_RaisesNoErrors_GivenSingleAnonymousQuery()
        {
            DocumentSourceString source = new()
            {
                Query = "{ name }"
            };

            await this.ExpectNoErrorsAsync(source);
        }

        [Fact]
        public async Task Enter_RaisesNoErrors_GivenSingleUnnamedQuery()
        {
            DocumentSourceString source = new()
            {
                Query = "query { name }"
            };

            await this.ExpectNoErrorsAsync(source);
        }

        [Fact]
        public async Task Enter_RaisesError_GivenAnonymousQueryWithNamedQuery()
        {
            DocumentSourceString source = new()
            {
                Query = @"
                {
                    name
                }

                query getName {
                    name
                }"
            };

            await this.ExpectErrorsAsync(source, error => error.Code.Should().Be(ErrorCodes.InvalidAnonymousOperation));
        }

        [Fact]
        public async Task Enter_RaisesNoErrors_GivenSingleNamedQuery()
        {
            DocumentSourceString source = new()
            {
                Query = "query getAllUsers { name }"
            };

            await this.ExpectNoErrorsAsync(source);
        }

        [Fact]
        public async Task Enter_RaisesErrors_GivenMultipleAnonymousQueries()
        {
            DocumentSourceString source = new()
            {
                Query = @"
                {
                    name
                }

                {
                    name
                }"
            };

            await this.ExpectErrorsAsync(source,
                error => error.Code.Should().Be(ErrorCodes.InvalidAnonymousOperation),
                error => error.Code.Should().Be(ErrorCodes.InvalidAnonymousOperation));
        }
    }
}