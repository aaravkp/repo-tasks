using System;
using System.Collections.Generic;
using TariffPlanNET.Model;


namespace TariffPlanNET.Services
{
    public interface ICalculateTariff
    {
        IEnumerable<ElectricityTariff> TariffPlan(string consumption);
    }
}
