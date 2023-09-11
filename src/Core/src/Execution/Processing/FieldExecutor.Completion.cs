using System.Collections;

using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public partial class FieldExecutor : IFieldExecutor
    {
        /// <summary>
        /// After the value of the field is resolved, it is validated against the expected return type.
        /// </summary>
        /// <param name="context">The current execution context.</param>
        /// <param name="fieldType">The type of the field being validated.</param>
        /// <param name="selection">The selection being resolved.</param>
        /// <param name="result">The resolved value of the field.</param>
        /// <returns>The completed value of the field.</returns>
        private async ValueTask<object?> CompleteValueAsync(
            QueryExecutionContext context,
            GraphType fieldType,
            GraphFieldSelection selection,
            object? result,
            Stack<object> path,
            CancellationToken cancellationToken)
        {
            if(result is null)
            {
                if(fieldType.NonNullable)
                {
                    context.RaiseFieldError(DefaultErrors.ValueNullabilityViolation(selection.Location!).WithPath(path));
                }
                return null;
            }

            if(fieldType is GraphListType listFieldType)
            {
                if(result is not IEnumerable resolvedListValue)
                {
                    context.RaiseFieldError(DefaultErrors.ValueNotCoercible(result, fieldType).WithPath(path));
                    return null;
                }

                List<object?> values = new();

                int i = 0;
                foreach(object? item in resolvedListValue)
                {
                    object? value = await this.CompleteValueAsync(
                        context,
                        listFieldType.UnderlyingType,
                        selection,
                        result,
                        path,
                        cancellationToken
                    );

                    values.Add(value);
                    i++;
                }

                return values;
            }

            if(fieldType is not GraphNamedType _fieldType)
            {
                throw new NotSupportedException("Any non-list types must be named types.");
            }

            if(!this._typeResolver.TryResolveTypeDefinition(_fieldType.Name, out TypeDefinition? typeDefinition))
            {
                context.RaiseRequestError(DefaultErrors.TypeNotFound(_fieldType.Name).WithPath(path));
                return null;
            }

            if(typeDefinition is ScalarType)
            {
                return result;
            }

            if(!context.Schema.TryGetDefinition(_fieldType.Name, out GraphDefinition? graphDefinition))
            {
                context.RaiseRequestError(DefaultErrors.TypeNotFound(_fieldType.Name).WithPath(path));
                return null;
            }

            // Object, interface or union type
            if(graphDefinition.IsCompositeType())
            {
                GraphObjectType objectType = graphDefinition switch
                {
                    GraphObjectType _typeDefinition => _typeDefinition,
                    GraphInterfaceDefinition _typeDefinition => this.ResolveAbstractValue(_typeDefinition, result),
                    GraphUnionDefinition _typeDefinition => this.ResolveAbstractValue(_typeDefinition, result),

                    _ => throw new NotSupportedException()
                };

                if(selection is { SelectionSet: null })
                {
                    Error error = new ErrorBuilder()
                        .SetCode(ErrorCodes.LeafSelectionNotAllowed)
                        .SetMessage($"Type '{_fieldType.Name}' is a composite type. Leaf selections on objects, interfaces, and unions without subfields are disallowed.")
                        .AddLocation(selection.Location)
                        .SetPath(path.ToList())
                        .SetReference("sec-Field-Selections-on-Objects-Interfaces-and-Unions-Types")
                        .Build();

                    context.RaiseRequestError(error);
                    return null;
                }

                List<GraphFieldSelection> subSelections = selection
                    .SelectionSet
                    .Selections
                    .Cast<GraphFieldSelection>()
                    .ToList();

                return await this.ExecuteSelectionsAsync(
                    context,
                    FieldExecutor.MergeSelectionSet(subSelections),
                    objectType,
                    result,
                    path,
                    cancellationToken
                );
            }

            object? coercedValue = this._valueCoercer.CoerceResult(result, fieldType);
            if(coercedValue is null)
            {
                context.RaiseFieldError(DefaultErrors.ValueNotCoercible(result, fieldType));
                return null;
            }

            return coercedValue;
        }

        /// <summary>
        /// Coerce the given argument definition with the given arguments.
        /// </summary>
        /// <returns>
        /// If successful, returns the unordered map of coerced argument values.
        /// If no arguments were passed, returns <see langword="null" />.
        /// If an error occurred, adds the error to the context and returns <see langword="null" />.
        /// </returns>
        protected internal Dictionary<string, object?>? CoerceArgumentValues(
            QueryExecutionContext context,
            GraphField field,
            GraphFieldSelection selection)
        {
            if(field.Arguments is null || selection.Arguments is null)
            {
                return null;
            }

            Dictionary<string, object?> coercedValues = new();

            foreach(GraphArgumentDefinition argumentDefinition in field.Arguments.Arguments)
            {
                GraphArgument? argument = selection.Arguments.Arguments
                    .FirstOrDefault(v => v.Name == argumentDefinition.Name);

                GraphName argumentName = argumentDefinition.Name;
                GraphType argumentType = argumentDefinition.Type;
                IGraphValue? argumentValue = argument?.Value;

                if(argumentValue is GraphVariableValue argumentVariableValue)
                {
                    if(context.VariableValues is null)
                    {
                        throw new NotImplementedException();
                    }

                    if(!context.VariableValues.TryGetValue(argumentVariableValue.Value, out object? variableValue))
                    {
                        throw new NotImplementedException();
                    }

                    argumentValue = this._valueRegistry.ResolveValue(variableValue);
                }

                argumentValue ??= argumentDefinition.DefaultValue;

                if(argumentType.NonNullable && argumentValue is null)
                {
                    context.RaiseFieldError(DefaultErrors.ArgumentNullabilityViolation(argumentDefinition));
                    continue;
                }

                if(argumentValue is not null)
                {
                    if(argumentValue is GraphNullValue)
                    {
                        coercedValues.Add(argumentName, null);
                        continue;
                    }

                    if(argumentValue is GraphVariableValue)
                    {
                        coercedValues.Add(argumentName, argumentValue);
                        continue;
                    }

                    IGraphValue? coercedValue = this._valueCoercer.CoerceInput(argumentValue, argumentType);
                    if(coercedValue is null)
                    {
                        context.RaiseFieldError(DefaultErrors.ValueNotCoercible(argumentValue, argumentType));
                        return null;
                    }

                    coercedValues.Add(argumentName, coercedValue);
                }
            }

            return coercedValues;
        }

        protected internal static List<GraphFieldSelection> MergeSelectionSet(List<GraphFieldSelection> fields)
        {
            Dictionary<string, GraphFieldSelection> selections = new();

            foreach(GraphFieldSelection selection in fields)
            {
                if(!selections.ContainsKey(selection.Name))
                {
                    selections.Add(selection.Name, selection);
                    continue;
                }

                if(selection.SelectionSet is not null && selection.SelectionSet.Selections.Count > 0)
                {
                    GraphFieldSelection existingSelection = selections[selection.Name];
                    existingSelection.SelectionSet ??= new GraphSelectionSet();
                    existingSelection.SelectionSet.Selections.AddRange(selection.SelectionSet.Selections);
                }
            }

            return selections
                .Select(v => v.Value)
                .ToList();
        }

        protected internal GraphObjectType ResolveAbstractValue<TDefinition>(TDefinition definition, object result)
            where TDefinition : GraphDefinition
        {
            throw new NotImplementedException();
        }
    }
}