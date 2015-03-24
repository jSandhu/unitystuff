using UnityEngine;

namespace LocalMultiPlayer
{
	public class ConnectionSettingsVO 
	{
		public string GameType = "LMP_NJS_TST";
		public int GamePort = 71010;
		public bool UseNatPunchThrough = true;
		public int Connections = 32;

		// Hosting
		public string CreatingGameMessage = "Creating Game...";
		public string CreationFailedMessage = "Failed to Create Game!";
		public string RegisteringWithMasterServMesg = "Registering With Master Server...";
		public string RegisteringWithMasterServFailMesg = "Failed to Register With Master Server.";	

		// Joining
		public string JoiningGameMessage = "Joining Game...";
		public string JoinFailedMessage = "Failed to join game!";
	}
}
