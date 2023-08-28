using Chart.Core.Validation;

using Microsoft.Extensions.DependencyInjection;

namespace Chart.Core
{
    public static partial class RequestServiceExtensions
    {
        public static RequestServiceBuilder AddValidation(this RequestServiceBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder, nameof(builder));

            return builder
                .AddValidationCore()
                .AddDefaultValidations();
        }

        public static RequestServiceBuilder AddValidationCore(this RequestServiceBuilder builder)
        {
            builder.Services
                .AddScoped<IValidationExecutor, ValidationExecutor>()
                .AddScoped<IValidationHandler, DefaultValidationHandler>();

            return builder;
        }

        public static RequestServiceBuilder AddDefaultValidations(this RequestServiceBuilder builder) => builder
            .AddValidationVisitor<DirectivesAreDefinedRule>()
            .AddValidationVisitor<DirectivesAreInValidLocationRule>()
            .AddValidationVisitor<DirectivesAreUniquePerLocationRule>()
            .AddValidationVisitor<FragmentNameUniquenessRule>()
            .AddValidationVisitor<FragmentsMustBeUsedRule>()
            .AddValidationVisitor<FragmentsOnCompositeTypesRule>()
            .AddValidationVisitor<FragmentSpreadsMustNotFormCyclesRule>()
            .AddValidationVisitor<FragmentSpreadTargetDefinedRule>()
            .AddValidationVisitor<FragmentSpreadTypeExistenceRule>()
            .AddValidationVisitor<LoneAnonymousOperationRule>()
            .AddValidationVisitor<OperationNameUniquenessRule>()
            .AddValidationVisitor<ReservedNameRule>();

        public static RequestServiceBuilder AddValidationVisitor<TVisitor>(this RequestServiceBuilder builder)
            where TVisitor : class, IValidationRule
        {
            ArgumentNullException.ThrowIfNull(builder, nameof(builder));

            builder.Services.AddScoped<IValidationRule, TVisitor>();
            return builder;
        }
    }
}