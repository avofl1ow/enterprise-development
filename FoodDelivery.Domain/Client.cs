using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Domain;

public class Client
{
    public int Id { get; set; }
    public required string FullName { get; set; }
    public required string Phone { get; set; }
    public required string DeliveryAddress { get; set; }
}
