namespace Chart.Language.Tests.DefinitionsTests
{
    public class IsDefinitionExecutableTests
    {
        [Theory]
        [MemberData(nameof(ExecutableTypes))]
        public void IsDefinitionExecutable_GivenDefinitionType(Type type, bool expected)
            => Definitions.IsDefinitionExecutable(type).Should().Be(expected);

        public static readonly IEnumerable<object[]> ExecutableTypes = new List<object[]>()
        {
            new object[] { typeof(GraphOperationDefinition), false },
            new object[] { typeof(GraphQueryOperation), true },
            new object[] { typeof(GraphMutationOperation), true },
            new object[] { typeof(GraphSubscriptionOperation), true },
            new object[] { typeof(GraphFragmentDefinition), true },
            new object[] { typeof(GraphEnumExtension), false },
            new object[] { typeof(GraphInputExtension), false },
            new object[] { typeof(GraphInterfaceExtension), false },
            new object[] { typeof(GraphObjectExtension), false },
            new object[] { typeof(GraphScalarExtension), false },
            new object[] { typeof(GraphSchemaExtension), false },
            new object[] { typeof(GraphUnionExtension), false },
            new object[] { typeof(GraphScalarType), false },
            new object[] { typeof(GraphObjectType), false },
            new object[] { typeof(GraphDirectiveDefinition), false },
            new object[] { typeof(GraphEnumDefinition), false },
            new object[] { typeof(GraphExtensionDefinition), false },
            new object[] { typeof(GraphInputDefinition), false },
            new object[] { typeof(GraphInterfaceDefinition), false },
            new object[] { typeof(GraphSchemaDefinition), false },
            new object[] { typeof(GraphTypeDefinition), false },
            new object[] { typeof(GraphUnionDefinition), false },
        };
    }
}