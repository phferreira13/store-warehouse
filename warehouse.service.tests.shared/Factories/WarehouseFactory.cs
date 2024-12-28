using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using warehouse.service.domain.Models;

namespace warehouse.service.tests.shared.Factories;
public static class WarehouseFactory
{
    public static Warehouse CreateWarehouse()
    {
        var faker = new Faker();
        return new Warehouse(faker.Company.CompanyName(), faker.Address.FullAddress());
    }

    public static IEnumerable<Warehouse> CreateWarehouses(int count)
    {
        return Enumerable.Range(0, count).Select(_ => CreateWarehouse());
    }
}