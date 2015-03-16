using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marvin.JsonPatch.XUnitTest
{

    /// <summary>
    /// Error Handling
    /// 
    /// There are several known conditions under which a PATCH request can
    /// fail.
    /// 
    /// Malformed patch document:  When the server determines that the patch
    ///    document provided by the client is not properly formatted, it
    ///    SHOULD return a 400 (Bad Request) response.  The definition of
    ///    badly formatted depends on the patch document chosen.
    /// 
    /// Unsupported patch document:  Can be specified using a 415
    ///    (Unsupported Media Type) response when the client sends a patch
    ///    document format that the server does not support for the resource
    ///    identified by the Request-URI.  Such a response SHOULD include an
    ///    Accept-Patch response header as described in Section 3.1 to notify
    ///    the client what patch document media types are supported.
    /// 
    /// Unprocessable request:  Can be specified with a 422 (Unprocessable
    ///    Entity) response ([RFC4918], Section 11.2) when the server
    ///    understands the patch document and the syntax of the patch
    ///    document appears to be valid, but the server is incapable of
    ///    processing the request.  This might include attempts to modify a
    ///    resource in a way that would cause the resource to become invalid;
    ///    for instance, a modification to a well-formed XML document that
    ///    would cause it to no longer be well-formed.  There may also be
    ///    more specific errors like "Conflicting State" that could be
    ///    signaled with this status code, but the more specific error would
    ///    generally be more helpful.
    /// 
    /// Resource not found:  Can be specified with a 404 (Not Found) status
    ///    code when the client attempted to apply a patch document to a non-
    ///    existent resource, but the patch document chosen cannot be applied
    ///    to a non-existent resource.
    /// 
    /// Conflicting state:  Can be specified with a 409 (Conflict) status
    ///    code when the request cannot be applied given the state of the
    ///    resource.  For example, if the client attempted to apply a
    ///    structural modification and the structures assumed to exist did
    ///    not exist (with XML, a patch might specify changing element 'foo'
    ///    to element 'bar' but element 'foo' might not exist).
    /// 
    /// Conflicting modification:  When a client uses either the If-Match or
    ///    If-Unmodified-Since header to define a precondition, and that
    ///    precondition failed, then the 412 (Precondition Failed) error is
    ///    most helpful to the client.  However, that response makes no sense
    ///    if there was no precondition on the request.  In cases when the
    ///    server detects a possible conflicting modification and no
    ///    precondition was defined in the request, the server can return a
    ///    409 (Conflict) response.
    /// 
    /// Concurrent modification:  Some applications of PATCH might require
    ///    the server to process requests in the order in which they are
    ///    received.  If a server is operating under those restrictions, and
    ///    it receives concurrent requests to modify the same resource, but
    ///    is unable to queue those requests, the server can usefully
    ///    indicate this error by using a 409 (Conflict) response.
    /// 
    /// Note that the 409 Conflict response gives reasonably consistent
    /// information to clients.  Depending on the application and the nature
    /// of the patch format, the client might be able to reissue the request
    /// as is (e.g., an instruction to append a line to a log file), have to
    /// retrieve the resource content to recalculate a patch, or have to fail
    /// the operation.
    /// 
    /// Other HTTP status codes can also be used under the appropriate
    /// circumstances.
    /// 
    /// The entity body of error responses SHOULD contain enough information
    /// to communicate the nature of the error to the client.  The content-
    /// type of the response entity can vary across implementations.
    /// </summary>
    class ErrorHandlingTests
    {
    }
}
