namespace Chart.Core.Tests.Naming.NameFormatterTests
{
    public class GetGenericsNameTests
    {
        [Fact]
        public void GetGenericsName_ReturnsPascalName_GivenNonGenericType()
        {
            // Arrange
            NameFormatter formatter = new NameFormatter();

            // Act
            string result = formatter.GetGenericsName(typeof(ClassWithoutTypeParameters));

            // Assert
            result.Should().Be("ClassWithoutTypeParameters");
        }

        [Theory]
        [InlineData(typeof(Tuple), "Tuple")]
        [InlineData(typeof(Tuple<string>), "TupleOfString")]
        [InlineData(typeof(Tuple<string, string>), "TupleOfStringAndString")]
        [InlineData(typeof(Tuple<Byte, Int16, Int32>), "TupleOfByteAndInt16AndInt32")]
        [InlineData(typeof(ClassWithSingleTypeParameter), "ClassWithSingleTypeParameter")]
        [InlineData(typeof(ClassWithTwoTypeParameters), "ClassWithTwoTypeParameters")]
        [InlineData(typeof(ClassWithSingleTypeParameter<String>), "ClassWithSingleTypeParameterOfString")]
        [InlineData(typeof(ClassWithTwoTypeParameters<Byte, Int16>), "ClassWithTwoTypeParametersOfByteAndInt16")]
        [InlineData(typeof(ClassWithSingleTypeParameter<ClassWithSingleTypeParameter>), "ClassWithSingleTypeParameterOfClassWithSingleTypeParameter")]
        [InlineData(typeof(ClassWithTwoTypeParameters<ClassWithTwoTypeParameters<Byte, Int16>, ClassWithSingleTypeParameter>), "ClassWithTwoTypeParametersOfClassWithTwoTypeParametersOfByteAndInt16AndClassWithSingleTypeParameter")]
        public void GetGenericsName_ReturnsTypeOfName_GivenGenericType(Type type, string expected)
        {
            // Arrange
            NameFormatter formatter = new NameFormatter();

            // Act
            string result = formatter.GetGenericsName(type);

            // Assert
            result.Should().Be(expected);
        }
    }

    internal class ClassWithoutTypeParameters
    {

    }

    internal class ClassWithSingleTypeParameter : Tuple<Byte>
    {
        ClassWithSingleTypeParameter(Byte item1) : base(item1) { }
    }

    internal class ClassWithSingleTypeParameter<TItem> : Tuple<TItem>
    {
        ClassWithSingleTypeParameter(TItem item1) : base(item1) { }
    }

    internal class ClassWithTwoTypeParameters : Tuple<Byte, Int16>
    {
        ClassWithTwoTypeParameters(Byte item1, Int16 item2) : base(item1, item2) { }
    }

    internal class ClassWithTwoTypeParameters<TItem1, TItem2> : Tuple<TItem1, TItem2>
    {
        ClassWithTwoTypeParameters(TItem1 item1, TItem2 item2) : base(item1, item2) { }
    }
}