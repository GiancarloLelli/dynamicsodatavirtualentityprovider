using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMDevLabs.VirtualEntity.Plugins.Models
{
    public class ExecutingUserInfo
    {
        public Guid InitiatinUserId { get; internal set; }
        public Guid CorrelationId { get; internal set; }
        public Guid OperationId { get; internal set; }
        public Guid BusinessUnitId { get; internal set; }
        public Guid OrganizationId { get; internal set; }
        public string OrganizationName { get; internal set; }
        public Guid? RequestId { get; internal set; }
        public Guid UserId { get; internal set; }
    }
}
