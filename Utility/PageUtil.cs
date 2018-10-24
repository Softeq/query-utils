// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Softeq.QueryUtils
{
    public static class PageUtil
    {
        public static async Task<PagedResults<TReturn>> CreatePagedResultsAsync<T, TReturn>(
            IQueryable<T> queryable,
            int page,
            int pageSize,
            Func<T, TReturn> mapper)
        {
            var skipAmount = pageSize * (page - 1);

            var projection = queryable
                .Skip(skipAmount)
                .Take(pageSize);

            var totalNumberOfRecords = await queryable.CountAsync();
            var results = (await projection.ToListAsync()).Select(mapper);

            var mod = totalNumberOfRecords % pageSize;
            var totalPageCount = (totalNumberOfRecords / pageSize) + (mod == 0 ? 0 : 1);

            return new PagedResults<TReturn>
            {
                Results = results,
                PageNumber = page,
                PageSize = results.Count(),
                TotalNumberOfPages = totalPageCount
            };
        }

        public static PagedResults<TReturn> CreatePagedResults<T, TReturn>(
            IEnumerable<T> response,
            int page,
            int pageSize,
            Func<T, TReturn> mapper)
        {
            var skipAmount = pageSize * (page - 1);

            var projection = response
                .Skip(skipAmount)
                .Take(pageSize);

            var totalNumberOfRecords = response.Count();
            var results = projection.Select(mapper);

            var mod = totalNumberOfRecords % pageSize;
            var totalPageCount = (totalNumberOfRecords / pageSize) + (mod == 0 ? 0 : 1);

            return new PagedResults<TReturn>
            {
                Results = results,
                PageNumber = page,
                PageSize = results.Count(),
                TotalNumberOfPages = totalPageCount
            };
        }

        public static PagedResults<T> CreatePagedResults<T>(
            IEnumerable<T> response,
            int page,
            int pageSize)
        {
            var skipAmount = pageSize * (page - 1);

            var projection = response.Skip(skipAmount).Take(pageSize);

            var totalNumberOfRecords = response.Count();

            var mod = totalNumberOfRecords % pageSize;
            var totalPageCount = (totalNumberOfRecords / pageSize) + (mod == 0 ? 0 : 1);

            return new PagedResults<T>
            {
                Results = projection,
                PageNumber = page,
                PageSize = projection.Count(),
                TotalNumberOfPages = totalPageCount
            };
        }
    }
}
