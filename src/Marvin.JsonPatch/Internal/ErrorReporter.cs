using Marvin.JsonPatch.Exceptions;
using System;

namespace Marvin.JsonPatch.Internal
{
    internal static class ErrorReporter
    {
        public static Action<JsonPatchError> Default = (e) => throw new JsonPatchException(e, 422);
    }
}
