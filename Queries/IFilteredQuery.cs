using System.Collections.Generic;

namespace Softeq.NoName.Components.QueryUtil.Queries
{
    public interface IFilteredQuery
    {
        ICollection<Filter> Filters { get; set; }
    }
}
