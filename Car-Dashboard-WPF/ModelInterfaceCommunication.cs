using System.Threading;

namespace Car_Dashboard_WPF
{
    class ModelInterfaceCommunication
    {
        public double currentSpeed { get; set; }

        public void UpdateCurrentValues(EngineModel engine)
        {
            currentSpeed = engine.currentSpeed;
        }
    }
}
