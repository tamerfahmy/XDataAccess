using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using XDataAccess.QueryBuilder.Attributes;

namespace XDataAccess.QueryBuilder.Metadata
{
    public sealed class EntityMetadata
    {
        public string EntityName { get; set; }
        public IEnumerable<AttributeMetadata> Attributes { get; set; }

        public IEnumerable<AttributeMetadata> AttributesForInsertOrUpdate
        {
            get
            {
                return Attributes.Where(a => !a.Ignore && !a.Identity);
            }
        }

        public IEnumerable<AttributeMetadata> IdentityAttributes
        {
            get
            {
                return Attributes.Where(a => a.Identity);
            }
        }

        public bool HasIdentityAttribute { get { return IdentityAttributes.Count() > 0; } }

        public bool IsValidForInsertOrUpdate { get { return AttributesForInsertOrUpdate.Count() > 0; } }

        public AttributeMetadata GetAttribute(string propertyName)
        {
            return Attributes.FirstOrDefault(a => a.PropertyName == propertyName);
        }

        public static EntityMetadata Parse<TEntity>() where TEntity : class
        {
            var entityName = GetEntityName<TEntity>();
            var attributes = GetAttributesMetadata<TEntity>();

            return new EntityMetadata()
            {
                EntityName = entityName,
                Attributes = attributes
            };
        }

        public static EntityMetadata Parse<TEntity>(TEntity entity) where TEntity : class
        {
            var entityName = GetEntityName<TEntity>();
            var attributes = GetAttributesMetadata<TEntity>(entity);

            return new EntityMetadata()
            {
                EntityName = entityName,
                Attributes = attributes
            };
        }

        private static IEnumerable<AttributeMetadata> GetAttributesMetadata<TEntity>(TEntity entity = null) where TEntity : class
        {
            var attributes = new List<AttributeMetadata>();
            var props = typeof(TEntity).GetProperties();

            foreach (var property in props)
            {
                var attribute = new AttributeMetadata()
                {
                    Ignore = property.GetCustomAttribute(typeof(IgnoreAttribute)) != null,
                    Identity = property.GetCustomAttribute(typeof(IdentityAttribute)) != null,
                };

                var propertyAttr = property.GetCustomAttribute(typeof(PropertyAttribute)) as PropertyAttribute;
                attribute.PropertyName = property.Name;
                attribute.Name = propertyAttr?.Name ?? property.Name;

                if (entity != null)
                    attribute.Value = property.GetValue(entity);

                attributes.Add(attribute);
            }

            return attributes;
        }

        private static string GetEntityName<TEntity>()
        {
            var entityType = typeof(TEntity);
            var entityAttribute = entityType.GetCustomAttributes(typeof(EntityAttribute), false)?.FirstOrDefault();

            if (entityAttribute != null)
                return (entityAttribute as EntityAttribute).Name;
            else
                return entityType.Name;
        }
    }
}