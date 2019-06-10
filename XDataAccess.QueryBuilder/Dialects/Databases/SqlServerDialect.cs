using System;
using System.Collections.Generic;
using System.Text;

namespace XDataAccess.QueryBuilder.Dialects.Databases
{
    public sealed class SqlServerDialect : BaseDialect
    {
        public override string OpeningIdentifier => "[";
        public override string ClosingIdentifier => "]";
    }
}
