JSON Patch for .NET (Web API / clients)
=======================================

JSON Patch (JsonPatchDocument) RFC 6902 implementation for .NET to easily allow & apply partial REST-ful service (through Web API) updates from any client (portable class library).  

NuGet package: https://www.nuget.org/packages/Marvin.JsonPatch

This component supports staticly typed objects.  Dynamically typed objects are supported as well: have a look at Marvin.JsonPatch.Dynamic (https://github.com/KevinDockx/JsonPatch.Dynamic). 

Important: if you're using ASP.NET Core, you don't need this component - I've been working with Microsoft on integrating this into ASP.NET Core.  Have a look at http://blog.kevindockx.com/post/Partial-Updates-for-Your-RESTful-API-with-JsonPatch-in-the-new-ASP-NET-5.aspx for more information, or have a look at http://benfoster.io/blog/aspnet-core-json-patch-partial-api-updates for a more recent post on this.

JSON Patch (https://tools.ietf.org/html/rfc6902) defines a JSON document structure for expressing a sequence of operations to apply to a JavaScript Object Notation (JSON) document; it is suitable for use with the HTTP PATCH method. The "application/json-patch+json" media type is used to identify such patch documents.

One of the things this can be used for is partial updates for REST-ful API's, or, to quote the IETF: "This format is also potentially useful in other cases in which it is necessary to make partial updates to a JSON document or to a data structure that has similar constraints (i.e., they can be serialized as an object or an array using the JSON grammar)."

That's what this package is all about. Web API supports the HttpPatch method, but there's currently no implementation of the JsonPatchDocument in .NET, making it hard to pass in a set of changes that have to be applied - especially if you're working cross-platform and standardization of your API is essential.  

It consists of two parts:
- on the client (consumer of the API): the JsonPatchDocument / JsonPatchDocument<T> class to build what's essentially a change set to be applied to your object on your API side.
- at (Web) API level: an ApplyTo method to apply those changes to your objects.

This combination should make partial update support for your RESTful API a breeze.

Here's how to use it:
- Build a patch document on the client.  You can use the operations as described in the IETF document: Add, Remove, Replace, Copy, Move.  Test support is planned.

```csharp
JsonPatchDocument<DTO.Expense> patchDoc = new JsonPatchDocument<DTO.Expense>();
patchDoc.Replace(e => e.Description, expense.Description);

// serialize
var serializedItemToUpdate = JsonConvert.SerializeObject(patchDoc);

// create the patch request
var method = new HttpMethod("PATCH");
var request = new HttpRequestMessage(method, "api/expenses/" + id)
{
    Content = new StringContent(serializedItemToUpdate,
    System.Text.Encoding.Unicode, "application/json")
};

// send it, using an HttpClient instance
client.SendAsync(request);
```

- On your API, in the patch method (accept document as parameter & use ApplyTo method)

```csharp
[Route("api/expenses/{id}")]
[HttpPatch]
public IHttpActionResult Patch(int id, [FromBody]JsonPatchDocument<DTO.Expense> expensePatchDocument)
{
      // get the expense from the repository
      var expense = _repository.GetExpense(id);

      // apply the patch document 
      expensePatchDocument.ApplyTo(expense);

      // changes have been applied.  Submit to backend, ... 
}
```


If you want to provide your own adapter (responsible for applying the operations to your objects), create a new class that implements the IObjectAdapter interface, and pass in an instance of that class in the ApplyTo method.

A few more examples of how you can create a JsonPatchDocument:

```csharp
JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();

// add "4" to a list of integers at position 0
patchDoc.Add<int>(o => o.IntegerList, 4, 0);

// add "4" to the end of that list
patchDoc.Add<int>(o => o.IntegerList, 5);

// remove the current value of StringProperty
patchDoc.Remove<string>(o => o.StringProperty);

// remove the value at position two from a list of integers
patchDoc.Remove<int>(o => o.IntegerList, 2);

// replace StringProperty with value "B"
patchDoc.Replace<string>(o => o.StringProperty, "B");

// replace value at position 4 in a list of integers with value 5
patchDoc.Replace<int>(o => o.IntegerList, 5, 4);

//copy value IntegerValue to position 0 in a list of integers
patchDoc.Copy<int>(o => o.IntegerValue, o => o.IntegerList, 0);

// move the integers at position 0 in a list of integers to position 1 in that same list
patchDoc.Move<int>(o => o.IntegerList, 0, o => o.IntegerList, 1);
```

To create a JsonPatchDocument directly in JSON:

```
[
    { "op": "add", "path": "/foo", "value": "bar"},
    { "op": "replace", "path": "/baz", "value": "boo" }
]
```

As the package is distributed as a Portable Class library, you can use it from (ASP) .NET (4+), Windows Phone (8.1), Windows Store apps (8+), Xamarin, ...


Any and all comments, issues, ... are welcome. :-)
