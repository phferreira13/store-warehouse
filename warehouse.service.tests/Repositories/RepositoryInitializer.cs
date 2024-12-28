using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using warehouse.service.entityframework.Context;

namespace warehouse.service.tests.Repositories;
public abstract class RepositoryInitializer
{
    protected readonly WarehouseContext _context;
    public RepositoryInitializer()
    {
        var options = new DbContextOptionsBuilder<WarehouseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new WarehouseContext(options);
    }

    public void ClearDatabase()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    protected class ClearDataBaseAttribute : Attribute
    {
        // if has this attribute, need call RepositoryInitializer.ClearDatabase() before test
        public void Invoke(MethodInfo method, object instance)
        {
            var repositoryInitializer = instance as RepositoryInitializer;
            repositoryInitializer?.ClearDatabase();
            method.Invoke(instance, null);
        }
    }
}

