JsonPatch
=========

JSON Patch (JsonPatchDocument) RFC 6902 implementation for .NET

NuGet package: 

JSON Patch (https://tools.ietf.org/html/rfc6902) defines a JSON document structure for expressing a sequence of operations to apply to a JavaScript Object Notation (JSON) document; it is suitable for use with the HTTP PATCH method. The "application/json-patch+json" media type is used to identify such patch documents.

One of the things this can be used for is partial updates for REST-ful API's, or, to quote the IETF: "This format is also potentially useful in other cases in which it is necessary to make partial updates to a JSON document or to a data structure that has similar constraints (i.e., they can be serialized as an object or an array using the JSON grammar)."

That's what this package is all about. Web API supports the HttpPatch method, but there's currently no implementation of the JsonPatchDocument in .NET, making it hard to pass in a set of changes that have to be applied - especially if you're working cross-platform and standardization of your API is essential.  

It consists of two parts:
- on the client (consumer of the API): the JsonPatchDocument / JsonPatchDocument<T> class to build what's essentially a change set to be applied to your object on your API side.
- at (Web) API level: an Apply method to apply those changes to your objects.

This combination should make partial update support for your RESTful API a breeze.

Here's how to use it:
- Build a patch document on the client.  You can use all operations as described in the IETF document: Add, Remove, Replace, Copy, Move & Test.

```
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


- On your API, in the patch method (accept document as parameter & use Apply method)

```
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


If you want to provide your own adapter (responsible for applying the operations to your objects), inherit IObjectAdapter, implement the interface and pass in an instance of that in the Apply method.

As the package is distributed as a Portable Class library, you can use it from ASP .NET, Windows Phone, Windows Store apps, ...

Please consider this an alpha version.  Not everything has been implemented (eg: ExpandoObject support), but the package is made with extensibility in mind.  Any and all comments, issues, ... are welcome.
