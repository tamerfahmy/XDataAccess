using System;

namespace XDataAccess.QueryBuilder.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EntityAttribute : Attribute
    {
        public string Name { get; set; }
        public EntityAttribute(string name)
        {
            Name = name;
        }
    }
}