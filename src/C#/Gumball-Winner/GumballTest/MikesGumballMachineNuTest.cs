using System;
using Gumball;
using NUnit.Framework;

namespace GumballTest
{
    [TestFixture]
    public class MikesGumballMachineNuTest
    {
        private TestDevice device;
        private GumballMachine gumballMachine;

        class NoWinnerGumballMachine : GumballMachine
        {
            public NoWinnerGumballMachine(GumballMachineHardwareDevice device) : base(device)
            {
            }

            public override bool IsWinner()
            {
                return false;
            }
        }

        [SetUp]
        public void Setup()
        {
            device = new TestDevice();
            gumballMachine = new NoWinnerGumballMachine(device);
        }

        // Sold Out
        [Test]
        public void InitialConditionsEmptyMachineShouldShowSO_StartMessage()
        {
            AssertDisplay(Messages.SO_START);
        }

        [Test]
        public void InitialConditionsEmptyMachineShouldNotDispenseQuarter()
        {
            Assert.False(device.WasQuarterEjected());
        }
        
        [Test]
        public void QuarterInsertedOnEmptyMachineShouldDisplaySO_QuartMessage()
        {
            gumballMachine.QuarterInserted();
            AssertDisplay(Messages.SO_QUART);
        }


        [Test]
        public void QuarterInsertedOnEmptyMachineShouldEjectQuarter()
        {
            gumballMachine.QuarterInserted();
            Assert.True(device.WasQuarterEjected());
        }
        
        [Test]
        public void EjectQuartertOnEmptyMachineShouldDisplaySO_EjectMessage()
        {
            gumballMachine.EjectQuarterRequested();
            AssertDisplay(Messages.SO_EJECT);
        }

        [Test]
        public void CrankTurnedOnEmptyMachineShouldDisplaySO_CrankMessage()
        {
            gumballMachine.CrankTurned();
            AssertDisplay(Messages.SO_CRANK);
        }

        // No Quarter
        [Test]
        public void ResetMachineShouldDisplayNQ_StartMessage()
        {
            RefillMachine();
            AssertDisplay(Messages.NQ_START);
        }

        [Test]
        public void QuarterInsertedOnRefilledMachineShouldDisplayHQ_StartMessage()
        {
            RefillMachine();
            gumballMachine.QuarterInserted();
            AssertDisplay(Messages.NQ_QUART);
        }

        [Test]
        public void EjectQuarterOnRefilledMachineShouldDisplayNQ_EjectMessage()
        {
            RefillMachine();
            gumballMachine.EjectQuarterRequested();
            AssertDisplay(Messages.NQ_EJECT);
        }

        [Test]
        public void EjectQuarterOnRefilledMachineShouldNotEjectQuarter()
        {
            RefillMachine();
            gumballMachine.EjectQuarterRequested();
            Assert.False(device.WasQuarterEjected());
        }

        [Test]
        public void CrankTurnedOnRefilledMachineShouldDisplayNQ_CrankMessage()
        {
            RefillMachine();
            gumballMachine.CrankTurned();
            AssertDisplay(Messages.NQ_CRANK);
        }
        
        [Test]
        public void ResetOnRefilledMachineShouldNotChangeNQ_StartMessage()
        {
            RefillMachine();
            gumballMachine.Reset();
            AssertDisplay(Messages.NQ_START);
        }

        // Has Quarter
        [Test]
        public void AtStartWithQuarterShouldDisplayHQ_StartMessage()
        {
            HasQuarterState();
            AssertDisplay(Messages.HQ_START);
        }
        
        [Test]
        public void QuarterInsertedWhenMachineHasQuarterShouldDisplayHQ_QuartMessage()
        {
            HasQuarterState();
            gumballMachine.QuarterInserted();
            AssertDisplay(Messages.HQ_QUART);
        }

        [Test]
        public void QuarterInsertedWhenMachineHasQuarterShouldEjectQuarter()
        {
            HasQuarterState();
            gumballMachine.QuarterInserted();
            Assert.True(device.WasQuarterEjected());
        }
        
        [Test]
        public void EjectQuarterWhenMachineHasQuarterShouldDisplayHQ_EjectMessage()
        {
            HasQuarterState();
            gumballMachine.EjectQuarterRequested();
            AssertDisplay(Messages.HQ_EJECT);
        }

        [Test]
        public void EjectQuarterWhenMachineHasQuarterShouldReleaseQuarter()
        {
            HasQuarterState();
            gumballMachine.EjectQuarterRequested();
            Assert.True(device.WasQuarterEjected());
        }
        
