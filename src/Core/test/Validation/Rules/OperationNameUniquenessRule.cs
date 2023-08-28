using Chart.Core.Validation;
using Chart.Language.SyntaxTree;

namespace Chart.Core.Tests.Validation.Rules.OperationNameUniquenessRuleTests
{
    public class ValidationTests : DocumentValidationTestBase
    {
        public ValidationTests()
            : base(builder => builder.AddValidationVisitor<OperationNameUniquenessRule>())
        { }

        [Fact]
        public async Task Enter_RaisesNoErrors_GivenAnonymousQuery()
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
        public async Task Enter_RaisesNoErrors_GivenAnonymousQueryWithNamedQuery()
        {
            DocumentSourceString source = new()
            {
                Query = @"
                {
                    name
                }

                query getName {
                    name
                }",
            };

            await this.ExpectNoErrorsAsync(source);
        }

        [Fact]
        public async Task Enter_RaisesNoErrors_GivenSingleNamedQuery()
        {
            DocumentSourceString source = new()
            {
                Query = "query getAllUsers { user }"
            };

            await this.ExpectNoErrorsAsync(source);
        }

        [Fact]
        public async Task Enter_RaisesError_GivenMultipleNamedQueriesWithSameName()
        {
            DocumentSourceString source = new()
            {
                Query = @"
                query getAllUsers {
                    user
                }

                query getAllUsers {
                    user
                }"
            };

            await this.ExpectErrorsAsync(source,
                error => error.Code.Should().Be(ErrorCodes.MultipleOperations),
                error => error.Code.Should().Be(ErrorCodes.MultipleOperations));
        }

        [Fact]
        public async Task Enter_RaisesError_GivenMultipleNamedOperationsWithSameName()
        {
            DocumentSourceString source = new()
            {
                Query = @"
                query getAllUsers {
                    user
                }

                mutation getAllUsers {
                    user
                }"
            };

            await this.ExpectErrorsAsync(source,
                error => error.Code.Should().Be(ErrorCodes.MultipleOperations),
                error => error.Code.Should().Be(ErrorCodes.MultipleOperations));
        }
    }
}