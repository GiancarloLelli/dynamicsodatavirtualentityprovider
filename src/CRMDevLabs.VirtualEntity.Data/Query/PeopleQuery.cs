using CRMDevLabs.VirtualEntity.Data.Clauses;
using CRMDevLabs.VirtualEntity.Extensibility.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NH = NHibernate.Criterion;

namespace CRMDevLabs.VirtualEntity.Data.Query
{
    public static class PeopleQuery
    {
        public static IList<People> Materialize(this IQueryOver<People, People> model) => model.List();

        public static IQueryOver<People, People> Pagination(this IQueryOver<People, People> model, string skip, string top)
        {
            var skipAsInteger = string.IsNullOrEmpty(skip) ? 0 : int.Parse(skip);
            var topAsInteger = string.IsNullOrEmpty(top) ? 0 : int.Parse(top);

            if (skipAsInteger > 0)
                model.Skip(skipAsInteger);

            if (topAsInteger > 0)
                model.Take(topAsInteger);

            return model;
        }

        public static IQueryOver<People, People> Ordering(this IQueryOver<People, People> model, IEnumerable<OrderClause> orderClauses)
        {
            if (orderClauses != null)
            {
                var first = orderClauses.First();
                var x = Expression.Parameter(typeof(People), "x");
                var body = Expression.PropertyOrField(x, first.Field);
                var conversion = Expression.Convert(body, typeof(object));
                var lambda = Expression.Lambda<Func<People, object>>(conversion, x);
                model = first.OrderDirection.ToLower().Equals("ascending", StringComparison.InvariantCultureIgnoreCase) ? model.OrderBy(lambda).Asc : model.OrderBy(lambda).Desc;

                if (orderClauses.Count() > 1)
                {
                    foreach (var thenByOrder in orderClauses.Skip(1))
                    {
                        var y = Expression.Parameter(typeof(People), "y");
                        var thenByBody = Expression.PropertyOrField(y, thenByOrder.Field);
                        var thenByConversion = Expression.Convert(thenByBody, typeof(object));
                        var thenByLambda = Expression.Lambda<Func<People, object>>(thenByConversion, y);
                        model = thenByOrder.OrderDirection.ToLower().Equals("ascending", StringComparison.InvariantCultureIgnoreCase) ? model.ThenBy(thenByLambda).Asc : model.ThenBy(thenByLambda).Desc;
                    }
                }

            }

            return model;
        }

        public static IQueryOver<People, People> SingleRecord(this IQueryOver<People, People> model, Guid pk, Func<bool> shouldApply)
        {
            if (shouldApply.Invoke())
            {
                model = model.Where(NH.Restrictions.Eq("Id", pk));
            }

            return model;
        }
    }
}
