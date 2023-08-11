using TaxCalculator.Enums;
using TaxCalculator.Interfaces;

namespace TaxCalculator.Models;

public class Vehicle : IVehicle
{
    private string vehicle;
    public Vehicle(string tollFreeVehicles)
    {
        vehicle = tollFreeVehicles;
    }
    public string GetVehicleType()
    {
        return vehicle;
    }
}
