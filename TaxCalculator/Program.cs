// See https://aka.ms/new-console-template for more information
using TaxCalculator;
using TaxCalculator.Enums;

Console.WriteLine("Congestion Tax Calculator");

CongestionTaxCalculator congestionTaxCalculator = new CongestionTaxCalculator();

var dates=new List<DateTime> {
    new DateTime(2013,2,12),
    new DateTime(2013,3,29),
    new DateTime(2013,5,1),
    new DateTime(2013,10,12),
    new DateTime(2013,9,8),
};

var v1Tax = congestionTaxCalculator.GetTax("Car", dates, "Gothenburg");
var v2Tax = congestionTaxCalculator.GetTax("Motorcycle", dates, "Gothenburg");
var v3Tax = congestionTaxCalculator.GetTax("Emergency", dates, "Gothenburg");

Console.WriteLine($"Car Tax: {v1Tax}");
Console.WriteLine($"Motorbike Tax: {v2Tax}");
Console.WriteLine($"Emergency Tax: {v3Tax}");

Console.ReadKey();