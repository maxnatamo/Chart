using Chart.Core.Validation;
using Chart.Language.SyntaxTree;

namespace Chart.Core.Tests.Validation.Rules.SubscriptionSingleRootFieldRuleTests
{
    public class ValidationTests : DocumentValidationTestBase
    {
        public ValidationTests()
            : base(builder => builder.AddValidationVisitor<SubscriptionSingleRootFieldRule>())
        { }

        [Fact]
        public async Task Enter_RaisesNoErrors_GivenSubscriptionWithSingleField()
        {
            DocumentSourceString source = new()
            {
                Query = "subscription { test }"
            };

            await this.ExpectNoErrorsAsync(source);
        }

        [Fact]
        public async Task Enter_RaisesNoErrors_GivenSubscriptionWithNestedField()
        {
            DocumentSourceString source = new()
            {
                Query = "subscription { newMessage { body sender } }"
            };

            await this.ExpectNoErrorsAsync(source);
        }

        [Fact]
        public async Task Enter_RaisesError_GivenSubscriptionWithMultipleField()
        {
            DocumentSourceString source = new()
            {
                Query = "subscription { dog cat }"
            };

            await this.ExpectErrorsAsync(source, error => error.Code.Should().Be(ErrorCodes.SubscriptionRootFieldMissing));
        }

        [Fact]
        public async Task Enter_RaisesNoErrors_GivenSubscriptionWithFragmentOfSingleField()
        {
            DocumentSourceString source = new()
            {
                Query = @"
                fragment newMessageFields on Subscription {
                    newMessage {
                        body
                        sender
                    }
                }

                subscription { ...newMessageFields }"
            };

            await this.ExpectNoErrorsAsync(source);
        }

        [Fact]
        public async Task Enter_RaisesError_GivenSubscriptionWithFragmentOfMultipleFields()
        {
            DocumentSourceString source = new()
            {
                Query = @"
                subscription {
                    newMessage {
                        body
                        sender
                    }
                    disallowedSecondRootField
                }"
            };

            await this.ExpectErrorsAsync(source, error => error.Code.Should().Be(ErrorCodes.SubscriptionRootFieldMissing));
        }

        [Fact]
        public async Task Enter_RaisesError_GivenSubscriptionWithNestedFragmentOfMultipleFields()
        {
            DocumentSourceString source = new()
            {
                Query = @"
                fragment multipleSubscriptions on Subscription {
                    newMessage {
                        body
                        sender
                    }
                    disallowedSecondRootField
                }

                subscription { ...multipleSubscriptions }"
            };

            await this.ExpectErrorsAsync(source, error => error.Code.Should().Be(ErrorCodes.SubscriptionRootFieldMissing));
        }

        [Fact]
        public async Task Enter_RaisesError_GivenSubscriptionWithNestedInlineFragmentOfMultipleFields()
        {
            DocumentSourceString source = new()
            {
                Query = @"
                subscription {
                    ... on Subscription {
                        newMessage {
                            body
                            sender
                        }
                        disallowedSecondRootField
                    }
                }"
            };

            await this.ExpectErrorsAsync(source, error => error.Code.Should().Be(ErrorCodes.SubscriptionRootFieldMissing));
        }

        [Fact]
        public async Task Enter_RaisesError_GivenSubscriptionWithIntrospectionField()
        {
            DocumentSourceString source = new()
            {
                Query = "subscription { __typename }"
            };

            await this.ExpectErrorsAsync(source, error => error.Code.Should().Be(ErrorCodes.IntrospectionNotValid));
        }
    }
}