using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Softeq.NoName.Components.QueryUtil.Queries
{
    public class QuerySpecification<TEntity>
    {
        private bool _applyFiltering;
        private readonly List<Func<IQueryable<TEntity>, IQueryable<TEntity>>> _filters;

        private bool _applyPagination;
        private int _take;

        private bool _applyOrdering;
        private Func<IQueryable<TEntity>, IQueryable<TEntity>> _ordering;

        public QuerySpecification()
        {
            _filters = new List<Func<IQueryable<TEntity>, IQueryable<TEntity>>>();
        }

        public static QuerySpecification<TEntity> Default => new QuerySpecification<TEntity>();

        public int Skip { get; private set; }

        public QuerySpecification<TEntity> WithOrdering(Func<IQueryable<TEntity>, IQueryable<TEntity>> ordering)
        {
            if (ordering != null)
            {
                _applyOrdering = true;
                _ordering = ordering;
            }
            return this;
        }

        public QuerySpecification<TEntity> WithFilters(IEnumerable<Func<IQueryable<TEntity>, IQueryable<TEntity>>> filters)
        {
            foreach (var filter in filters)
            {
                WithFilter(filter);
            }
            return this;
        }

        public QuerySpecification<TEntity> WithFilter(Func<IQueryable<TEntity>, IQueryable<TEntity>> filter)
        {
            if (filter != null)
            {
                _applyFiltering = true;

                _filters.Add(filter);
            }
            return this;
        }

        public IQueryable<TEntity> ApplyFiltering(IQueryable<TEntity> query)
        {
            if (_applyFiltering)
            {
                foreach (var filter in _filters)
                {
                    query = filter(query);
                }
            }

            if (_applyOrdering)
            {
                query = _ordering(query);
            }

            return query;
        }
    }
}
