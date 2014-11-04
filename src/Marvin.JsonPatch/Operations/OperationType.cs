using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Marvin.JsonPatch.Operations
{
    public enum OperationType
    {
        Add,
        Remove,
        Replace,
        Move,
        Copy,
        Test
    }
}
