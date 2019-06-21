![Azure DevOps builds](https://dev.azure.com/eugenypetlakh/serilog.extension/_apis/build/status/Softeq.serilog.extension?branchName=master)
![NuGet](https://img.shields.io/nuget/v/Softeq.Serilog.Extension.svg)

# Query Utils

Query Utils extensions for IQueryable to perform Paging, Filer, Sorting

## Usage
Query Utils extension currently supports the following types of data manipulations, which you may implement in your query:
- **IPagedQuery** (allows you to get a sample of data spitted by pages)
- **IFilteredQuery** (allows to set collection of filters in request)
- **ISortedQuery** (allows `ASC/DESC` ordering response data by definite property)

Query implementation:
```csharp
    public class GetProfilesQuery : IPagedQuery, IFilteredQuery, ISortedQuery
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public ICollection<Filter> Filters { get; set; }
        public Sort Sort { get; set; }
    }
```
Filtering:
```csharp
public static IEnumerable<Func<IQueryable<UserProfile>, IQueryable<UserProfile>>> CreateFilters(this GetProfilesQuery query)
{
    var filters = new List<Func<IQueryable<UserProfile>, IQueryable<UserProfile>>>();
    var map = typeof(ProfileResponse).GetProperties()
            .Where(x => x.GetCustomAttribute<JsonPropertyAttribute>() != null)
            .ToLookup(x => x.GetCustomAttribute<JsonPropertyAttribute>().PropertyName);

    if (query.Filters != null)
    {
        foreach (var filter in query.Filters)
        {
            var property = map[filter.PropertyName].FirstOrDefault();
            if (property == null)
            {
                throw new QueryException($"Could not find '{filter.PropertyName}' in model scheme.");
            }

            switch (property.Name)
            {
                case nameof(ProfileResponse.FirstName):
                {
                    filters.Add(x => x.Where(e => e.FirstName.Contains(filter.Value)));
                    break;
                }
...
             }
         }
     }

     return filters;
}
```
Sorting:
```csharp
public static Func<IQueryable<UserProfile>, IQueryable<UserProfile>> CreateOrdering(this GetProfilesQuery query)
{
    Func<IQueryable<UserProfile>, IQueryable<UserProfile>> order = null;

    if (query.Sort != null)
    {
        var map = typeof(ProfileResponse).GetProperties()
               .Where(x => x.GetCustomAttribute<JsonPropertyAttribute>() != null)
               .ToLookup(x => x.GetCustomAttribute<JsonPropertyAttribute>().PropertyName);

        var property = map[query.Sort.PropertyName].FirstOrDefault();
        if (property == null)
        {
            throw new QueryException($"Could not find '{query.Sort.PropertyName}' in model scheme.");
        }

        Expression<Func<UserProfile, object>> sortExpression;

        switch (property.Name)
        {
            case nameof(ProfileResponse.FirstName):
            {
                sortExpression = x => x.FirstName;
                break;
            }
...
        }

        order = query.Sort.Order == SortOrder.Asc 
              ? (Func<IQueryable<UserProfile>, IQueryable<UserProfile>>) (x => x.OrderBy(sortExpression))
              : (x => x.OrderByDescending(sortExpression));
    }

    return order;
}
```


## About
This project is maintained by [Softeq Development Corp.](https://www.softeq.com/)
We specialize in .NET core applications.

 - [Facebook](https://web.facebook.com/Softeq.by/)
 - [Instagram](https://www.instagram.com/softeq/)
 - [Twitter](https://twitter.com/Softeq)
 - [Vk](https://vk.com/club21079655)

## Contributing
We welcome any contributions.

## License
The **Query Utils** project is available for free use, as described by the [LICENSE](/LICENSE) (MIT).
