# Parsers

This directory contains multiple parsers/converters/serializers for GraphQL schemas, IR and C# models. As part of the making an execution context for the resolver, we need to be able to parse and validate GraphQL schemas, which takes place here.

All the parsers take use of the IR (Intermediate representation) structures defined in `Chart.Models.AST`.

Each parser has it's own responsibility, as described below:

```
Tokenizer:     GraphQL SDL -> Tokens

SchemaParser:  GraphQL SDL -> IR
SchemaWriter:  IR -> GraphQL SDL

ModelParser:   C# models -> IR
```