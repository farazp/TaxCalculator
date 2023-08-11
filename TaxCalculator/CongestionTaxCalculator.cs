using System.ComponentModel.Design;

using TaxCalculator.Enums;
using TaxCalculator.Helpers;
using TaxCalculator.Interfaces;
using TaxCalculator.Models;

namespace TaxCalculator;

public class CongestionTaxCalculator
{
    public int GetTax(string vehicleType, List<DateTime> dates, string city)
    {
        int totalFee = 0;
        switch (city)
        {
            case "Gothenburg":
                for (int i = 0; i < dates.Count; i++)
                {
                    if (i == 0)
                    {
                        totalFee = GetTollFee(new TollModel() { CityName = city, DateTime = dates[i], VehicleType = vehicleType });
                    }
                    else
                    {
                        int nextFee = GetTollFee(new TollModel() { CityName = city, DateTime = dates[i], VehicleType = vehicleType });
                        long diffInMillies = dates[i].Millisecond - dates[i - 1].Millisecond;
                        long minutes = diffInMillies / 1000 / 60;
                        if (minutes <= 60)
                        {
                            if (nextFee >= totalFee)
                            {
                                totalFee = nextFee;
                            }
                        }
                        else
                        {
                            totalFee += nextFee;
                        }
                    }
                }

                if (totalFee > 60) totalFee = 60;
                break;

            default:
                totalFee = 0;
                break;

        }
        return totalFee;
    }

    private Boolean IsTollFreeDate(DateTime date)
    {
        int year = date.Year;
        int month = date.Month;
        int day = date.Day;
        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;
        if (year == 2013)
        {
            if (month == 1 && day == 1 ||
                month == 3 && (day == 28 || day == 29) ||
                month == 4 && (day == 1 || day == 30) ||
                month == 5 && (day == 1 || day == 8 || day == 9) ||
                month == 6 && (day == 5 || day == 6 || day == 21) ||
                month == 7 ||
                month == 11 && day == 1 ||
                month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
            {
                return true;
            }
        }
        return false;
    }

    public int GetTollFee(TollModel tollModel)
    {
        TimeSpan timeSpan = 
            new DateTime(
                tollModel.DateTime.Year, 
                tollModel.DateTime.Month, 
                tollModel.DateTime.Day, 
                tollModel.DateTime.Hour, 
                tollModel.DateTime.Minute, 0)
                .TimeOfDay;

        var vType = EnumHelper.ParseEnum<TollFreeVehicles>(tollModel.VehicleType, TollFreeVehicles.None);

        if (IsTollFreeDate(tollModel.DateTime) || IsTollFreeVehicle(vType)) return 0;

        switch (tollModel.CityName)
        {
            case "Gothenburg":

                if (
                    (timeSpan >= new TimeSpan(6, 0, 0)) && 
                    (timeSpan <= new TimeSpan(6, 29, 0)))
                {
                    return 8;
                }
                else if 
                    ((timeSpan >= new TimeSpan(6, 30, 0)) && 
                    (timeSpan <= new TimeSpan(6, 59, 0)))
                {
                    return 13;
                }
                else if 
                    ((timeSpan >= new TimeSpan(7, 0, 0)) && 
                    (timeSpan <= new TimeSpan(7, 59, 0)))
                {
                    return 18;
                }
                else if 
                    ((timeSpan >= new TimeSpan(8, 0, 0)) && 
                    (timeSpan <= new TimeSpan(8, 29, 0)))
                {
                    return 13;
                }
                else if 
                    ((timeSpan >= new TimeSpan(8, 30, 0)) && 
                    (timeSpan <= new TimeSpan(14, 59, 0)))
                {
                    return 8;
                }
                else if 
                    ((timeSpan >= new TimeSpan(15, 0, 0)) && 
                    (timeSpan <= new TimeSpan(15, 29, 0)))
                {
                    return 13;
                }
                else if 
                    ((timeSpan >= new TimeSpan(15, 30, 0)) && 
                    (timeSpan <= new TimeSpan(15, 59, 0)))
                {
                    return 18;
                }
                else if 
                    ((timeSpan >= new TimeSpan(17, 0, 0)) && 
                    (timeSpan <= new TimeSpan(17, 59, 0)))
                {
                    return 13;
                }
                else if 
                    ((timeSpan >= new TimeSpan(18, 0, 0)) && 
                    (timeSpan <= new TimeSpan(18, 29, 0)))
                {
                    return 8;
                }
                else if 
                    ((timeSpan >= new TimeSpan(18, 30, 0)) && 
                    (timeSpan <= new TimeSpan(23, 59, 0)))
                {
                    return 0;
                }
                else if 
                    ((timeSpan >= new TimeSpan(0, 0, 0)) && 
                    (timeSpan <= new TimeSpan(5, 59, 0)))
                {
                    return 0;
                }
                else
                    return 0;
            default:
                return 0;
        }
    }

    private bool IsTollFreeVehicle(TollFreeVehicles vehicleType)
    {
        if (vehicleType.Equals(TollFreeVehicles.None))
            return false;
        else
            return vehicleType.Equals(TollFreeVehicles.Motorcycle) ||
                   vehicleType.Equals(TollFreeVehicles.Tractor) ||
                   vehicleType.Equals(TollFreeVehicles.Emergency) ||
                   vehicleType.Equals(TollFreeVehicles.Diplomat) ||
                   vehicleType.Equals(TollFreeVehicles.Foreign) ||
                   vehicleType.Equals(TollFreeVehicles.Military);
    }


}
