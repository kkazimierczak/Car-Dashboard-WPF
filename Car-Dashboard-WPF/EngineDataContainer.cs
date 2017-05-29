using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Dashboard_WPF
{
    public class EngineDataContainer
    {
        public double
            speed,
            rpm,
            fuelUsage,
            temperature;
        
        internal void UpdateData(EngineModel engine)
        {
            speed = engine.currentSpeed;
            rpm = engine.currentRPM;
            fuelUsage = engine.fuelUsage;
        }
    }
}
