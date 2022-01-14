using Gumball;
using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace GumballTest
{
    [TestFixture]
    public class GumballMachineTest
    {
        private TestDevice device;
        private GumballMachine gumballMachine;

        [SetUp]
        public void Setup()
        {
            device = new TestDevice();
            gumballMachine = new GumballMachine(device);
        }

        [Test]
        public void InitialConditionsEmptyMachineShouldShowSO_StartMessage()
        {
            AreEqual(Messages.SO_START, device.GetDisplayedMessage());
        }

        [Test]
        public void InitialConditionsEmptyMachineShouldNotDispenseQuarter()
        {
            IsFalse(device.WasQuarterEjected());
        }
    }
}
