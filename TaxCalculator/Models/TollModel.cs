using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Models;

public struct TollModel
{
    public string CityName { get; set; }
    public DateTime DateTime { get; set; }
    public string VehicleType { get; set; }
}
