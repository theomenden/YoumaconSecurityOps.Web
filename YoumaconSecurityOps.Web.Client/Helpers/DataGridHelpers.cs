namespace YoumaconSecurityOps.Web.Client.Helpers;

public class DataGridHelpers<T>
{
    private readonly DataGridReadDataEventArgs<T> _gridReadEventArgs;

    private readonly List<ColumnState> _columnInfo;

    public DataGridHelpers(DataGridReadDataEventArgs<T> gridReadEventArgs)
    {
        _gridReadEventArgs = gridReadEventArgs;

        _columnInfo = GetColumnState().ToList();
    }
    
    public IEnumerable<ColumnState> ColumnStates => _columnInfo;

    private IEnumerable<ColumnState> GetColumnState()
    {
        return _gridReadEventArgs.Columns.Select(column => new ColumnState
        {
            SortDirection = column.SortDirection,
            Field = column.Field,
            SearchValue = column.SearchValue?.ToString() ?? string.Empty,
        });
    }
}

