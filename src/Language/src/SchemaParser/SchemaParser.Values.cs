using System.Globalization;

using Chart.Language.SyntaxTree;

namespace Chart.Language
{
    public partial class SchemaParser
    {
        /// <summary>
        /// Parse a variable value.
        /// </summary>
        /// <param name="constant">
        /// Whether the input value is constant.
        /// See <see href="https://spec.graphql.org/October2021/#sec-Input-Values">reference.</see>
        /// </param>
        /// <returns>The parsed GraphValue-object.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private IGraphValue ParseValue(bool constant = false)
        {
            if(!constant && this.Peek(TokenType.DOLLAR_SIGN))
            {
                return this.ParseVariableValue();
            }

            return this.CurrentToken.Type switch
            {
                TokenType.INT => this.ParseIntValue(),
                TokenType.FLOAT => this.ParseFloatValue(),
                TokenType.STRING => this.ParseStringValue(),
                TokenType.BOOLEAN => this.ParseBooleanValue(),
                TokenType.NULL => this.ParseNullValue(),
                TokenType.NAME => this.ParseEnumValue(),
                TokenType.BRACKET_LEFT => this.ParseListValue(),
                TokenType.BRACE_LEFT => this.ParseObjectValue(),

                TokenType.DOLLAR_SIGN => throw new VariableNotAllowedException(this.CurrentToken.Start),
                _ => throw this.UnexpectedToken()
            };
        }

        /// <summary>
        /// Parse a value whose type is a variable.
        /// </summary>
        /// <returns>The parsed GraphVariableValue-object.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphVariableValue ParseVariableValue()
        {
            GraphVariableValue def = new GraphVariableValue();
            def.Location = this.CurrentToken.Location.Clone();

            this.Expect(TokenType.DOLLAR_SIGN);
            this.Skip();

            def.Value = this.ParseName();

            return def;
        }

        /// <summary>
        /// Parse a value whose type is an integer.
        /// </summary>
        /// <returns>The parsed GraphIntValue-object.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphIntValue ParseIntValue()
        {
            GraphIntValue def = new GraphIntValue();
            def.Location = this.CurrentToken.Location.Clone();

            NumberStyles style = NumberStyles.AllowLeadingSign;

            this.Expect(TokenType.INT);
            if(Int32.TryParse(this.CurrentToken.Value, style, null, out Int32 value))
            {
                def.Value = value;
                this.Skip();
            }
            else
            {
                throw this.UnexpectedToken();
            }

            return def;
        }

        /// <summary>
        /// Parse a value whose type is a float.
        /// </summary>
        /// <returns>The parsed GraphFloatValue-object.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphFloatValue ParseFloatValue()
        {
            GraphFloatValue def = new GraphFloatValue();
            def.Location = this.CurrentToken.Location.Clone();

            NumberStyles style = NumberStyles.AllowLeadingSign |
                                 NumberStyles.AllowThousands |
                                 NumberStyles.AllowExponent |
                                 NumberStyles.AllowDecimalPoint;

            this.Expect(TokenType.FLOAT);
            if(Double.TryParse(this.CurrentToken.Value, style, null, out Double value))
            {
                def.Value = value;
                this.Skip();
            }
            else
            {
                throw this.UnexpectedToken();
            }

            return def;
        }

        /// <summary>
        /// Parse a value whose type is a string.
        /// </summary>
        /// <returns>The parsed GraphStringValue-object.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphStringValue ParseStringValue()
        {
            GraphStringValue def = new GraphStringValue();
            def.Location = this.CurrentToken.Location.Clone();

            this.Expect(TokenType.STRING);
            def.Value = this.CurrentToken.Value;
            this.Skip();

            return def;
        }

        /// <summary>
        /// Parse a value whose type is a boolean.
        /// </summary>
        /// <returns>The parsed GraphBooleanValue-object.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphBooleanValue ParseBooleanValue()
        {
            GraphBooleanValue def = new GraphBooleanValue();
            def.Location = this.CurrentToken.Location.Clone();

            this.Expect(TokenType.BOOLEAN);
            def.Value = this.CurrentToken.Value == "true";
            this.Skip();

            return def;
        }

        /// <summary>
        /// Parse a value whose type is a null.
        /// </summary>
        /// <returns>The parsed GraphNullValue-object.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphNullValue ParseNullValue()
        {
            this.Expect(TokenType.NULL);
            this.Skip();

            return new GraphNullValue()
            {
                Location = this.CurrentToken.Location.Clone()
            };
        }

        /// <summary>
        /// Parse a value whose type is an enum.
        /// </summary>
        /// <returns>The parsed GraphEnumValue-object.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphEnumValue ParseEnumValue()
        {
            GraphEnumValue def = new GraphEnumValue();
            def.Location = this.CurrentToken.Location.Clone();

            this.Expect(TokenType.NAME);
            def.Value = new GraphName(this.CurrentToken.Value);
            this.Skip();

            return def;
        }

        /// <summary>
        /// Parse a value whose type is a list.
        /// </summary>
        /// <returns>The parsed GraphListValue-object.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphListValue ParseListValue()
        {
            GraphListValue def = new GraphListValue();
            def.Location = this.CurrentToken.Location.Clone();

            this.Expect(TokenType.BRACKET_LEFT);
            this.Skip();

            while(!this.Peek(TokenType.BRACKET_RIGHT))
            {
                def.Value.Add(this.ParseValue());
            }

            this.Expect(TokenType.BRACKET_RIGHT);
            this.Skip();

            return def;
        }

        /// <summary>
        /// Parse a value whose type is an object.
        /// </summary>
        /// <returns>The parsed GraphObjectValue-object.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphObjectValue ParseObjectValue()
        {
            GraphObjectValue def = new GraphObjectValue();
            def.Location = this.CurrentToken.Location.Clone();

            this.Expect(TokenType.BRACE_LEFT);
            this.Skip();

            while(!this.Peek(TokenType.BRACE_RIGHT))
            {
                GraphName name = this.ParseName();

                this.Expect(TokenType.COLON);
                this.Skip();

                IGraphValue value = this.ParseValue();

                def.Value.Add(name, value);
            }

            this.Expect(TokenType.BRACE_RIGHT);
            this.Skip();

            return def;
        }
    }
}