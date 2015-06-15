using Marvin.JsonPatch.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Marvin.JsonPatch
{
    public interface IJsonPatchDocument
    {
        List<Operation> GetOperations();
    }
}
