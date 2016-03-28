using Apworks.Repositories.EntityFramework.Properties;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apworks.Specifications;
using Apworks.Querying;
using System.Linq.Expressions;

namespace Apworks.Repositories.EntityFramework
{
    internal sealed class EntityFrameworkRepository<TKey, TAggregateRoot> : Repository<TKey, TAggregateRoot>
        where TKey : IEquatable<TKey>
        where TAggregateRoot : class, IAggregateRoot<TKey>
    {
        private readonly DbContext dbContext;

        public EntityFrameworkRepository(IRepositoryContext context)
            : base(context)
        {
            this.dbContext = (context as EntityFrameworkRepositoryContext)?.DbContext;
            if (this.dbContext == null)
            {
                throw new RepositoryException(Resources.InvalidDbContextInstance);
            }
        }

        public override IQueryable<TAggregateRoot> FindAll()
        {
            return FindAll(new AnySpecification<TAggregateRoot>(), SortSpecification<TKey, TAggregateRoot>.None);
        }

        public override IQueryable<TAggregateRoot> FindAll(Specification<TAggregateRoot> specification, SortSpecification<TKey, TAggregateRoot> sortSpecification)
        {
            var query = this.dbContext.Set<TAggregateRoot>().Where(specification);
            IOrderedQueryable<TAggregateRoot> orderedQueryable;
            if (sortSpecification?.Count > 0)
            {
                var sortSpecificationList = sortSpecification.Specifications.ToList();
                var firstSortSpecification = sortSpecificationList[0];
                switch(firstSortSpecification.Item2)
                {
                    case SortOrder.Ascending:
                        orderedQueryable = query.OrderBy(firstSortSpecification.Item1);
                        break;
                    case SortOrder.Descending:
                        orderedQueryable = query.OrderByDescending(firstSortSpecification.Item1);
                        break;
                    default:
                        return query;
                }
                for (var i = 1; i < sortSpecificationList.Count; i++)
                {
                    var spec = sortSpecificationList[i];
                    switch(spec.Item2)
                    {
                        case SortOrder.Ascending:
                            orderedQueryable = orderedQueryable.ThenBy(spec.Item1);
                            break;
                        case SortOrder.Descending:
                            orderedQueryable = orderedQueryable.ThenByDescending(spec.Item1);
                            break;
                        default:
                            continue;
                    }
                }
                return orderedQueryable;
            }
            return query;
        }

        public override void Add(TAggregateRoot aggregateRoot)
        {
            this.dbContext.Entry(aggregateRoot).State = EntityState.Added;
        }

        public override void Update(TAggregateRoot aggregateRoot)
        {
            this.dbContext.Entry(aggregateRoot).State = EntityState.Modified;
        }

        public override void Remove(TAggregateRoot aggregateRoot)
        {
            this.dbContext.Entry(aggregateRoot).State = EntityState.Deleted;
        }

        public override bool Exists(Specification<TAggregateRoot> specification)
        {
            return this.dbContext.Set<TAggregateRoot>().Any(specification);
        }

    }
}
