using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Domain;

public class Order
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeliveredAt { get; set; } 
    public decimal TotalAmount { get; set; } 
    public int ClientId { get; set; }
    public required Client Client { get; set; }
    public int RestaurantId { get; set; }
    public required Restaurant Restaurant { get; set; }
    public int CategoryId { get; set; } 

}
