using Microsoft.Extensions.DependencyInjection;

namespace Chart.Core
{
    public static partial class RequestServiceBuilderExtensions
    {
        public static RequestServiceBuilder AddTypeServices(this RequestServiceBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder, nameof(builder));

            builder.Services.AddSingleton<ITypeConverter, TypeConverter>();
            builder.Services.AddSingleton<ITypeRegistry, TypeRegistry>();
            builder.Services.AddSingleton<IValueRegistry, ValueRegistry>();
            builder.Services.AddSingleton<IResolverCache, ResolverCache>();

            builder.Services.AddScoped<IAttributeResolver, AttributeResolver>();
            builder.Services.AddScoped<IFieldResolver, FieldResolver>();
            builder.Services.AddScoped<IFieldCollector, FieldCollector>();
            builder.Services.AddScoped<IFieldExecutor, FieldExecutor>();
            builder.Services.AddScoped<IResolverCompiler, ResolverCompiler>();
            builder.Services.AddScoped<ITypeChecker, TypeChecker>();
            builder.Services.AddScoped<ITypeCreator, TypeCreator>();
            builder.Services.AddScoped<ITypeRegistrator, TypeRegistrator>();
            builder.Services.AddScoped<ITypeResolver, TypeResolver>();
            builder.Services.AddScoped<IValueCoercer, ValueCoercer>();

            builder.AddBuiltinTypes();

            return builder;
        }

        public static RequestServiceBuilder AddBuiltinTypes(this RequestServiceBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder, nameof(builder));

            builder.AddBuiltinScalarTypes();
            builder.AddBuiltinDirectives();

            return builder;
        }

        public static RequestServiceBuilder AddBuiltinScalarTypes(this RequestServiceBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder, nameof(builder));

            builder.AddType<StringType>();
            builder.AddType<BooleanType>();
            builder.AddType<SingleType>();
            builder.AddType<FloatType>();
            builder.AddType<DecimalType>();
            builder.AddType<ByteType>();
            builder.AddType<ShortType>();
            builder.AddType<IntType>();
            builder.AddType<LongType>();
            builder.AddType<IdType>();
            builder.AddType<DateTimeType>();

            return builder;
        }

        public static RequestServiceBuilder AddBuiltinDirectives(this RequestServiceBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder, nameof(builder));

            builder.AddType<DeprecatedDirective>();
            builder.AddType<IncludeDirective>();
            builder.AddType<SkipDirective>();
            builder.AddType<SpecifiedByDirective>();

            return builder;
        }
    }
}