using System;

namespace Gumball
{
    internal abstract class GumballMachineState
    {
        abstract public void Start(GumballMachine gumballMachine);

        abstract public void QuarterInserted(GumballMachine gumballMachine);

        abstract public void EjectQuarterRequested(GumballMachine gumballMachine);

        abstract public void CrankTurned(GumballMachine gumballMachine);

        virtual public void Reset(GumballMachine gumballMachine) { }
    }

    class SoldOutState : GumballMachineState
    {
        public override void Start(GumballMachine gumballMachine)
        {
            gumballMachine.Device.DisplayMessage(Messages.SO_START);
        }

        public override void QuarterInserted(GumballMachine gumballMachine)
        {
            gumballMachine.DisplayMessage(Messages.SO_QUART);
            gumballMachine.Device.DispenseQuarter();
        }

        public override void EjectQuarterRequested(GumballMachine gumballMachine)
        {
            gumballMachine.DisplayMessage(Messages.SO_EJECT);
        }

        public override void CrankTurned(GumballMachine gumballMachine)
        {
            gumballMachine.DisplayMessage(Messages.SO_CRANK);
        }

        public override void Reset(GumballMachine gumballMachine)
        {
            gumballMachine.SetState(State.NQ);
        }
    }

    class NoQuarterState : GumballMachineState
    {
        public override void Start(GumballMachine gumballMachine)
        {
            gumballMachine.DisplayMessage(Messages.NQ_START);
        }

        public override void QuarterInserted(GumballMachine gumballMachine)
        {
            gumballMachine.SetState(State.HQ);
        }

        public override void EjectQuarterRequested(GumballMachine gumballMachine)
        {
            gumballMachine.DisplayMessage(Messages.NQ_EJECT);
        }

        public override void CrankTurned(GumballMachine gumballMachine)
        {
            gumballMachine.DisplayMessage(Messages.NQ_CRANK);
        }
    }

    class HasQuarterState : GumballMachineState
    {
        public override void Start(GumballMachine gumballMachine)
        {
            gumballMachine.DisplayMessage(Messages.HQ_START);
        }

        public override void QuarterInserted(GumballMachine gumballMachine)
        {
            gumballMachine.DisplayMessage(Messages.HQ_QUART);
            gumballMachine.Device.DispenseQuarter();
        }

        public override void EjectQuarterRequested(GumballMachine gumballMachine)
        {
            gumballMachine.SetState(State.NQ);
            gumballMachine.Device.DispenseQuarter();
            gumballMachine.DisplayMessage(Messages.HQ_EJECT);
        }

        public override void CrankTurned(GumballMachine gumballMachine)
        {
            if (gumballMachine.Device.DispenseGumball())
            {
                if (gumballMachine.IsWinner())
                {
                    gumballMachine.SetState(State.WN);
                } else
                {
                    gumballMachine.SetState(State.NQ);
                }
            }
            else
            {
                gumballMachine.Device.DispenseQuarter();
                gumballMachine.SetState(State.SO);
            }
        }
    }

    class WinnerState : HasQuarterState
    {
        public override void Start(GumballMachine gumballMachine)
        {
            gumballMachine.DisplayMessage(Messages.WN_START);
        }

        public override void QuarterInserted(GumballMachine gumballMachine)
        {
            gumballMachine.DisplayMessage(Messages.WN_QUART);
            gumballMachine.Device.DispenseQuarter();
        }

        public override void EjectQuarterRequested(GumballMachine gumballMachine)
        {
            gumballMachine.DisplayMessage(Messages.WN_EJECT);
        }

        public override void CrankTurned(GumballMachine gumballMachine)
        {
            gumballMachine.DisplayMessage(Messages.WN_CRANK);
            if (gumballMachine.Device.DispenseGumball())
            {
                gumballMachine.SetState(State.NQ);
            } else
            {
                gumballMachine.Device.DispenseQuarter();
                gumballMachine.SetState(State.SO);
            }
        }
    }
}