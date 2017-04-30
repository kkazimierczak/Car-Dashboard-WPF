using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace Car_Dashboard_WPF
{
    class EngineModel
    {
        const int MIN_GEAR = 1, MAX_GEAR = 6;

        public double
            wantedSpeed,
            currentSpeed,
            currentRPM,
            gain,
            acceleration,
            gearChangingAcceleration;
        public int gear { get; private set; }

        double[] gearCoefficient = { 0, 133.3, 66.7, 40, 28.6, 22.2, 16.7 };
        bool reducingGear, raisingGear;

        Timer timer;
        List<double> speedHistory;

        public EngineModel()
        {
            InitializeTimer();
            speedHistory = new List<double>();
            PopulateList();

            gear = 1;
            raisingGear = false;
            reducingGear = false;
            wantedSpeed = 0;
            currentSpeed = 0;
            currentRPM = 0;
            gain = 0.03;
            acceleration = 0.2;
            gearChangingAcceleration = 0;
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
        private void PopulateList()
        {
            for (int i = 0; i < 20; i++)
            {
                speedHistory.Add(0);
            }
        }
        
        public void RunEngine(double wantedSpeed)
        {
            //currentSpeed = gain * SamplingTime / timeConstant * wantedSpeed + (timeConstant - SamplingTime) / timeConstant * previousSpeed;
            double speedDifference = wantedSpeed - currentSpeed;
            //if (Math.Abs(speedDifference) < 0.5 && currentRPM < 100) return;

            CalculateSpeed(speedDifference);
            CalculateRPM(speedDifference);
            AutomaticTransmission();
        }
        private void AutomaticTransmission()
        {
            if (currentRPM > 2500 && gear < MAX_GEAR)
            {
                RaiseGear();
            }
            else if (currentRPM < 1500 && gear > MIN_GEAR)
            {
                ReduceGear();
            }
            else
            {
                if (raisingGear && currentRPM < 1800)
                {
                    raisingGear = false;
                    gearChangingAcceleration = 0;
                }
                if (reducingGear && currentRPM > 2000)
                {
                    reducingGear = false;
                    gearChangingAcceleration = 0;
                }
            }
        }
        private void CalculateRPM(double speedDifference)
        {
            currentRPM += (Math.Sign(speedDifference) * acceleration + speedDifference * gain) * gearCoefficient[gear] + gearChangingAcceleration;
        }
        private void CalculateSpeed(double speedDifference)
        {
            speedHistory.Add(currentRPM / gearCoefficient[gear]);
            speedHistory.RemoveAt(0);
            currentSpeed = speedHistory.Average();
        }
        private void RaiseGear()
        {
            gear++;
            gearChangingAcceleration = -200;
            raisingGear = true;
        }
        private void ReduceGear()
        {
            gear--;
            gearChangingAcceleration = 200;
            reducingGear = true;
        }
    }
}
