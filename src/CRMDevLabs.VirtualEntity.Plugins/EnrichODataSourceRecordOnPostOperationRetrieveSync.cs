using CRMDevLabs.VirtualEntity.Extensibility.Schema;
using CRMDevLabs.VirtualEntity.Plugins.Models;
using Microsoft.Xrm.Sdk;
using System;

namespace CRMDevLabs.VirtualEntity.Plugins
{
    public class EnrichODataSourceRecordOnPostOperationRetrieveSync : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            try
            {
                var context = serviceProvider.GetService(typeof(IPluginExecutionContext)) as IPluginExecutionContext;
                var messageCondition = !context.MessageName.Equals(Messages.Retrieve, StringComparison.InvariantCultureIgnoreCase);
                var entityCondition = !context.PrimaryEntityName.Equals(nameof(msdyn_odatav4ds), StringComparison.InvariantCultureIgnoreCase);
                var target = context.OutputParameters.ContainsKey(PluginInput.BusinessEntity) ? context.OutputParameters[PluginInput.BusinessEntity] as Entity : null;

                if (messageCondition || entityCondition || target == null || context.Depth > 2)
                    return;

                var info = new ExecutingUserInfo
                {
                    BusinessUnitId = context.BusinessUnitId,
                    CorrelationId = context.CorrelationId,
                    InitiatinUserId = context.InitiatingUserId,
                    OperationId = context.OperationId,
                    OrganizationId = context.OrganizationId,
                    OrganizationName = context.OrganizationName,
                    RequestId = context.RequestId,
                    UserId = context.UserId
                };

                var enrichedDataSource = ExecuteLogic(info, target);
                context.OutputParameters[PluginInput.BusinessEntity] = enrichedDataSource;
            }
            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException(ex.Message, ex);
            }
        }

        public void ExecuteWrapper(ExecutingUserInfo mockInfo, Entity ds) => ExecuteLogic(mockInfo, ds);

        private Entity ExecuteLogic(ExecutingUserInfo info, Entity dataSource)
        {
            dataSource[msdyn_odatav4ds.msdyn_isparameter1header] = true;
            dataSource[msdyn_odatav4ds.msdyn_parameter1name] = Headers.BU;
            dataSource[msdyn_odatav4ds.msdyn_parameter1value] = info.BusinessUnitId.ToString();

            dataSource[msdyn_odatav4ds.msdyn_isparameter2header] = true;
            dataSource[msdyn_odatav4ds.msdyn_parameter2name] = Headers.Correlation;
            dataSource[msdyn_odatav4ds.msdyn_parameter2value] = info.CorrelationId.ToString();

            dataSource[msdyn_odatav4ds.msdyn_isparameter3header] = true;
            dataSource[msdyn_odatav4ds.msdyn_parameter3name] = Headers.InitiatingUser;
            dataSource[msdyn_odatav4ds.msdyn_parameter3value] = info.InitiatinUserId.ToString();

            dataSource[msdyn_odatav4ds.msdyn_isparameter4header] = true;
            dataSource[msdyn_odatav4ds.msdyn_parameter4name] = Headers.Operation;
            dataSource[msdyn_odatav4ds.msdyn_parameter4value] = info.OperationId.ToString();

            dataSource[msdyn_odatav4ds.msdyn_isparameter5header] = true;
            dataSource[msdyn_odatav4ds.msdyn_parameter5name] = Headers.Organization;
            dataSource[msdyn_odatav4ds.msdyn_parameter5value] = info.OrganizationId.ToString();

            dataSource[msdyn_odatav4ds.msdyn_isparameter6header] = true;
            dataSource[msdyn_odatav4ds.msdyn_parameter6name] = Headers.OrganizationName;
            dataSource[msdyn_odatav4ds.msdyn_parameter6value] = info.OrganizationName;

            dataSource[msdyn_odatav4ds.msdyn_isparameter7header] = true;
            dataSource[msdyn_odatav4ds.msdyn_parameter7name] = Headers.Request;
            dataSource[msdyn_odatav4ds.msdyn_parameter7value] = info.RequestId.ToString();

            dataSource[msdyn_odatav4ds.msdyn_isparameter8header] = true;
            dataSource[msdyn_odatav4ds.msdyn_parameter8name] = Headers.User;
            dataSource[msdyn_odatav4ds.msdyn_parameter8value] = info.UserId.ToString();

            return dataSource;
        }
    }
}
