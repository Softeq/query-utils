// Developed by Softeq Development Corporation
// http://www.softeq.com

namespace Softeq.QueryUtils
{
    public interface IPagedQuery
    {
        int Page { get; set; }
        int PageSize { get; set; }
    }
}
