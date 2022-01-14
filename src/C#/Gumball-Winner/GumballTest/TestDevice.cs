using System;
using Gumball;

namespace GumballTest
{
    public class TestDevice : GumballMachineHardwareDevice
    {
		private bool wasQuarterEjected;
		private string displayedMessage;
		private int numGumballs;

        void GumballMachineHardwareDevice.DisplayMessage(string message)
        {
			this.displayedMessage = message;
        }

        bool GumballMachineHardwareDevice.DispenseGumball()
        {
            numGumballs--;
            return numGumballs >= 0;
        }

        void GumballMachineHardwareDevice.DispenseQuarter()
        {
            wasQuarterEjected = true;
        }

        public string GetDisplayedMessage()
        {
            return displayedMessage;
        }

        public bool WasQuarterEjected()
        {
            return wasQuarterEjected;
        }

        public int GetCount()
        {
            return numGumballs;
        }

        public void AddGumballs(int count)
        {
            numGumballs = Math.Max(numGumballs, 0) + count;
        }
    }
}