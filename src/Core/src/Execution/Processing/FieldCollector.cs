using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public interface IFieldCollector
    {
        /// <summary>
        /// Convert all selections in the given selection set into simple field selections and skip non-applying fragments.
        /// </summary>
        /// <remarks>
        /// All sub-selections in the returned selections are ensured to be field selections.
        /// </remarks>
        /// <param name="context">The current execution context.</param>
        /// <returns>List of all field selections found.</returns>
        List<GraphFieldSelection> CollectFields(QueryExecutionContext context);

        /// <inheritdoc cref="CollectFields(QueryExecutionContext)" />
        /// <param name="objectType">The object type which the selection is being applied on.</param>
        /// <param name="selectionSet">The selection set to collect fields from.</param>
        List<GraphFieldSelection> CollectFields(
            QueryExecutionContext context,
            GraphDefinition objectType,
            GraphSelectionSet selectionSet);

        /// <inheritdoc cref="CollectFields(QueryExecutionContext, GraphDefinition, GraphSelectionSet)" />
        List<GraphFieldSelection> CollectFields(
            QueryExecutionContext context,
            GraphDefinition objectType,
            GraphSelection selection);
    }

    public class FieldCollector : IFieldCollector
    {
        /// <inheritdoc />
        public List<GraphFieldSelection> CollectFields(
            QueryExecutionContext context,
            GraphDefinition objectType,
            GraphSelectionSet selectionSet)
        {
            List<GraphFieldSelection> groupedFields = new();

            this.CollectFields(context, objectType, selectionSet, groupedFields);

            return groupedFields;
        }

        public List<GraphFieldSelection> CollectFields(QueryExecutionContext context) =>
            this.CollectFields(
                context,
                context.RootGraphType,
                context.Operation.Selections);

        public List<GraphFieldSelection> CollectFields(
            QueryExecutionContext context,
            GraphDefinition objectType,
            GraphSelection selection)
        {
            List<GraphFieldSelection> groupedFields = new();

            this.CollectFields(context, objectType, selection, groupedFields);

            return groupedFields;
        }

        /// <inheritdoc cref="CollectFields(QueryExecutionContext, GraphDefinition, GraphSelectionSet)" />
        internal void CollectFields(
            QueryExecutionContext context,
            GraphDefinition objectType,
            GraphSelectionSet selectionSet,
            List<GraphFieldSelection> groupedFields)
        {
            foreach(GraphSelection selection in selectionSet.Selections)
            {
                this.CollectFields(context, objectType, selection, groupedFields);
            }
        }

        internal void CollectFields(
            QueryExecutionContext context,
            GraphDefinition objectType,
            GraphSelection selection,
            List<GraphFieldSelection> groupedFields)
        {
            if(FieldCollector.ShouldSkipSelection(context, selection))
            {
                return;
            }

            switch(selection)
            {
                case GraphFieldSelection _selection:
                    this.CollectFields(
                        context,
                        objectType,
                        _selection,
                        groupedFields);
                    break;

                case GraphFragmentSelection _selection:
                    this.CollectFields(
                        context,
                        objectType,
                        _selection,
                        groupedFields);
                    break;

                case GraphInlineFragmentSelection _selection:
                    this.CollectFields(
                        context,
                        objectType,
                        _selection,
                        groupedFields);
                    break;

                default:
                    throw new NotSupportedException();
            };
        }

        /// <summary>
        /// Collect the fields of the given field selection.
        /// </summary>
        /// <param name="context">The current execution context.</param>
        /// <param name="objectType">The object type which the selection is being applied on.</param>
        /// <param name="fieldSelection">The field selection to collect fields from.</param>
        /// <param name="groupedFields">List of field selections to populate.</param>
        internal void CollectFields(
            QueryExecutionContext context,
            GraphDefinition objectType,
            GraphFieldSelection fieldSelection,
            List<GraphFieldSelection> groupedFields)
        {
            GraphFieldSelection selection = new()
            {
                Alias = fieldSelection.Alias,
                Name = fieldSelection.Name,
                Arguments = fieldSelection.Arguments,
                Directives = fieldSelection.Directives,
                Location = fieldSelection.Location
            };

            GraphDefinition? selectionType = objectType.GetObjectFieldType(fieldSelection.Name, context);
            if(selectionType is null)
            {
                return;
            }

            if(fieldSelection.SelectionSet is not null)
            {
                selection.SelectionSet = new GraphSelectionSet()
                {
                    Selections = this.CollectFields(
                        context,
                        selectionType,
                        fieldSelection.SelectionSet
                    )
                    .Cast<GraphSelection>()
                    .ToList()
                };
            }

            groupedFields.Add(selection);
        }

        /// <summary>
        /// Collect the fields of the given fragment selection.
        /// </summary>
        /// <param name="fragmentSelection">The fragment selection to collect fields from.</param>
        /// <inheritdoc cref="CollectFields(QueryExecutionContext, GraphDefinition, GraphFieldSelection, List{GraphFieldSelection})" />
        internal void CollectFields(
            QueryExecutionContext context,
            GraphDefinition objectType,
            GraphFragmentSelection fragmentSelection,
            List<GraphFieldSelection> groupedFields)
        {
            if(!context.Query.TryGetDefinition(
                fragmentSelection.Name,
                out GraphFragmentDefinition? fragment))
            {
                context.RaiseRequestError(DefaultErrors.FragmentNotFound(fragmentSelection));
                return;
            }

            if(!FieldCollector.IsFragmentTypeDerived(context, objectType, fragment.Type.Name))
            {
                return;
            }

            if(!context.Schema.TryGetDefinition(fragment.Type.Name, out GraphDefinition? typeDefinition))
            {
                context.RaiseRequestError(DefaultErrors.TypeNotFound(fragment.Type.Name));
                return;
            }

            this.CollectFields(
                context,
                typeDefinition,
                fragment.SelectionSet,
                groupedFields
            );
        }

        /// <summary>
        /// Collect the fields of the given inline fragment selection.
        /// </summary>
        /// <param name="inlineSelection">The inline fragment selection to collect fields from.</param>
        /// <inheritdoc cref="CollectFields(QueryExecutionContext, GraphDefinition, GraphFieldSelection, List{GraphFieldSelection})" />
        internal void CollectFields(
            QueryExecutionContext context,
            GraphDefinition objectType,
            GraphInlineFragmentSelection inlineSelection,
            List<GraphFieldSelection> groupedFields)
        {
            GraphNamedType? fragmentType = inlineSelection.TypeCondition;

            if(fragmentType is not null)
            {
                if(!FieldCollector.IsFragmentTypeDerived(context, objectType, fragmentType.Name))
                {
                    return;
                }

                if(!context.Schema.TryGetDefinition(fragmentType.Name, out GraphDefinition? typeDefinition))
                {
                    context.RaiseRequestError(DefaultErrors.TypeNotFound(fragmentType.Name));
                    return;
                }

                this.CollectFields(
                    context,
                    typeDefinition,
                    inlineSelection.SelectionSet,
                    groupedFields
                );

                return;
            }

            this.CollectFields(
                context,
                objectType,
                inlineSelection.SelectionSet,
                groupedFields
            );
        }

        /// <summary>
        /// Whether to skip the given selection, depending on the existence and arguments of '@see' and '@include' directives.
        /// </summary>
        /// <param name="selection">The selection to check against.</param>
        /// <returns><see langword="true" />, if the selection should be skipped. Otherwise, <see langword="false" />.</returns>
        internal static bool ShouldSkipSelection(QueryExecutionContext context, GraphSelection selection)
        {
            bool skip = false;
            bool include = true;

            GraphDirective? skipDirective = selection.Directives?.Directives.FirstOrDefault(v => v.Name == "skip");
            if(skipDirective is not null)
            {
                if(skipDirective.IsDirectiveArgumentEqual(true, variableValues: context.VariableValues))
                {
                    skip = true;
                }
            }

            GraphDirective? includeDirective = selection.Directives?.Directives.FirstOrDefault(v => v.Name == "include");
            if(includeDirective is not null)
            {
                if(includeDirective.IsDirectiveArgumentEqual(false, variableValues: context.VariableValues))
                {
                    include = false;
                }
            }

            // https://spec.graphql.org/October2021/#note-f3059
            return (skip, include) switch
            {
                (false, true) => false,
                (true, _) => true,
                (_, false) => true,
            };
        }

        /// <summary>
        /// Whether the given fragment applies to the given object type.
        /// </summary>
        internal static bool IsFragmentTypeDerived(QueryExecutionContext context, GraphDefinition type, GraphName fragmentType)
        {
            if(type.Name == fragmentType)
            {
                return true;
            }

            if(!context.Schema.TryGetDefinition(fragmentType, out GraphDefinition? fragmentTypeDefinition))
            {
                context.RaiseRequestError(DefaultErrors.TypeNotFound(fragmentType));
                return false;
            }

            return type.IsDerivedFrom(fragmentTypeDefinition);
        }
    }
}