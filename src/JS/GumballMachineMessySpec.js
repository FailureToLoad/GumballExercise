var GumballHardwareDevice = function() {
	return {
		displayMessage: function(message) { throw new Error("Needs Overwritten Error"); },
		displayGumball: function() { throw new Error("Needs Overwritten Error"); },
		dispenseQuarter: function() { throw new Error("Needs Overwritten Error"); }
	}
}

var TestDevice = function() {
	var device = new GumballHardwareDevice();
	var displayedMessage = "";
	var numGumballs = 0;
	var wasQuarterEjected = false;
	device.displayMessage = function(message) {
		displayedMessage = message;
	}
	device.dispenseGumball = function() {
		numGumballs--;
		return numGumballs >= 0;
	}
	device.dispenseQuarter = function() {
		wasQuarterEjected = true;
	}
	device.getDisplayedMessage = function() {
		return displayedMessage;
	}
	device.isQuarterEjected = function() {
		return wasQuarterEjected;
	}
	device.getCount = function() {
		return numGumballs;
	}
	device.addGumballs = function(count) {
		numGumballs = Math.max(numGumballs, 0) + count;
	}

	return device;

}
var Messages = {
	SO_START: "Sorry, the machine is sold out.",
	SO_QUART: "There are no Gumballs, please pick up your Quarter.",
	SO_EJECT: "This is not a Slot Machine.",
	SO_CRANK: "There are no Gumballs. :(",
	SO_RESET: "Quarter for a Gumball!",
	NQ_QUART: "Turn the Crank for a Gumball!",
	NQ_EJECT: "You haven't inserted a Quarter yet.",
	NQ_CRANK: "You need to pay first.",
	HQ_QUART: "You can't insert another Quarter.",
	HQ_EJECT: "Pick up your Quarter from the tray."
}
var GumballMachine = function(hardwareDevice) {
	var device = hardwareDevice;

	device.displayMessage(Messages.SO_START);

	var that = this;
	var quarterInserted = false;
	var soldOutMode = true;
	that.quarterInserted = function() {
		if(soldOutMode) {
			device.displayMessage(Messages.SO_QUART);
			device.dispenseQuarter();
		} else {
			if(quarterInserted) {
				device.displayMessage(Messages.HQ_QUART);
				device.dispenseQuarter();
			} else {
				quarterInserted = true;
				device.displayMessage(Messages.NQ_QUART);
			}

		}
	}

	that.ejectQuarterRequested = function() {
		if(soldOutMode) {
			device.displayMessage(Messages.SO_EJECT);
		} else {
			if(quarterInserted) {
				device.displayMessage(Messages.HQ_EJECT);
				device.dispenseQuarter();
			} else {
				device.displayMessage(Messages.NQ_EJECT);
			}
		}
	}

	that.crankTurned = function() {
		if(soldOutMode) {
			device.displayMessage(Messages.SO_CRANK);
		} else {
			if(quarterInserted) {
				device.displayMessage(Messages.SO_RESET);
				var moreThan0Gumballs = device.dispenseGumball();
				if(!moreThan0Gumballs) {
					device.displayMessage(Messages.SO_START);
					soldOutMode = true;
				}
			} else {
				device.displayMessage(Messages.NQ_CRANK);
			}
		}
	}

	that.reset = function() {
		device.displayMessage(Messages.SO_RESET);
		soldOutMode = false;
	}

	return that;
}


describe("Gumball Machine Test Suite", function(){
	var device;
	var machine;
	beforeEach(function(){
		device = new TestDevice();
		machine = new GumballMachine(device);
	});
	var refillMachine = function() {
		device.addGumballs(1);
		machine.reset();
	}
	describe("Gumball Machine is Sold Out", function(){

		it("At Start, display appropiate message", function(){
			expect(device.getDisplayedMessage()).toBe(Messages.SO_START);
		});

		it("At Start, if quarter is inserted, display appropiate message.", function() {
			machine.quarterInserted();
			expect(device.getDisplayedMessage()).toBe(Messages.SO_QUART);
		});

		it("At Start, if quarter is inserted, ensure that quarter is dispensed.", function(){
			machine.quarterInserted();
			expect(device.isQuarterEjected()).toBe(true);
		});

		it("At Start, if quarter eject, display message.", function() {
			machine.ejectQuarterRequested();
			expect(device.getDisplayedMessage()).toBe(Messages.SO_EJECT);
		});

		it("At Start, if quarter eject, ensure there are no quarters ejected", function() {
			machine.ejectQuarterRequested();
			expect(device.isQuarterEjected()).toBe(false);
		});

		it("At Start, if crank turned, there display message", function(){
			machine.crankTurned();
			expect(device.getDisplayedMessage()).toBe(Messages.SO_CRANK);
		});

		it("At Start, if reset pressed, display message", function(){
			machine.reset();
			expect(device.getDisplayedMessage()).toBe(Messages.SO_RESET);
		});
	});


	describe("No Quarter Suite", function(){
		it("No Quarter, with Quarter Inserted", function() {
			refillMachine();
			machine.quarterInserted();
			expect(device.getDisplayedMessage()).toBe(Messages.NQ_QUART);
		});

		it("No Quarter, eject quarter, display message", function(){
			refillMachine();
			machine.ejectQuarterRequested();
			expect(device.getDisplayedMessage()).toBe(Messages.NQ_EJECT);
		});

		it("No Quarter, crank turned, display message", function(){
			refillMachine();
			machine.crankTurned();
			expect(device.getDisplayedMessage()).toBe(Messages.NQ_CRANK);
		});
	});

	describe("Quarter Suite", function(){
		it("Quarter, can't insert another quarter message displayed", function(){
			refillMachine();
			machine.quarterInserted();
			machine.quarterInserted();
			expect(device.getDisplayedMessage()).toBe(Messages.HQ_QUART);
		});

		it("Quarter, if user inserts another quarter, ensure quarter is dispensed", function(){
			refillMachine();
			machine.quarterInserted();
			machine.quarterInserted();
			expect(device.isQuarterEjected()).toBe(true);
		});

		it("Quarter, if user ejects quarter, ensure message is displayed", function(){
			refillMachine();
			machine.quarterInserted();
			machine.ejectQuarterRequested();
			expect(device.getDisplayedMessage()).toBe(Messages.HQ_EJECT);
		});

		it("Quarter, if user ejects quarter, ensure user gets quarter", function(){
			refillMachine();
			machine.quarterInserted();
			machine.ejectQuarterRequested();
			expect(device.isQuarterEjected()).toBe(true);
		});

		it("Quarter, crank turned, ensure state is reset to NQ state", function(){
			refillMachine();
			machine.quarterInserted();
			machine.crankTurned();
			expect(device.getDisplayedMessage()).toBe(Messages.SO_RESET);
		});

		it("Quarter, crank turned, ensure gumball dispensed if there are gumballs.", function(){
			refillMachine();
			var originalGumballCount = device.getCount();
			machine.quarterInserted();
			machine.crankTurned();
			expect(device.getCount()).toBe(originalGumballCount - 1);
		});

		it("Quarter, crank turned, display message if there are no gumballs", function() {
			refillMachine();
			machine.quarterInserted();
			machine.crankTurned();
			machine.crankTurned();
			expect(device.getDisplayedMessage()).toBe(Messages.SO_START);
		});
	});
});