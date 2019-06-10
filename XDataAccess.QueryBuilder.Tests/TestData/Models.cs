using System;
using XDataAccess.QueryBuilder.Attributes;

namespace XDataAccess.QueryBuilder.Tests.TestData
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    [Entity("dbo.employee")]
    public class EmployeeWithAttributes
    {
        [Identity]
        public int Id { get; set; }

        [PropertyAttribute("name")]
        public string Name { get; set; }

        [Ignore]
        public DateTime CreateDate { get; set; }
    }
}