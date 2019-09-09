using System;
using System.Collections.Generic;
using TariffPlanNET.Model;
using System.Linq;
using System.Globalization;

namespace TariffPlanNET.Services
{
    public class CalculateTariff : ICalculateTariff
    {
        public IEnumerable<ElectricityTariff> TariffPlan(string consumption)
        {

            CultureInfo ci = new CultureInfo("sk-SK");
            ci.NumberFormat.CurrencySymbol = "€";
            ci.NumberFormat.CurrencyDecimalSeparator = ",";
            ci.NumberFormat.CurrencyGroupSeparator = ".";
            CultureInfo.DefaultThreadCurrentCulture = ci;

            try
            {
                double _consumption = Convert.ToDouble(consumption);

                IList<ElectricityTariff> tariffList = new List<ElectricityTariff>();

                double basicCost = 0;
                double packagedCost = 0;

                if (_consumption > 0)
                {

                    basicCost = (5 * 12) + (_consumption * 0.22);

                    tariffList.Add(new ElectricityTariff() { TariffName = "Basic electricity tariff", AnnualCosts = basicCost.ToString("c") });

                    if  (_consumption <= 4000)
                    {
                        packagedCost = 800;
                    }
                    else
                    {
                        packagedCost = 800 + ((_consumption - 4000) * 0.30);
                    }

                    tariffList.Add(new ElectricityTariff() { TariffName = "Packaged tariff", AnnualCosts = packagedCost.ToString("c") });

                    var orderByCost = from tc in tariffList
                                      orderby tc.AnnualCosts 
                                      select tc;

                    return orderByCost;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

        }
    }
}
