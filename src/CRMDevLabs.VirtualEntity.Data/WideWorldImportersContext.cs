using CRMDevLabs.VirtualEntity.Data.Mappers;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;

namespace CRMDevLabs.VirtualEntity.Data
{
    public class WideWorldImportersContext
    {
        public readonly Configuration Configuration;

        public WideWorldImportersContext(string connection)
        {
            Configuration = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connection))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<PeopleMapper>())
                .BuildConfiguration();
        }
    }
}
