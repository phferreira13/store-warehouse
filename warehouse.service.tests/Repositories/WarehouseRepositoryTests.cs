using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using warehouse.service.entityframework.Repositories;
using warehouse.service.entityframework.Context;
using warehouse.service.tests.shared.Factories;
using warehouse.service.domain.Models;

namespace warehouse.service.tests.Repositories;

[TestClass]
public class WarehouseRepositoryTests : RepositoryInitializer
{
    private readonly WarehouseRepository _warehouseRepository;

    public WarehouseRepositoryTests()
    {
        _warehouseRepository = new WarehouseRepository(_context);
    }

    [TestMethod]
    [ClearDataBase]
    public async Task GetWarehouse_ShouldReturnWarehouse()
    {
        // Arrange
        var warehouse = WarehouseFactory.CreateWarehouse();
        await _context.Warehouses.AddAsync(warehouse);
        await _context.SaveChangesAsync();
        // Act
        var result = await _warehouseRepository.GetWarehouseAsync(warehouse.Id);
        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(warehouse.Id, result?.Id);
    }

    [TestMethod]
    [ClearDataBase]
    public async Task GetWarehouses_ShouldReturnAllWarehouses()
    {
        // Arrange
        var warehouses = WarehouseFactory.CreateWarehouses(5).ToList();
        foreach (var warehouse in warehouses)
        {
            await _warehouseRepository.AddWarehouseAsync(warehouse);
        }
        // Act
        var result = await _warehouseRepository.GetWarehousesAsync();
        // Assert
        Assert.AreEqual(warehouses.Count, result.Count());
    }

    [TestMethod]
    [ClearDataBase]
    public async Task AddWarehouse_ShouldAddWarehouse()
    {
        // Arrange
        var warehouse = WarehouseFactory.CreateWarehouse();
        // Act
        await _warehouseRepository.AddWarehouseAsync(warehouse);
        // Assert
        var result = await _context.Warehouses.FirstOrDefaultAsync(x => x.Id == warehouse.Id);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    [ClearDataBase]
    public async Task UpdateWarehouse_ShouldUpdateWarehouse()
    {
        // Arrange
        var warehouse = WarehouseFactory.CreateWarehouse();
        await _warehouseRepository.AddWarehouseAsync(warehouse);
        var newName = "Updated Name";
        var newLocation = "Updated Location";
        // Act
        await _warehouseRepository.UpdateWarehouseAsync(warehouse.Id, newName, newLocation);
        var updatedWarehouse = await _warehouseRepository.GetWarehouseAsync(warehouse.Id);
        // Assert
        Assert.IsNotNull(updatedWarehouse);
        Assert.AreEqual(newName, updatedWarehouse?.Name);
        Assert.AreEqual(newLocation, updatedWarehouse?.Location);
    }

    [TestMethod]
    [ClearDataBase]
    public async Task AddItem_ShouldAddItemToWarehouse()
    {
        // Arrange
        var warehouse = WarehouseFactory.CreateWarehouse();
        await _warehouseRepository.AddWarehouseAsync(warehouse);
        var item = ItemFactory.CreateItem();
        var quantity = 10;
        // Act
        await _warehouseRepository.AddItemAsync(warehouse.Id, item, quantity);
        var updatedWarehouse = await _warehouseRepository.GetWarehouseAsync(warehouse.Id);
        // Assert
        Assert.IsNotNull(updatedWarehouse);
        var warehouseItem = updatedWarehouse?.Items.FirstOrDefault(x => x.ItemId == item.Id);
        Assert.IsNotNull(warehouseItem);
        Assert.AreEqual(quantity, warehouseItem?.Quantity);
    }

    [TestMethod]
    [ClearDataBase]
    public async Task IncreaseItemQuantity_ShouldIncreaseItemQuantity()
    {
        // Arrange
        var warehouse = WarehouseFactory.CreateWarehouse();
        await _warehouseRepository.AddWarehouseAsync(warehouse);
        var item = ItemFactory.CreateItem();
        var initialQuantity = 5;
        await _warehouseRepository.AddItemAsync(warehouse.Id, item, initialQuantity);
        var increaseQuantity = 10;
        // Act
        await _warehouseRepository.IncreaseItemQuantityAsync(warehouse.Id, item.Id, increaseQuantity);
        var updatedWarehouse = await _warehouseRepository.GetWarehouseAsync(warehouse.Id);
        // Assert
        Assert.IsNotNull(updatedWarehouse);
        var warehouseItem = updatedWarehouse?.Items.FirstOrDefault(x => x.ItemId == item.Id);
        Assert.IsNotNull(warehouseItem);
        Assert.AreEqual(initialQuantity + increaseQuantity, warehouseItem?.Quantity);
    }

    [TestMethod]
    [ClearDataBase]
    public async Task DecreaseItemQuantity_ShouldDecreaseItemQuantity()
    {
        // Arrange
        var warehouse = WarehouseFactory.CreateWarehouse();
        await _warehouseRepository.AddWarehouseAsync(warehouse);
        var item = ItemFactory.CreateItem();
        var initialQuantity = 15;
        await _warehouseRepository.AddItemAsync(warehouse.Id, item, initialQuantity);
        var decreaseQuantity = 5;
        // Act
        await _warehouseRepository.DecreaseItemQuantityAsync(warehouse.Id, item.Id, decreaseQuantity);
        var updatedWarehouse = await _warehouseRepository.GetWarehouseAsync(warehouse.Id);
        // Assert
        Assert.IsNotNull(updatedWarehouse);
        var warehouseItem = updatedWarehouse?.Items.FirstOrDefault(x => x.ItemId == item.Id);
        Assert.IsNotNull(warehouseItem);
        Assert.AreEqual(initialQuantity - decreaseQuantity, warehouseItem?.Quantity);
    }

    [TestMethod]
    [ClearDataBase]
    public async Task DeleteWarehouse_ShouldDeleteWarehouse()
    {
        // Arrange
        var warehouse = WarehouseFactory.CreateWarehouse();
        await _warehouseRepository.AddWarehouseAsync(warehouse);
        // Act
        await _warehouseRepository.DeleteWarehouseAsync(warehouse.Id);
        var result = await _warehouseRepository.GetWarehouseAsync(warehouse.Id);
        // Assert
        Assert.IsNull(result);
    }
}
