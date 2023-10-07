using System.Collections.Generic;

namespace Newtonsoft.Json.Linq.JsonPath
{
    internal class QueryFilter : PathFilter
    {
        internal QueryExpression Expression;

        public QueryFilter(QueryExpression expression)
        {
            Expression = expression;
        }

        public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current,
            bool errorWhenNoMatch)
        {
            foreach (var t in current)
            foreach (var v in t)
                if (Expression.IsMatch(root, v))
                    yield return v;
        }
    }
}