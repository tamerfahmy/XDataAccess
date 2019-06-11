using System;
using System.Collections.Generic;
using System.Text;

namespace XDataAccess.QueryBuilder.Dialects.Databases
{
    public sealed class MySqlDialect : BaseDialect
    {
        public override string OpeningIdentifier => "`";

        public override string ClosingIdentifier => "`";

        public override string AppendParameter => ",";
    }
}
