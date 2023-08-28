using Chart.Language.SyntaxTree;

namespace Chart.Core.Validation
{
    /// <summary>
    /// Validation rule for <see href="https://spec.graphql.org/October2021/#sec-Fragment-spreads-must-not-form-cycles" />
    /// </summary>
    public class FragmentSpreadsMustNotFormCyclesRule : DocumentValidationRule
    {
        public override IValidationVisitor CreateVisitor() =>
            new NodeValidationVisitor<GraphFragmentDefinition, DocumentValidationContext>(this.ValidateAsync);

        private async Task ValidateAsync(GraphFragmentDefinition definition, DocumentValidationContext context)
        {
            this.DetectFragmentCycles(
                definition,
                context,
                new HashSet<GraphName>(),
                new Stack<object>());

            await Task.CompletedTask;
        }

        private void DetectFragmentCycles(
            GraphFragmentDefinition definition,
            DocumentValidationContext context,
            HashSet<GraphName> visitedFragments,
            Stack<object> spreadPath)
        {
            visitedFragments.Add(definition.Name);

            List<GraphFragmentSelection> spreads = this.GetFragmentSpreads(definition.SelectionSet);

            foreach(GraphFragmentSelection spread in spreads)
            {
                spreadPath.Push(spread.Name);

                if(visitedFragments.Contains(spread.Name))
                {
                    context.RaiseError(DefaultErrors.FragmentSpreadsFormCycle(definition, spreadPath));
                    return;
                }

                if(!context.Query.TryGetDefinition(spread.Name, out GraphFragmentDefinition? fragmentDefinition))
                {
                    return;
                }

                this.DetectFragmentCycles(
                    fragmentDefinition,
                    context,
                    visitedFragments,
                    spreadPath);

                spreadPath.Pop();
            }
        }

        /// <summary>
        /// Get all descending fragment spreads throughout the given selection set.
        /// </summary>
        /// <remarks>
        /// Adapted from <seealso href="https://github.com/graphql-dotnet/graphql-dotnet/blob/87da1379e040ffee36bf5458f084e6af9aece39c/src/GraphQL/Validation/ValidationContext.cs#L102">graphql-dotnet</seealso>
        /// </remarks>
        private List<GraphFragmentSelection> GetFragmentSpreads(GraphSelectionSet node)
        {
            List<GraphFragmentSelection> spreads = new();
            Stack<GraphSelectionSet> setsToVisit = new();

            setsToVisit.Push(node);

            while(setsToVisit.Count > 0)
            {
                GraphSelectionSet set = setsToVisit.Pop();

                foreach(GraphSelection selection in set.Selections)
                {
                    if(selection is GraphFragmentSelection spread)
                    {
                        spreads.Add(spread);
                    }
                    else if(selection is GraphFieldSelection field && field.SelectionSet is not null)
                    {
                        setsToVisit.Push(field.SelectionSet);
                    }
                    else if(selection is GraphInlineFragmentSelection inlineSpread)
                    {
                        setsToVisit.Push(inlineSpread.SelectionSet);
                    }
                }
            }

            return spreads;
        }
    }
}