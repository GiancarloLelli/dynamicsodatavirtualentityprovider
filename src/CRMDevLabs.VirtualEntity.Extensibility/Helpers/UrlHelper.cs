using System;

namespace CRMDevLabs.VirtualEntity.Extensibility.Helpers
{
    public class UrlHelper
    {
        public static Guid ExtractGuid(string path)
        {
            var pk = Guid.Empty;

            if (path.Contains("(") && path.Contains(")"))
            {
                var openIndex = path.IndexOf('(');
                var closeIndex = path.IndexOf(')');
                var subString = path.Substring((openIndex + 1), ((closeIndex - openIndex) - 1));
                Guid.TryParse(subString, out pk);
            }

            return pk;
        }
    }
}
