using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IiMwT_Projekt_CS_WPF_2017
{
    class Model_Interface_Communication
    {
        public int currentSpeed { get; set; }

        public void UpdateCurrentValues(EngineModel engine)
        {
            currentSpeed = engine.currentSpeed;
        }
    }
}
