using System;
using System.Data.Common;
using CourseEindcase.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CourseEindcase.UnitTests.Repositories;

public class DBContextBase : IDisposable
{
    private readonly DbConnection _dbConnection;
    public DbContextOptions<CaseContext> ContextOptions { get; }
    
    public DBContextBase()
    {
        ContextOptions = new DbContextOptionsBuilder<CaseContext>().UseSqlite(CreateInMemoryDatabase()).Options;
            
        _dbConnection = RelationalOptionsExtension.Extract(ContextOptions).Connection;
    }

    private static DbConnection CreateInMemoryDatabase()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        
        connection.Open();

        return connection;
    }

    void IDisposable.Dispose() => _dbConnection.Dispose();

}