using System.Linq;
using Dapper;
using XDataAccess.QueryBuilder.Attributes;

namespace XDataAccess.Dapper
{
    public class ColumnAttributeTypeMapper<TEntity> : FallbackTypeMapper where TEntity : class
    {
        public ColumnAttributeTypeMapper()
            : base(new SqlMapper.ITypeMap[]
                {
                    new CustomPropertyTypeMap(typeof(TEntity), (type, columnName) =>
                       type.GetProperties().FirstOrDefault(prop => prop.GetCustomAttributes(false)
                                                                        .OfType<PropertyAttribute>()
                                                                        .Any(attr => attr.Name == columnName))),
                    new DefaultTypeMap(typeof(TEntity))
                })
        {

        }
    }
}