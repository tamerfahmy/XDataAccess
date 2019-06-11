using System;
using System.Collections.Generic;
using System.Text;
using XDataAccess.QueryBuilder.Compilers;

namespace XDataAccess.QueryBuilder.CRUD
{
    public interface ICrudOperation
    {
        ICompiler Compiler { get; }
    }
}
