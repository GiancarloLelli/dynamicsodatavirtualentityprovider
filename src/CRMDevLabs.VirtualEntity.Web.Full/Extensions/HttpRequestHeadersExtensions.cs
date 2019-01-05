using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace CRMDevLabs.VirtualEntity.Web.Full.Extensions
{
    public static class HttpRequestHeadersExtensions
    {
        public static bool CompareValue(this HttpRequestHeaders headers, string key, string value)
        {
            var result = false;

            if (headers.TryGetValues(key, out IEnumerable<string> results))
            {
                var first = results.FirstOrDefault();
                result = first != null ? first.Equals(value) : false;
            }

            return result;
        }
    }
}