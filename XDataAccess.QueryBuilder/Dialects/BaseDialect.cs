using System;
using System.Collections.Generic;
using System.Text;

namespace XDataAccess.QueryBuilder.Dialects
{
    public abstract class BaseDialect : IDialect
    {
        public virtual string ParameterPrefix => "@";

        public virtual string OpeningIdentifier => "\"";

        public virtual string ClosingIdentifier => "\"";

        public virtual string Comma => ",";

        public virtual string StartGroup => "(";

        public virtual string EndGroup => ")";

        public virtual string Wildchar => "%";

        public virtual string Equal => "=";

        public virtual string NotEqual => "<>";

        public virtual string LessThan => "<";

        public virtual string LessThanOrEqual => "<=";

        public virtual string GreaterThan => ">";

        public virtual string GreaterThanOrEqual => ">=";

        public virtual string Is => "IS";

        public virtual string IsNot => "IS NOT";

        public virtual string Insert => "INSERT INTO";

        public virtual string Select => "SELECT";

        public virtual string Delete => "DELETE FROM";

        public virtual string Update => "UPDATE";

        public virtual string Values => "VALUES";

        public virtual string And => "AND";

        public virtual string Or => "OR";

        public virtual string Where => "WHERE";

        public virtual string Like => "LIKE";

        public virtual string Quote => "'";

        public virtual string AppendParameter => "+";

        public virtual string GetAttributeName(string entityName, string attributeName)
        {
            return $"{OpeningIdentifier}{entityName}{ClosingIdentifier}.{OpeningIdentifier}{attributeName}{ClosingIdentifier}";
        }

        public virtual string GetEntityName(string entityName)
        {
            return $"{OpeningIdentifier}{entityName}{ClosingIdentifier}";
        }
    }
}
