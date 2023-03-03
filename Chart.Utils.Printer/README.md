# DocumentPrinter

The `DocumentPrinter` is used to pretty-print ASTs, as defined in the [Chart.Models.AST](/Chart.Models.AST/)-directory.

The DocumentPrinter can be used to print ASTs in the following format:
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

## Getting started

`DocumentPrinter` does not take GraphQL-expressions directly. Instead, is descends into a `GraphDocument`-object, which can be obtained by parsing the expression, using the [SchemaParser](/Chart.Core.Parsers/SchemaParser//)-class.

```cs
SchemaParser parser = new SchemaParser();
DocumentPrinter printer = new DocumentPrinter();

string expression = @"
query getUser($id: Int) {
    user(id: $id) {
        username
        email age
    }
}";

// Parse the expression into a GraphDocument-object.
GraphDocument document = parser.Parse(expression);

// Make the printer descend into the document.
printer.Visit(document);

Console.WriteLine(printer.ToString());
```