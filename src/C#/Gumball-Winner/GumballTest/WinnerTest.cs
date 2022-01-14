using System;
using Gumball;
using NUnit.Framework;

namespace GumballTest
{
    [TestFixture]
    public class WinnerTest
    {
        private TestDevice device;
        private GumballMachine gumballMachine;
        private int count;

        class WinnerGumballMachine : GumballMachine
        {
            public WinnerGumballMachine(GumballMachineHardwareDevice device) : base(device)
            {
            }

            public override bool IsWinner()
            {
                return true;
            }
        }

        [SetUp]
        public void Setup()
        {
            device = new TestDevice();
            gumballMachine = new WinnerGumballMachine(device);
            HasWinnerState();
        }

        [Test]
        public void AtStartWinnerShouldDisplayStartMessage()
        {
            AssertDisplay(Messages.WN_START);
        }

        [Test]
        public void AtWinnerStartShouldDispenseOneGumball()
        {
            Assert.AreEqual(count - 1, device.GetCount());
        }

        [Test]
        public void InsertQuarterWinnerShouldDisplayQuarterMessage()
        {
            gumballMachine.QuarterInserted();
            AssertDisplay(Messages.WN_QUART);
        }

        [Test]
        public void InsertQuarterWinnerShouldEjectQuarter()
        {
            gumballMachine.QuarterInserted();
            Assert.IsTrue(device.WasQuarterEjected());
        }

        [Test]
        public void EjectQuarterWinnerShouldDisplayEjectMessage()
        {
            gumballMachine.EjectQuarterRequested();
            AssertDisplay(Messages.WN_EJECT);
        }

        [Test]
        public void TurnCrankWinnerShouldDisplayCrankMessage()
        {
            gumballMachine.CrankTurned();
            AssertDisplay(Messages.WN_CRANK);
        }

        [Test]
        public void TurnCrankWinnerShouldHaveSoldTwoGumballs()
        {
            gumballMachine.CrankTurned();
            Assert.AreEqual(count - 2, device.GetCount());
        }

        [Test]
        public void SellingOutAsWinnerShouldReleaseQuarterAndGumball()
        {
            SellGumball();
            device.AddGumballs(1);
            count = device.GetCount();
            SellGumball();

            Assert.AreEqual(count - 2, device.GetCount());
            Assert.IsTrue(device.WasQuarterEjected());
        }

        [Test]
        public void EmptyMachineShouldNotHaveWinner()
        {
            SellGumball();
            count = device.GetCount();
            SellGumball();

            Assert.AreEqual(count - 1, device.GetCount());
            Assert.IsTrue(device.WasQuarterEjected());
        }


        // Helper Methods
        private void AssertDisplay(String message)
        {
            Assert.AreEqual(message, device.GetDisplayedMessage());
        }

        private void RefillMachine()
        {
            device.AddGumballs(2);
            gumballMachine.Reset();
        }

        private void HasQuarterState()
        {
            RefillMachine();
            gumballMachine.QuarterInserted();
        }

        private void HasWinnerState()
        {
            HasQuarterState();
            count = device.GetCount();
            gumballMachine.CrankTurned();
        }

        private void SellGumball()
        {
            gumballMachine.QuarterInserted();
            gumballMachine.CrankTurned();
            gumballMachine.CrankTurned();
        }

        private void CycleQuarter()
        {
            gumballMachine.QuarterInserted();
            gumballMachine.EjectQuarterRequested();
        }

    }
}
