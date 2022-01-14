namespace Gumball
{
    public class GumballMachine
    {
		private readonly GumballMachineHardwareDevice device;

        public GumballMachine(GumballMachineHardwareDevice device)
        {
			this.device = device;
            device.DisplayMessage(Messages.SO_START);
        }

        public void QuarterInserted()
        {
            device.DisplayMessage(Messages.SO_QUART);
        }

        public void EjectQuarterRequested()
        {
        }

        public void CrankTurned()
        {
        }

        public void Reset()
        {
        }
    }
}
