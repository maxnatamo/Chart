namespace Chart.Utilities.Tests.DocumentPrinterTests
{
    public class VisitGraphUnionDefinitionTests
    {
        [Fact]
        public void Visit_PrintsUnion_GivenEmptyUnion()
        {
            // Arrange
            GraphUnionDefinition unionDefinition = new GraphUnionDefinition();
            unionDefinition.Name = new GraphName("enumeration");

            // Act

            // Assert
            new DocumentPrinter()
                .Visit((GraphDefinition) unionDefinition)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsUnion_GivenEmptyUnionWithDescription()
        {
            // Arrange
            GraphUnionDefinition unionDefinition = new GraphUnionDefinition();
            unionDefinition.Name = new GraphName("enumeration");
            unionDefinition.Description = new GraphDescription("A custom description.");

            // Act

            // Assert
            new DocumentPrinter()
                .Visit((GraphDefinition) unionDefinition)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsUnion_GivenUnionWithSingleValue()
        {
            // Arrange
            GraphUnionDefinition unionDefinition = new GraphUnionDefinition();
            unionDefinition.Name = new GraphName("Creature");
            unionDefinition.Members = new GraphUnionMembers()
            {
                Members = new List<GraphName>
                {
                    new GraphName("Human"),
                    new GraphName("Dog"),
                    new GraphName("Cat")
                }
            };

            // Act

            // Assert
            new DocumentPrinter()
                .Visit((GraphDefinition) unionDefinition)
                .ToString()
                .MatchSnapshot();
        }
    }
}