![Azure DevOps builds](https://dev.azure.com/eugenypetlakh/serilog.extension/_apis/build/status/Softeq.serilog.extension?branchName=master)
![NuGet](https://img.shields.io/nuget/v/Softeq.Serilog.Extension.svg)

# Query Utils

Query Utils extensions for IQueryable to perform Paging, Filer, Sorting

## Usage
Query Utils has interfaces:
- IPagedQuery - pagination results
```csharp
    public interface IPagedQuery
    {
        int Page { get; set; }
        int PageSize { get; set; }
    }
```
- IFilteredQuery - filter results in query
```csharp
    public interface IFilteredQuery
    {
        ICollection<Filter> Filters { get; set; }
    }
```
- ISortedQuery - sorting results in query
```csharp
    public interface ISortedQuery
    {
        Sort Sort { get; set; }
    }
```

Using in code
```csharp
    public class GetProfilesQuery : IPagedQuery, IFilteredQuery, ISortedQuery
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public ICollection<Filter> Filters { get; set; }
        public Sort Sort { get; set; }
    }
```
```csharp
     var getProfilesRequest = new GetProfilesQuery
     {
        Page = 1,
        PageSize = 2,
        Filters = new List<Filter> { new Filter { PropertyName = "PropertyName", Value = "Value" } },
        Sort = new Sort { PropertyName = "PropertyName", Order = SortOrder.Asc }
      };
```
```csharp
 var result = await _profileService.GetProfilesAsync(getProfilesRequest);
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
