using CRMDevLabs.VirtualEntity.Data;
using CRMDevLabs.VirtualEntity.Data.Clauses;
using CRMDevLabs.VirtualEntity.Data.Query;
using CRMDevLabs.VirtualEntity.Extensibility.Helpers;
using CRMDevLabs.VirtualEntity.Extensibility.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace CRMDevLabs.VirtualEntity.Web.Controllers
{
    public class PeopleController : ODataController
    {
        private readonly WideWorldImportersContext _repository;

        public PeopleController()
        {
            // This can be stored on Azure Key Vault
            var conn = ConfigurationManager.ConnectionStrings["SQL"].ConnectionString;
            _repository = new WideWorldImportersContext(conn);
        }

        [EnableQuery]
        public IQueryable<People> Get(ODataQueryOptions<People> queryOptions)
        {
            IList<People> materializedList = new List<People>();

            var top = queryOptions.Top?.RawValue;
            var skip = queryOptions.Skip?.RawValue;
            var order = queryOptions.OrderBy?.OrderByNodes.Select(x => new OrderClause { OrderDirection = x.Direction.ToString(), Field = (x as OrderByPropertyNode).Property.Name });
            var pk = UrlHelper.ExtractGuid(Request.RequestUri.AbsoluteUri);

            using (var fac = _repository.Configuration.BuildSessionFactory())
            {
                using (var sx = fac.OpenStatelessSession())
                {
                    var data = sx.QueryOver<People>();
                    data = data.Pagination(skip, top);
                    data = data.Ordering(order);
                    data = data.SingleRecord(pk, () => pk != Guid.Empty);

                    // Transformation
                    materializedList = data.Materialize();
                    foreach (var item in materializedList)
                    {
                        item.TransformObject();
                    }
                }
            }

            GC.Collect(2, GCCollectionMode.Forced);
            return materializedList.AsQueryable();
        }
    }
}