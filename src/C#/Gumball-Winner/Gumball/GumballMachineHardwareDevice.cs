namespace Gumball
{
	public interface GumballMachineHardwareDevice
	{

		/**
		 * This method will display the message parameter on the gumball machine's
		 * hardware display.
		 * 
		 * @param message
		 */
		void DisplayMessage(string message);

		/**
		 * This method will force the gumball machine's hardware to release a
		 * gumball. If it was successfully able to release a gumball, it returns
		 * true, otherwise it returns false.
		 * 
		 * @return boolean {true on success, otherwise false}
		 */
		bool DispenseGumball();

		/**
	 	* This method will force the gumball machine hardware to release a quarter
	 	* to the coin return slot. Calling this multiple times will release
	 	* multiple quarters.
	 	* 
	 	*/
		void DispenseQuarter();
	}
}

