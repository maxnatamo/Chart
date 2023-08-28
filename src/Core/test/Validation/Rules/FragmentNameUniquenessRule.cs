using Chart.Core.Validation;
using Chart.Language.SyntaxTree;

namespace Chart.Core.Tests.Validation.Rules.FragmentNameUniquenessRuleTests
{
    public class ValidationTests : DocumentValidationTestBase
    {
        public ValidationTests()
            : base(builder => builder.AddValidationVisitor<FragmentNameUniquenessRule>())
        { }

        [Fact]
        public async Task Enter_RaisesNoErrors_GivenSingleFragment()
        {
            DocumentSourceString source = new()
            {
                Query = "fragment fragmentOne on Dog { name }"
            };

            await this.ExpectNoErrorsAsync(source);
        }

        [Fact]
        public async Task Enter_RaisesNoErrors_GivenMultipleFragmentsWithDifferentNames()
        {
            DocumentSourceString source = new()
            {
                Query = @"
                fragment fragmentOne on Dog { name }
                fragment fragmentTwo on Dog { name }"
            };

            await this.ExpectNoErrorsAsync(source);
        }

        [Fact]
        public async Task Enter_RaisesError_GivenMultipleFragmentsWithSameName()
        {
            DocumentSourceString source = new()
            {
                Query = @"
                fragment fragmentOne on Dog { name }
                fragment fragmentOne on Dog { name }"
            };

            await this.ExpectErrorsAsync(
                source,
                error => error.Code.Should().Be(ErrorCodes.AmbiguousDefinition),
                error => error.Code.Should().Be(ErrorCodes.AmbiguousDefinition));
        }

        [Fact]
        public async Task Enter_RaisesError_GivenMultipleFragmentsWithSameNameAndDifferentType()
        {
            DocumentSourceString source = new()
            {
                Query = @"
                fragment fragmentOne on Dog { name }
                fragment fragmentOne on Cat { name }"
            };

            await this.ExpectErrorsAsync(
                source,
                error => error.Code.Should().Be(ErrorCodes.AmbiguousDefinition),
                error => error.Code.Should().Be(ErrorCodes.AmbiguousDefinition));
        }
    }
}