using Chart.Core.Validation;
using Chart.Language.SyntaxTree;

namespace Chart.Core.Tests.Validation.Rules.ReservedNameTests
{
    public class ValidationTests : SchemaValidationTestBase
    {
        public ValidationTests()
            : base(builder => builder.AddValidationVisitor<ReservedNameRule>())
        { }

        [Theory]
        [InlineData("query", false)]
        [InlineData("_test", false)]
        [InlineData("__test", true)]
        [InlineData("__schema", false)]
        [InlineData("__SCHema", true)]
        [InlineData("__type", false)]
        [InlineData("__field", false)]
        [InlineData("__inputvalue", false)]
        [InlineData("__enumvalue", false)]
        [InlineData("__directive", false)]
        [InlineData("__TEST", true)]
        [InlineData("__NotLegal", true)]
        [InlineData("a__Legal", false)]
        public async Task Enter_ReturnsCorrectResult_GivenName(string name, bool shouldHaveError)
        {
            SchemaSourceString source = new()
            {
                Schema = $"type {name} {{ test: Int }}"
            };

            if(shouldHaveError)
            {
                await this.ExpectErrorsAsync(source, error => error.Code.Should().Be(ErrorCodes.InvalidName));
            }
            else
            {
                await this.ExpectNoErrorsAsync(source);
            }
        }
    }
}