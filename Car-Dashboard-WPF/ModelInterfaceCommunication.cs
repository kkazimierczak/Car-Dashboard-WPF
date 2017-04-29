using System.Threading;
using System.Windows.Controls;

namespace Car_Dashboard_WPF
{
    class ModelInterfaceCommunication
    {
        public double currentSpeed { get; set; }

        public void UpdateCurrentValues(EngineModel engine, Slider slider)
        {
            currentSpeed = engine.currentSpeed;
        }
    }
}
