using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dapper;

namespace XDataAccess.Dapper
{
    public class FallbackTypeMapper : SqlMapper.ITypeMap
    {
        private readonly IEnumerable<SqlMapper.ITypeMap> _mappers;

        public FallbackTypeMapper(IEnumerable<SqlMapper.ITypeMap> mappers)
        {
            _mappers = mappers;
        }

        public SqlMapper.IMemberMap GetMember(string columnName)
        {
            foreach (var mapper in _mappers)
            {
                try
                {
                    var result = mapper.GetMember(columnName);
                    if (result != null)
                        return result;
                }
                catch
                {

                }
            }

            return null;
        }

        public ConstructorInfo FindExplicitConstructor()
        {
            return _mappers
                .Select(mapper => mapper.FindExplicitConstructor())
                .FirstOrDefault(result => result != null);
        }

        public ConstructorInfo FindConstructor(string[] names, Type[] types)
        {
            return _mappers
                .Select(mapper => mapper.FindConstructor(names, types))
                .FirstOrDefault(result => result != null);
        }

        public SqlMapper.IMemberMap GetConstructorParameter(ConstructorInfo constructor, string columnName)
        {
            return _mappers
                .Select(mapper => mapper.GetConstructorParameter(constructor, columnName))
                .FirstOrDefault(result => result != null);
        }
    }
}