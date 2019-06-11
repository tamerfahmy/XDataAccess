using System;
using System.Collections.Generic;
using System.Text;

namespace XDataAccess.QueryBuilder.Dialects.Databases
{
    public sealed class OracleDialect : BaseDialect
    {
        public override string ParameterPrefix => ":";

        public override string AppendParameter => "||";
    }
}
