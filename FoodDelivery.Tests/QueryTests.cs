using FoodDelivery.Tests.Fixtures;

namespace FoodDelivery.Tests;

public class DeliveryAnalyticsTests : IClassFixture<FoodDeliveryFixture>
{
    private readonly FoodDeliveryFixture _fixture;

    public DeliveryAnalyticsTests(FoodDeliveryFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void Top5RestaurantsByOrderCount()
    {
        var topRestaurants = _fixture.Orders
            .GroupBy(o => o.Restaurant)
            .Select(g => new { Restaurant = g.Key, OrderCount = g.Count() })
            .OrderByDescending(x => x.OrderCount)
            .Take(5)
            .ToList();

        Assert.NotEmpty(topRestaurants);
        Assert.Equal("Тануки", topRestaurants.First().Restaurant.Name);
        Assert.Equal(5, topRestaurants.First().OrderCount);
    }
    [Fact]
    public void OrdersWithMinimumDeliveryTime()
    {
        var minDuration = _fixture.Orders
            .Where(o => o.DeliveredAt.HasValue)
            .Min(o => (o.DeliveredAt!.Value - o.CreatedAt).TotalMinutes);
        var fastestOrders = _fixture.Orders
            .Where(o => o.DeliveredAt.HasValue &&
                        Math.Abs((o.DeliveredAt.Value - o.CreatedAt).TotalMinutes - minDuration) < 0.01)
            .ToList();
        Assert.Equal(20, minDuration);
        Assert.Equal(2, fastestOrders.Count);
    }
    [Fact]
    public void ClientsOfSelectedRestaurantOrderedByName()
    {
        var targetRestaurantId = 2;
        var clients = _fixture.Orders
            .Where(o => o.RestaurantId == targetRestaurantId)
            .Select(o => o.Client)
            .Distinct()
            .OrderBy(c => c.FullName)
            .ToList();

        Assert.NotEmpty(clients);
        for (var i = 0; i < clients.Count - 1; i++)
        {
            Assert.True(string.Compare(clients[i].FullName, clients[i + 1].FullName, StringComparison.OrdinalIgnoreCase) <= 0);
        }
    }

    [Fact]
    public void CategorySummaryForPeriod()
    {
        var startDate = new DateTime(2026, 10, 1, 0, 0, 0);
        var endDate = new DateTime(2026, 10, 3, 23, 59, 59);
        var summary = _fixture.Orders
            .Where(o => o.CreatedAt >= startDate && o.CreatedAt <= endDate)
            .GroupBy(o => o.CategoryId)
            .Select(g => new
            {
                CategoryId = g.Key,
                OrderCount = g.Count(),
                AverageAmount = g.Average(o => o.TotalAmount),
                TotalSum = g.Sum(o => o.TotalAmount)
            })
            .ToList();

        Assert.NotEmpty(summary);
        var pizzaStats = summary.FirstOrDefault(s => s.CategoryId == 1);
        Assert.NotNull(pizzaStats);
        Assert.Equal(5, pizzaStats.OrderCount);
        Assert.Equal(7500m, pizzaStats.TotalSum);
    }

    [Fact]
    public void ClientWithHighestTotalSpend()
    {
        var topSpender = _fixture.Orders
            .GroupBy(o => o.Client)
            .Select(g => new { Client = g.Key, TotalSpent = g.Sum(o => o.TotalAmount) })
            .OrderByDescending(x => x.TotalSpent)
            .FirstOrDefault();
        Assert.NotNull(topSpender);
        Assert.Equal("Алексеева Ольга Игоревна", topSpender.Client.FullName);
        Assert.Equal(7700m, topSpender.TotalSpent);
    }
}

