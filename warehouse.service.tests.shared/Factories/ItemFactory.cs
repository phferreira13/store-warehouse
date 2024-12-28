using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using warehouse.service.domain.Models;

namespace warehouse.service.tests.shared.Factories;
public static class ItemFactory
{
    public static Item CreateItem()
    {
        var faker = new Faker();
        return new Item(faker.Commerce.ProductName(), faker.Random.Decimal(1, 100), faker.Lorem.Sentence());
    }

    public static IEnumerable<Item> CreateItems(int count)
    {
        return Enumerable.Range(0, count).Select(_ => CreateItem());
    }
}
