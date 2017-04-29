using System;
using System.Timers;

namespace Car_Dashboard_WPF
{
    class EngineModel
    {
        public double wantedSpeed, currentSpeed, previousSpeed, gain, acceleration, SamplingTime;

        Timer timer;
                
        public EngineModel()
        {
            InitializeTimer();

            wantedSpeed = 50;
            currentSpeed = 0;
            previousSpeed = 0;
            gain = 0.03;
            acceleration = 0.2;
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
            RunEngine(wantedSpeed);
        }

        public void RunEngine(double wantedSpeed)
        {
            //currentSpeed = gain * SamplingTime / timeConstant * wantedSpeed + (timeConstant - SamplingTime) / timeConstant * previousSpeed;
            double speedDifference = wantedSpeed - currentSpeed;
            if (Math.Abs(speedDifference) < 0.5) return;
            currentSpeed += Math.Sign(speedDifference) * acceleration + speedDifference * gain;
        }
    }
}
