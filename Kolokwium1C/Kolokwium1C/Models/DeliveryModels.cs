namespace Kolokwium1C.Models;

public class Customer
{
    string firstName { get; set; }
    string lastName { get; set; }
    DateTime dateOfBirth { get; set; }
}

public class Driver
{
    string firstName { get; set; }
    string lastName { get; set; }
    string licenseNumber { get; set; }
}

public class Product
{
    string name { get; set; }
    decimal price { get; set; }
    int amount {get;set;}
}

public class DeliveryDTO
{
    DateTime date { get; set; }
    Customer customer { get; set; }
    Driver driver { get; set; }
    List<Product> products { get; set; }
}
