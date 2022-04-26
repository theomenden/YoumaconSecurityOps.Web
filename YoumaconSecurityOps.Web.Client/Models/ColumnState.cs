namespace YoumaconSecurityOps.Web.Client.Models;
public class ColumnState
{
    /// <value>
    /// The Column to sort on
    /// </value>
    public string Field { get; init; }

    /// <value>
    /// The direction of the sort <c>asc</c>/<c>desc</c>
    /// </value>
    public SortDirection SortDirection { get; init; }
    
    /// <value>
    /// The value that the caller is searching for
    /// </value>
    public String SearchValue { get; init; }

    /// <summary>
    /// Determines the direction for sorting on a column
    /// </summary>
    /// <param name="sortDirection"></param>
    /// <returns>"asc" for ascending, "desc" for descending</returns>
    public String GetSortDirection() =>
        SortDirection switch
        {
            SortDirection.Ascending => DynamicSortingLabels.Ascending,
            SortDirection.Descending => DynamicSortingLabels.Descending,
            _ => String.Empty
        };
}

