using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace Marvin.JsonPatch.Adapters
{
    public class ExpandoObjectAdapter : IDynamicObjectAdapter<ExpandoObject>
    {

        public void Add(Operations.Operation operation, ExpandoObject objectToApplyTo)
        {
            throw new NotImplementedException();
        }

        public void Copy(Operations.Operation operation, ExpandoObject objectToApplyTo)
        {
            throw new NotImplementedException();
        }

        public void Move(Operations.Operation operation, ExpandoObject objectToApplyTo)
        {
            throw new NotImplementedException();
        }

        public void Remove(Operations.Operation operation, ExpandoObject objectToApplyTo)
        {
            throw new NotImplementedException();
        }

        public void Replace(Operations.Operation operation, ExpandoObject objectToApplyTo)
        {
            throw new NotImplementedException();
        }

        public void Test(Operations.Operation operation, ExpandoObject objectToApplyTo)
        {
            throw new NotImplementedException();
        }
    }
}