        [Test]
        public void CrankTurnedWhenMachineHasQuarterShouldDisplayHQ_CrankMessage()
        {
            HasQuarterState();
            gumballMachine.CrankTurned();
            AssertDisplay(Messages.HQ_CRANK);
        }

        [Test]
        public void CrankTurnedWhenMachineHasQuarterShouldDispenseGumball()
        {
            HasQuarterState();
            int count = device.GetCount();
            gumballMachine.CrankTurned();
            Assert.AreEqual(count - 1, device.GetCount());
        }

        [Test]
        public void ResetWhenMachineHasQuarterShouldNotChangeHQ_StartMessage()
        {
            HasQuarterState();
            gumballMachine.Reset();
            AssertDisplay(Messages.HQ_START);
        }

        [Test]
        public void SellingGumballShouldDisplayNQ_StartMessage()
        {
            RefillMachine();
            SellGumball();
            AssertDisplay(Messages.NQ_START);
        }

        // Sell Out
        [Test]
        public void QuarterInsertedWhenSoldOutShouldDisplaySO_StartMessage()
        {
            SellOutState();
            AssertDisplay(Messages.SO_START);
        }

        [Test]
        public void SellingOutShouldReturnTheQuarter()
        {
            SellOutState();
            Assert.True(device.WasQuarterEjected());
        }

        [Test]
        public void QuarterInsertedWhenSoldOutShouldDisplaySO_QuartMessage()
        {
            SellOutState();
            gumballMachine.QuarterInserted();
            AssertDisplay(Messages.SO_QUART);
        }

        [Test]
        public void EjectQuarterWhenSoldOutShouldDisplaySO_EjectMessage()
        {
            SellOutState();
            gumballMachine.EjectQuarterRequested();
            AssertDisplay(Messages.SO_EJECT);
        }

        [Test]
        public void CrankTurnedWhenSoldOutShouldDisplaySO_CrankMessage()
        {
            SellOutState();
            gumballMachine.CrankTurned();
            AssertDisplay(Messages.SO_CRANK);
        }

        [Test]
        public void ResetWhenSoldOutShouldDisplayNQ_StartMessage()
        {
            SellOutState();
            gumballMachine.Reset();
            AssertDisplay(Messages.NQ_START);
        }

        // Error checks
        [Test]
        public void CyclingQuarterOnEmptyMachineShouldDisplaySO_EjectMessage()
        {
            CycleQuarter();
            AssertDisplay(Messages.SO_EJECT);
        }

        [Test]
        public void CrankTurnedAfterGumballSoldShouldDisplayNQ_CrankMessage()
        {
            RefillMachine();
            SellGumball();
            gumballMachine.CrankTurned();
            AssertDisplay(Messages.NQ_CRANK);
        }

        [Test]
        public void CyclingQuarterShouldEjectQuarter()
        {
            RefillMachine();
            CycleQuarter();
            Assert.True(device.WasQuarterEjected());
        }

        [Test]
        public void QuarterInsertedAfterCyclingQuarterShouldDisplayHQ_StartMessage()
        {
            RefillMachine();
            CycleQuarter();
            gumballMachine.QuarterInserted();
            AssertDisplay(Messages.HQ_START);
        }

        [Test]
        public void ResetMidCycleShouldNotEffectHasQuarter()
        {
            RefillMachine();
            gumballMachine.QuarterInserted();
            RefillMachine();
            AssertDisplay(Messages.HQ_START);
        }



        // Helper Methods

        private void AssertDisplay(String message)
        {
            Assert.AreEqual(message, device.GetDisplayedMessage());
        }

        private void RefillMachine()
        {
            device.AddGumballs(1);
            gumballMachine.Reset();
        }

        private void HasQuarterState()
        {
            RefillMachine();
            gumballMachine.QuarterInserted(); 
        }

        private void SellGumball()
        {
            gumballMachine.QuarterInserted();
            gumballMachine.CrankTurned();
        }

        private void CycleQuarter()
        {
            gumballMachine.QuarterInserted();
            gumballMachine.EjectQuarterRequested();
        }

        private void SellOutState()
        {
            RefillMachine();

            int count = device.GetCount();
            while(0 < device.GetCount())
            {
                SellGumball();
                if (count == device.GetCount())
                    throw new Exception("Gumball machine isn't selling gumballs yet");
            }

            gumballMachine.QuarterInserted();
            gumballMachine.CrankTurned();
        }
    }
}