namespace Chart.Utilities.Tests.DocumentPrinterTests
{
    public class VisitGraphExtensionDefinitionTests
    {
        [Fact]
        public void Visit_ThrowsException_GivenInvalidExtensionKind()
        {
            // Arrange
            TestExtensionDefinition definition = new TestExtensionDefinition();

            // Act
            Action act = () => new DocumentPrinter().Visit(definition);

            // Assert
            act.Should().Throw<Exception>();
        }

        [Fact]
        public void Visit_PrintsExtension_GivenEmptyScalarExtension()
        {
            // Arrange
            GraphScalarExtension extension = new GraphScalarExtension();
            extension.Name = new GraphName("Test");

            // Act

            // Assert
            new DocumentPrinter()
                .Visit((GraphDefinition) extension)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsExtension_GivenEmptyScalarExtensionWithDirective()
        {
            // Arrange
            GraphScalarExtension extension = new GraphScalarExtension();
            extension.Name = new GraphName("Test");
            extension.Directives = this.DemoDirective();

            // Act

            // Assert
            new DocumentPrinter()
                .Visit((GraphDefinition) extension)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsExtension_GivenEmptySchemaExtension()
        {
            // Arrange
            GraphSchemaExtension extension = new GraphSchemaExtension();
            extension.Name = new GraphName("Test");

            // Act

            // Assert
            new DocumentPrinter()
                .Visit((GraphDefinition) extension)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsExtension_GivenSchemaExtension()
        {
            // Arrange
            GraphSchemaExtension extension = new GraphSchemaExtension();
            extension.Name = new GraphName("Test");
            extension.Values = new GraphSchemaValues()
            {
                Values = new List<GraphSchemaValue>()
                {
                    new GraphSchemaValue
                    {
                        Operation = new GraphName("query"),
                        Type = new GraphNamedType("Query")
                    }
                }
            };

            // Act

            // Assert
            new DocumentPrinter()
                .Visit((GraphDefinition) extension)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsExtension_GivenEmptyEnumExtension()
        {
            // Arrange
            GraphEnumExtension extension = new GraphEnumExtension();
            extension.Name = new GraphName("Test");

            // Act

            // Assert
            new DocumentPrinter()
                .Visit((GraphDefinition) extension)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsExtension_GivenEnumExtension()
        {
            // Arrange
            GraphEnumExtension extension = new GraphEnumExtension();
            extension.Name = new GraphName("Direction");
            extension.Values = new GraphEnumDefinitionValues()
            {
                Values = new List<GraphEnumDefinitionValue>()
                {
                    new GraphEnumDefinitionValue()
                    {
                        Name = new GraphName("South")
                    }
                }
            };

            // Act

            // Assert
            new DocumentPrinter()
                .Visit((GraphDefinition) extension)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsExtension_GivenEmptyUnionExtension()
        {
            // Arrange
            GraphUnionExtension extension = new GraphUnionExtension();
            extension.Name = new GraphName("Test");

            // Act

            // Assert
            new DocumentPrinter()
                .Visit((GraphDefinition) extension)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsExtension_GivenUnionExtension()
        {
            // Arrange
            GraphUnionExtension extension = new GraphUnionExtension();
            extension.Name = new GraphName("Direction");
            extension.Members = new GraphUnionMembers()
            {
                Members = new List<GraphName>()
                {
                    new GraphName("NORTH"),
                    new GraphName("EAST"),
                    new GraphName("WEST"),
                    new GraphName("SOUTH"),
                }
            };

            // Act

            // Assert
            new DocumentPrinter()
                .Visit((GraphDefinition) extension)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsExtension_GivenEmptyInputExtension()
        {
            // Arrange
            GraphInputExtension extension = new GraphInputExtension();
            extension.Name = new GraphName("Test");

            // Act

            // Assert
            new DocumentPrinter()
                .Visit((GraphDefinition) extension)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsExtension_GivenInputExtension()
        {
            // Arrange
            GraphInputExtension extension = new GraphInputExtension();
            extension.Name = new GraphName("Direction");
            extension.Arguments = new GraphArgumentsDefinition()
            {
                Arguments = new List<GraphArgumentDefinition>()
                {
                    new GraphArgumentDefinition()
                    {
                        Name = new GraphName("test"),
                        Type = new GraphNamedType("String")
                    }
                }
            };

            // Act

            // Assert
            new DocumentPrinter()
                .Visit((GraphDefinition) extension)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsExtension_GivenEmptyObjectExtension()
        {
            // Arrange
            GraphObjectExtension extension = new GraphObjectExtension();
            extension.Name = new GraphName("Test");

            // Act

            // Assert
            new DocumentPrinter()
                .Visit((GraphDefinition) extension)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsExtension_GivenEmptyObjectExtensionWithDirective()
        {
            // Arrange
            GraphObjectExtension extension = new GraphObjectExtension();
            extension.Name = new GraphName("Test");
            extension.Directives = this.DemoDirective();

            // Act

            // Assert
            new DocumentPrinter()
                .Visit((GraphDefinition) extension)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsExtension_GivenObjectExtension()
        {
            // Arrange
            GraphObjectExtension extension = new GraphObjectExtension();
            extension.Name = new GraphName("Test");
            extension.Fields = new GraphFields()
            {
                Fields = new List<GraphField>()
                {
                    new GraphField()
                    {
                        Name = new GraphName("age"),
                        Type = new GraphNamedType("Int")
                    },
                    new GraphField()
                    {
                        Name = new GraphName("ageInDogYears"),
                        Type = new GraphNamedType("Int"),
                        Description = new GraphDescription("Super useful description!")
                    }
                }
            };

            // Act

            // Assert
            new DocumentPrinter()
                .Visit((GraphDefinition) extension)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsExtension_GivenObjectExtensionWithInterface()
        {
            // Arrange
            GraphObjectExtension extension = new GraphObjectExtension();
            extension.Name = new GraphName("Test");
            extension.Interface = new GraphInterfaces()
            {
                Implements = new List<GraphNamedType>()
                {
                    new GraphNamedType("ITest")
                }
            };

            // Act

            // Assert
            new DocumentPrinter()
                .Visit((GraphDefinition) extension)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsExtension_GivenEmptyInterfaceExtension()
        {
            // Arrange
            GraphInterfaceExtension extension = new GraphInterfaceExtension();
            extension.Name = new GraphName("Test");

            // Act

            // Assert
            new DocumentPrinter()
                .Visit((GraphDefinition) extension)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsExtension_GivenEmptyInterfaceExtensionWithDirective()
        {
            // Arrange
            GraphInterfaceExtension extension = new GraphInterfaceExtension();
            extension.Name = new GraphName("Test");
            extension.Directives = this.DemoDirective();

            // Act

            // Assert
            new DocumentPrinter()
                .Visit((GraphDefinition) extension)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsExtension_GivenInterfaceExtension()
        {
            // Arrange
            GraphInterfaceExtension extension = new GraphInterfaceExtension();
            extension.Name = new GraphName("Test");
            extension.Fields = new GraphFields()
            {
                Fields = new List<GraphField>()
                {
                    new GraphField()
                    {
                        Name = new GraphName("age"),
                        Type = new GraphNamedType("Int")
                    }
                }
            };

            // Act

            // Assert
            new DocumentPrinter()
                .Visit((GraphDefinition) extension)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsExtension_GivenInterfaceExtensionWithInterface()
        {
            // Arrange
            GraphInterfaceExtension extension = new GraphInterfaceExtension();
            extension.Name = new GraphName("Test");
            extension.Interface = new GraphInterfaces()
            {
                Implements = new List<GraphNamedType>()
                {
                    new GraphNamedType("ITest")
                }
            };

            // Act

            // Assert
            new DocumentPrinter()
                .Visit((GraphDefinition) extension)
                .ToString()
                .MatchSnapshot();
        }

        private GraphDirectives DemoDirective() =>
            new GraphDirectives()
            {
                Directives = new List<GraphDirective>()
                {
                    new GraphDirective
                    {
                        Name = new GraphName("ignore")
                    }
                }
            };

        internal class TestExtensionDefinition : GraphExtensionDefinition
        {
            public override GraphDefinitionKind DefinitionKind => GraphDefinitionKind.Extension;
            public override GraphExtensionKind ExtensionKind => (GraphExtensionKind) 2000;

            public override bool Executable => false;

            public override string ToString()
                => throw new NotSupportedException();
        }
    }
}