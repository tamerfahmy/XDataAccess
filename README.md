## xDataAccess

### WhatIs((x, DataAccess) => { x = "Generic"; DataAccess = "Data Access Layer"; });
xDataAccess is an open-source cross-platform framework to dynamically build quires from lambda expression up to providing a complete data access layer for different data sources from the query generation up to the execution and data manipulation. It is Generic and extensible with different data source dialects.

<!-- TOC -->
- [Libraries](#libraries)
- [Features](#features)
- [How to Use](#how-to-use)
  - [Models](#models)
  - [XDataAccess.QueryBuilder](#xdataaccessquerybuilder)
    - [Select](#select)
    - [Update](#update)
    - [Delete](#delete)
    - [Insert](#insert)
<!-- /TOC -->

#### Libraries
| Project | Pipeline  Status | Latest Release |
| --- | --- | --- |
| XDataAccess.QueryBuilder | [![Build Status](https://tamerfahmy.visualstudio.com/XDataAccess/_apis/build/status/tamerfahmy.XDataAccess?branchName=master)](https://tamerfahmy.visualstudio.com/XDataAccess/_build/latest?definitionId=6&branchName=master) |  | XDataAccess.Dapper |  |  |
| XDataAccess.DapperRepository |  |  |
| XDataAccess.ORMLite |  |  |
| XDataAccess.ORMLiteRepository |  |  |
| XDataAccess.CDS |  |  |
| XDataAccess.CDSRepository |  |  |

#### Features
- [x]  **Query Generation**
  - [x] Sql Server dialect
  - [x] Oracle dialect
  - [x] MySql dialect
  - [x] Postgres dialect
  
#### How to Use
##### Models
```csharp
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
```
##### XDataAccess.QueryBuilder
```csharp
var compiler = new SqlServerCompiler();
var builder = new QueryBuilder<EmployeeWithAttributes>(compiler);
```
###### Select
```csharp
var output1 = builder.Select() as DbCompileResult; 
// "SELECT * FROM dbo.employee"

var output2 = builder.Select(e => (e.Id == 1 && e.Name == "value1") || e.Name != "value2") as DbCompileResult; 
// "SELECT * FROM dbo.employee WHERE ((([Id] = @P0) AND ([name] = @P1)) OR ([name] <> @P2))"

var output3 = builder.Select(e => e.Name == null) as DbCompileResult; 
// "SELECT * FROM dbo.employee WHERE ([name] IS NULL)"

var output4 = builder.Select(e => e.Name.Contains ("test")) as DbCompileResult; 
// "SELECT * FROM dbo.employee WHERE ([name] LIKE '%' + @P0 + '%')"
```

###### Update
```csharp
var employeeToUpdate = new EmployeeWithAttributes() { Id = 1, Name = "test" };
var result = builder.Update(employeeToUpdate) as DbCompileResult; 
// "UPDATE dbo.employee SET [name] = @P0 WHERE [Id] = @P1"
```
###### Or
```csharp
var result = builder.Update(employeeToUpdate, e => e.Id == 1) as DbCompileResult; 
// "UPDATE dbo.employee SET [name] = @P0 WHERE ([Id] = @P1)"
```

###### Delete
```csharp
var result = builder.Delete (e => e.Id == 1) as DbCompileResult; 
// "DELETE FROM Employee WHERE ([Id] = @P0)"
```

###### Insert
```csharp
var employee = new Employee() { Id = 1, Name = "Employee Name" };
var result = builder.Insert(employee) as DbCompileResult; 
// "INSERT INTO Employee ([Id],[Name]) VALUES (@P0,@P1)"
```

