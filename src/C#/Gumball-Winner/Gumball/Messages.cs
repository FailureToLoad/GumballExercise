namespace Gumball
{
	public static class Messages
	{
        public static string SO_START = "Sorry, the machine is sold out";
        public static string SO_QUART = "There are no Gumballs, please pick up your Quarter";
        public static string SO_EJECT = "This is not a slot machine";
        public static string SO_CRANK = "There are no Gumballs :(";

        public static string NQ_START = "Quarter for a Gumball";
		public static string NQ_QUART = "Turn the Crank for a Gumball";
		public static string NQ_EJECT = "You haven't inserted a Quarter yet";
		public static string NQ_CRANK = "You need to pay first";

		public static string HQ_START = NQ_QUART;
		public static string HQ_QUART = "You can't insert another Quarter";
		public static string HQ_EJECT = "Pick up your Quarter from the tray";
		public static string HQ_CRANK = NQ_START;

        public static string WN_START = "You are a Winner!! Turn the Crank again for another Gumball";
        public static string WN_QUART = "You don't need to add a Quarter. Turn the Crank for a Gumball";
        public static string WN_EJECT = "Sorry, you don't get your Quarter back. Turn the Crank for a Gumball";
        public static string WN_CRANK = HQ_CRANK;
    }
}

