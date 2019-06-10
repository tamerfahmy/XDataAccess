using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using XDataAccess.QueryBuilder.Attributes;
using XDataAccess.QueryBuilder.Metadata;

namespace XDataAccess.QueryBuilder
{
    internal static class Utilities
    {
        internal static bool IsNumberType(Type type)
        {
            return type == null ? false : Constants.NumberTypes.Value.Any(t => t == type);
        }

        internal static bool IsNullableType(Type type)
        {
            return type == null ? false : Nullable.GetUnderlyingType(type) != null;
        }
    }
}