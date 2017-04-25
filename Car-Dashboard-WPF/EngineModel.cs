using System;
using System.Timers;

namespace Car_Dashboard_WPF
{
    class EngineModel
    {
        public int currentSpeed;

        Timer timer; 
                
        public EngineModel()
        {
            currentSpeed = 50;
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 50;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            RunEngine();
        }

        public void RunEngine()
        {
            currentSpeed += 1;
        }
    }
}
