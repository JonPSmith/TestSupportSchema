# TestSupportSchema

This is a TEMPERARY project *(it will move into EfCore.TestSupport when EF Core 5 is released)* to contain the method `EnsureClean` which wipes the database schema and replaces it with a database that matches the current DbContext's configuration.

## Why is this useful?
When you are writing your unit tests you are likely to use the command `context.Database.EnsureCreated()`  to set up the database you use for unit testing. That works for Sqlite in-memory databases, but it you are using a real database, then you need to call below to make sure that the database schema is up to date
```c#
context.Database.EnsureDeleted();
context.Database.EnsureCreated();
```
The problem is the EnsureDeleted/EnsureCreated calls take a **LONG** time. This feature has the same effect (e.g. correct schema, empty of data) but is **MUCH** quicker e.g. for a very small database the timings are:

| What                        | First use | Second use |
|:----------------------------|----------:| ----------:|
| EnsureDeleted/EnsureCreated | 10 secs.  | 10 secs.   |
| EnsureClean                 | 300 ms    | 30 ms      |
| Respawn (see note below)    | 170 ms    | 35 ms      |

*NOTE: Respawn is a [NuGet library](https://www.nuget.org/packages/Respawn/) that removes all the rows in all the tables in a database. Useful if you have already called `EnsureClean` on the database and just want to remove the rows.*

## Where did this come from?
The idea and code came from the EF Core team. @ajcvickers described how they managed this issue and I have copied [with the knowledge of the EF Core team](https://github.com/dotnet/efcore/issues/19635#issuecomment-613276164) some of EF Core's internal classes and built this feature for a Sql Server database provider.

## How to use this feature
1. Include the EfCore.TestSupportSchema NuGet package
2. Replace any EnsureDeleted/EnsureCreated calls with `context.EnsureClean()`.

Here is an example of a unit test using EfCore.TestSupport's `CreateUniqueClassOptions` to give you a unique database name for the test class it is in.

```c#
[Fact]
public void TestExampleOk()
{
    //SETUP
    var options = this.CreateUniqueClassOptions<YourDbContext>();
    using (var context = new YourDbContext(options))
    {
        context.EnsureClean();

        //... put your unit tests here

    }
}
```

*NOTE. This only works with Sql Server databases.*

## What does EnsureClean remove?

It is pretty comprehensive in what it removes. i.e. indexes, foreign key constraints, tables, sequences, views, functions, aggegates, procs, types, schema names. 
