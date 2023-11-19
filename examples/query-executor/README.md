# Query executor

Execute a GraphQL query against a model, using the `IQueryExecutor`-class.

## Define the model

First, define the model in C#. This can be any model, as long as the fields/properties and/or methods are public.
```cs
public class Query
{
    public User GetUser() => new User();
}

public class User
{
    public string Username { get; set; } = "maxnatamo";

    public string Name { get; set; } = "Max T. Kristiansen";
}
```
> NOTE: The query-model doesn't need to be named anything specific, but it helps with organization.

## Define the schema

The schema is the foundation of GraphQL. It defines all the possible fields and directives that the endpoint supports.
```cs
Schema schema = Schema
    .Create(c => c.AddType<Query>());
```

## Create an executor

To execute a query against the schema, you need to create an executor.
```cs
IQueryExecutor executor = schema.MakeExecutable();
```
> NOTE: This executor can be re-used as much as you want. In fact, it is recommended to.

## Execute a query

Lastly, send a request.

Method 1:
```cs
QueryRequest request = new QueryRequestBuilder()
    .SetQuery("{ user { username name } }")
    .Create();

ExecutionResult result = await executor.ExecuteAsync(request);
```

Method 2:
```cs
ExecutionResult result = await executor.ExecuteAsync("{ user { username name } }");
```

Both methods are completely valid. `QueryRequestBuilder` allows for more complex queries, such as variables.

The results are returned in a dictionary:
```cs
string username = result.Data["username"].ToString();
string name = result.Data["name"].ToString();
```