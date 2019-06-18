using System;
using System.Collections.Generic;
using System.Text;
using XDataAccess.QueryBuilder.Compilers;

namespace XDataAccess.QueryBuilder.CrudOperation
{
    public interface ICrudOperation
    {
        ICompiler Compiler { get; }
    }
}