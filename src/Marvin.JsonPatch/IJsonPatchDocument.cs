// Any comments, input: @KevinDockx
// Any issues, requests: https://github.com/KevinDockx/JsonPatch
//
// Enjoy :-)
using Marvin.JsonPatch.Operations;
using System.Collections.Generic;

namespace Marvin.JsonPatch
{
    public interface IJsonPatchDocument
    {
        IList<Operation> GetOperations();
    }
}
