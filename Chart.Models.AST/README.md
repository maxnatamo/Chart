# Abstract-Syntax-Tree (AST) Models

The AST is a hierarchical structure, representing a GraphQL expression in a C# model, which can be traversed or used for further parsing.

An example on how the AST is structure is shown below:

Consider the following GraphQL expression
```
query getUser($id: Int) {
    user(id: $id) {
        username
        email age
    }
}
```

which will produce the following AST:
```
[GraphDocument]
   [GraphOperation] Query
      [GraphName] getUser
      [GraphVariablesDefinition]
         [GraphVariableDefinition]
            [GraphName] id
            [GraphType] Int
      [GraphSelectionSet]
         [GraphFieldSelection]
            [GraphName] user
            [GraphArguments]
               [GraphArgument]
                  [GraphName] id
                  [GraphVariableValue]
                     [GraphValue] id
            [GraphSelectionSet]
               [GraphFieldSelection]
                  [GraphName] username
               [GraphFieldSelection]
                  [GraphName] email
               [GraphFieldSelection]
                  [GraphName] age
```
> **Note**
> The parse the GraphQL-expression into an AST, use the [SchemaParser](/Chart.Core.Parsers/SchemaParser/).

> **Note**
> To print the AST, like done above, use the [DocumentPrinter](/Chart.Utils.Printer/).

To not clutter the output, descriptions are excluded.