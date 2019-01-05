using CRMDevLabs.VirtualEntity.Extensibility.Contracts;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Query;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CRMDevLabs.VirtualEntity.Web.Extensions
{
    public static class EdmModelInspectorExtensions
    {
        public static void Inspect(this ODataConventionModelBuilder builder)
        {
            var path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "bin");
            var asmpath = $"{path}\\CRMDevLabs.VirtualEntity.Extensibility.dll";

            var extensibilityAssembly = Assembly.LoadFrom(asmpath);
            var tableTypes = from t in extensibilityAssembly.GetTypes() where t.GetInterfaces().Count(x => x.Name.Equals(nameof(ITable))) > 0 select t;

            foreach (var tableType in tableTypes)
            {
                builder.AddEntitySet(tableType.Name, new EntityTypeConfiguration(builder, tableType)
                {
                    QueryConfiguration = new QueryConfiguration
                    {
                        ModelBoundQuerySettings = new ModelBoundQuerySettings
                        {
                            Countable = true,
                            DefaultEnableFilter = true,
                            DefaultEnableOrderBy = true,
                            DefaultExpandType = SelectExpandType.Allowed,
                            DefaultSelectType = SelectExpandType.Allowed,
                            MaxTop = 5000,
                            PageSize = 250
                        }
                    }
                });

                var entityConfiguration = builder.AddEntityType(tableType);
                entityConfiguration.QueryConfiguration = new QueryConfiguration
                {
                    ModelBoundQuerySettings = new ModelBoundQuerySettings
                    {
                        Countable = true,
                        DefaultEnableFilter = true,
                        DefaultEnableOrderBy = true,
                        DefaultExpandType = SelectExpandType.Allowed,
                        DefaultSelectType = SelectExpandType.Allowed,
                        MaxTop = 5000,
                        PageSize = 250
                    }
                };
            }
        }
    }
}
