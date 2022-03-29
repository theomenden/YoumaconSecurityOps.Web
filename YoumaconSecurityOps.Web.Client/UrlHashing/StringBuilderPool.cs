using System.Collections.Concurrent;
using System.Text;

namespace YoumaconSecurityOps.Web.Client.UrlHashing;
public class StringBuilderPool
{
    private readonly ConcurrentBag<StringBuilder> _builders = new();

    public StringBuilder GetStringBuilderFromPool => _builders.TryTake(out var sb) ? sb : new();

    public void ReturnStringBuilderToPool(StringBuilder sb)
    {
        sb.Clear();
        _builders.Add(sb);
    }
}
