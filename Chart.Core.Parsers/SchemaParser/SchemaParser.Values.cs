using System.Globalization;
using Chart.Models.AST;
using Chart.Shared.Exceptions;

namespace Chart.Core.Parsers
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
        private GraphValue ParseValue(bool constant = false)
        {
            if(!constant && this.Peek(TokenType.DOLLAR_SIGN))
            {
                return this.ParseVariableValue();
            }

            return this.CurrentToken.Type switch
            {
                TokenType.INT           => this.ParseIntValue(),
                TokenType.FLOAT         => this.ParseFloatValue(),
                TokenType.STRING        => this.ParseStringValue(),
                TokenType.BOOLEAN       => this.ParseBooleanValue(),
                TokenType.NULL          => this.ParseNullValue(),
                TokenType.NAME          => this.ParseEnumValue(),
                TokenType.BRACKET_LEFT  => this.ParseListValue(),
                TokenType.BRACE_LEFT    => this.ParseObjectValue(),

                TokenType.DOLLAR_SIGN   => throw new VariableNotAllowedException(this.CurrentToken.Start),
                _                       => throw UnexpectedToken()
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

            this.Expect(TokenType.DOLLAR_SIGN);
            this.Skip();

            this.Expect(TokenType.NAME);
            def.Variable = new GraphName(this.CurrentToken.Value);
            this.Skip();

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
            NumberStyles style = NumberStyles.AllowLeadingSign;

            this.Expect(TokenType.INT);
            if(Int32.TryParse(this.CurrentToken.Value, style, null, out Int32 value))
            {
                def.Value = value;
                this.Skip();
            }
            else
            {
                throw UnexpectedToken();
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
                throw UnexpectedToken();
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

            return new GraphNullValue();
        }

        /// <summary>
        /// Parse a value whose type is an enum.
        /// </summary>
        /// <returns>The parsed GraphEnumValue-object.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphEnumValue ParseEnumValue()
        {
            GraphEnumValue def = new GraphEnumValue();

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

            this.Expect(TokenType.BRACKET_LEFT);
            this.Skip();

            while(!this.Peek(TokenType.BRACKET_RIGHT))
            {
                def.Values.Add(this.ParseValue());
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

            this.Expect(TokenType.BRACE_LEFT);
            this.Skip();

            while(!this.Peek(TokenType.BRACE_RIGHT))
            {
                this.Expect(TokenType.NAME);
                GraphName name = new GraphName(this.CurrentToken.Value);
                this.Skip();

                this.Expect(TokenType.COLON);
                this.Skip();

                GraphValue value = this.ParseValue();

                def.Fields.Add(name, value);
            }

            this.Expect(TokenType.BRACE_RIGHT);
            this.Skip();

            return def;
        }
    }
}