using System.Collections.Generic;

namespace Newtonsoft.Json.Linq.JsonPath
{
    internal class QueryScanFilter : PathFilter
    {
        internal QueryExpression Expression;

        public QueryScanFilter(QueryExpression expression)
        {
            Expression = expression;
        }

        public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current,
            bool errorWhenNoMatch)
        {
            foreach (var t in current)
                if (t is JContainer c)
                {
                    foreach (var d in c.DescendantsAndSelf())
                        if (Expression.IsMatch(root, d))
                            yield return d;
                }
                else
                {
                    if (Expression.IsMatch(root, t)) yield return t;
                }
        }
    }
}