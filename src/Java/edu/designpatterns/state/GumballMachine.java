package edu.designpatterns.state;

public class GumballMachine {
	private GumballHardwareDevice device;

	public GumballMachine(GumballHardwareDevice device) {
		this.device = device;
		device.displayMessage(Messages.SO_START);
	}

	public void quarterInserted() {
		device.displayMessage(Messages.SO_QUART);
		device.dispenseQuarter();
	}

	public void ejectQuarterRequested() {
	}

	public void crankTurned() {
	}

	public void reset() {
	}
}
