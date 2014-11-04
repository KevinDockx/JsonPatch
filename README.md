JsonPatch
=========

JSON Patch (JsonPatchDocument) RFC 6902 implementation for .NET

JSON Patch (https://tools.ietf.org/html/rfc6902) defines a JSON document structure for expressing a sequence of operations to apply to a JavaScript Object Notation (JSON) document; it is suitable for use with the HTTP PATCH method. The "application/json-patch+json" media type is used to identify such patch documents.

Web API supports the HttpPatch method, but there's currently no implementation of the JsonPatchDocument in .NET.  

It consists of two parts:
- on the client (consumer of the API): the JsonPatchDocument / JsonPatchDocument<T> class to build what's essentially a change set to be applied to your object on your API side.
- at (Web) API level: an Apply method to apply those changes to your objects.

This combination should make partial update support for your RESTful API a breeze.

Here's how to use it:
- Building a patch document on the client:


- On your API, in the patch method (accept document as parameter & use Apply method)



- if you want to provide your own adapter (responsible for applying the operations to your objects), inherit IObjectAdapter, implement the interface and pass in an instance of that in the Apply method.

Please consider this an alpha version.  Not everything has been implemented (eg: ExpandoObject support), but the package is made with extensibility in mind.  Any and all comments, issues, ... are welcome.
