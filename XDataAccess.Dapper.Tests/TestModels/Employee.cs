using System;
using XDataAccess.QueryBuilder.Attributes;

namespace XDataAccess.Dapper.Tests.TestModels
{
    [Entity("dbo_employee")]
    public class Employee
    {
        // [Identity]
        [Property("id")]
        public Guid Guid { get; set; }

        [Property("fname")]
        public string FirstName { get; set; }

        [Property("lname")]
        public string LastName { get; set; }

        public bool Active { get; set; }

        [Ignore]
        public DateTime CreateDate { get; set; }
    }
}