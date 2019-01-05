using CRMDevLabs.VirtualEntity.Extensibility.Helpers;
using System;
using System.Collections.Generic;

namespace CRMDevLabs.VirtualEntity.Extensibility.Base
{
    public class ObjectModelBase
    {
        public virtual Dictionary<string, object> DynamicProperties { get; set; }

        public ObjectModelBase()
        {
            DynamicProperties = new Dictionary<string, object>();
        }

        protected Guid GenerateDeterministicGuid(string externalPkField) => DeterministicGuidHelper.Generate(externalPkField);
    }
}
