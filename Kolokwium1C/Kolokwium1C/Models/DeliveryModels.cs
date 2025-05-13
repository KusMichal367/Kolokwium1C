namespace Kolokwium1C.Models;

public class Customer
{
    public string firstName { get; set; }
    public string lastName { get; set; }
    public DateTime dateOfBirth { get; set; }
}

public class Driver
{
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string licenseNumber { get; set; }
}

public class Product
{
    public string name { get; set; }
    public decimal price { get; set; }
    public int amount {get;set;}
}

public class DeliveryDTO
{
    public DateTime date { get; set; }
    public Customer customer { get; set; }
    public Driver driver { get; set; }
    public List<Product> products { get; set; }
}
