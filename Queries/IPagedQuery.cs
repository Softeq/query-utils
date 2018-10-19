namespace Softeq.NoName.Components.QueryUtil.Queries
{
    public interface IPagedQuery
    {
        int Page { get; set; }
        int PageSize { get; set; }
    }
}
