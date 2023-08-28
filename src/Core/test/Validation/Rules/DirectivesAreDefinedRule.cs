using Chart.Core.Validation;
using Chart.Language.SyntaxTree;

namespace Chart.Core.Tests.Validation.Rules.DirectivesAreDefinedRuleTests
{
    public class ValidationTests : SchemaValidationTestBase
    {
        public ValidationTests()
            : base(builder => builder.AddValidationVisitor<DirectivesAreDefinedRule>())
        { }

        [Fact]
        public async Task Enter_RaisesNoErrors_GivenTypeWithoutDirectives()
        {
            SchemaSourceString source = new()
            {
                Schema = "type Query { }"
            };

            await this.ExpectNoErrorsAsync(source);
        }

        [Fact]
        public async Task Enter_RaisesNoErrors_GivenExistingDirective()
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
        public async Task Enter_RaisesNoErrors_GivenImplicitDirective()
        {
            SchemaSourceString source = new()
            {
                Schema = "type Query @deprecated { }"
            };

            await this.ExpectNoErrorsAsync(source);
        }

        [Fact]
        public async Task Enter_RaisesErrors_GivenNonExistingDirective()
        {
            SchemaSourceString source = new()
            {
                Schema = "type Query @test { }"
            };

            await this.ExpectErrorsAsync(
                source,
                error => error.Code.Should().Be(ErrorCodes.DirectiveNotFound));
        }
    }
}