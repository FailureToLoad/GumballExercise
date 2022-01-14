namespace Gumball
{
    internal class State
    {
        public static GumballMachineState SO = new SoldOutState();
        public static GumballMachineState NQ = new NoQuarterState();
        public static GumballMachineState HQ = new HasQuarterState();
        public static GumballMachineState WN = new WinnerState();
    }
}