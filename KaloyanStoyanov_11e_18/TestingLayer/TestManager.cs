namespace TestingLayer;

using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using NUnit.Framework;

[TestFixture]
public class TestManager
{
    internal static GamingDbContext dbContext;

    static TestManager()
    {
        DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
        builder.UseInMemoryDatabase("TestDb");
        dbContext = new GamingDbContext(builder.Options);
    }

    [OneTimeTearDown]
    public void Dispose()
    {
        dbContext.Dispose();
    }

    [Test]
    public void Test1()
    {
    }

    [Test]
    public void Test2()
    {
    }
}