using UnityEngine;
using UnityEngine.UI;
using System;

namespace LocalMultiPlayer
{
	public class HostPanel : MonoBehaviour 
	{
		public event Action Canceled;
		public event Action<string> GameRegistered;

	    public EnterGameNamePanel EnterGameNamePanel;
		public CreatingGamePanel CreatingGamePanel;
		public Button CancelHostingButton;

		private ConnectionSettingsVO connectionSettingsVO;
		public ConnectionSettingsVO ConnectionSettingsVO{
			get{if(connectionSettingsVO == null) {connectionSettingsVO = new ConnectionSettingsVO();} return connectionSettingsVO;} 
			set{connectionSettingsVO = value;}
		}

		private string gameName;
		
		private void Start() 
		{
			CancelHostingButton.onClick.AddListener(OnHostingCanceled);
	        EnterGameNamePanel.GameNameEntered += OnGameNameEntered;
			CreatingGamePanel.Closed += ShowEnterGameNamePanel;
		}
	    
	    private void OnDestroy()
	    {
			CancelHostingButton.onClick.RemoveAllListeners();
			GameRegistered = null;
	        EnterGameNamePanel.GameNameEntered -= OnGameNameEntered;
	    }

		private void OnHostingCanceled()
		{
			if(Canceled != null)
			{
				Canceled();
			}
			gameObject.SetActive(false);
		}
	    
	    public void ResetAndShow()
	    {
	        ShowEnterGameNamePanel();
			gameObject.SetActive(true);
	    }
	    
	    private void ShowEnterGameNamePanel()
	    {
			CreatingGamePanel.gameObject.SetActive(false);
	        EnterGameNamePanel.gameObject.SetActive(true);
	    }
	    
	    private void OnGameNameEntered(string gameName)
	    {
			this.gameName = gameName;
			EnterGameNamePanel.gameObject.SetActive(false);
			CreatingGamePanel.gameObject.SetActive(true);
			CreatingGamePanel.SetStatusText(ConnectionSettingsVO.CreatingGameMessage);
			NetworkConnectionError error = Network.InitializeServer(ConnectionSettingsVO.Connections, ConnectionSettingsVO.GamePort, ConnectionSettingsVO.UseNatPunchThrough);
			if (error != NetworkConnectionError.NoError)
			{
				CreatingGamePanel.SetStatusText(ConnectionSettingsVO.CreationFailedMessage + " : Error : " + error, true);
			}
	    }

		private void OnServerInitialized() 
		{
			CreatingGamePanel.SetStatusText(ConnectionSettingsVO.RegisteringWithMasterServMesg);
			MasterServer.RegisterHost(ConnectionSettingsVO.GameType, gameName, "TODO: game data");
		}

		private void OnMasterServerEvent(MasterServerEvent msEvent) 
		{
			if (msEvent == MasterServerEvent.RegistrationSucceeded)
			{
				if (GameRegistered != null)
				{
					GameRegistered(gameName);
				}
			}
			else if (msEvent == MasterServerEvent.RegistrationFailedGameName ||
			         msEvent == MasterServerEvent.RegistrationFailedGameType ||
			         msEvent == MasterServerEvent.RegistrationFailedNoServer)
			{
				CreatingGamePanel.SetStatusText(ConnectionSettingsVO.RegisteringWithMasterServFailMesg + " : Error : " + msEvent, true);
			}
		}
	}
}
