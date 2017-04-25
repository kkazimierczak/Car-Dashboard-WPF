using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IiMwT_Projekt_CS_WPF_2017
{
    class EngineModel
    {
        public int currentSpeed;
        public int previousSpeed;
        
        public EngineModel()
        {
            currentSpeed = 50;
        }

        public void RunEngine()
        {
            currentSpeed += 1;
            Thread.Sleep(500);
        }
    }
}
