using System;
using Gumball;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GumballTest
{
    [TestClass]
    public class MikesGumballMachineMSTest
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

        [TestInitialize]
        public void Setup()
        {
            device = new TestDevice();
            gumballMachine = new NoWinnerGumballMachine(device);
        }

        [TestMethod]
        public void InitialConditionsEmptyMachineShouldShowSO_StartMessage()
        {
            AssertDisplay(Messages.SO_START);
        }

        [TestMethod]
        public void InitialConditionsEmptyMachineShouldNotDispenseQuarter()
        {
            Assert.IsFalse(device.WasQuarterEjected());
        }
        
        [TestMethod]
        public void QuarterInsertedOnEmptyMachineShouldDisplaySO_QuartMessage()
        {
            gumballMachine.QuarterInserted();
            AssertDisplay(Messages.SO_QUART);
        }

        [TestMethod]
        public void QuarterInsertedOnEmptyMachineShouldEjectQuarter()
        {
            gumballMachine.QuarterInserted();
            Assert.IsTrue(device.WasQuarterEjected());
        }

        [TestMethod]
        public void EjectQuartertOnEmptyMachineShouldDisplaySO_EjectMessage()
        {
            gumballMachine.EjectQuarterRequested();
            AssertDisplay(Messages.SO_EJECT);
        }
        
        [TestMethod]
        public void CrankTurnedOnEmptyMachineShouldDisplaySO_CrankMessage()
        {
            gumballMachine.CrankTurned();
            AssertDisplay(Messages.SO_CRANK);
        }
        
        // No Quarter
        [TestMethod]
        public void ResetMachineShouldDisplayNQ_StartMessage()
        {
            RefillMachine();
            AssertDisplay(Messages.NQ_START);
        }
        
        [TestMethod]
        public void QuarterInsertedOnRefilledMachineShouldDisplayHQ_StartMessage()
        {
            RefillMachine();
            gumballMachine.QuarterInserted();
            AssertDisplay(Messages.NQ_QUART);
        }

        [TestMethod]
        public void EjectQuarterOnRefilledMachineShouldDisplayNQ_EjectMessage()
        {
            RefillMachine();
            gumballMachine.EjectQuarterRequested();
            AssertDisplay(Messages.NQ_EJECT);
        }

        [TestMethod]
        public void EjectQuarterOnRefilledMachineShouldNotEjectQuarter()
        {
            RefillMachine();
            gumballMachine.EjectQuarterRequested();
            Assert.IsFalse(device.WasQuarterEjected());
        }

        [TestMethod]
        public void CrankTurnedOnRefilledMachineShouldDisplayNQ_CrankMessage()
        {
            RefillMachine();
            gumballMachine.CrankTurned();
            AssertDisplay(Messages.NQ_CRANK);
        }
        
        [TestMethod]
        public void ResetOnRefilledMachineShouldNotChangeNQ_StartMessage()
        {
            RefillMachine();
            gumballMachine.Reset();
            AssertDisplay(Messages.NQ_START);
        }
        
        // Has Quarter
        [TestMethod]
        public void AtStartWithQuarterShouldDisplayHQ_StartMessage()
        {
            HasQuarterState();
            AssertDisplay(Messages.HQ_START);
        }

        [TestMethod]
        public void QuarterInsertedWhenMachineHasQuarterShouldDisplayHQ_QuartMessage()
        {
            HasQuarterState();
            gumballMachine.QuarterInserted();
            AssertDisplay(Messages.HQ_QUART);
        }

        [TestMethod]
        public void QuarterInsertedWhenMachineHasQuarterShouldEjectQuarter()
        {
            HasQuarterState();
            gumballMachine.QuarterInserted();
            Assert.IsTrue(device.WasQuarterEjected());
        }

        [TestMethod]
        public void EjectQuarterWhenMachineHasQuarterShouldDisplayHQ_EjectMessage()
        {
            HasQuarterState();
            gumballMachine.EjectQuarterRequested();
            AssertDisplay(Messages.HQ_EJECT);
        }

        [TestMethod]
        public void EjectQuarterWhenMachineHasQuarterShouldReleaseQuarter()
        {
            HasQuarterState();
            gumballMachine.EjectQuarterRequested();
            Assert.IsTrue(device.WasQuarterEjected());
        }
        
        [TestMethod]
        public void CrankTurnedWhenMachineHasQuarterShouldDisplayHQ_CrankMessage()
        {
            HasQuarterState();
            gumballMachine.CrankTurned();
            AssertDisplay(Messages.HQ_CRANK);
        }

        [TestMethod]
        public void CrankTurnedWhenMachineHasQuarterShouldDispenseGumball()
        {
            HasQuarterState();
            int count = device.GetCount();
            gumballMachine.CrankTurned();
            Assert.AreEqual(count - 1, device.GetCount());
        }

        [TestMethod]
        public void ResetWhenMachineHasQuarterShouldNotChangeHQ_StartMessage()
        {
            HasQuarterState();
            gumballMachine.Reset();
            AssertDisplay(Messages.HQ_START);
        }

        [TestMethod]
        public void SellingGumballShouldDisplayNQ_StartMessage()
        {
            RefillMachine();
            SellGumball();
            AssertDisplay(Messages.NQ_START);
        }

        // Sell Out
        [TestMethod]
        public void QuarterInsertedWhenSoldOutShouldDisplaySO_StartMessage()
        {
            SellOutState();
            AssertDisplay(Messages.SO_START);
        }

        [TestMethod]
        public void SellingOutShouldReturnTheQuarter()
        {
            SellOutState();
            Assert.IsTrue(device.WasQuarterEjected());
        }

        [TestMethod]
        public void QuarterInsertedWhenSoldOutShouldDisplaySO_QuartMessage()
        {
            SellOutState();
            gumballMachine.QuarterInserted();
            AssertDisplay(Messages.SO_QUART);
        }

        [TestMethod]
        public void EjectQuarterWhenSoldOutShouldDisplaySO_EjectMessage()
        {
            SellOutState();
            gumballMachine.EjectQuarterRequested();
            AssertDisplay(Messages.SO_EJECT);
        }

        [TestMethod]
        public void CrankTurnedWhenSoldOutShouldDisplaySO_CrankMessage()
        {
            SellOutState();
            gumballMachine.CrankTurned();
            AssertDisplay(Messages.SO_CRANK);
        }

        [TestMethod]
        public void ResetWhenSoldOutShouldDisplayNQ_StartMessage()
        {
            SellOutState();
            gumballMachine.Reset();
            AssertDisplay(Messages.NQ_START);
        }

        // Error checks
        [TestMethod]
        public void CyclingQuarterOnEmptyMachineShouldDisplaySO_EjectMessage()
        {
            CycleQuarter();
            AssertDisplay(Messages.SO_EJECT);
        }

        [TestMethod]
        public void CrankTurnedAfterGumballSoldShouldDisplayNQ_CrankMessage()
        {
            RefillMachine();
            SellGumball();
            gumballMachine.CrankTurned();
            AssertDisplay(Messages.NQ_CRANK);
        }

        [TestMethod]
        public void CyclingQuarterShouldEjectQuarter()
        {
            RefillMachine();
            CycleQuarter();
            Assert.IsTrue(device.WasQuarterEjected());
        }

        [TestMethod]
        public void QuarterInsertedAfterCyclingQuarterShouldDisplayHQ_StartMessage()
        {
            RefillMachine();
            CycleQuarter();
            gumballMachine.QuarterInserted();
            AssertDisplay(Messages.HQ_START);
        }

        [TestMethod]
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
            while (0 < device.GetCount())
            {
                SellGumball();
                if (count == device.GetCount())
                {
                    throw new Exception("Machine can't sell gumballs!");
                }
            }

            gumballMachine.QuarterInserted();
            gumballMachine.CrankTurned();
        }
    }
}
