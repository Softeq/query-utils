// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Collections.Generic;

namespace Softeq.QueryUtils
{
    public interface IFilteredQuery
    {
        ICollection<Filter> Filters { get; set; }
    }
}
