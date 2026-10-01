using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Domain;

public class Dish
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public int Weight { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public required Category Category { get; set; }
}
