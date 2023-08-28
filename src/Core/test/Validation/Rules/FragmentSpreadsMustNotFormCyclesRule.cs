using Chart.Core.Validation;

namespace Chart.Core.Tests.Validation.Rules.FragmentSpreadsMustNotFormCyclesRuleTests
{
    public class ValidationTests : DocumentValidationTestBase
    {
        public ValidationTests()
            : base(builder => builder.AddValidationVisitor<FragmentSpreadsMustNotFormCyclesRule>())
        { }

        [Fact]
        public async Task Enter_RaisesNoErrors_GivenFragment()
        {
            DocumentSourceString source = new()
            {
                Query = @"
                    fragment nameFragment on Dog {
                        name
                    }"
            };

            await this.ExpectNoErrorsAsync(source);
        }

        [Fact]
        public async Task Enter_RaisesNoErrors_GivenNestedFragments()
        {
            DocumentSourceString source = new()
            {
                Query = @"
                    fragment petFragment on Pet {
                        name
                        ...driversLicenseFragment
                    }

                    fragment driversLicenseFragment on Dog {
                        license
                    }"
            };

            await this.ExpectNoErrorsAsync(source);
        }

        [Fact]
        public async Task Enter_RaisesError_GivenCyclicFragment()
        {
            DocumentSourceString source = new()
            {
                Query = @"
                    fragment nameFragment on Dog {
                        name
                        ...barkVolumeFragment
                    }

                    fragment barkVolumeFragment on Dog {
                        barkVolume
                        ...nameFragment
                    }"
            };

            await this.ExpectErrorsAsync(source,
                error => error.Code.Should().Be(ErrorCodes.FragmentSpreadFormsCycle),
                error => error.Code.Should().Be(ErrorCodes.FragmentSpreadFormsCycle));
        }

        [Fact]
        public async Task Enter_RaisesError_GivenCyclicInlineFragment()
        {
            DocumentSourceString source = new()
            {
                Query = @"
                    fragment nameFragment on Dog {
                        name
                        owner {
                            ...nameFragment
                        }
                    }

                    query {
                        dogs {
                            ... on Dogs {
                                ...nameFragment
                            }
                        }
                    }"
            };

            await this.ExpectErrorsAsync(source,
                error => error.Code.Should().Be(ErrorCodes.FragmentSpreadFormsCycle));
        }
    }
}