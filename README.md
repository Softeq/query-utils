![Azure DevOps builds](https://dev.azure.com/eugenypetlakh/serilog.extension/_apis/build/status/Softeq.serilog.extension?branchName=master)
![NuGet](https://img.shields.io/nuget/v/Softeq.Serilog.Extension.svg)

# Query Utils

Query Utils extensions for IQueryable to perform Paging, Filer, Sorting

## Usage
Query Utils has interfaces:
- IPagedQuery - pagination results
- IFilteredQuery - filter results in query
- ISortedQuery - sorting results in query

Query class
```csharp
    public class GetProfilesQuery : IPagedQuery, IFilteredQuery, ISortedQuery
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public ICollection<Filter> Filters { get; set; }
        public Sort Sort { get; set; }
    }
```
Using in code
```csharp
var filters = query.CreateFilters();
var ordering = query.CreateOrdering();

var querySpecification = new QuerySpecification<UserProfile>()
                .WithFilters(filters)
                .WithOrdering(ordering);

var profiles = UnitOfWork.UserProfileRepository.GetAll();
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

The Query Utils project is available for free use, as described by the [LICENSE](/LICENSE) (MIT).
