using FoodDelivery.Tests.Fixtures;

namespace FoodDelivery.Tests;

public class DeliveryAnalyticsTests : IClassFixture<FoodDeliveryFixture>
{
    private readonly FoodDeliveryFixture _fixture;

    public DeliveryAnalyticsTests(FoodDeliveryFixture fixture)
    {
        _fixture = fixture;
    }

    // 1. Вывести топ 5 ресторанов по количеству заказов
    [Fact]
    public void Request1_Top5RestaurantsByOrderCount()
    {
        // Группируем по ресторану, считаем количество элементов, сортируем по убыванию и берем топ-5
        var topRestaurants = _fixture.Orders
            .GroupBy(o => o.Restaurant)
            .Select(g => new { Restaurant = g.Key, OrderCount = g.Count() })
            .OrderByDescending(x => x.OrderCount)
            .Take(5)
            .ToList();

        Assert.NotEmpty(topRestaurants);
        // Самым популярным должен стать ресторан "Тануки" (Id = 2), у него захардкожено 5 заказов
        Assert.Equal("Тануки", topRestaurants.First().Restaurant.Name);
        Assert.Equal(5, topRestaurants.First().OrderCount);
    }

    // 2. Вывести список заказов с минимальным временем доставки
    [Fact]
    public void Request2_OrdersWithMinimumDeliveryTime()
    {
        // Находим минимальную разницу во времени среди всех закрытых заказов (в минутах)
        var minDuration = _fixture.Orders
            .Where(o => o.DeliveredAt.HasValue)
            .Min(o => (o.DeliveredAt!.Value - o.CreatedAt).TotalMinutes);

        // Выбираем все заказы, у которых длительность доставки равна этой минимальной величине
        var fastestOrders = _fixture.Orders
            .Where(o => o.DeliveredAt.HasValue &&
                        Math.Abs((o.DeliveredAt.Value - o.CreatedAt).TotalMinutes - minDuration) < 0.01)
            .ToList();

        // Минимальное время равно 20 минутам (заказы под номерами 10 и 11)
        Assert.Equal(20, minDuration);
        Assert.Equal(2, fastestOrders.Count);
    }

    // 3. Вывести сведения обо всех клиентах, заказывавших в выбранном ресторане, упорядочить по ФИО
    [Fact]
    public void Request3_ClientsOfSelectedRestaurantOrderedByName()
    {
        var targetRestaurantId = 2; // Выбираем ресторан "Тануки"

        // Выбираем клиентов из заказов нужного ресторана, убираем дубликаты через Distinct и сортируем по ФИО
        var clients = _fixture.Orders
            .Where(o => o.RestaurantId == targetRestaurantId)
            .Select(o => o.Client)
            .Distinct()
            .OrderBy(c => c.FullName)
            .ToList();

        Assert.NotEmpty(clients);
        // Проверяем попарно, что алфавитный порядок ФИО соблюден
        for (var i = 0; i < clients.Count - 1; i++)
        {
            Assert.True(string.Compare(clients[i].FullName, clients[i + 1].FullName, StringComparison.OrdinalIgnoreCase) <= 0);
        }
    }

    // 4. Вывести сводную информацию о заказах по каждой категории блюд за указанный период
    [Fact]
    public void Request4_CategorySummaryForPeriod()
    {
        // Задаем интервал времени
        var startDate = new DateTime(2026, 10, 1, 0, 0, 0);
        var endDate = new DateTime(2026, 10, 3, 23, 59, 59);

        // Фильтруем по периоду, группируем по ID категории и вычисляем агрегатные показатели
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

        // Категория Пицца (Id = 1) попадает в период. Проверим её общую сумму (заказы 1, 2, 3, 4, 15)
        var pizzaStats = summary.FirstOrDefault(s => s.CategoryId == 1);
        Assert.NotNull(pizzaStats);
        Assert.Equal(5, pizzaStats.OrderCount);
        Assert.Equal(7500m, pizzaStats.TotalSum);
    }

    // 5. Вывести информацию о клиенте, который потратил на доставку наибольшую сумму за все время работы
    [Fact]
    public void Request5_ClientWithHighestTotalSpend()
    {
        // Группируем заказы по клиенту, суммируем их чеки и выбираем лидера по убыванию суммы
        var topSpender = _fixture.Orders
            .GroupBy(o => o.Client)
            .Select(g => new { Client = g.Key, TotalSpent = g.Sum(o => o.TotalAmount) })
            .OrderByDescending(x => x.TotalSpent)
            .FirstOrDefault();

        Assert.NotNull(topSpender);

        // В нашей фикстуре больше всего денег оставила Алексеева Ольга Игоревна (Id = 4)
        // Она сделала два крупных заказа в Тануки: 3500 + 4200 = 7700 руб.
        Assert.Equal("Алексеева Ольга Игоревна", topSpender.Client.FullName);
        Assert.Equal(7700m, topSpender.TotalSpent);
    }
}

