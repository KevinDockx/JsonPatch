// Any comments, input: @KevinDockx
// Any issues, requests: https://github.com/KevinDockx/JsonPatch
//
// Enjoy :-)

using Marvin.JsonPatch.Operations;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace Marvin.JsonPatch
{
    public interface IJsonPatchDocument
    {
        IContractResolver ContractResolver { get; set; }

        IList<Operation> GetOperations();
    }
}
