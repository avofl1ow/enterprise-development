using System;
using System.Collections.Generic;
using System.Linq;
using FoodDelivery.Domain;

namespace FoodDelivery.Tests.Fixtures;

public class FoodDeliveryFixture
{
    public List<Category> Categories { get; }
    public List<Restaurant> Restaurants { get; }
    public List<Client> Clients { get; }
    public List<Order> Orders { get; }

    public FoodDeliveryFixture()
    {
        Categories = [
            new Category { Id = 1, Name = "Пицца" },
            new Category { Id = 2, Name = "Суши" },
            new Category { Id = 3, Name = "Бургеры" },
            new Category { Id = 4, Name = "Десерты" },
            new Category { Id = 5, Name = "Напитки" }
        ];

        Restaurants = [
            new Restaurant { Id = 1, Name = "Додо Пицца", Address = "ул. Ленина, 5", Rating = 4.8, WorkingHours = "10:00-23:00" },
            new Restaurant { Id = 2, Name = "Тануки", Address = "ул. Садовая, 12", Rating = 4.5, WorkingHours = "11:00-23:00" },
            new Restaurant { Id = 3, Name = "Вкусно и точка", Address = "пр. Мира, 3", Rating = 4.2, WorkingHours = "06:00-00:00" },
            new Restaurant { Id = 4, Name = "Шоколадница", Address = "ул. Чехова, 8", Rating = 4.4, WorkingHours = "08:00-22:00" },
            new Restaurant { Id = 5, Name = "Папа Джонс", Address = "ул. Новая, 1", Rating = 4.6, WorkingHours = "10:00-23:00" }
        ];

        Clients = [
            new Client { Id = 1, FullName = "Иванов Иван Иванович", Phone = "+79991112233", DeliveryAddress = "ул. Гагарина, 1" },
            new Client { Id = 2, FullName = "Петрова Анна Сергеевна", Phone = "+79994445566", DeliveryAddress = "ул. Полевая, 12" },
            new Client { Id = 3, FullName = "Смирнов Дмитрий Александрович", Phone = "+79997778899", DeliveryAddress = "ул. Лесная, 5" },
            new Client { Id = 4, FullName = "Алексеева Ольга Игоревна", Phone = "+79992223344", DeliveryAddress = "ул. Чехова, 3" },
            new Client { Id = 5, FullName = "Кузнецов Сергей Петрович", Phone = "+79995556677", DeliveryAddress = "ул. Новая, 8" },
            new Client { Id = 6, FullName = "Васильева Елена Николаевна", Phone = "+79998889900", DeliveryAddress = "ул. Пушкина, 15" },
            new Client { Id = 7, FullName = "Попов Игорь Сергеевич", Phone = "+79993334455", DeliveryAddress = "ул. Садовая, 2" },
            new Client { Id = 8, FullName = "Морозова Наталья Юрьевна", Phone = "+79996667788", DeliveryAddress = "ул. Полевая, 21" },
            new Client { Id = 9, FullName = "Федоров Андрей Олегович", Phone = "+79999990011", DeliveryAddress = "ул. Лесная, 4" },
            new Client { Id = 10, FullName = "Соколов Денис Витальевич", Phone = "+79991239876", DeliveryAddress = "ул. Набережная, 30" }
        ];

        // Извлекаем ссылки на объекты для сборки матрешки записей
        var c = Clients;
        var r = Restaurants;

        // Распределяем 15 заказов с точным указанием времени оформления и доставки
        // Разница во времени захардкожена как время доставки (минуты)
        Orders = [
            // Ресторан 1 (Додо Пицца) - 4 заказа
            new Order { Id = 1, CreatedAt = new DateTime(2026, 10, 1, 12, 0, 0), DeliveredAt = new DateTime(2026, 10, 1, 12, 40, 0), TotalAmount = 1500m, ClientId = c[0].Id, Client = c[0], RestaurantId = r[0].Id, Restaurant = r[0], CategoryId = 1 }, // 40 мин
            new Order { Id = 2, CreatedAt = new DateTime(2026, 10, 1, 13, 0, 0), DeliveredAt = new DateTime(2026, 10, 1, 13, 35, 0), TotalAmount = 1200m, ClientId = c[1].Id, Client = c[1], RestaurantId = r[0].Id, Restaurant = r[0], CategoryId = 1 }, // 35 мин
            new Order { Id = 3, CreatedAt = new DateTime(2026, 10, 2, 18, 0, 0), DeliveredAt = new DateTime(2026, 10, 2, 18, 50, 0), TotalAmount = 2100m, ClientId = c[2].Id, Client = c[2], RestaurantId = r[0].Id, Restaurant = r[0], CategoryId = 1 }, // 50 мин
            new Order { Id = 4, CreatedAt = new DateTime(2026, 10, 2, 19, 0, 0), DeliveredAt = new DateTime(2026, 10, 2, 19, 25, 0), TotalAmount = 900m,  ClientId = c[0].Id, Client = c[0], RestaurantId = r[0].Id, Restaurant = r[0], CategoryId = 1 }, // 25 мин

            // Ресторан 2 (Тануки) - 5 заказов (Лидер по количеству заказов)
            new Order { Id = 5, CreatedAt = new DateTime(2026, 10, 3, 12, 0, 0), DeliveredAt = new DateTime(2026, 10, 3, 13, 0, 0),  TotalAmount = 3500m, ClientId = c[3].Id, Client = c[3], RestaurantId = r[1].Id, Restaurant = r[1], CategoryId = 2 }, // 60 мин
            new Order { Id = 6, CreatedAt = new DateTime(2026, 10, 3, 14, 0, 0), DeliveredAt = new DateTime(2026, 10, 3, 14, 45, 0), TotalAmount = 2800m, ClientId = c[4].Id, Client = c[4], RestaurantId = r[1].Id, Restaurant = r[1], CategoryId = 2 }, // 45_мин
            new Order { Id = 7, CreatedAt = new DateTime(2026, 10, 4, 13, 0, 0), DeliveredAt = new DateTime(2026, 10, 4, 13, 55, 0), TotalAmount = 4200m, ClientId = c[3].Id, Client = c[3], RestaurantId = r[1].Id, Restaurant = r[1], CategoryId = 2 }, // 55 мин
            new Order { Id = 8, CreatedAt = new DateTime(2026, 10, 4, 19, 0, 0), DeliveredAt = new DateTime(2026, 10, 4, 20, 10, 0), TotalAmount = 1900m, ClientId = c[5].Id, Client = c[5], RestaurantId = r[1].Id, Restaurant = r[1], CategoryId = 2 }, // 70 мин
            new Order { Id = 9, CreatedAt = new DateTime(2026, 10, 5, 20, 0, 0), DeliveredAt = new DateTime(2026, 10, 5, 20, 40, 0), TotalAmount = 3100m, ClientId = c[6].Id, Client = c[6], RestaurantId = r[1].Id, Restaurant = r[1], CategoryId = 2 }, // 40 мин

            // Ресторан 3 (Вкусно и точка) - 3 заказа
            new Order { Id = 10, CreatedAt = new DateTime(2026, 10, 1, 08, 0, 0), DeliveredAt = new DateTime(2026, 10, 1, 08, 20, 0), TotalAmount = 600m,  ClientId = c[7].Id, Client = c[7], RestaurantId = r[2].Id, Restaurant = r[2], CategoryId = 3 }, // 20 мин (Самый быстрый!)
            new Order { Id = 11, CreatedAt = new DateTime(2026, 10, 2, 09, 0, 0), DeliveredAt = new DateTime(2026, 10, 2, 09, 20, 0), TotalAmount = 850m,  ClientId = c[8].Id, Client = c[8], RestaurantId = r[2].Id, Restaurant = r[2], CategoryId = 3 }, // 20 мин (Самый быстрый!)
            new Order { Id = 12, CreatedAt = new DateTime(2026, 10, 5, 15, 0, 0), DeliveredAt = new DateTime(2026, 10, 5, 15, 30, 0), TotalAmount = 1100m, ClientId = c[7].Id, Client = c[7], RestaurantId = r[2].Id, Restaurant = r[2], CategoryId = 3 }, // 30 мин

            // Ресторан 4 (Шоколадница) - 2 заказа
            new Order { Id = 13, CreatedAt = new DateTime(2026, 10, 1, 16, 0, 0), DeliveredAt = new DateTime(2026, 10, 1, 16, 45, 0), TotalAmount = 750m,  ClientId = c[9].Id, Client = c[9], RestaurantId = r[3].Id, Restaurant = r[3], CategoryId = 4 }, // 45 мин
            new Order { Id = 14, CreatedAt = new DateTime(2026, 10, 4, 11, 0, 0), DeliveredAt = new DateTime(2026, 10, 4, 11, 35, 0), TotalAmount = 1400m, ClientId = c[9].Id, Client = c[9], RestaurantId = r[3].Id, Restaurant = r[3], CategoryId = 4 }, // 35 мин

            // Ресторан 5 (Папа Джонс) - 1 заказ
            new Order { Id = 15, CreatedAt = new DateTime(2026, 10, 2, 21, 0, 0), DeliveredAt = new DateTime(2026, 10, 2, 21, 45, 0), TotalAmount = 1800m, ClientId = c[0].Id, Client = c[0], RestaurantId = r[4].Id, Restaurant = r[4], CategoryId = 1 }  // 45 мин
        ];
    }
}
