using System;

namespace Gumball
{
    public class GumballMachine
    {
        private readonly GumballMachineHardwareDevice device;
        private GumballMachineState state;
        private Random rand = new Random();

        public GumballMachine(GumballMachineHardwareDevice device)
        {
            this.device = device;
            SetState(State.SO);
        }

        internal void SetState(GumballMachineState state)
        {
            this.state = state;
            state.Start(this);
        }

        public void QuarterInserted()
        {
            state.QuarterInserted(this);
        }

        public void EjectQuarterRequested()
        {
            state.EjectQuarterRequested(this);
        }

        public void CrankTurned()
        {
            state.CrankTurned(this);
        }

        public void Reset()
        {
            state.Reset(this);
        }

        virtual public bool IsWinner()
        {
            int val = (int)(rand.NextDouble() * 10) + 1;
            return val == 1;
        }

        internal GumballMachineHardwareDevice Device
        {
            get
            {
                return device;
            }
        }

        internal void DisplayMessage(string message)
        {
            device.DisplayMessage(message);
        }
    }
}