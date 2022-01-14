using Gumball;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace GumballTest
{
    [TestClass]
    public class GumballMachineMSTest
    {
        private TestDevice device;
        private GumballMachine gumballMachine;

        [TestInitialize]
        public void Setup()
        {
            device = new TestDevice();
            gumballMachine = new GumballMachine(device);
        }

        [TestMethod]
        public void InitialConditionsEmptyMachineShouldShowSO_StartMessage()
        {
            AreEqual(Messages.SO_START, device.GetDisplayedMessage());
        }

        [TestMethod]
        public void InitialConditionsEmptyMachineShouldNotDispenseQuarter()
        {
            IsFalse(device.WasQuarterEjected());
        }


        [TestMethod]
        public void QuarterInsteredOnEmptyMachineShouldShowSO_QuartMessage()
        {
            gumballMachine.QuarterInserted();
            AreEqual(Messages.SO_QUART, device.GetDisplayedMessage());
        }

    }
}
