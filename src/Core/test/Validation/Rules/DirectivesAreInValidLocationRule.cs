using Chart.Core.Validation;
using Chart.Language.SyntaxTree;

namespace Chart.Core.Tests.Validation.Rules.DirectivesAreInValidLocationRuleTests
{
    public class ValidationTests : SchemaValidationTestBase
    {
        public ValidationTests()
            : base(builder => builder.AddValidationVisitor<DirectivesAreInValidLocationRule>())
        { }

        [Fact]
        public async Task Enter_RaisesNoErrors_GivenSingleCorrectDirectiveLocation()
        {
            SchemaSourceString source = new()
            {
                Schema = @"
                directive @test on OBJECT
                type Query @test { }"
            };

            await this.ExpectNoErrorsAsync(source);
        }

        [Fact]
        public async Task Enter_RaisesNoErrors_GivenMultipleCorrectDirectiveLocation()
        {
            SchemaSourceString source = new()
            {
                Schema = @"
                directive @test on SCALAR | OBJECT
                type Query @test { }"
            };

            await this.ExpectNoErrorsAsync(source);
        }

        [Fact]
        public async Task Enter_RaisesError_GivenDirectiveInInvalidLocation()
        {
            SchemaSourceString source = new()
            {
                Schema = @"
                directive @test on SCALAR
                type Query @test { }"
            };

            await this.ExpectErrorsAsync(
                source,
                error => error.Code.Should().Be(ErrorCodes.DirectiveLocationNotValid));
        }
    }
}