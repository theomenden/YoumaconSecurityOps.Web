namespace YoumaconSecurityOps.Web.Client.Models;
public class ColumnState
{
    /// <value>
    /// The Field to sort on
    /// </value>
    public string Field { get; set; }

    /// <value>
    /// The direction of the sort <c>asc</c>/<c>desc</c>
    /// </value>
    public SortDirection SortDirection { get; set; }
    
    /// <value>
    /// 
    /// </value>
    public String SearchValue { get; set; }

    /// <summary>
    /// Determines the direction for sorting on a column
    /// </summary>
    /// <param name="sortDirection"></param>
    /// <returns>"asc" for ascending, "desc" for descending</returns>
    public String GetSortDirection() =>
        SortDirection switch
        {
            SortDirection.Ascending => "asc",
            SortDirection.Descending => "desc",
            _ => String.Empty
        };
}

