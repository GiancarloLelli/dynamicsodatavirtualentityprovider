using CRMDevLabs.VirtualEntity.Extensibility.Models;
using FluentNHibernate.Mapping;

namespace CRMDevLabs.VirtualEntity.Data.Mappers
{
    public class PeopleMapper : ClassMap<People>
    {
        public PeopleMapper()
        {
            Id(x => x.PersonID);
            Map(x => x.Id).Access.Property().Formula("(SELECT CAST(HASHBYTES('MD5', CONVERT(NVARCHAR(MAX), PersonID)) AS UNIQUEIDENTIFIER))");
            Map(x => x.EmailAddress);
            Map(x => x.FullName);
            Table("[Application].[People]");
        }
    }
}
