using System;
using System.Collections.Generic;
using System.Text;

namespace XDataAccess.QueryBuilder.Dialects
{
    public interface IDialect
    {
        string ParameterPrefix { get; }
        string OpeningIdentifier { get; }
        string ClosingIdentifier { get; }
        string Comma { get; }
        string StartGroup { get; }
        string EndGroup { get; }
        string Wildchar { get; }
        string Equal { get; }
        string NotEqual { get; }
        string LessThan { get; }
        string LessThanOrEqual { get; }
        string GreaterThan { get; }
        string GreaterThanOrEqual { get; }
        string Is { get; }
        string IsNot { get; }

        string Insert { get; }
        string Select { get; }

        string Delete { get; }

        string Update { get; }

        string Values { get; }

        string And { get; }

        string Or { get; }

        string Where { get; }

        string Like { get; }

        string Quote { get; }

        string AppendParameter { get; }

        string GetEntityName(string entityName);

        string GetAttributeName(string entityName, string attributeName);
    }
}
