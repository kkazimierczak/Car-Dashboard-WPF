using System;
using System.Timers;

namespace Car_Dashboard_WPF
{
    class EngineModel
    {
        public double currentSpeed, previousSpeed, gain, timeConstant, SamplingTime;

        Timer timer;
                
        public EngineModel()
        {
            InitializeTimer();

            currentSpeed = 0;
            previousSpeed = 0;
            gain = 1;
            timeConstant = 100;
            SamplingTime = timer.Interval/1000;
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
            RunEngine(100);
        }

        public void RunEngine(int wantedSpeed)
        {
            //currentSpeed = gain * SamplingTime / timeConstant * wantedSpeed + (timeConstant - SamplingTime) / timeConstant * previousSpeed;
            if (currentSpeed < wantedSpeed)
            {
                currentSpeed++;
            }
            else if (currentSpeed > wantedSpeed)
            {
                currentSpeed--;
            }
        }
    }
}
