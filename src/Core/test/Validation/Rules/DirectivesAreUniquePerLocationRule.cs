using Chart.Core.Validation;
using Chart.Language.SyntaxTree;

namespace Chart.Core.Tests.Validation.Rules.DirectivesAreUniquePerLocationRuleTests
{
    public class ValidationTests : SchemaValidationTestBase
    {
        public ValidationTests()
            : base(builder => builder.AddValidationVisitor<DirectivesAreUniquePerLocationRule>())
        { }

        [Fact]
        public async Task Enter_RaisesNoErrors_GivenSingleDirective()
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
        public async Task Enter_RaisesNoErrors_GivenSingleRepeatableDirective()
        {
            SchemaSourceString source = new()
            {
                Schema = @"
                directive @test repeatable on OBJECT
                type Query @test { }"
            };

            await this.ExpectNoErrorsAsync(source);
        }

        [Fact]
        public async Task Enter_RaisesError_GivenMultipleDirectives()
        {
            SchemaSourceString source = new()
            {
                Schema = @"
                directive @test on OBJECT
                type Query @test @test { }"
            };

            await this.ExpectErrorsAsync(
                source,
                error => error.Code.Should().Be(ErrorCodes.DirectiveNotRepeatable));
        }

        [Fact]
        public async Task Enter_RaisesNoErrors_GivenMultipleRepeatableDirectives()
        {
            SchemaSourceString source = new()
            {
                Schema = @"
                directive @test repeatable on OBJECT
                type Query @test @test { }"
            };

            await this.ExpectNoErrorsAsync(source);
        }
    }
}